using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FEFTwiddler.Extensions;

namespace FEFTwiddler.Data
{
    public class PrisonerDatabase : BaseDatabase
    {
        public PrisonerDatabase(Enums.Language language) : base(language)
        {
            _data = XElement.Parse(Properties.Resources.Data_Prisoners);
        }

        public Prisoner GetByID(byte prisonerId)
        {
            var row = _data
                .Elements("prisoner")
                .Where((x) => x.Attribute("id").Value == ((byte)prisonerId).ToString())
                .First();

            return new Prisoner
            {
                PrisonerID = prisonerId,
                DisplayName = GetDisplayName(row)
            };
        }
    }
}
