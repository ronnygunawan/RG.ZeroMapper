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
        Console.WriteLine("=== Intersection Example ===");
        // Intersection: A or B can be assigned to D without explicit casting
        A instanceA = new A { X = 10, Y = 42 };
        D intersection = instanceA; // Implicit conversion from A to D
        Console.WriteLine($"Intersection.Y = {intersection.Y}");
        
        B instanceB = new B { Y = 99, Z = 5 };
        intersection = instanceB; // Implicit conversion from B to D
        Console.WriteLine($"Intersection.Y = {intersection.Y}");
        
        // The reverse is NOT valid - cannot assign D to A or B without explicit cast
        // A cannotDoThis = intersection; // This would not compile
        
        Console.WriteLine("\n=== Union Example ===");
        // Union: E can be assigned to A or B without explicit casting
        var union = new E { X = 1, Y = 2, Z = 3 };
        Console.WriteLine($"Union.X = {union.X}, Union.Y = {union.Y}, Union.Z = {union.Z}");
        
        A fromUnionA = union; // Implicit conversion from E to A
        Console.WriteLine($"Converted to A: X = {fromUnionA.X}, Y = {fromUnionA.Y}");
        
        B fromUnionB = union; // Implicit conversion from E to B
        Console.WriteLine($"Converted to B: Y = {fromUnionB.Y}, Z = {fromUnionB.Z}");
        
        // The reverse is NOT valid - cannot assign A or B to E without explicit cast
        // E cannotDoThis = instanceA; // This would not compile
        
        Console.WriteLine("\n=== OneOf Example ===");
        // OneOf: can be assigned to a variable without explicit casting, 
        // but will throw runtime error if wrong type
        F oneOf = new A { X = 100, Y = 200 };
        
        // This works - oneOf contains A
        A asA = oneOf; // Implicit conversion, no runtime error
        Console.WriteLine($"OneOf as A: X = {asA.X}, Y = {asA.Y}");
        
        // This throws runtime error - oneOf contains A, not B
        try
        {
            B asB = oneOf; // Runtime error!
            Console.WriteLine($"OneOf as B: Y = {asB.Y}, Z = {asB.Z}");
        }
        catch (InvalidCastException ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }
        
        // oneOf can be reassigned with a value of type B
        oneOf = new B { Y = 300, Z = 400 };
        Console.WriteLine("Reassigned oneOf with B");
        
        // Now this works
        B newAsB = oneOf; // Implicit conversion, no runtime error
        Console.WriteLine($"OneOf (now B) as B: Y = {newAsB.Y}, Z = {newAsB.Z}");
        
        // And this throws runtime error
        try
        {
            A newAsA = oneOf; // Runtime error!
            Console.WriteLine($"OneOf as A: X = {newAsA.X}, Y = {newAsA.Y}");
        }
        catch (InvalidCastException ex)
        {
            Console.WriteLine($"Expected error: {ex.Message}");
        }
        
        Console.WriteLine("\nStructural typing demonstration complete!");
    }
}
