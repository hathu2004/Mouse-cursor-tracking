using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace mouse_cursor
{
    public class HeatMap
    {
        private List<Point> points;

        private Bitmap bitmap;
       
        private int w;

        private int h;

        private int gSize;

        private double gSigma;

        private int r;

        private double[,] heatVals;

        private double[,] kernel;

        public int W { get => w; set => w = value; }

        public int H { get => h; set => h = value; }

        public double[,] Kernel { get => kernel; }

        public double[,] HeatVals { get => heatVals; }

        public HeatMap(int width, int height, int gSize, double gSigma)
        {
            this.w = width;
            this.h = height;

            if (gSize < 3 || gSize > 400)
            {
                throw new Exception("Kernel size is invalid");
            }
            this.gSize = gSize % 2 == 0 ? gSize + 1 : gSize;
            
            this.r = this.gSize / 2;
            this.gSigma = gSigma;
            
            kernel = new double[this.gSize, this.gSize];
            this.gaussiankernel();
            
            heatVals = new double[h, w];
            bitmap = new Bitmap(w, h, PixelFormat.Format32bppArgb);
        }

        private void gaussiankernel()
        {
            for (int y = -r, i = 0; i < gSize; y++, i++)
            {
                for (int x = -r, j = 0; j < gSize; x++, j++)
                {
                    kernel[i, j] = Math.Exp(((x * x) + (y * y)) / (-2 * gSigma * gSigma)) / (2 * Math.PI * gSigma * gSigma);
                }
            }
        }

        public void SetDatas(List<Point> points)
        {
            foreach (Point point in points)
            {
                int i, j, tx, ty, ir, jr;
                int radius = gSize >> 1;

                int x = point.X;
                int y = point.Y;

                for (i = 0; i < gSize; i++)
                {
                    ir = i - radius;
                    ty = y + ir;

                    // skip row
                    if (ty < 0)
                    {
                        continue;
                    }

                    // break Height
                    if (ty >= h)
                    {
                        break;
                    }

                    // for each kernel column
                    for (j = 0; j < gSize; j++)
                    {
                        jr = j - radius;
                        tx = x + jr;

                        // skip column
                        if (tx < 0)
                        {
                            continue;
                        }

                        if (tx < w)
                        {
                            heatVals[ty, tx] += kernel[i, j];
                        }
                    }
                }
            }
        }
        public void GenerateHeatMap()
        {;
            int rows = heatVals.GetLength(0);
            int cols = heatVals.GetLength(1);

            double max = double.MinValue; // Khởi tạo bằng giá trị nhỏ nhất có thể
            bool hasValidValue = false;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!double.IsNaN(heatVals[i, j]))
                    {
                        max = Math.Max(max, heatVals[i, j]);
                        hasValidValue = true;
                    }
                    else { heatVals[i, j] = 0; }
                }
            }

            if (!hasValidValue) max = 1; // Nếu mảng toàn NaN hoặc rỗng

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    double normalizedValue = Math.Min(1, Math.Max(0, (double)heatVals[i, j] / max));

                    // Tính toán màu từ xanh lam (khi normalizedValue = 0) đến đỏ (khi normalizedValue = 1)
                    int r = (int)(Math.Max(0, Math.Min(255, 255 * (1.5 - Math.Abs(4 * normalizedValue - 3)))));
                    int g = (int)(Math.Max(0, Math.Min(255, 255 * (1.5 - Math.Abs(4 * normalizedValue - 2)))));
                    int b = (int)(Math.Max(0, Math.Min(255, 255 * (1.5 - Math.Abs(4 * normalizedValue - 1)))));
                    int alpha = (int)(255 * normalizedValue); ;       // Tăng dần từ 0 đến 255                
                    if (normalizedValue > 0.2)
                    {
                        alpha = (int)(255 * Math.Min(1, normalizedValue + 0.1));
                    }
                    else if (normalizedValue > 0.3)
                    {
                        alpha = (int)(255 * Math.Min(1, normalizedValue + 0.2));
                    }

                    Color color = Color.FromArgb(alpha, r, g, b);
                    bitmap.SetPixel(j, i, color);
                }
            }

        }

        public void SaveHeatMap(string filename)
        {
            try
            {
                // Dùng đường dẫn tương đối (ưu tiên)
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string path = Path.Combine(appPath, "images");

                Directory.CreateDirectory(path); // Tạo thư mục nếu chưa có
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                filename = filename + "_" + timestamp + ".png";
                string fullPath = Path.Combine(path, filename);
                bitmap.Save(fullPath, ImageFormat.Png);

                DialogResult result= MessageBox.Show("Đã lưu ảnh vào: " + fullPath);
                Bitmap img = new Bitmap("D:\\eye-tracking\\Mouse\\mouse_cursor\\layer.png");
                // Nếu nhấn OK, mở Form preview
                if (result == DialogResult.OK)
                {
                    using (HeatmapPreviewForm previewForm = new HeatmapPreviewForm(img, bitmap))
                    {
                        previewForm.ShowDialog(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message);
            }
        }
    }
}
