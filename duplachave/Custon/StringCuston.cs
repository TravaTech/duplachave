using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave.Custon
{
    public class StringCuston
    {
        public static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static List<string> getBetween(string strSource, string strStart, string strEnd)
        {
            List<string> result = new List<string>();
            int Start = 0, End;

            int pos = CountStringOccurrences(strSource, strStart);

            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                while (pos > 0)
                {
                    Start = strSource.IndexOf(strStart, Start) + strStart.Length;
                    End = strSource.IndexOf(strEnd, Start);

                    if (Start != -1 && End != -1)
                    {
                        result.Add(strSource.Substring(Start, End - Start));
                    }
                    pos--;
                }
            }
            return result;
        }

        public static string getLine(string strSource, string str)
        {
            string result = string.Empty;
            int Start = 0, End;

            if (strSource.Contains(str))
            {

                Start = strSource.IndexOf(str, Start);
                End = Start;

                while (strSource[Start] != '\r' && strSource[Start] != '\n' && Start > 0)
                {
                    Start--;
                }

                while (strSource[End] != '\r' && strSource[End] != '\n' && End < strSource.Length)
                {
                    End++;
                }
                result = strSource.Substring(Start, End - Start);
            }
            return result;
        }

        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

    }
}
