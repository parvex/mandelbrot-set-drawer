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
using System.IO;
using System.Drawing;
using mandelbrot;

namespace MandelbrottZoomApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int nrZoom = 1;
        LimitedStack<MandelbrotImage> stack = new LimitedStack<MandelbrotImage>(10);
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            stack.Push(new MandelbrotImage());
            InitializeComponent();
            set.Source = getSource(stack.Peek().getBitmap());
        }


        BitmapImage getSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private void setCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position = Mouse.GetPosition(set);
            
            stack.Push(new MandelbrotImage(stack.Peek()));
            stack.Peek().centerAndZoom(position.X, position.Y);
            set.Source = getSource(stack.Peek().getBitmap());
            nrZoom++;

        }

        private void setCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (stack.Count == 1)
                return;
            stack.Pop();
            set.Source = getSource(stack.Peek().getBitmap());
            nrZoom--;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            stack.Clear();
            stack.Push(new MandelbrotImage());
            set.Source = getSource(stack.Peek().getBitmap());
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Mandelbrott";
            dlg.DefaultExt = ".png"; 
            dlg.Filter = "Png files (.png)|*.png"; 

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {

                stack.Peek().getBitmap().Save(dlg.FileName);
            }
        }
    }
}
