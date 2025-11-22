// Sample code from README.md demonstrating RG.ZeroMapper

partial class Foo
{
    public int X;
    public string? S;
}

partial class Bar
{
    // same name, but it's a property instead of a field
    public int X { get; set; }
    
    // same name, different name casing
    public string? s;
}

class Program
{
    static void Main(string[] args)
    {
        Bar b = new Bar { X = 1, s = "test" };
        
        // Auto implicit cast
        Foo f = b;
        
        Console.WriteLine($"Foo.X = {f.X}, Foo.S = {f.S}");
    }
}
