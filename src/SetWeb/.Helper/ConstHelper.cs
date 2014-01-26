using System.Collections.Generic;
using System.Globalization;

namespace SetWeb.Helpers
{
    public static class ConstHelper
    {
        public const string CultureNameTR = "tr-TR";
        public const string CultureNameEN = "en-US";

        public static string LocalizationStringsTR = "LocalizationStringsTR";
        public static string LocalizationStringsEN = "LocalizationStringsEN";

        public const string __SetLang = "__SetLang";

        private static CultureInfo _cultureTR;
        public static CultureInfo CultureTR { get { return _cultureTR ?? (_cultureTR = new CultureInfo(CultureNameTR)); } }
        private static CultureInfo _cultureEN;
        public static CultureInfo CultureEN { get { return _cultureEN ?? (_cultureEN = new CultureInfo(CultureNameEN)); } }

        public const string Admin = "Admin";
        public const string Developer = "Developer";
        public const string User = "User";
        public static List<string> BasicRoles = new List<string> { Admin, Developer, User };
        

        public const string undefined = "undefined";
        public const string Authorization = "Authorization";

        public const int PageSize = 25;
    }
}
