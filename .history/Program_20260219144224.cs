using projectLibrary;

class Program
{
    public static int Main()
    {
        Method2();
        return 0;
    }

    static void Method2()
    {
        Console.WriteLine("Calculate (supported operators: +   -   *   /):");
        string calculation = Console.ReadLine() ?? "Incorrect input. Please try again.";                // ?? ... gives feedback if input is null

        string[] parts = LibraryMethods.Tokenize(calculation);

        parts = LibraryMethods.PrioOp(parts);

        double result = LibraryMethods.ParseNumber(parts[0]);

        double finalResult = LibraryMethods.LowPrioOp(parts, result);

        Console.WriteLine("Result: " + finalResult);
    }
}
