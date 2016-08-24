namespace Fractal
{
	public class Range
	{
		public double From { get; }
		public double To { get; }
		public double Span => To - From;
		public Range(double from, double to)
		{
			From = from;
			To = to;
		}
		public double AsUnit(double value) =>
			(value - From) / Span;
		public double Map(double value, Range to) =>
			to.From + AsUnit(value) * to.Span;
	}
}