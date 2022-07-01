using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMS
{
    public class policy
    {

        /// <summary>
        /// Logika klasy walidującej złożoność haseł
        /// </summary>
        private static int Minimum_Length = 6;
        private static int NonAlpha_length = 1;

        /// <summary>
        /// Parametrem jest hasło, metoda sprawdza czy jest odpowiednio długie, czy zawiera min. jedną cyfrę oraz wystarczającą liczbę znaków nienumerycznych
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool IsValid(string Password)
        {
            if (Password.Length < Minimum_Length)
                return false;
            if (NumericCount(Password) < 1)
                return false;
            if (NonAlphaCount(Password) < NonAlpha_length)
                return false;
            return true;
        }
        private static int NumericCount(string Password)
        {
            return Regex.Matches(Password, "[0-9]").Count;
        }
        private static int NonAlphaCount(string Password)
        {
            return Regex.Matches(Password, @"[^0-9a-zA-Z\._]").Count;
        }
    }
}
