namespace projectLibrary;

using System.Text.RegularExpressions;

public class LibraryMethods
{
    static string[] Tokenize(string input)                                                              // input = calculation f.e. 4+3 -1
    {
        var matches = Regex.Matches(input, @"(?<![\d.,])-\d+([.,]\d+)?|\d+([.,]\d+)?|[+\-*/]");         // Regex = Regular Expressions → Regex.Matches() searches for numbers and operators
        string[] parts = new string[matches.Count];                                                     // .Count counts elements, string[] parts length = matches.Count (matches.Count = 7 → parts.Length = 7)
        for (int i = 0; i < matches.Count; i++)
            parts[i] = matches[i].Value;                                                                // puts each token into the right position of the parts array
        return parts;
    }
}
