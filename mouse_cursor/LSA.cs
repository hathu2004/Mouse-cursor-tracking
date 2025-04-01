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
using static mouse_cursor.Form1;
using MathNet.Numerics.Statistics;

namespace mouse_cursor
{
    
    public class LSA
    {
        private List<PanelBounds> _aoiRegion;

        // Constructor nhận dữ liệu từ Form1
        public LSA(List<PanelBounds> aoiRegion)
        {
            this._aoiRegion = aoiRegion;
        }
        private List<AoiPoint> _aoiPoints = new List<AoiPoint>();
        public void assign_aoi(List<Point> points)
        {
            foreach (Point point in points)
            { 
                bool isAssigned = false;
                foreach (PanelBounds aoi in _aoiRegion)
                {
                    if (point.X >= aoi.Xmin && point.X <= aoi.Xmax && point.Y >= aoi.Ymin && point.Y <= aoi.Ymax)
                    {
                        _aoiPoints.Add(new AoiPoint(aoi.PanelName, point.X, point.Y));
                        isAssigned = true;
                    }
                }
                if (!isAssigned)
                {
                    _aoiPoints.Add(new AoiPoint("Outside", point.X, point.Y));
                }
            }
        }
        public readonly struct Transition
        {
            public string FromAoi { get; }  // AOI nguồn
            public string ToAoi { get; }    // AOI đích

            public Transition(string fromAoi, string toAoi)
            {
                FromAoi = fromAoi;
                ToAoi = toAoi;
            }
        }

        public List<Transition> CalculateTransitions()
        {
            List<Transition> _transitions = new List<Transition>();
            for (int i = 0; i < _aoiPoints.Count - 1; i++)
            {
                var fromAoi = _aoiPoints[i].AoiName;
                var toAoi = _aoiPoints[i + 1].AoiName;
                if (fromAoi != toAoi)
                {
                    _transitions.Add(new Transition(fromAoi, toAoi));
                }
            }
            return _transitions;
        }

        public Dictionary<(string From, string To), (int Count, double Percentage)>
            CalculateTransitionStats(List<Transition> transitions)
        {
            // Bước 1: Nhóm các transition giống nhau và đếm số lần xuất hiện
            var transitionCounts = transitions
                .GroupBy(t => (t.FromAoi, t.ToAoi))  // Nhóm theo cặp (From, To)
                .ToDictionary(
                    g => g.Key,                      // Key: (FromAoi, ToAoi)
                    g => g.Count()                   // Value: Số lần xuất hiện
                );

            // Bước 2: Tính tổng số transitions
            int totalTransitions = transitions.Count;

            // Bước 3: Tính phần trăm cho mỗi transition
            var stats = transitionCounts
                .ToDictionary(
                    kvp => kvp.Key,                  // Giữ nguyên key (From, To)
                    kvp => (
                        Count: kvp.Value,            // Số lần xuất hiện
                        Percentage: Math.Round((double)kvp.Value / totalTransitions * 100, 2)  // Phần trăm
                    )
                );

            return stats;
        }
        public Dictionary<string, double[]> CountConsecutiveAoisLinq()
        {
            List<string> aoiList = _aoiPoints.Select(a => a.AoiName).ToList();
            const double interval = 0.05;
            return aoiList
                .Select((aoi, index) => new { Aoi = aoi, Index = index })
                .GroupBy(x => x.Aoi)
                .ToDictionary(
                    g => g.Key,
                    g => {
                        var pointer = -1;
                        return g.Select(x => x.Index)
                              .OrderBy(i => i)
                              .Aggregate(
                                  new List<double>(),
                                  (list, index) =>
                                  {
                                      if (list.Count == 0 || index != pointer + 1)
                                      {
                                          list.Add(interval);
                                      }
                                      else
                                      {
                                          list[list.Count - 1]+= interval;
                                      }
                                      pointer = index;
                                      return list;
                                  })
                              .Select(x => (double)x)
                              .ToArray();
                    }
                );
        }

        public readonly struct AoiStats
        {
            public string AoiName { get;}  // Tên AOI (key của Dictionary)
            public double Mean { get;}     // Giá trị trung bình
            public double StdDev { get;}   // Độ lệch chuẩn
            public double Max { get;}      // Giá trị lớn nhất
            public double Min { get;}      // Giá trị nhỏ nhất

            public AoiStats(string aoiName, double mean, double stdDev, double max, double min)
            {
                AoiName = aoiName;
                Mean = mean;
                StdDev = stdDev;
                Max = max;
                Min = min;
            }
        }

        public List<AoiStats> CalculateAoiStats(Dictionary<string, double[]> aoiTime)
        {
            var statsList = new List<AoiStats>();

            foreach (var kvp in aoiTime)
            {
                string aoiName = kvp.Key;
                double[] values = kvp.Value;

                if (values == null || values.Length == 0)
                    continue; // Bỏ qua nếu mảng rỗng

                // Tính toán các giá trị thống kê
                statsList.Add(new AoiStats(aoiName, values.Average(), values.StandardDeviation(), values.Max(), values.Min()));
            }

            return statsList;
        }

    }
}
