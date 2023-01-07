using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextTerm
{
    private static Dictionary<string, string> _terms = new();

    public static string Get(string term)
    {
        return _terms.TryGetValue(term, out var value)
            ? value
            : string.Empty;
    }

    public static void Set(string term, string value)
    {
        _terms[term] = value;
    }
}
