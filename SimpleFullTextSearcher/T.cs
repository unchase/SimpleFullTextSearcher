using System.Globalization;
using System.IO;
using NGettext;

namespace SimpleFullTextSearcher
{
    internal class T
    {
        private static ICatalog ru_Catalog;

        private static ICatalog en_Catalog;

        private static ICatalog Catalog;

        private static readonly string _localesDir;

        public enum CatalogLocale
        {
            Ru,
            En
        }

        static T()
        {
            _localesDir = Path.Combine(Directory.GetCurrentDirectory(), "Loc");
            ru_Catalog = new Catalog("sfts", _localesDir, new CultureInfo("ru-RU"));
            en_Catalog = new Catalog("sfts", _localesDir, new CultureInfo("en-US"));
            Catalog = new Catalog("sfts", _localesDir);
        }

        public static void SetCatalogLanguage(CatalogLocale locale)
        {
            switch (locale)
            {
                case CatalogLocale.En:
                    Catalog = en_Catalog;
                    break;
                case CatalogLocale.Ru:
                    Catalog = ru_Catalog;
                    break;
            }
        }

        public static string _(string text)
        {
            return Catalog.GetString(text);
        }

        public static string _(string text, params object[] args)
        {
            return Catalog.GetString(text, args);
        }

        public static string _n(string text, string pluralText, long n)
        {
            return Catalog.GetPluralString(text, pluralText, n);
        }

        public static string _n(string text, string pluralText, long n, params object[] args)
        {
            return Catalog.GetPluralString(text, pluralText, n, args);
        }

        public static string _p(string context, string text)
        {
            return Catalog.GetParticularString(context, text);
        }

        public static string _p(string context, string text, params object[] args)
        {
            return Catalog.GetParticularString(context, text, args);
        }

        public static string _pn(string context, string text, string pluralText, long n)
        {
            return Catalog.GetParticularPluralString(context, text, pluralText, n);
        }

        public static string _pn(string context, string text, string pluralText, long n, params object[] args)
        {
            return Catalog.GetParticularPluralString(context, text, pluralText, n, args);
        }
    }
}
