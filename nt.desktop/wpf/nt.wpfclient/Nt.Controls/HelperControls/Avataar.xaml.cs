using Nt.Utils.Helper.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nt.Controls.HelperControls
{
    /// <summary>
    /// Interaction logic for Avataar.xaml
    /// </summary>
    public partial class Avataar : UserControl
    {
        public Avataar()
        {
            InitializeComponent();
        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register(nameof(UserName), typeof(string), typeof(Avataar), new PropertyMetadata("Naina"));




        public Direction ImagePosition
        {
            get { return (Direction)GetValue( ImagePositionProperty); }
            set { SetValue(ImagePositionProperty, value); }
        }

        public static readonly DependencyProperty ImagePositionProperty =
            DependencyProperty.Register(nameof(ImagePosition), typeof(Direction), typeof(Avataar), new PropertyMetadata(Direction.Left));


    }
}
