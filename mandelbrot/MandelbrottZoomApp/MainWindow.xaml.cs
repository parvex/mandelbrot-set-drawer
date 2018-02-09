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
        MandelbrotImage mandelbrott;
        public MainWindow()
        {
            mandelbrott = new MandelbrotImage();
            InitializeComponent();
            set.Source = getSource(mandelbrott.getBitmap());
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

            mandelbrott.centerAndZoom(position.X, position.Y);
            set.Source = getSource(mandelbrott.getBitmap());
        }
    }
}
