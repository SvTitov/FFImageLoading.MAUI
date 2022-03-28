using System;
using SkiaSharp;

namespace FFImageLoading.Svg.MAUI
{
    internal interface ISKSvgFill
    {
        void ApplyFill(SKPaint fill, SKRect bounds);
    }
}
