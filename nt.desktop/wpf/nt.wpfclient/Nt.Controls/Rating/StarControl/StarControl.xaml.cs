using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nt.Controls.Rating.StarControl
{
    /// <summary>
    /// Interaction logic for StarControl.xaml
    /// </summary>
    public partial class StarControl : UserControl
    {
        public StarControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            
        }

        public int EndPoints
        {
            get { return (int)GetValue(EndPointsProperty); }
            set { SetValue(EndPointsProperty, value); }
        }

        public static readonly DependencyProperty EndPointsProperty =
            DependencyProperty.Register(nameof(EndPoints), typeof(int), typeof(StarControl), new PropertyMetadata(0));



        public Size Size
        {
            get { return (Size)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(nameof(Size), typeof(Size), typeof(StarControl), new PropertyMetadata(new Size(10,10)));




    }
}
