using System.Globalization;
using System.Text.RegularExpressions;

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

        string[] parts = Tokenize(calculation);

        parts = PrioOp(parts);

        double result = ParseNumber(parts[0]);

        for (int i = 1; i < parts.Length; i += 2)
        {
            string op = parts[i];
            double nextNumber = ParseNumber(parts[i + 1]);

            switch (op)
            {
                case "+":
                    result += nextNumber;
                    break;

                case "-":
                    result -= nextNumber;
                    break;

                default:
                    Console.WriteLine("Unknown operator");
                    break;
            }
        }
        Console.WriteLine("Result: " + result);
    }

    static string[] Tokenize(string input)                                                              // input = calculation f.e. 4+3 -1
    {
        var matches = Regex.Matches(input, @"(?<![\d.,])-\d+([.,]\d+)?|\d+([.,]\d+)?|[+\-*/]");         // Regex = Regular Expressions → Regex.Matches() searches for numbers and operators
        string[] parts = new string[matches.Count];                                                     // .Count counts elements, string[] parts length = matches.Count (matches.Count = 7 → parts.Length = 7)
        for (int i = 0; i < matches.Count; i++)
            parts[i] = matches[i].Value;                                                                // puts each token into the right position of the parts array
        return parts;
    }

    static double ParseNumber(string token)
    {
        token = token.Replace(',', '.');
        return double.Parse(token, CultureInfo.InvariantCulture);
    }

    static string[] PrioOp(string[] parts)
    {
        List<string> partList = new List<string>(parts);                                                // new List which contains the same values as the parts array

        while (partList.Contains("*") || partList.Contains("/"))                                        // runs until theres no more * and /
        {
            for (int i = 0; i < partList.Count; i++)
            {
                if (partList[i] == "*" || partList[i] == "/")
                {
                    double left = ParseNumber(partList[i - 1]);
                    double right = ParseNumber(partList[i + 1]);

                    double tempResult = 0;

                    if (partList[i] == "*")
                        tempResult = left * right;
                    else
                        tempResult = left / right;

                    partList.RemoveAt(i + 1);
                    partList.RemoveAt(i);
                    partList[i - 1] = tempResult.ToString();                                            // converts number into text (string) f. e. 5 → "5"

                    break;
                }
            }
        }
        return partList.ToArray();                                                                      // converts List into array
    }
}
