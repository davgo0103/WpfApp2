using System;
using System.Collections.Generic;
using System.IO;
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
        string currtShape = "Line";
        string currentAction = "Draw";
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
            switch (currtShape)
            {
                case "Line":
                    DrawLine();
                    break;
                case "Rectangle":
                    DrawRectangle();
                    break;
                case "Ellipse":
                    DrawEllipse();
                    break;

            }
            MyCanvas.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private void DrawRectangle()
        {
            AdjustPoint();
            

        }

        private void DrawEllipse()
        {
            throw new NotImplementedException();
        }

        private void AdjustPoint()
        {
            throw new NotImplementedException();
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
            currentStrokeBrush = new SolidColorBrush(currentStrokeColor);
            color_button.Content = $"筆刷色彩:{currentStrokeColor.ToString()}";
        }

        private Color GetDialogColor()
        {
            ColorDialog dlg = new ColorDialog();
            dlg.ShowDialog();

            System.Drawing.Color dlgColor = dlg.Color;
            return Color.FromArgb(dlgColor.A, dlgColor.R, dlgColor.G, dlgColor.B);
        }

        private void ShapeButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as System.Windows.Controls.Button;
            currtShape = btn.Content.ToString();
        }

        private void MenuCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (MenuCheckBox.IsChecked == true)
            {
                MyTooBarTray.Visibility = Visibility.Visible;
                MyCanvas.Height -= MyTooBarTray.Height;
            }
            else
            {
                MyTooBarTray.Visibility = Visibility.Collapsed;
                MyCanvas.Height += MyTooBarTray.Height;
            }
        }


        private void ClearCanvasButton_Click(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
            MyCanvas.Cursor = System.Windows.Input.Cursors.Arrow;
            MyLabel.Content = "Ready";
        }

        private void SaveCanvasMenuItem_Click(object sender, RoutedEventArgs e)
        {
            int w = Convert.ToInt32(MyCanvas.RenderSize.Width);
            int h = Convert.ToInt32(MyCanvas.RenderSize.Height);

            
            //將MyCabvas轉成bitmap
            RenderTargetBitmap rtb = new RenderTargetBitmap(w, h, 64d, 64d, PixelFormats.Default);
            rtb.Render(MyCanvas);

            //將bitmap編碼成png
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));

            //開啟存檔對話方塊
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "儲存檔案";
            saveFileDialog.DefaultExt = "*.png";
            saveFileDialog.Filter = "PNG檔案(*.png)|*.png|全部檔案|*.*";
            saveFileDialog.ShowDialog();
            string path = saveFileDialog.FileName;

            //儲存png
            using (var fs = File.Create(path))
            {

            }

        }

        private void MyCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            switch (currentAction)
            {
                case "Draw":
                    //畫圖模式
                    if(e.LeftButton == MouseButtonState.Pressed)
                    {
                        MyCanvas.Cursor = System.Windows.Input.Cursors.Pen;
                        dest = e.GetPosition(MyCanvas);
                        MyLabel.Content = $"座標點:({e.GetPosition(MyCanvas)})-({dest})";
                    }
                    break;
                case "Erase":
                    var selectedShaoe = e.OriginalSource as Shape;
                    MyCanvas.Children.Remove(selectedShaoe);
                    break;
            }
            
        }
    }
}
