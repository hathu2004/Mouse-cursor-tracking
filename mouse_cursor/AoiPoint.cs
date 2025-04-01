using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mouse_cursor
{
    public class AoiPoint
    {
        public string AoiName { get; set; }
        public float X { get; set; }
        public float Y { get; set; }


        // Constructor mặc định
        public AoiPoint() { }

        // Constructor có tham số
        public AoiPoint(string aoiName, float x, float y)
        {
            AoiName = aoiName;
            X = x;
            Y = y;
        }
    }
}
