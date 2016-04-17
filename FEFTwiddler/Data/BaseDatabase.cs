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
            var rows = _data.Elements("character");
            for (var i = 0; i < rows.Count(); i++)
            {
                var row = rows.ElementAt(i);

                var flags = XElement.Parse(@"<flags canUseStones=""false""/>");
                row.Add(flags);

                // skill flags
                //if (i <= 112)
                //{
                //    row.SetAttributeValue("enemyOnly", "false");
                //    row.SetAttributeValue("dlc", "false");
                //    row.SetAttributeValue("personal", "false");
                //    row.SetAttributeValue("unlearnable", "false");
                //}
                //else if (i <= 159)
                //{
                //    row.SetAttributeValue("enemyOnly", "true");
                //    row.SetAttributeValue("dlc", "false");
                //    row.SetAttributeValue("personal", "false");
                //    row.SetAttributeValue("unlearnable", "false");
                //}
                //else
                //{
                //    row.SetAttributeValue("enemyOnly", "false");
                //    row.SetAttributeValue("dlc", "false");
                //    row.SetAttributeValue("personal", "true");
                //    row.SetAttributeValue("unlearnable", "false");
                //}

                // learned skill info
                //var lsi = row.Elements("learnedSkillInfo").First();
                //var byteOffset = i / 8;
                ////var bitMask = Math.Pow(2, (7 - (i % 8)));
                //var bitMask = Math.Pow(2, (i % 8));

                //if (byteOffset > 19)
                //{
                //    byteOffset = 0;
                //    bitMask = 0;
                //}

                //lsi.SetAttributeValue("byteOffset", byteOffset);
                //lsi.SetAttributeValue("bitMask", bitMask);
            }
            var breakpoint = true;
        }
    }
}
