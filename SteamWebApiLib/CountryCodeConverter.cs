using System;
using System.Collections.Generic;

namespace SteamWebApiLib
{
    internal static class CountryCodeConverter
    {
        private static readonly Dictionary<CountryCode, string> _countryCodes =
            new Dictionary<CountryCode, string>();


        static CountryCodeConverter()
        {
            RegisterCountryCodes();
        }

        private static void RegisterCountryCodes()
        {
            _countryCodes.Add(CountryCode.Brazil,       "BR");
            _countryCodes.Add(CountryCode.Bulgaria,     "BG");
            _countryCodes.Add(CountryCode.China,        "CN");
            _countryCodes.Add(CountryCode.Czechia,      "CZ");
            _countryCodes.Add(CountryCode.Denmark,      "DK");
            _countryCodes.Add(CountryCode.Finland,      "FI");
            _countryCodes.Add(CountryCode.France,       "FR");
            _countryCodes.Add(CountryCode.Germany,      "DE");
            _countryCodes.Add(CountryCode.GreatBritain, "GB");
            _countryCodes.Add(CountryCode.Greece,       "GR");
            _countryCodes.Add(CountryCode.Hungary,      "HU");
            _countryCodes.Add(CountryCode.Italy,        "IT");
            _countryCodes.Add(CountryCode.Japan,        "JP");
            _countryCodes.Add(CountryCode.Korea,        "KR");
            _countryCodes.Add(CountryCode.Netherlands,  "NL");
            _countryCodes.Add(CountryCode.Norway,       "NO");
            _countryCodes.Add(CountryCode.Poland,       "PL");
            _countryCodes.Add(CountryCode.Portugal,     "PT");
            _countryCodes.Add(CountryCode.Romania,      "RO");
            _countryCodes.Add(CountryCode.Russia,       "RU");
            _countryCodes.Add(CountryCode.Spain,        "ES");
            _countryCodes.Add(CountryCode.Sweden,       "SE");
            _countryCodes.Add(CountryCode.Thailand,     "TH");
            _countryCodes.Add(CountryCode.Turkey,       "TR");
            _countryCodes.Add(CountryCode.Ukraine,      "UA");
            _countryCodes.Add(CountryCode.USA,          "US");
        }

        public static string GetCountryCodeStringValue(CountryCode countryCode)
        {
            if (!_countryCodes.TryGetValue(countryCode, out string value))
            {
                throw new ArgumentException(
                    $"Country code {countryCode} didn't register.", nameof(countryCode)
                );
            }
            return value;
        }
    }
}
