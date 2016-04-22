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

                var hairColor = row.Elements("hairColor").First();

                var r = new byte[1];
                r.TryParseHex(hairColor.GetAttribute("r"));
                hairColor.SetAttributeValue("r", r[0]);

                var g = new byte[1];
                g.TryParseHex(hairColor.GetAttribute("g"));
                hairColor.SetAttributeValue("g", g[0]);

                var b = new byte[1];
                b.TryParseHex(hairColor.GetAttribute("b"));
                hairColor.SetAttributeValue("b", b[0]);


                //var hairColor = XElement.Parse(@"<hairColor r=""255"" g=""255"" b=""255"" a=""255"" />");
                //row.Add(hairColor);

                //var mainSupports = XElement.Parse(@"<mainSupports count=""0"" />");
                //row.Add(mainSupports);

                //var familySupports = XElement.Parse(@"<familySupports count=""0"" />");
                //row.Add(familySupports);

                //var categories = XElement.Parse(@"<categories corrinOnly=""false"" azuraOnly=""false"" kitsuneOnly=""false"" wolfskinOnly=""false"" pathBonusClass=""false"" dlcClass=""false"" amiiboClass=""false"" enemyAndNpc=""false"" />");
                //row.Add(categories);

                //categories.SetAttributeValue("dlcClass", row.GetAttribute("dlc"));
                //categories.SetAttributeValue("enemyAndNpc", row.GetAttribute("enemyOnly"));
                //var removeAttrs = row.Attributes().Where((x) => x.Name == "dlc" || x.Name == "enemyOnly");
                //foreach (var attr in removeAttrs)
                //{
                //    attr.Remove();
                //}

                //var flags = XElement.Parse(@"<flags canUseStones=""false""/>");
                //row.Add(flags);

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
