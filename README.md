# RG.ZeroMapper

`RG.ZeroMapper` is a Zero boilerplate (kind of) C# object mapper.

```cs
Bar b = new { X = 1 };

// Auto implicit cast
Foo f = b;

// The only boilerplate: Mark destination class as partial
partial class Foo {
    public int X;
    public string S;
}

partial class Bar {
    // same name, but it's a property instead of a field
    public int X { get; set; }
    
    // same name, different name casing
    public string s;
}
```

# RG.ZeroMapper.Structural

`RG.ZeroMapper.Structural` leverages `RG.ZeroMapper` to enable structural typing in your C# code:

```cs
class A {
    public int X;
    public int Y;
}

class B {
    public int Y;
    public int Z;
}

record C() : Constant(1);

partial class D : Intersect<A, B, C> {
    // D implicitly contains Y
}

partial class E : Union<A, B, C> {
    // E implicitly contains X, Y, Z
    // E can be implicitly cast to int with the value of 1
    // E can be implicitly cast from int as long as the value is 1
    // E can be implicitly cast to C?
    // Unions can only contain 1 constant value of each value type
}

partial class F : OneOf<A, B, C> {
    // F is a discriminated union
    // F can be cast into A?, B?, or C?
    // Discriminated unions don't inherit any property
}
```
