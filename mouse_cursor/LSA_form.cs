using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static mouse_cursor.LSA;

namespace mouse_cursor
{
    public partial class LSA_form : Form
    {
        private List<AoiStats> _aoiStats;
        private Dictionary<(string From, string To), (int Count, double Percentage)> _transitionStats;
        public LSA_form(List<AoiStats> aoiStats, Dictionary<(string From, string To), (int Count, double Percentage)> transitionStats)
        {
            InitializeComponent();
            this.Text = "LSA - Fullscreen (ESC to exit)";
            this.WindowState = FormWindowState.Maximized;
            this.KeyPreview = true;
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            };
            _aoiStats = aoiStats;
            _transitionStats = transitionStats;
            List<AoiStats> sortedList = _aoiStats.OrderBy(x => x.AoiName).ToList();
            DisplayAoiStats(dataGridView1, sortedList);
            var sortedDict = _transitionStats.OrderBy(x => x.Key)
                              .ToDictionary(x => x.Key, x => x.Value);
            DisplayTransitionStats(dataGridView2, sortedDict);
        }

        public void DisplayAoiStats(DataGridView dataGridView, List<AoiStats> statsList)
        {
            // Xóa dữ liệu cũ (nếu có)
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);

            // Tạo cột cho DataGridView
            dataGridView.Columns.Add("AoiName", "AOI Name");
            dataGridView.Columns.Add("Mean", "Mean");
            dataGridView.Columns.Add("StdDev", "Standard Deviation");
            dataGridView.Columns.Add("Max", "Max");
            dataGridView.Columns.Add("Min", "Min");

            // Định dạng cột số
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Index > 0) // Cột số (Mean, StdDev, Max, Min)
                {
                    column.DefaultCellStyle.Format = "F2";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Thêm dữ liệu từ statsList vào DataGridView
            foreach (var stats in statsList)
            {
                dataGridView.Rows.Add(
                    stats.AoiName,
                    stats.Mean,
                    stats.StdDev,
                    stats.Max,
                    stats.Min
                );
            }

            // Tự động điều chỉnh kích thước cột
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }
        public void DisplayTransitionStats(DataGridView dataGridView, Dictionary<(string From, string To), (int Count, double Percentage)> statsDict)
        {
            // Xóa dữ liệu cũ (nếu có)
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Regular);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            // Tạo cột cho DataGridView
            dataGridView.Columns.Add("FromAoi", "From AOI");
            dataGridView.Columns.Add("ToAoi", "To AOI");
            //dataGridView.Columns.Add("Count", "Count");
            dataGridView.Columns.Add("Percentage", "Percentage (%)");
            // Định dạng cột số để hiển thị 2 chữ số thập phân
            dataGridView.Columns["Percentage"].DefaultCellStyle.Format = "F2";
            // Thêm dữ liệu từ statsDict vào DataGridView
            foreach (var stats in statsDict)
            {
                dataGridView.Rows.Add(
                    stats.Key.From,
                    stats.Key.To,
                    //stats.Value.Count,
                    stats.Value.Percentage
                );
            }
            // Tự động điều chỉnh kích thước cột
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
    }
}
