using System;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public abstract class BaseDatabase
    {
        protected XElement _data;
        protected Enums.Language _language;

        public BaseDatabase(Enums.Language language)
        {
            SetLanguage(language);
        }

        public void SetLanguage(Enums.Language language)
        {
            _language = language;
        }

        protected string GetDisplayName(XElement xe)
        {
            XElement lang;

            // Get desired language
            lang = xe.Elements("text").Where((x) => x.GetAttribute("lang") == _language.ToString()).FirstOrDefault();

            // Get fallback language (English)
            if (lang == null) lang = xe.Elements("text").Where((x) => x.GetAttribute("lang") == Enums.Language.English.ToString()).FirstOrDefault();

            // Fallback on internal name if no display name exists yet
            var displayName = (lang != null ? lang.GetAttribute("displayName") : "DisplayName");
            if (displayName == "DisplayName")
            {
                return xe.GetAttribute("name");
            }
            else
            {
                return displayName;
            }
        }














        /// <summary>
        /// Leaving this in case more batch XML changing is needed
        /// </summary>
        protected void UpdateXmlStructure()
        {
            var rows = _data.Elements("class");
            for (var i = 0; i < rows.Count(); i++)
            {
                var row = rows.ElementAt(i);

                var growthRates = XElement.Parse(@"<growthRates hp=""0"" str=""0"" mag=""0"" skl=""0"" spd=""0"" lck=""0"" def=""0"" res=""0"" />");

                row.Add(growthRates);


            }
            var breakpoint = true;
        }
    }
}
