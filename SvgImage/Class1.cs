using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SvgImage
{
    public partial class SvgImage : SkiaSharp.Views.UWP.SKXamlCanvas
    {
        SkiaSharp.Extended.Svg.SKSvg _skSvg;

        public SvgImage(Assembly assembly, string resourceId)
        {
            PaintSurface += OnPaintSurface;

            var name = assembly.FullName.Split(',')[0];
            using (var stream = assembly.GetManifestResourceStream(name + "." + resourceId))
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


    }
}
