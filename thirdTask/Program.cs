using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace thirdTask
{
  internal class Program
  {
    // d is the number of characters in the input alphabet
    private readonly static int d = 256;

    /* Methods */
    // method of Rubin Karp algorithm
    private static void s_rubinKarpAlgorithm(String pat, String txt, int q)
    {
      /* Variables */
      /* pat -> pattern
         txt -> text
         q -> A prime number
      */
      var M = pat.Length;
      var N = txt.Length;
      var p = 0; // hash value for pattern
      var t = 0; // hash value for txt
      var h = 1;
      int i, j;

      // The value of h would be "pow(d, M-1)%q"
      for (i = 0; i < M - 1; i++)
        h = (h * d) % q;

      // Calculate the hash value of pattern and first
      // window of text
      for (i = 0; i < M; i++)
      {
        p = (d * p + pat[i]) % q;
        t = (d * t + txt[i]) % q;
      }

      // Slide the pattern over text one by one
      for (i = 0; i <= N - M; i++)
      {
        // Check the hash values of current window of
        // text and pattern. If the hash values match
        // then only check for characters one by one
        if (p == t)
        {
          /* Check for characters one by one */
          for (j = 0; j < M; j++)
            if (txt[i + j] != pat[j]) break;

          // if p == t and pat[0...M-1] = txt[i, i+1,
          // ...i+M-1]
          if (j == M)
            Console.WriteLine("Шаблон найден по индексу: " + i);
        }

        // Calculate hash value for next window of text:
        // Remove leading digit, add trailing digit
        if (i < N - M)
        {
          t = (d * (t - txt[i] * h) + txt[i + M]) % q;

          // We might get negative value of t,
          // converting it to positive
          if (t < 0) t = (t + q);
        }
      }
    }

    // method of Boyer and Moore algorithm
    private static void s_boyerMooreAlgorithm()
    {

    }

    /* Main method */
    static void Main()
    {
      var text = "Какой-то текст в строке";
      var pat = "то";

      // A prime number
      int q = 101;

      // Function Call
      Console.WriteLine("Поиск алгоритмом Рабина-Карпа:");
      Console.WriteLine($"Текст для поиска: {text}");
      s_rubinKarpAlgorithm(pat, text, q);
    }
  }
}
