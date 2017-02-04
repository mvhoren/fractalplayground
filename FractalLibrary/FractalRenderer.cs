using Fractal.Library.FSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;

namespace Fractal
{
	public class FractalRenderer
	{
		private double phi = 1.61803398874989484820458683436;

		public ComplexPlane ComplexPlane { get; }
		public Size Size { get; }
		Range horizontal { get; }
		Range vertical { get; }

		private readonly int iterationCutoff = 500;
		private Color[] palette;

		public FractalRenderer(ComplexPlane complexPlane, Size size)
		{
			ComplexPlane = complexPlane;
			Size = size;
			horizontal = new Range(0, Size.Width);
			vertical = new Range(0, Size.Height);
			palette = Enumerable.Range(0, 16).Select(i => Color.FromArgb(0, 0, 255 - i * 8)).Union(
					  Enumerable.Range(0, 16).Select(i => Color.FromArgb(0, 255 - i * 8, 0))).Union(
					  Enumerable.Range(0, 16).Select(i => Color.FromArgb(255 - i * 8, 0, 0))).ToArray();
		}

		public void RenderAsBitmap()
		{
			Bitmap bitmap = new Bitmap(Size.Width, Size.Height);

			Func<int, int, int> getIterations = (x, y) =>
			{
				var r = horizontal.Map(x, ComplexPlane.Real);
				var i = vertical.Map(y, ComplexPlane.Imaginary);

				var numIterations = 0;
				
				while (numIterations < iterationCutoff && i < 2.0 && r < 2.0)
				{
					var rnew =  r * r + i * i * -1 + 1 - phi;
					var ri = r * i;
					var inew = ri + ri;

					r = rnew;
					i = inew;

					numIterations++;
				}

				return numIterations;
			};

			var pixels = (
				from x in Enumerable.Range(0, Size.Width)
				from y in Enumerable.Range(0, Size.Height)
				select new { x, y })
			.AsParallel()
			.Select(p => new 
			{
				x = p.x,
				y = p.y, 
				i = getIterations(p.x, p.y) 
			})
			.ToArray();

			BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int stride = ;
			
			unsafe
			{
				byte* bitmapPointer = (byte*)data.Scan0;

				var i = 1;

				for (var x = 0; x < bitmap.Width; x++)
				{
					var offset = x * 3;

					for (var y = 0; y < bitmap.Height; y++)
					{
						var stride = y * data.Stride;

						//Color c = i == iterationCutoff ? Color.Black : palette[i % palette.Length];

						bitmapPointer[offset + stride] = 255;// c.B;
						bitmapPointer[offset + stride + 1] = 255;// c.G;
						bitmapPointer[offset + stride + 2] = 255; // c.R;
					}
				}
			}

			bitmap.UnlockBits(data);
			*/

			//return bitmap;
		}
	}
}
