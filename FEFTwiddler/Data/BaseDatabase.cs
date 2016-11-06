using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public abstract class BaseDatabase
    {
        protected XElement _data;
        protected Enums.Language _language;
        private const string _addonDataDirectory = "AddonData";

        public BaseDatabase(Enums.Language language)
        {
            SetLanguage(language);
        }

        public void SetLanguage(Enums.Language language)
        {
            _language = language;
        }

        protected void LoadData(string data)
        {
            _data = XElement.Parse(data);
        }

        protected void LoadAddonData(string addonDataSubDirectory)
        {
            // Don't call this in design mode, since we may not have permission to do Directory stuff in the Visual Studio temp folders
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            var path = _addonDataDirectory + "/" + addonDataSubDirectory;
            Directory.CreateDirectory(path);
            var addonDataFiles = Directory.GetFiles(path, "*.xml");

            foreach (var addonDataFile in addonDataFiles)
            {
                var addonData = XElement.Parse(File.ReadAllText(addonDataFile));

                // Remove elements already in the data if we are overwriting them
                // Hopefully this doesn't have *too* much impact on load time! TODO: Research ways to make this faster if needed
                // It might be better to change calling code to use .Last instead of .First and for .GetAll calls to filter duplicates on-the-fly
                var elements = addonData.Elements();
                foreach (var element in elements)
                {
                    _data.Elements()
                        .Where(x => x.GetAttribute("id") == element.GetAttribute("id"))
                        .Remove();
                }

                _data.Add(addonData.Elements());
            }
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

                var categories = row.Elements("categories").First();
                var gender = row.GetAttribute("gender");
                if (gender == "Female")
                {
                    categories.SetAttributeValue("isFemale", "true");
                }
                else
                {
                    categories.SetAttributeValue("isFemale", "false");
                }
                categories.SetAttributeValue("isNpcOnly", "false");

                row.Attribute("gender").Remove();

                //var categories = XElement.Parse(@"<categories isPromoted=""false"" isSpecial=""false"" />");

                //row.Add(categories);


            }
            var breakpoint = true;
        }
    }
}
