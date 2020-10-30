using Nt.Utils.Helper.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nt.Controls.HelperControls
{
    public class AvataarContentTemplateSelector : DataTemplateSelector 
    {
        public DataTemplate ImageOnLeftTemplate { get; set; }
        public DataTemplate ImageOnRightTemplate { get; set; }
        public DataTemplate ImageOnTopTemplate { get; set; }
        public DataTemplate ImageOnBottomTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is Avataar avataar)) return base.SelectTemplate(item, container);
            switch ((Direction)avataar.ImagePosition)
            {
                case Direction.Left: return ImageOnLeftTemplate;
                case Direction.Right: return ImageOnRightTemplate;
                default: return base.SelectTemplate(item, container);
            }
        }
    }
}
