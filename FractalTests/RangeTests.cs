using Fractal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FractalTests
{
	[TestClass]
	public class RangeTests
	{
		[TestMethod]
		public void can_calculate_span() =>
			Assert.IsTrue(new Range(1, 3).Span == 2);

		[TestMethod]
		public void can_express_value_as_unit() =>
			Assert.IsTrue(new Range(0, 10).AsUnit(5) == 0.5);

		[TestMethod]
		public void can_map_value_to_range()
		{
			var fromRange = new Range(0, 1);
			var toRange = new Range(1, 10);

			var x = fromRange.Map(fromRange.From, toRange);
			Assert.IsTrue(x == toRange.From);

			var y = fromRange.Map(fromRange.To, toRange);
			Assert.IsTrue(y == toRange.To);

			var z = fromRange.Map(fromRange.To - fromRange.From, toRange);
			Assert.IsTrue(z == toRange.To);
		}
	}
}
