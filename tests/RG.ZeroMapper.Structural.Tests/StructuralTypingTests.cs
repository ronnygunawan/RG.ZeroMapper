using RG.ZeroMapper.Structural;

namespace RG.ZeroMapper.Structural.Tests;

public class IntersectTests
{
    [Fact]
    public void Intersect_ShouldContainOnlyCommonProperties()
    {
        // Arrange & Act
        var intersection = new TestIntersection
        {
            Y = 42
        };

        // Assert
        intersection.Y.ShouldBe(42);
    }

    [Fact]
    public void Intersect_ShouldAllowSettingAndGettingCommonProperty()
    {
        // Arrange
        var intersection = new TestIntersection();

        // Act
        intersection.Y = 100;

        // Assert
        intersection.Y.ShouldBe(100);
    }

    [Fact]
    public void Intersect_WithThreeTypes_ShouldContainOnlyCommonProperty()
    {
        // Arrange & Act
        var intersection = new ThreeWayIntersection
        {
            Common = "shared"
        };

        // Assert
        intersection.Common.ShouldBe("shared");
    }

    [Fact]
    public void Intersect_ShouldAllowImplicitConversionFromFirstType()
    {
        // Arrange
        var typeA = new TypeA { X = 10, Y = 42 };

        // Act
        TestIntersection intersection = typeA; // Implicit conversion

        // Assert
        intersection.Y.ShouldBe(42);
    }

    [Fact]
    public void Intersect_ShouldAllowImplicitConversionFromSecondType()
    {
        // Arrange
        var typeB = new TypeB { Y = 99, Z = 5 };

        // Act
        TestIntersection intersection = typeB; // Implicit conversion

        // Assert
        intersection.Y.ShouldBe(99);
    }
}

public class UnionTests
{
    [Fact]
    public void Union_ShouldContainAllProperties()
    {
        // Arrange
        var union = new TestUnion
        {
            X = 1,
            Y = 2,
            Z = 3
        };

        // Act & Assert
        union.X.ShouldBe(1);
        union.Y.ShouldBe(2);
        union.Z.ShouldBe(3);
    }

    [Fact]
    public void Union_ShouldAllowNullableProperties()
    {
        // Arrange
        var union = new TestUnion();

        // Act
        union.X = null;
        union.Y = null;

        // Assert
        union.X.ShouldBeNull();
        union.Y.ShouldBeNull();
    }

    [Fact]
    public void Union_WithMultipleTypes_ShouldContainAllUniqueProperties()
    {
        // Arrange
        var union = new MultiTypeUnion
        {
            Name = "test",
            Age = 25,
            Active = true
        };

        // Act & Assert
        union.Name.ShouldBe("test");
        union.Age.ShouldBe(25);
        union.Active.ShouldBe(true);
    }

    [Fact]
    public void Union_ShouldAllowImplicitConversionToFirstType()
    {
        // Arrange
        var union = new TestUnion { X = 1, Y = 2, Z = 3 };

        // Act
        TypeA asA = union; // Implicit conversion

        // Assert
        asA.X.ShouldBe(1);
        asA.Y.ShouldBe(2);
    }

    [Fact]
    public void Union_ShouldAllowImplicitConversionToSecondType()
    {
        // Arrange
        var union = new TestUnion { X = 1, Y = 2, Z = 3 };

        // Act
        TypeB asB = union; // Implicit conversion

        // Assert
        asB.Y.ShouldBe(2);
        asB.Z.ShouldBe(3);
    }
}

