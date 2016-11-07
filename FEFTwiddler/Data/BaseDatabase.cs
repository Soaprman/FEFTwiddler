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
            if (Config.InDesignMode()) return;

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
            var rows = _data.Elements("building");
            for (var i = 0; i < rows.Count(); i++)
            {
                var row = rows.ElementAt(i);

                //var categories = row.Elements("categories").First();
                //var gender = row.GetAttribute("gender");
                //if (gender == "Female")
                //{
                //    categories.SetAttributeValue("isFemale", "true");
                //}
                //else
                //{
                //    categories.SetAttributeValue("isFemale", "false");
                //}
                //categories.SetAttributeValue("isNpcOnly", "false");

                //row.Attribute("gender").Remove();

                var frontName = row.Attribute("name").Value;
                var size = "3";
                var rank = "0";
                var usesRankBackground = "true";
                var isStatue = "false";
                var isGatheringSpot = "false";

                if (frontName.Contains("Statue"))
                {
                    var suffix = frontName.Substring(frontName.Length - 2, 2);
                    frontName = "Statue" + suffix;
                    size = "1";
                    rank = suffix.Substring(1);
                    usesRankBackground = "false";
                    isStatue = "true";
                }
                else if (frontName.Contains("Golem"))
                {
                    var suffix = frontName.Substring(frontName.Length - 2, 2);
                    frontName = "Golem" + suffix;
                    size = "1";
                    rank = suffix.Substring(1);
                    usesRankBackground = "false";
                }
                else if (frontName.Contains("Puppet"))
                {
                    var suffix = frontName.Substring(frontName.Length - 2, 2);
                    frontName = "Puppet" + suffix;
                    size = "1";
                    rank = suffix.Substring(1);
                    usesRankBackground = "false";
                }
                else if (frontName.Contains("Ballista") || frontName.Contains("FireOrb") || frontName.Contains("Launcher"))
                {
                    var suffix = frontName.Substring(frontName.Length - 2, 2);
                    frontName = "Turret" + suffix;
                    size = "1";
                    rank = suffix.Substring(1);
                    usesRankBackground = "false";
                }
                else if (frontName.EndsWith("_1") || frontName.EndsWith("_2") || frontName.EndsWith("_3"))
                {
                    var suffix = frontName.Substring(frontName.Length - 2, 2);
                    frontName = frontName.Substring(0, frontName.Length - 2);
                    rank = suffix.Substring(1);
                }

                if (int.Parse(row.Attribute("id").Value) >= 0x3A &&
                    int.Parse(row.Attribute("id").Value) <= 0x7B)
                {
                    isGatheringSpot = "true";
                }

                if (size == "1" || rank == "0")
                {
                    usesRankBackground = "false";
                }

                var properties = XElement.Parse(@"<properties size=""" + size + @""" rank=""" + rank + @""" isStatue=""" + isStatue + @""" isGatheringSpot=""" + isGatheringSpot + @""" isLilithsTemple=""false"" isTravelersPlaza=""false"" />");

                row.Add(properties);

                var image = XElement.Parse(@"<image name=""" + frontName + @""" usesRankBackground=""" + usesRankBackground + @""" />");

                row.Add(image);
            }
            // Stop here and look in the debugger for the updated XML string
            var breakpoint = true;
        }
    }
}
