using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrototiposPoltran
{
    public static class Validacion
    {
        public static bool IsValidCIP(String str)
        {
            return !(Regex.IsMatch(str, @"^[0-9]{5,15}$"));
        }
        public static bool IsValidCodPapeleta(String str)
        {
            return !(Regex.IsMatch(str, @"^[0-9]{6}$"));
        }
        public static bool IsValidPlaca(String str)
        {
            return !(Regex.IsMatch(str, @"^[0-9A-Z]{3}[-][0-9A-Z]{3}$"));
        }
        public static bool IsValidInfraccion(String str)
        {
            return !(Regex.IsMatch(str, @"^[GM]{3}[-][0-9A-Z]{1,2}$"));
        }
        public static bool IsValidUsuario(String str)
        {
            return !(Regex.IsMatch(str, @"^[0-9A-Z]+$"));
        }
        public static bool IsDNI(String str)
        {
            return !(Regex.IsMatch(str, @"^[0-9]{8}$"));
        }
        public static bool IsBrevete(String str)
        {
            return !(Regex.IsMatch(str, @"^[0-9A-Z]{8}$"));
        }
        
    }

}