public class OneOfTests
{
    [Fact]
    public void OneOf_ShouldAcceptFirstType()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };

        // Act
        TestOneOf oneOf = typeA;

        // Assert
        oneOf.ShouldNotBeNull();
    }

    [Fact]
    public void OneOf_ShouldCastBackToFirstType()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act
        TypeA result = oneOf; // Implicit conversion

        // Assert
        result.ShouldNotBeNull();
        result.X.ShouldBe(100);
        result.Y.ShouldBe(200);
    }

    [Fact]
    public void OneOf_ShouldAcceptSecondType()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };

        // Act
        TestOneOf oneOf = typeB;

        // Assert
        oneOf.ShouldNotBeNull();
    }

    [Fact]
    public void OneOf_ShouldCastBackToSecondType()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };
        TestOneOf oneOf = typeB;

        // Act
        TypeB result = oneOf; // Implicit conversion

        // Assert
        result.ShouldNotBeNull();
        result.Y.ShouldBe(300);
        result.Z.ShouldBe(400);
    }

    [Fact]
    public void OneOf_ShouldThrowExceptionForWrongType()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act & Assert
        Should.Throw<InvalidCastException>(() =>
        {
            TypeB result = oneOf; // This should throw
        });
    }

    [Fact]
    public void OneOf_ShouldAllowReassignment()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act
        oneOf = new TypeB { Y = 300, Z = 400 }; // Reassign with different type
        TypeB result = oneOf; // Should work now

        // Assert
        result.Y.ShouldBe(300);
        result.Z.ShouldBe(400);
    }

    [Fact]
    public void OneOf_AsT1_ShouldReturnValueWhenHoldingT1()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act
        var result = oneOf.AsT1();

        // Assert
        result.ShouldNotBeNull();
        result.X.ShouldBe(100);
        result.Y.ShouldBe(200);
    }

    [Fact]
    public void OneOf_AsT1_ShouldReturnNullWhenHoldingT2()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };
        TestOneOf oneOf = typeB;

        // Act
        var result = oneOf.AsT1();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void OneOf_AsT2_ShouldReturnValueWhenHoldingT2()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };
        TestOneOf oneOf = typeB;

        // Act
        var result = oneOf.AsT2();

        // Assert
        result.ShouldNotBeNull();
        result.Y.ShouldBe(300);
        result.Z.ShouldBe(400);
    }

    [Fact]
    public void OneOf_AsT2_ShouldReturnNullWhenHoldingT1()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act
        var result = oneOf.AsT2();

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public void OneOf_TryCastT1_ShouldReturnTrueWhenHoldingT1()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act
        var success = oneOf.TryCastT1(out var result);

        // Assert
        success.ShouldBeTrue();
        result.ShouldNotBeNull();
        result.X.ShouldBe(100);
        result.Y.ShouldBe(200);
    }

    [Fact]
    public void OneOf_TryCastT1_ShouldReturnFalseWhenHoldingT2()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };
        TestOneOf oneOf = typeB;

        // Act
        var success = oneOf.TryCastT1(out var result);

        // Assert
        success.ShouldBeFalse();
        result.ShouldBeNull();
    }

    [Fact]
    public void OneOf_TryCastT2_ShouldReturnTrueWhenHoldingT2()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };
        TestOneOf oneOf = typeB;

        // Act
        var success = oneOf.TryCastT2(out var result);

        // Assert
        success.ShouldBeTrue();
        result.ShouldNotBeNull();
        result.Y.ShouldBe(300);
        result.Z.ShouldBe(400);
    }

    [Fact]
    public void OneOf_TryCastT2_ShouldReturnFalseWhenHoldingT1()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act
        var success = oneOf.TryCastT2(out var result);

        // Assert
        success.ShouldBeFalse();
        result.ShouldBeNull();
    }

    [Fact]
    public void OneOf_Switch_ShouldExecuteCase1WhenHoldingT1()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;
        var case1Executed = false;
        var case2Executed = false;

        // Act
        oneOf.Switch(
            a => case1Executed = true,
            b => case2Executed = true
        );

        // Assert
        case1Executed.ShouldBeTrue();
        case2Executed.ShouldBeFalse();
    }

    [Fact]
    public void OneOf_Switch_ShouldExecuteCase2WhenHoldingT2()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };
        TestOneOf oneOf = typeB;
        var case1Executed = false;
        var case2Executed = false;

        // Act
        oneOf.Switch(
            a => case1Executed = true,
            b => case2Executed = true
        );

        // Assert
        case1Executed.ShouldBeFalse();
        case2Executed.ShouldBeTrue();
    }

    [Fact]
    public void OneOf_Switch_ShouldPassCorrectValueToCase1()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;
        TypeA? capturedValue = null;

        // Act
        oneOf.Switch(
            a => capturedValue = a,
            b => { }
        );

        // Assert
        capturedValue.ShouldNotBeNull();
        capturedValue.X.ShouldBe(100);
        capturedValue.Y.ShouldBe(200);
    }

    [Fact]
    public void OneOf_Switch_ShouldPassCorrectValueToCase2()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };
        TestOneOf oneOf = typeB;
        TypeB? capturedValue = null;

        // Act
        oneOf.Switch(
            a => { },
            b => capturedValue = b
        );

        // Assert
        capturedValue.ShouldNotBeNull();
        capturedValue.Y.ShouldBe(300);
        capturedValue.Z.ShouldBe(400);
    }

    [Fact]
    public void OneOf_SwitchWithReturn_ShouldReturnCase1ResultWhenHoldingT1()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act
        var result = oneOf.Switch(
            a => $"TypeA: {a.X}",
            b => $"TypeB: {b.Z}"
        );

        // Assert
        result.ShouldBe("TypeA: 100");
    }

    [Fact]
    public void OneOf_SwitchWithReturn_ShouldReturnCase2ResultWhenHoldingT2()
    {
        // Arrange
        var typeB = new TypeB { Y = 300, Z = 400 };
        TestOneOf oneOf = typeB;

        // Act
        var result = oneOf.Switch(
            a => $"TypeA: {a.X}",
            b => $"TypeB: {b.Z}"
        );

        // Assert
        result.ShouldBe("TypeB: 400");
    }

    [Fact]
    public void OneOf_SwitchWithReturn_ShouldWorkWithDifferentReturnTypes()
    {
        // Arrange
        var typeA = new TypeA { X = 100, Y = 200 };
        TestOneOf oneOf = typeA;

        // Act
        var result = oneOf.Switch<int>(
            a => a.X + a.Y,
            b => b.Y + b.Z
        );

        // Assert
        result.ShouldBe(300);
    }
}

// Test types for Intersect
class TypeA
{
    public int X { get; set; }
    public int Y { get; set; }
}

class TypeB
{
    public int Y { get; set; }
    public int Z { get; set; }
}

class Type1
{
    public string Common { get; set; } = string.Empty;
    public int Prop1 { get; set; }
}

class Type2
{
    public string Common { get; set; } = string.Empty;
    public string Prop2 { get; set; } = string.Empty;
}

class Type3
{
    public string Common { get; set; } = string.Empty;
    public bool Prop3 { get; set; }
}

class PersonType
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}

class StatusType
{
    public bool Active { get; set; }
}

partial class TestIntersection : Intersect<TypeA, TypeB>
{
    // Should only contain Y property (common to both TypeA and TypeB)
}

partial class ThreeWayIntersection : Intersect<Type1, Type2, Type3>
{
    // Should only contain Common property
}

partial class TestUnion : Union<TypeA, TypeB>
{
    // Should contain X, Y, Z properties (all from both types)
}

partial class MultiTypeUnion : Union<PersonType, StatusType>
{
    // Should contain Name, Age, Active properties
}

partial class TestOneOf : OneOf<TypeA, TypeB>
{
    // Discriminated union that can hold either TypeA or TypeB
}
