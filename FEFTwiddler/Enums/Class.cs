namespace FEFTwiddler.Enums
{
    public enum Class : byte
    {
        None = 0x00,
        NohrNoble_M = 0x01,
        NohrNoble_F = 0x02,
        NohrPrince_M = 0x03,
        NohrPrince_F = 0x04,
        HoshidoNoble_M = 0x05,
        HoshidoNoble_F = 0x06,
        Paladin_M = 0x07,
        Paladin_F = 0x08,
        Cavalier_M = 0x09,
        Cavalier_F = 0x0A,
        GreatKnight_M = 0x0B,
        GreatKnight_F = 0x0C,
        Knight_M = 0x0D,
        Knight_F = 0x0E,
        General_M = 0x0F,
        General_F = 0x10,
        Berserker_M = 0x11,
        Berserker_F = 0x12,
        Fighter_M = 0x13,
        Fighter_F = 0x14,
        Hero_M = 0x15,
        Hero_F = 0x16,
        Mercenary_M = 0x17,
        Mercenary_F = 0x18,
        BowKnight_M = 0x19,
        BowKnight_F = 0x1A,
        Outlaw_M = 0x1B,
        Outlaw_F = 0x1C,
        Adventurer_M = 0x1D,
        Adventurer_F = 0x1E,
        Swordmaster_M = 0x1F,
        Swordmaster_F = 0x20,
        Samurai_M = 0x21,
        Samurai_F = 0x22,
        MasterOfArms_M = 0x23,
        MasterOfArms_F = 0x24,
        OniChieftain_M = 0x25,
        OniChieftain_F = 0x26,
        OniSavage_M = 0x27,
        OniSavage_F = 0x28,
        Blacksmith_M = 0x29,
        Blacksmith_F = 0x2A,
        SpearMaster_M = 0x2B,
        SpearMaster_F = 0x2C,
        SpearFighter_M = 0x2D,
        SpearFighter_F = 0x2E,
        Basara_M = 0x2F,
        Basara_F = 0x30,
        Diviner_M = 0x31,
        Diviner_F = 0x32,
        Onmyoji_M = 0x33,
        Onmyoji_F = 0x34,
        Monk = 0x35,
        ShrineMaiden = 0x36,
        GreatMaster = 0x37,
        Priestess = 0x38,
        FalconKnight_M = 0x39,
        FalconKnight_F = 0x3A,
        SkyKnight_M = 0x3B,
        SkyKnight_F = 0x3C,
        KinshiKnight_M = 0x3D,
        KinshiKnight_F = 0x3E,
        Archer_M = 0x3F,
        Archer_F = 0x40,
        Sniper_M = 0x41,
        Sniper_F = 0x42,
        WyvernLord_M = 0x43,
        WyvernLord_F = 0x44,
        WyvernRider_M = 0x45,
        WyvernRider_F = 0x46,
        MaligKnight_M = 0x47,
        MaligKnight_F = 0x48,
        MasterNinja_M = 0x49,
        MasterNinja_F = 0x4A,
        Ninja_M = 0x4B,
        Ninja_F = 0x4C,
        Mechanist_M = 0x4D,
        Mechanist_F = 0x4E,
        Apothecary_M = 0x4F,
        Apothecary_F = 0x50,
        Merchant_M = 0x51,
        Merchant_F = 0x52,
        Sorcerer_M = 0x53,
        Sorcerer_F = 0x54,
        DarkMage_M = 0x55,
        DarkMage_F = 0x56,
        DarkKnight_M = 0x57,
        DarkKnight_F = 0x58,
        Strategist_M = 0x59,
        Strategist_F = 0x5A,
        Troubadour_M = 0x5B,
        Troubadour_F = 0x5C,
        Butler = 0x5D,
        Maid = 0x5E,
        Wolfskin_M = 0x5F,
        Wolfskin_F = 0x60,
        Wolfssegner_M = 0x61,
        Wolfssegner_F = 0x62,
        Kitsune_M = 0x63,
        Kitsune_F = 0x64,
        NineTails_M = 0x65,
        NineTails_F = 0x66,
        Songstress = 0x67,
        Villager_M = 0x68,
        Villager_F = 0x69,
        // Enemy/NPC only
        // Lancer may be capturable? idk
        Lancer = 0x6A,
        Faceless = 0x6B,
        Stoneborn = 0x6C,
        Automaton = 0x6D,
        AstralDragon = 0x6E,
        FeralDragon_M = 0x6F,
        FeralDragon_F = 0x70,
        DarkDragon = 0x71,
        InvisibleDragon1 = 0x72,
        InvisibleDragon2 = 0x73,
        DarkLord = 0x74,
        Familiar = 0x75,
        // DLC
        DreadFighter_M = 0x76,
        DreadFighter_F = 0x77,
        DarkFalcon_M = 0x78,
        DarkFalcon_F = 0x79,
        Ballistician = 0x7A,
        Witch = 0x7B,
        Lodestar = 0x7C,
        Vanguard = 0x7D,
        GreatLord = 0x7E,
        Grandmaster = 0x7F,
        Outrealm = 0x80,
        // More DLC
        PegasusKnight = 0x88,
    }

    public static class ClassExtensions
    {
        public static bool IsPromoted(this Class classId)
        {
            return classId == Class.NohrNoble_M ||
                classId == Class.NohrNoble_F ||
                classId == Class.HoshidoNoble_M ||
                classId == Class.HoshidoNoble_F ||
                classId == Class.Paladin_M ||
                classId == Class.Paladin_F ||
                classId == Class.GreatKnight_M ||
                classId == Class.GreatKnight_F ||
                classId == Class.General_M ||
                classId == Class.General_F ||
                classId == Class.Berserker_M ||
                classId == Class.Berserker_F ||
                classId == Class.Hero_M ||
                classId == Class.Hero_F ||
                classId == Class.BowKnight_M ||
                classId == Class.BowKnight_F ||
                classId == Class.Adventurer_M ||
                classId == Class.Adventurer_F ||
                classId == Class.Swordmaster_M ||
                classId == Class.Swordmaster_F ||
                classId == Class.MasterOfArms_M ||
                classId == Class.MasterOfArms_F ||
                classId == Class.OniChieftain_M ||
                classId == Class.OniChieftain_F ||
                classId == Class.Blacksmith_M ||
                classId == Class.Blacksmith_F ||
                classId == Class.SpearMaster_M ||
                classId == Class.SpearMaster_F ||
                classId == Class.Basara_M ||
                classId == Class.Basara_F ||
                classId == Class.Onmyoji_M ||
                classId == Class.Onmyoji_F ||
                classId == Class.GreatMaster ||
                classId == Class.Priestess ||
                classId == Class.FalconKnight_M ||
                classId == Class.FalconKnight_F ||
                classId == Class.KinshiKnight_M ||
                classId == Class.KinshiKnight_F ||
                classId == Class.Sniper_M ||
                classId == Class.Sniper_F ||
                classId == Class.WyvernLord_M ||
                classId == Class.WyvernLord_F ||
                classId == Class.MaligKnight_M ||
                classId == Class.MaligKnight_F ||
                classId == Class.MasterNinja_M ||
                classId == Class.MasterNinja_F ||
                classId == Class.Mechanist_M ||
                classId == Class.Mechanist_F ||
                classId == Class.Merchant_M ||
                classId == Class.Merchant_F ||
                classId == Class.Sorcerer_M ||
                classId == Class.Sorcerer_F ||
                classId == Class.DarkKnight_M ||
                classId == Class.DarkKnight_F ||
                classId == Class.Strategist_M ||
                classId == Class.Strategist_F ||
                classId == Class.Butler ||
                classId == Class.Maid ||
                classId == Class.Wolfssegner_M ||
                classId == Class.Wolfssegner_F ||
                classId == Class.NineTails_M ||
                classId == Class.NineTails_F ||
                classId == Class.Songstress ||
                classId == Class.Lancer ||
                classId == Class.Faceless ||
                classId == Class.Stoneborn ||
                classId == Class.Automaton ||
                classId == Class.AstralDragon ||
                classId == Class.FeralDragon_M ||
                classId == Class.FeralDragon_F ||
                classId == Class.DarkDragon ||
                classId == Class.InvisibleDragon1 ||
                classId == Class.InvisibleDragon2 ||
                classId == Class.DarkLord ||
                classId == Class.Familiar ||
                classId == Class.DreadFighter_M ||
                classId == Class.DreadFighter_F ||
                classId == Class.DarkFalcon_M ||
                classId == Class.DarkFalcon_F ||
                classId == Class.Ballistician ||
                classId == Class.Witch ||
                classId == Class.Lodestar ||
                classId == Class.Vanguard ||
                classId == Class.GreatLord ||
                classId == Class.Grandmaster ||
                classId == Class.Outrealm ||
                classId == Class.PegasusKnight;
        }
    }
}
