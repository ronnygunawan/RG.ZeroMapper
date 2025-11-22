namespace RG.ZeroMapper.Tests;

public class BasicMappingTests
{
    [Fact]
    public void ImplicitCast_ShouldMapFieldToField_WhenTypesMatch()
    {
        // Arrange
        var source = new SourceWithFields { X = 42, Y = "test" };

        // Act
        TargetWithFields target = source;

        // Assert
        target.X.ShouldBe(42);
        target.Y.ShouldBe("test");
    }

    [Fact]
    public void ImplicitCast_ShouldMapPropertyToProperty_WhenTypesMatch()
    {
        // Arrange
        var source = new SourceWithProperties { A = 100, B = "hello" };

        // Act
        TargetWithProperties target = source;

        // Assert
        target.A.ShouldBe(100);
        target.B.ShouldBe("hello");
    }

    [Fact]
    public void ImplicitCast_ShouldMapPropertyToField_WhenTypesMatch()
    {
        // Arrange
        var source = new SourceMixed { Value = 99 };

        // Act
        TargetMixed target = source;

        // Assert
        target.Value.ShouldBe(99);
    }

    [Fact]
    public void ImplicitCast_ShouldMapCaseInsensitively()
    {
        // Arrange
        var source = new SourceCasing { lowercase = "abc" };

        // Act
        TargetCasing target = source;

        // Assert
        target.LOWERCASE.ShouldBe("abc");
    }

    [Fact]
    public void ImplicitCast_ShouldMapOnlyMatchingProperties()
    {
        // Arrange
        var source = new SourcePartial { X = 1, Y = 2, Z = 3 };

        // Act
        TargetPartial target = source;

        // Assert
        target.X.ShouldBe(1);
        target.Y.ShouldBe(2);
        target.W.ShouldBe(0); // Should remain default
    }
}

// Test classes for field-to-field mapping
partial class SourceWithFields
{
    public int X;
    public string? Y;
}

partial class TargetWithFields
{
    public int X;
    public string? Y;
}

// Test classes for property-to-property mapping
partial class SourceWithProperties
{
    public int A { get; set; }
    public string? B { get; set; }
}

partial class TargetWithProperties
{
    public int A { get; set; }
    public string? B { get; set; }
}

// Test classes for mixed mapping
partial class SourceMixed
{
    public int Value { get; set; }
}

partial class TargetMixed
{
    public int Value;
}

// Test classes for case-insensitive mapping
partial class SourceCasing
{
    public string? lowercase;
}

partial class TargetCasing
{
    public string? LOWERCASE;
}

// Test classes for partial mapping
partial class SourcePartial
{
    public int X;
    public int Y;
    public int Z;
}

partial class TargetPartial
{
    public int X;
    public int Y;
    public int W;
}
