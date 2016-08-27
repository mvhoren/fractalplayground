using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fractal;
using System.Drawing;
using System.Numerics;

namespace FractalTests
{
	[TestClass]
	public class FractalTests
	{
		private FractalRenderer fractalRenderer;

		[TestInitialize]
		public void Test() =>
			fractalRenderer = new FractalRenderer(new ComplexPlane(new Range(-1, 1), new Range(-1, 1)), new Size(1024, 1024));
		
		[TestMethod]
		public void convert_screen_to_complex() 
		{
			Assert.AreEqual(fractalRenderer.ScreenToComplex(new Point(0, 0)), new Complex(-1, -1));
			Assert.AreEqual(fractalRenderer.ScreenToComplex(new Point(1024, 0)), new Complex(1, -1));
			Assert.AreEqual(fractalRenderer.ScreenToComplex(new Point(1024, 1024)), new Complex(1, 1));
			Assert.AreEqual(fractalRenderer.ScreenToComplex(new Point(0, 1024)), new Complex(-1, 1));
		}
		
	}
}
