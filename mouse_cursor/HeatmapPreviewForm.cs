using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mouse_cursor
{
    public partial class HeatmapPreviewForm : Form
    {
        private readonly Bitmap _backgroundImage;
        private readonly Bitmap _heatmap;
        private readonly PictureBox _pictureBox;

        public HeatmapPreviewForm(Bitmap backgroundImage, Bitmap heatmap)
        {
            InitializeComponent();

            // Thiết lập form
            this.Text = "Heatmap Overlay - Fullscreen (ESC to exit)";
            this.WindowState = FormWindowState.Maximized;
            this.KeyPreview = true;

            // Lưu trữ ảnh
            _backgroundImage = backgroundImage;
            _heatmap = heatmap;

            // Tạo PictureBox composite
            _pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White // Màu nền nếu ảnh nhỏ hơn màn hình
            };
            this.Controls.Add(_pictureBox);

            // Tạo ảnh kết hợp
            UpdateCompositeImage();

            // Sự kiện bàn phím
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            };
        }

        private void UpdateCompositeImage()
        {
            // Tạo ảnh kết hợp giữa nền và heatmap
            Bitmap composite = new Bitmap(_backgroundImage.Width, _backgroundImage.Height);

            using (Graphics g = Graphics.FromImage(composite))
            {
                // Vẽ ảnh nền
                g.DrawImage(_backgroundImage, 0, 0);

                // Vẽ heatmap chỉ tại các pixel không trong suốt
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorKey(Color.Transparent, Color.Transparent);

                g.DrawImage(
                    _heatmap,
                    new Rectangle(0, 0, _backgroundImage.Width, _backgroundImage.Height),
                    0, 0, _heatmap.Width, _heatmap.Height,
                    GraphicsUnit.Pixel,
                    attributes);
            }

            _pictureBox.Image = composite;
        }
    }

}
