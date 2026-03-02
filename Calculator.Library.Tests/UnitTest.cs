namespace Calculator.Library.Tests;

public class ExceptionTests
{
    // ParseNumber Exception Tests
    [Fact]
    public void ParseNumber_InvalidFormat_ThrowsFormatException()
    {
        // Arrange
        string invalidInput = "2.3.4";

        // Act & Assert
        Assert.Throws<FormatException>(() => Calculator.Library.LibraryMethods.ParseNumber(invalidInput));
    }

    [Fact]
    public void ParseNumber_NonNumericString_ThrowsFormatException()
    {
        // Arrange
        string invalidInput = "abc";

        // Act & Assert
        Assert.Throws<FormatException>(() => Calculator.Library.LibraryMethods.ParseNumber(invalidInput));
    }

    [Fact]
    public void ParseNumber_EmptyString_ThrowsFormatException()
    {
        // Arrange
        string invalidInput = "";

        // Act & Assert
        Assert.Throws<FormatException>(() => Calculator.Library.LibraryMethods.ParseNumber(invalidInput));
    }

    [Fact]
    public void ParseNumber_OnlySpecialCharacters_ThrowsFormatException()
    {
        // Arrange
        string invalidInput = "!@#$";

        // Act & Assert
        Assert.Throws<FormatException>(() => Calculator.Library.LibraryMethods.ParseNumber(invalidInput));
    }

    // Division by Zero Tests
    [Fact]
    public void PrioOp_DivisionByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        string[] parts = { "10", "/", "0" };

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => Calculator.Library.LibraryMethods.PrioOp(parts));
    }

    [Fact]
    public void PrioOp_MultiplicationByZeroThenDivision_ThrowsDivideByZeroException()
    {
        // Arrange
        string[] parts = { "5", "*", "0", "/", "0" };

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => Calculator.Library.LibraryMethods.PrioOp(parts));
    }

    // Tokenize Exception Tests
    [Fact]
    public void Tokenize_NullInput_ThrowsNullReferenceException()
    {
        // Arrange
        string? nullInput = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => Calculator.Library.LibraryMethods.Tokenize(nullInput!));
    }

    // PrioOp with Invalid Input
    [Fact]
    public void PrioOp_InvalidNumberFormat_ThrowsFormatException()
    {
        // Arrange
        string[] parts = { "10", "*", "abc" };

        // Act & Assert
        Assert.Throws<FormatException>(() => Calculator.Library.LibraryMethods.PrioOp(parts));
    }

    [Fact]
    public void PrioOp_OnlyOperator_ThrowsIndexOutOfRangeException()
    {
        // Arrange
        string[] parts = { "*" };

        // Act & Assert
        Assert.Throws<IndexOutOfRangeException>(() => Calculator.Library.LibraryMethods.PrioOp(parts));
    }
}
