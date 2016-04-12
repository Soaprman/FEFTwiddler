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
            var items = _data.Elements("item");
            foreach (var item in items)
            {
                var displayName = item.GetAttribute("displayName");
                var langEn = XElement.Parse(@"<text lang=""English"" displayName=""" + displayName + @""" />");
                item.Add(langEn);
                var langJp = XElement.Parse(@"<text lang=""Japanese"" displayName=""" + "DisplayName" + @""" />");
                item.Add(langJp);

                item.Attributes("displayName").First().Remove();
                //item.Attributes().Reverse(); // Maintain the order of attributes in the XML document
            }
            var breakpoint = true;
        }
    }
}
