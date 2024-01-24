using System;
using System.Collections.Generic;

internal abstract class Program
{
  private const int D = 256;
  
  private static void s_rubinKarpAlgorithmMethod(string pat, string txt, int q)
  {
    var m = pat.Length;
    var n = txt.Length;
    var p = 0; 
    var t = 0; 
    var h = 1;
    int i;
    
    for (i = 0; i < m - 1; i++)
      h = (h * D) % q;
    
    for (i = 0; i < m; i++)
    {
      p = (D * p + pat[i]) % q;
      t = (D * t + txt[i]) % q;
    }
    
    for (i = 0; i <= n - m; i++)
    {
      if (p == t)
      {
        int j;
        for (j = 0; j < m; j++)
          if (txt[i + j] != pat[j]) break;
        
        if (j == m)
          Console.WriteLine("Шаблон найден по индексу: " + i);
      }
      
      if (i >= n - m) continue;
      t = (D * (t - txt[i] * h) + txt[i + m]) % q;
      
      if (t < 0) t = (t + q);
    }
  }

  private static void s_boyerMooreAlgorithmMethod(IReadOnlyList<char> text, char[] pat)
  {
    int s = 0;
    var m = pat.Length;
    var n = text.Count;

    var bpos = new int[m + 1];
    var shift = new int[m + 1];
    
    for (var i = 0; i < m + 1; i++)
      shift[i] = 0;
    
    s_preprocessStrongSuffix(shift, bpos, pat, m);
    s_preprocessSecondCase(shift, bpos, m);

    while (s <= n - m)
    {
      var j = m - 1;
      
      while (j >= 0 && pat[j] == text[s + j])
        j--;
      
      if (j < 0)
      {
        Console.Write($"Шаблонн содержится при сдвиге = {s}\n");
        s += shift[0];
      }
      else
        s += shift[j + 1];
    }
  }

  #region SupportMethods
  // preprocessing for case 2
  private static void s_preprocessSecondCase(IList<int> shift, IReadOnlyList<int> bpos, int m)
  {
    int i;
    var j = bpos[0];
    for (i = 0; i <= m; i++)
    {
      if (shift[i] == 0)
        shift[i] = j;
      
      if (i == j)
        j = bpos[j];
    }
  }
  
  private static void s_preprocessStrongSuffix(IList<int> shift, IList<int> bpos, IReadOnlyList<char> pat, int m)
  {
    int i = m, j = m + 1;
    bpos[i] = j;

    while (i > 0)
    {
      while (j <= m && pat[i - 1] != pat[j - 1])
      {
        if (shift[j] == 0)
          shift[j] = j - i;

        j = bpos[j];
      }
      i--; j--;
      bpos[i] = j;
    }
  }
  #endregion
  
  public static void Main()
  {
    const string text = "Какой-то текст в строке";
    const string pat = "то";

    const int q = 101;

    Console.WriteLine("Поиск алгоритмом Рабина-Карпа:");
    Console.WriteLine($"Текст для поиска: {text}");
    s_rubinKarpAlgorithmMethod(pat, text, q);
    Console.WriteLine();

    Console.WriteLine("Поиск алгоритмом Бойера и Мура:");  
    var text2 = "ABAAAABAACD".ToCharArray();
    var pat2 = "ABA".ToCharArray();
    s_boyerMooreAlgorithmMethod(text2, pat2);
  }
}