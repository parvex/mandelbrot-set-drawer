using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;


namespace mandelbrot
{
    class MandelbrotImage
    {
        Dictionary<int, Color> colors = new Dictionary<int, Color>();
        Bitmap bitmap;
        //size of the image
        int bmpXmax = 1024;
        int bmpYmax = 1024;
        //domain in which mandelbrott set is being tested  
        double xmin = -2.2;
        double xmax = 1.2;
        double ymin = -1.7;
        double ymax = 1.7;
        double height;
        double width;
        //number of iterations before accepting point
        int iter_max = 150;

        int zoom = 4;


        //picking random colors for every iter break numbers
        private void generateColors()
        {
            for(int i = 0; i < iter_max; i++)
            {
                colors[i] = IntToColor(i);
            }
            colors[iter_max] = Color.Black;
        }

        //gradient color generator from 0 to iter_max
        private Color IntToColor(int i)
        {
            Color[] colorz = new Color[] { Color.Black, Color.Blue, Color.Cyan, Color.Green, Color.Yellow, Color.Orange, Color.Red, Color.White, Color.Transparent };
            float scaled = (float)(i) / iter_max * 7;
            Color color0 = colorz[(int)scaled];
            Color color1 = colorz[(int)scaled + 1];
            float fraction = scaled - (int)scaled;

            int resultR = (byte)((1 - fraction) * (float)color0.R + fraction * (float)color1.R);
            int resultG = (byte)((1 - fraction) * (float)color0.G + fraction * (float)color1.G);
            int resultB = (byte)((1 - fraction) * (float)color0.B + fraction * (float)color1.B);
            return Color.FromArgb(resultR, resultG, resultB);
        }

        public MandelbrotImage()
        {
            height = ymax - ymin;
            width = xmax - xmin;
            generateColors();
            bitmap = new Bitmap(bmpXmax, bmpYmax);
            generateBitmap();
        }

        private void generateBitmap()
        {

            for (int x = 1; x < bmpXmax; ++x)
                for (int y = 1; y < bmpYmax; ++y)
                {
                    double Xtemp;
                    DecPoint point = scale(x, y);
                    DecPoint z = new DecPoint(0, 0);
                    int iter;
                    for (iter = 0; iter < iter_max && z.Abs2() < 4; ++iter)
                    {
                        Xtemp = z.X;
                        z.X = z.X * z.X - z.Y * z.Y + point.X;
                        z.Y = 2 * Xtemp * z.Y + point.Y;
                    }

                    bitmap.SetPixel(x, y, colors[iter]);
                }
        }

        //scales pixel to fit in mandelbrott domain
        private DecPoint scale(double x, double y)
        {
            return new DecPoint( ((double)x / bmpXmax) * width + xmin, ((double)y / bmpYmax) * height + ymin);
        }


        public Bitmap getBitmap()
        {
            return bitmap;
        }

        class DecPoint
        {
            DecPoint() { }
            public DecPoint(double x, double y) { X = x;  Y = y; }
            public double X;
            public double Y;

            public double Abs2()
            {
                return X * X + Y * Y;
            }
        }

        public void centerAndZoom(double x, double y)
        {
            DecPoint center = scale((double)x, (double)y);
            height = height / zoom;
            width = width / zoom;
            xmin = center.X - width / zoom;
            xmax = center.X + width / zoom;
            ymin = center.Y - height / zoom;
            ymax = center.Y + height / zoom;
            generateBitmap();
        }
    }

   

}
