﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point start, dest;
        Color currentStrokeColor;
        Brush currentStrokeBrush = new SolidColorBrush(Colors.Black);
        int currentStrokeThinkness;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyCanvas.Cursor = System.Windows.Input.Cursors.Cross;
            start = e.GetPosition(MyCanvas);
            MyLabel.Content = $"座標點:({start})-({dest})";
        }

        private void MyCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DrawLine();
        }

        private void DrawLine()
        {
            Line newLine = new Line();
            newLine.Stroke = currentStrokeBrush;
            newLine.StrokeThickness = currentStrokeThinkness;
            newLine.X1 = start.X;
            newLine.Y1 = start.Y;
            newLine.X2 = dest.X;
            newLine.Y2 = dest.Y;

            MyCanvas.Children.Add(newLine);

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentStrokeThinkness = Convert.ToInt32(slider.Value);
        }

        private void coloer_button_Click(object sender, RoutedEventArgs e)
        {
            currentStrokeColor = GetDialogColor();
        }

        private Color GetDialogColor()
        {
            ColorDialog dlg = new ColorDialog();
        }

        private void MyCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            dest = e.GetPosition(MyCanvas);

            MyLabel.Content = $"座標點:({e.GetPosition(MyCanvas)})-({dest})";
        }
    }
}
