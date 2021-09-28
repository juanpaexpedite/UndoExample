using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndoExample.Models
{
    public class ColorDescriptor : Entity
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                //SetTrackProperty(ref name, value);
                SetProperty(ref name, value);
            }
        }

        private string color = "#FF00FF";
        public string Color
        {
            get { return color; }
            set
            {
                SetTrackProperty(ref color, value);
                //SetProperty(ref color, value);
            }
        }
    }
}
