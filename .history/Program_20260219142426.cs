using projectLibrary;

/* static class IntCalculator
{
    static int Add(int a, int b) { return a + b; }
} */

class Program
{
    public static int Main()
    {
        //Method1();
        Method2();
        return 0;
    }

    static void Method1()
    {
        /* Console.WriteLine("Enter a number: ");
        int firstNumber = int.Parse(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Enter 1 for +");
        Console.WriteLine("Enter 2 for -");
        Console.WriteLine("Enter 3 for *");
        Console.WriteLine("Enter 4 for /");
        Console.WriteLine("Your selection: ");
        string selection = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Enter a second number: ");
        int secondNumber = int.Parse(Console.ReadLine());
        Console.Clear();

        switch (selection)
        {
            case "1":
            case "+":
                Console.Write("Result: " + IntCalculator.Add(firstNumber, secondNumber));
                break;

            case "2":
            case "-":
                Console.Write("Result: " + (firstNumber - secondNumber));
                break;

            case "3":
            case "*":
                Console.Write("Result: " + (firstNumber * secondNumber));
                break;

            case "4":
            case "/":
                Console.Write("Result: " + (firstNumber / secondNumber));
                break;

            default:
                Console.Write("Your selection was incorrect");
                break;
        } */
    }

    static void Method2()
    {
        Console.WriteLine("Calculate (supported operators: +   -   *   /):");
        string calculation = Console.ReadLine() ?? "Incorrect input. Please try again.";                // ?? ... gives feedback if input is null

        string[] parts = LibraryMethods.Tokenize(calculation);

        parts = LibraryMethods.PrioOp(parts);

        double result = LibraryMethods.ParseNumber(parts[0]);

        double finalResult = LibraryMethods.LowPrioOp(result);

        Console.WriteLine("Result: " + finalResult);
    }
}
