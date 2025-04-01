using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static mouse_cursor.LSA;
using System.Collections;

namespace mouse_cursor
{
    public partial class Form1: Form
    {
        private List<Point> mousePositions;
        private Timer timer;
        private bool islogging;
        private bool isPaused;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // <= Fullscreen
            mousePositions = new List<Point>();
            timer = new Timer();
            timer.Interval = 100; // log every 100ms
            timer.Tick += Timer_Tick;

            islogging = false;
            isPaused = false;
            btnPause.Visible = false;
            btnStop.Visible = false;
            btnExport.Visible = false;
            btnheat.Visible = false;
            lsa_btn.Visible = false;
            label1.Text = "Press Start to begin logging";
            
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            if(islogging && !isPaused)
            {
                mousePositions.Add(Cursor.Position);
                label1.Text = $"(x, y) = ({Cursor.Position.X}, {Cursor.Position.Y})";
            }
            
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            mousePositions.Clear();
            islogging = true;
            timer.Start();
            btnStart.Visible = false;
            btnPause.Visible = true;
            btnStop.Visible = true;
            btnExport.Visible = false;
            lsa_btn.Visible = false;
            btnheat.Visible = false;

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(isPaused)
            {
                isPaused = false;
                btnPause.Text = "Pause";
            }
            else
            {
                isPaused = true;
                btnPause.Text = "Resume";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            islogging = false;
            isPaused = false;
            timer.Stop();
            btnStart.Visible = true;
            btnPause.Visible = false;
            btnStop.Visible = false;
            btnExport.Visible = true;
            btnheat.Visible = true;
            lsa_btn.Visible = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (mousePositions.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                saveFileDialog.Title = "Save the mouse cursor data";
                saveFileDialog.ShowDialog();

                if(saveFileDialog.FileName != "")
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (Point p in mousePositions)
                        {
                            sw.WriteLine($"{p.X},{p.Y}");
                        }
                    }
                    MessageBox.Show("Data exported successfully!", "Export complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No data to export!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnheat_Click(object sender, EventArgs e)
        {

            // Lấy kích thước full màn hình
            Screen screen = Screen.PrimaryScreen;
            int screenWidth = screen.Bounds.Width;
            int screenHeight = screen.Bounds.Height;

            HeatMap heatMapImage = new HeatMap(screenWidth, screenHeight, 61, 12);
            heatMapImage.SetDatas((mousePositions));
            heatMapImage.GenerateHeatMap();
            heatMapImage.SaveHeatMap("heatmap");

        }

        public readonly struct PanelBounds
        {
            public string PanelName { get; }  // Tên Panel
            public float Xmin { get; }       // Tọa độ X nhỏ nhất
            public float Ymin { get; }       // Tọa độ Y nhỏ nhất
            public float Xmax { get; }       // Tọa độ X lớn nhất
            public float Ymax { get; }       // Tọa độ Y lớn nhất

            // Constructor
            public PanelBounds(string panelName, float xmin, float ymin, float xmax, float ymax)
            {
                PanelName = panelName;
                Xmin = xmin;
                Ymin = ymin;
                Xmax = xmax;
                Ymax = ymax;
            }

            // Tính chiều rộng/chiều cao (tuỳ chọn)
            public float Width => Xmax - Xmin;
            public float Height => Ymax - Ymin;

            // In thông tin (tuỳ chọn)
            public override string ToString() =>
                $"{PanelName}: Xmin={Xmin}, Ymin={Ymin}, Xmax={Xmax}, Ymax={Ymax}";
        }

        List<PanelBounds> aoi_region;
        List<Transition> _transitions;
        private void lsa_btn_Click(object sender, EventArgs e)
        {
            // Tạo danh sách chứa tọa độ các Panel
            if (aoi_region != null) { aoi_region.Clear(); }
            aoi_region = new List<PanelBounds>();

            // Lưu tọa độ vào danh sách
            SavePanelCoordinates(AOI1);
            SavePanelCoordinates(AOI2);
            SavePanelCoordinates(AOI3);
            SavePanelCoordinates(AOI4);

            // Truyền aoi_region sang class LSA
            LSA lsa = new LSA(aoi_region);
            lsa.assign_aoi(mousePositions);
            _transitions=lsa.CalculateTransitions();
            Dictionary<(string From, string To), (int Count, double Percentage)> transitionStats = lsa.CalculateTransitionStats(_transitions);
            Dictionary<string, double[]> aoiTime = lsa.CountConsecutiveAoisLinq();
            List<AoiStats> aoiStats = lsa.CalculateAoiStats(aoiTime);
            using (LSA_form lsaForm = new LSA_form(aoiStats, transitionStats))
            {
                lsaForm.ShowDialog();
            }
        }

        // Hàm lưu tọa độ Panel
        void SavePanelCoordinates(Panel panel)
        {
            float xMin = panel.Location.X;
            float yMin = panel.Location.Y;
            float xMax = panel.Location.X + panel.Width;
            float yMax = panel.Location.Y + panel.Height;
            string panelName = panel.Name;

            aoi_region.Add(new PanelBounds(panelName, xMin, yMin, xMax, yMax));
        }
    }
}
