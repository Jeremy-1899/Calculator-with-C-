namespace projectLibrary;

using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;

public class LibraryMethods
{
    public static string[] Tokenize(string input)                                                       // input = calculation f.e. 4+3 -1
    {
        if (input == null)
        {
            throw new NullReferenceException("You cannot calculate nothing XD");
        }
        else
        {
            var matches = Regex.Matches(input, @"(?<![\d.,])-\d+([.,]\d+)?|\d+([.,]\d+)?|[+\-*/]");     // Regex = Regular Expressions → Regex.Matches() searches for numbers and operators
            string[] parts = new string[matches.Count];                                                 // .Count counts elements, string[] parts length = matches.Count (matches.Count = 7 → parts.Length = 7)
            for (int i = 0; i < matches.Count; i++)
                parts[i] = matches[i].Value;                                                            // puts each token into the right position of the parts array
            return parts;
        }
    }

    private static void ValidateOperatorPlacement(string[] parts)
    {
        if (parts.Length < 3)
        {
            throw new IndexOutOfRangeException("Invalid expression: need at least one operand, one operator, and one operand");
        }

        if (IsOperator(parts[0]))
        {
            throw new FormatException("Expression cannot start with an operator");
        }

        if (IsOperator(parts[parts.Length - 1]))
        {
            throw new FormatException("Expression cannot end with an operator");
        }

        for (int i = 0; i < parts.Length; i++)
        {
            bool isCurrentOperator = IsOperator(parts[i]);
            bool shouldBeOperator = (i % 2 == 1);

            if (isCurrentOperator != shouldBeOperator)
            {
                if (isCurrentOperator)
                {
                    throw new FormatException($"Consecutive operators found at position {i}: {parts[i - 1]} {parts[i]}");
                }
                else
                {
                    throw new FormatException($"Consecutive operands found at position {i}: {parts[i - 1]} {parts[i]}");
                }
            }
        }
    }

    private static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    public static string[] PrioOp(string[] parts)
    {
        ValidateOperatorPlacement(parts);

        List<string> partList = new List<string>(parts);                                                // new List which contains the same values as the parts array

        while (partList.Contains("*") || partList.Contains("/"))                                        // runs until theres no more * and /
        {
            for (int i = 0; i < partList.Count; i++)
            {
                if (partList[i] == "*" || partList[i] == "/")
                {
                    if (partList[i - 1] == "0" || partList[i + 1] == "0")
                    {
                        throw new DivideByZeroException("You cannot multiply or divide by 0");
                    }
                    else
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
                        partList[i - 1] = tempResult.ToString();                                        // converts number into text (string) f. e. 5 → "5"

                        break;
                    }
                }
            }
        }
        return partList.ToArray();                                                                      // converts List into array
    }

    public static double ParseNumber(string token)
    {
        token = token.Replace(',', '.');
        return double.Parse(token, CultureInfo.InvariantCulture);
    }

    public static double LowPrioOp(string[] parts, double result)
    {
        for (int i = 1; i < parts.Length; i += 2)
        {
            string op = parts[i];
            double nextNumber = LibraryMethods.ParseNumber(parts[i + 1]);

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
        return result;
    }
}
