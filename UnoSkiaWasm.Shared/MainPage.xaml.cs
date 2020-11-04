using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoSkiaWasm
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SkiaSharp.Extended.Svg.SKSvg _skSvg;

        public MainPage()
        {
            this.InitializeComponent();

            var x = new SkiaSharp.Views.UWP.SKXamlCanvas();
            Grid.SetRow(x, 1);
            _grid.Children.Add(x);
            x.PaintSurface += OnPaintSurface;

            var name = GetType().Assembly.FullName.Split(',')[0];
            using (var stream = GetType().Assembly.GetManifestResourceStream(name + ".Resources.black_cat.svg"))
            {
                _skSvg = new SkiaSharp.Extended.Svg.SKSvg();
                _skSvg.Load(stream);
            }

        }

        void OnPaintSurface(object sender, SkiaSharp.Views.UWP.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            canvas.DrawPicture(_skSvg.Picture);
        }

        /*
        private void OnPaintSurface(object sender, SkiaSharp.Views.UWP.SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var surface = e.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var center = new SKPoint(info.Width / 2, info.Height / 2);
            var radius = Math.Min(center.X, center.Y);

            using (SKPath path = new SKPath())
            {
                for (float angle = 0; angle < 3600; angle += 1)
                {
                    var scaledRadius = radius * angle / 3600;
                    var radians = Math.PI * angle / 180;
                    var x = center.X + scaledRadius * (float)Math.Cos(radians);
                    var y = center.Y + scaledRadius * (float)Math.Sin(radians);
                    var point = new SKPoint(x, y);

                    if (angle == 0)
                    {
                        path.MoveTo(point);
                    }
                    else
                    {
                        path.LineTo(point);
                    }
                }

                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Red,
                    StrokeWidth = 5
                };

                canvas.DrawPath(path, paint);
            }
        }
        */
    }
}
