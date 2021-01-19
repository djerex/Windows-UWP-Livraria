﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace LivrariaVirtualApp.UWP.local_Controls
{
    public class WindowTitle : Control
    {
        public string Prefix
        {
            get { return (string)GetValue(PrefixProperty); }
            set { SetValue(PrefixProperty, value); }
        }
        public static readonly DependencyProperty PrefixProperty = DependencyProperty.Register(nameof(Prefix), typeof(string), typeof(WindowTitle), new PropertyMetadata(null, TitleChanged));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(WindowTitle), new PropertyMetadata(null, TitleChanged));

        private static void TitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as WindowTitle;
            ApplicationView.GetForCurrentView().Title = $"{control.Prefix} {control.Title}".Trim();
        }
    }


}
