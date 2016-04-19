using System.Drawing;

namespace FEFTwiddler.Extensions
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// Sets the opacity (alpha) of this bitmap, from 0 (transparent) to 255 (opaque).
        /// </summary>
        public static void SetOpacity(this Bitmap bmp, byte opacity)
        {
            var width = bmp.Size.Width;
            var height = bmp.Size.Height;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var currentPixel = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(opacity, currentPixel.R, currentPixel.G, currentPixel.B));
                }
            }
        }

        /// <summary>
        /// Sets the opacity (alpha) of this bitmap, from 0 (transparent) to 255 (opaque). Ignore pixels whose opacity is below <param name="unlessOpacityBelow"/>.
        /// </summary>
        public static void SetOpacity(this Bitmap bmp, byte opacity, byte unlessOpacityBelow)
        {
            var width = bmp.Size.Width;
            var height = bmp.Size.Height;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var currentPixel = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb((currentPixel.A <= unlessOpacityBelow ? currentPixel.A : opacity), currentPixel.R, currentPixel.G, currentPixel.B));
                }
            }
        }
    }
}
