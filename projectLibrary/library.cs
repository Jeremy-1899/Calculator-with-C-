namespace projectLibrary;

using System.Text.RegularExpressions;
using System.Globalization;

public class LibraryMethods
{
    public static string[] Tokenize(string input)                                                       // input = calculation f.e. 4+3 -1
    {
        var matches = Regex.Matches(input, @"(?<![\d.,])-\d+([.,]\d+)?|\d+([.,]\d+)?|[+\-*/]");         // Regex = Regular Expressions → Regex.Matches() searches for numbers and operators
        string[] parts = new string[matches.Count];                                                     // .Count counts elements, string[] parts length = matches.Count (matches.Count = 7 → parts.Length = 7)
        for (int i = 0; i < matches.Count; i++)
            parts[i] = matches[i].Value;                                                                // puts each token into the right position of the parts array
        return parts;
    }

    public static string[] PrioOp(string[] parts)
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
