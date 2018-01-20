using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PenteApplication
{
    public class Intersection : INotifyPropertyChanged
    {
        public Intersection()
        {
            IntersectionFill = Fill.Empty;
        }

        private Fill intersectionFill;

        public Fill IntersectionFill
        {
            get { return intersectionFill; }
            set {
                intersectionFill = value;
                FieldChanged();
            }
        }


        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        protected void FieldChanged([CallerMemberName] string field = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(field));
            }
        }
    }

    public enum Fill
    {
        Black,
        White,
        Empty
    }
}
