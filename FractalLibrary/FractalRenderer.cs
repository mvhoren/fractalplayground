using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Fractal
{
	public class FractalRenderer
	{
		public ComplexPlane ComplexPlane { get; }
		public Size Size { get; }
		Range horizontal { get; }
		Range vertical { get; }

		private readonly int iterationCutoff = 150;
		private Color[] palette;

		public FractalRenderer(ComplexPlane complexPlane, Size size)
		{
			ComplexPlane = complexPlane;
			Size = size;
			horizontal = new Range(0, size.Width);
			vertical = new Range(0, size.Height);
			palette = Enumerable.Range(0, 16).Select(i => Color.FromArgb(0, 0, 255 - i * 8)).Union(
					  Enumerable.Range(0, 16).Select(i => Color.FromArgb(0, 255 - i * 8, 0))).Union(
					  Enumerable.Range(0, 16).Select(i => Color.FromArgb(255 - i * 8, 0, 0))).ToArray();
		}

		public Complex ScreenToComplex(Point p) =>
			new Complex(horizontal.Map(p.X, ComplexPlane.Real), vertical.Map(p.Y, ComplexPlane.Imaginary));

		public Bitmap RenderAsBitmap(Func<Complex, Complex> function)
		{
			Bitmap bitmap = new Bitmap(Size.Width, Size.Height);

			Func<int, int, int> getIterations = (x, y) =>
			{
				var complex = ScreenToComplex(new Point(x, y));
				var numIterations = 0;

				while (numIterations < iterationCutoff && complex.Imaginary < 2.0 && complex.Real < 2.0)
				{
					complex = function(complex);
					numIterations++;
				}

				return numIterations;
			};

			var processedPixels = (
				from x in Enumerable.Range(0, bitmap.Width)
				from y in Enumerable.Range(0, bitmap.Height)
				select new { x, y })
			.AsParallel()
			.WithDegreeOfParallelism(Environment.ProcessorCount)
			.Select(p => new 
			{
				x = p.x,
				y = p.y, 
				i = getIterations(p.x, p.y) 
			})
			.ToArray();

			//var paletteScale = palette.Length / (double)processedPixels.Where(p => p.i != iterationCutoff).Max(p => p.i);

			
			BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int stride = data.Stride;
			
			unsafe
			{
				byte* bitmapPointer = (byte*)data.Scan0;

				foreach (var p in processedPixels)
				{
					Color c = p.i == iterationCutoff ? Color.Black : palette[p.i % palette.Length];

					bitmapPointer[(p.x * 3) + p.y * stride] = c.B;
					bitmapPointer[(p.x * 3) + p.y * stride + 1] = c.G;
					bitmapPointer[(p.x * 3) + p.y * stride + 2] = c.R;
				}
			}

			bitmap.UnlockBits(data);
			return bitmap;
		}
	}
}
