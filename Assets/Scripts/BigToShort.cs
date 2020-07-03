using System;
using System.Numerics;

namespace Game_Classes
{
    public static class BigToShort
    {
        private static string[] decades =
        {
            "", "K", "million", "billion", "trillion", "quadrillion", "quintillion","sextillion", "septillion", "octillion",
            "nonillion", "decillion", "Undecillion", "Duodecillion", "Tredecillion", "Quattuodecillion", "Quindecillion",
            "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion", "Vigintillion"
            
        }; 
        
        public static string Convert(string a)
        {
            /*Функция переводит большое число в краткую  и красивую форму записи*/
            string result = "";

            int len = a.Length;
            --len;
            if (len < 6) return a.Substring(0, 1 + len % 3) + (len>2 ?"'":"")+ 
                                a.Substring(1 + len % 3, len - (len % 3));

            result += decades[len / 3];
            result = a.Substring(0, 1 + len % 3) + "." 
                                                 + a.Substring(1 + len % 3, 3) + " "+ result+" ";
            return result;
        }

        public static string Convert(BigInteger a)
        {
            return Convert(a.ToString());
        }
    }
}