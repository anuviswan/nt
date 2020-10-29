using Nt.Utils.Helper.Enumerations;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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
          //  DataContext = this;
        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register(nameof(UserName), typeof(string), typeof(Avataar), new PropertyMetadata("Naina",UserNameChanged));

        private static void UserNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public Direction ImagePosition
        {
            get { return (Direction)GetValue( ImagePositionProperty); }
            set 
            { 
                SetValue(ImagePositionProperty, value);
                this.ContentTemplate = this.ContentTemplateSelector.SelectTemplate(value,this);
            }
        }


        public static readonly DependencyProperty ImagePositionProperty =
            DependencyProperty.Register(nameof(ImagePosition), typeof(Direction), typeof(Avataar), new PropertyMetadata(Direction.Left,DirectionChanged));

        private static void DirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
