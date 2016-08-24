namespace Fractal
{
	public class ComplexPlane
	{
		public Range Imaginary { get; }
		public Range Real { get; }

		public ComplexPlane(Range real, Range imaginary)
		{
			Imaginary = imaginary;
			Real = real;
		}
	}
}