using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteApplication
{
    public class Intersection
    {
        public Fill IntersectionFill { get; set; }
    }

    public enum Fill
    {
        Black,
        White,
        Empty
    }
}
