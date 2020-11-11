﻿using Nt.Utils.Helper.Enumerations;
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
        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register(nameof(UserName), typeof(string), typeof(Avataar), new PropertyMetadata("Default",UserNamePropertyChanged));

        private static void UserNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public Direction ImagePosition
        {
            get { return (Direction)GetValue( ImagePositionProperty); }
            set 
            { 
                SetValue(ImagePositionProperty, value);
            }
        }


        public static readonly DependencyProperty ImagePositionProperty =
            DependencyProperty.Register(nameof(ImagePosition), typeof(Direction), typeof(Avataar), new PropertyMetadata(Direction.Left,OnImagePositionChanged));

        private static void OnImagePositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
