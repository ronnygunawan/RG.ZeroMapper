using RG.ZeroMapper.Structural;

// Sample code demonstrating RG.ZeroMapper.Structural

class A
{
    public int X { get; set; }
    public int Y { get; set; }
}

class B
{
    public int Y { get; set; }
    public int Z { get; set; }
}

// Intersection contains only common properties (Y)
partial class D : Intersect<A, B>
{
    // D implicitly contains Y
}

// Union contains all properties (X, Y, Z)
partial class E : Union<A, B>
{
    // E implicitly contains X, Y, Z
}

// OneOf is a discriminated union
partial class F : OneOf<A, B>
{
    // F can hold either A or B
}

class Program
{
    static void Main(string[] args)
    {
        // Intersection example
        var intersection = new D { Y = 42 };
        Console.WriteLine($"Intersection.Y = {intersection.Y}");
        
        // Union example
        var union = new E { X = 1, Y = 2, Z = 3 };
        Console.WriteLine($"Union.X = {union.X}, Union.Y = {union.Y}, Union.Z = {union.Z}");
        
        // OneOf example
        F oneOf = new A { X = 100, Y = 200 };
        var asA = (A?)oneOf;
        if (asA != null)
        {
            Console.WriteLine($"OneOf as A: X = {asA.X}, Y = {asA.Y}");
        }
        
        Console.WriteLine("Structural typing demonstration complete!");
    }
}
