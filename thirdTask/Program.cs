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
    private static void s_boyerMooreAlgorithm(char[] text, char[] pat)
    {
      // s is shift of the pattern 
      // with respect to text
      int s = 0, j;
      var m = pat.Length;
      var n = text.Length;

      var bpos = new int[m + 1];
      var shift = new int[m + 1];

      // initialize all occurrence of shift to 0
      for (int i = 0; i < m + 1; i++)
        shift[i] = 0;

      // do preprocessing
      s_preprocessStrongSuffix(shift, bpos, pat, m);
      s_preprocessSecondCase(shift, bpos, pat, m);

      while (s <= n - m)
      {
        j = m - 1;

        /* Keep reducing index j of pattern while 
        characters of pattern and text are matching 
        at this shift s*/
        while (j >= 0 && pat[j] == text[s + j])
          j--;

        /* If the pattern is present at the current shift, 
        then index j will become -1 after the above loop */
        if (j < 0)
        {
          Console.Write($"Шаблонн содержится при сдвиге = {s}\n");
          s += shift[0];
        }
        else
          /*pat[i] != pat[s+j] so shift the pattern
          shift[j+1] times */
          s += shift[j + 1];
      }
    }
        
    /* Support methods */
    //Preprocessing for case 2
    private static void s_preprocessSecondCase(int[] shift, int[] bpos, char[] pat, int m)
    {
      int i, j;
      j = bpos[0];
      for (i = 0; i <= m; i++)
      {
        /* set the border position of the first character 
        of the pattern to all indices in array shift
        having shift[i] = 0 */
        if (shift[i] == 0)
          shift[i] = j;

        /* suffix becomes shorter than bpos[0], 
        use the position of next widest border
        as value of j */
        if (i == j)
          j = bpos[j];
      }
    }

    private static void s_preprocessStrongSuffix(int[] shift, int[] bpos, char[] pat, int m)
    {
      // m is the length of pattern 
      int i = m, j = m + 1;
      bpos[i] = j;

      while (i > 0)
      {
        /*if character at position i-1 is not 
        equivalent to character at j-1, then 
        continue searching to right of the
        pattern for border */
        while (j <= m && pat[i - 1] != pat[j - 1])
        {
          /* the character preceding the occurrence of t 
          in pattern P is different than the mismatching 
          character in P, we stop skipping the occurrences 
          and shift the pattern from i to j */
            if (shift[j] == 0)
              shift[j] = j - i;

            //Update the position of next border 
            j = bpos[j];
        }
        /* p[i-1] matched with p[j-1], border is found.
        store the beginning position of border */
        i--; j--;
        bpos[i] = j;
      }
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
      Console.WriteLine();

      Console.WriteLine("Поиск алгоритмом Бойера и Мура:");  
      char[] text2 = "ABAAAABAACD".ToCharArray();
      char[] pat2 = "ABA".ToCharArray();
      s_boyerMooreAlgorithm(text2, pat2);
    }
  }
}