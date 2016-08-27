using Fractal;
using Fractal.Library.FSharp;
using System.Drawing;
using System.Windows.Forms;

namespace FractalWinForm
{
	public partial class FormMain : Form
	{
		private double phi = 1.61803398874989484820458683436;
		private Range realRange = new Range(-2, 2);
		private Range imaginaryRange = new Range(-2, 2);
		private bool isZooming = false;

		public FormMain() 
		{
			InitializeComponent();
			viewport.MouseDown += Viewport_MouseDown;
			viewport.MouseUp += Viewport_MouseUp;
			viewport.MouseMove += Viewport_MouseMove;

			Render();
		}

		private void Viewport_MouseDown(object sender, MouseEventArgs e)
		{
			isZooming = true;
			panel1.Location = e.Location;
			panel1.Visible = true;
		}

		private void Viewport_MouseMove(object sender, MouseEventArgs e)
		{
			if (!isZooming)
				return;

			panel1.Width = e.Location.X - panel1.Location.X;
			panel1.Height = e.Location.Y - panel1.Location.Y;
		}

		private void Viewport_MouseUp(object sender, MouseEventArgs e)
		{
			if (!isZooming)
				return;

			panel1.Visible = false;
			isZooming = false;

			var viewportWidth = new Range(0, viewport.Width);
			realRange = new Range
			(
				viewportWidth.Map(panel1.Location.X, realRange),
				viewportWidth.Map(panel1.Location.X + panel1.Width, realRange)
			);

			var viewportHeight = new Range(0, viewport.Height);
			imaginaryRange = new Range
			(
				viewportHeight.Map(panel1.Location.Y, imaginaryRange),
				viewportHeight.Map(panel1.Location.Y + panel1.Height, imaginaryRange)
			);

			Render();
		}

		private void Render()  =>
			viewport.Image = new FractalRenderer
			(
				new ComplexPlane
				(
					realRange,
					imaginaryRange
				), 
				new Size(viewport.Width, viewport.Height)
			)
			.RenderAsBitmap((c) => c * c + 1.0 - phi);
	}
}
