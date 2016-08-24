using System;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Fractal
{
	public class FractalRenderer
	{
		public ComplexPlane ComplexPlane { get; }
		public Size Size { get; }
		Range horizontal { get; }
		Range vertical { get; }
		public FractalRenderer(ComplexPlane complexPlane, Size size)
		{
			ComplexPlane = complexPlane;
			Size = size;
			horizontal = new Range(0, size.Width);
			vertical = new Range(0, size.Height);
		}

		public Complex ScreenToComplex(Point p) =>
			new Complex(horizontal.Map(p.X, ComplexPlane.Real), vertical.Map(p.Y, ComplexPlane.Imaginary));

		public Bitmap RenderAsBitmap(Func<Complex, Complex> map)
		{
			Bitmap bitmap = new Bitmap(Size.Width, Size.Height);

			var maxIterations = 0;
			var iterationMap = new int[Size.Width, Size.Height];

			for (var x = 0; x < bitmap.Width; x++)
				for (var y = 0; y < bitmap.Height; y++)
				{
					var complex = ScreenToComplex(new Point(x, y));
					var iteration = 0;
					var iterationCutoff = 1000;

					while (iteration < iterationCutoff && complex.Magnitude < 2.0)
					{
						complex = map(complex);
						iteration++;
					}

					iterationMap[x, y] = iteration;
					maxIterations = Math.Max(maxIterations, iteration);
				}

			for (var x = 0; x < bitmap.Width; x++)
				for (var y = 0; y < bitmap.Height; y++)
				{
					var s = iterationMap[x, y] / (double)maxIterations;
					bitmap.SetPixel(x, y, Color.FromArgb((byte)Math.Floor(s * 255.0), 0, 0));
				}

			return bitmap;
		}
	}
}
