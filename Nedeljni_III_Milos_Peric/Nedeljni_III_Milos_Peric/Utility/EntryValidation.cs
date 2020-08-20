using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nedeljni_III_Milos_Peric.Utility
{
    class EntryValidation
    {
        public static bool IsOnlyLetters(string nameSurname)
        {
            bool isOnlyLetters = Regex.IsMatch(nameSurname, @"^[a-zA-Z ]+$");
            return isOnlyLetters;
        }
    }
}
