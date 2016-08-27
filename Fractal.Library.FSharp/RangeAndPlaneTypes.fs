namespace Fractal.Library.FSharp

type Range = { From: double; To: double } with
    member x.Span = x.To - x.From
    member x.AsUnit value = (value - x.From) / x.Span
    member x.Map value toRange = toRange.From + x.AsUnit(value) * toRange.Span

type ComplexPlane = { Real: Range; Imaginary: Range }