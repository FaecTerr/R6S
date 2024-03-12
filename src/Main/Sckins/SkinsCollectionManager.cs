using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public static class SkinsCollectionManager
    {
        public static List<SkinElement> allCollection = new List<SkinElement>()
        {
            new AK47MEWMEW(),
            new AR33ICE(),
            new C416Vector(),
            new F2Angel(),
            new FMGICE(),
            new L85A2Shapes(),
            new MP5Devil(),
            new MP5Layout(),
            new MP7Axe(),
            new MP7Iron(),
            new P12Flag(),
            new P90Antipods(),
            new P9Wood(),
            new R4CBlood(),
            new RP41Beetles(),
            new SMG11Love(),
            new VSNMutagen(),
            new xi556Colours(),
            new L85A2LOVECRAFT(),
            new UMP45Magnet(),
            new P226Silver(),
            new GSH18Neon(),
            new Commando552Umbrella(),
            new PathfinderBandit(),
            new NyanDoc(),
            new BloodsplatDoc(),
            new PathfinderGlaz(),
            new PathfinderIQ(),
            new MotoRiderPulse(),
            new WardenRook(),
            new FutuSledge(),
            new LordThatcher(),
            new ThermiteNord(),
            new PureTwitch(),
            new TwitchTwitch(),
            new SlavaKoroluTachanka(),
            new SkullKapkan(),
            new PathfinderFuze(),
            new PlagueDoctorSmoke(),
            new OilSpitPMM(),
            new G36Toxicity(),
            new PathfinderBlitz(),
            new AlternatedMonty(),
            new DevilMute(),
            new GoldMeusoc(),
            new LFP586Flag(),
            new FivesevenDrill(),
            new ToxicityM590A1(),
            new IceCGCQB(),
            new DP27Fmeech(),
            new FmeechTachanka(),
            new MP5Autumn(),
            new AlternatedM1014(),
            new DarkSightM870(),
            new SeptemberM4(),
            new BshieldAngryBoi(),
            new RenewedCastle(),
            new TRONJager(),
            new WoundedPulse(),
            new EncasedHK417(),
            new SyringeSASG11(),
            new FractureASH(),
            new StyledCutMaverick(),
            new G8A1Sideways(),
            new Bearing9Crystals(),
            new P229Crystals(),
            new WorkedOutT5(),
            new OTs11Shiny(),
            new SleepyMonky(),
            new LesionWorkedOut(),
            new GamerJackal(),
            new CaptainCapitao(),
            new FuzeNoEvidence(),
            new ValkyrieRaver(),
            new RainbowMPX(),
            new AR1550Transisted(),
            new GoldenD50(),
            new LMGE105ICE(),
            new BurningPDW(),
            new BanditBattery(),
            new Scorpionx2(),
            new BurningMP7(),
            new ITA12SIcemaiden(),
            new GlazBadTime(),
            new RG15Freezed(),
            new AlibiWhitenoise(),
            new BurningMPX(),
            new AK12ICE(),
            new Mag44Heart(),
            new CrocoTCSG12(),
            new BurningARX200(),
            new RetireeKaid(),
            new VibeNomad(),
            new TrueGamerFinka(),
            new LFPLighting(),
            new SMG12Tears(),
            new K1ADummy(),
            new MissingVigil(),
            new FellaDokkaebi(),
            new ElaEmo(),
            new BloodshedBanner(),
            new CurcuitBanner(),
            new VineBanner(),
            new DemonsBanner(),
            new JackalParty(),
            new BannerEyes(),
            new BannerChains(),
            new GlazElite(),
            new BannerAnarchy()
        };
        public static List<SkinElement> commonCollection = new List<SkinElement>() { new L85A2Shapes(), new MP5Layout(),  new MP7Iron(), new P9Wood(), new R4CBlood(), new C416Vector(), new UMP45Magnet(), new P226Silver(),
        new PathfinderBandit(), new PathfinderGlaz(), new PathfinderIQ(), new FutuSledge(), new PathfinderFuze(), new G36Toxicity(), new PathfinderBlitz(), new ToxicityM590A1(), new FivesevenDrill(),
        new AlternatedM1014(), new DarkSightM870(), new StyledCutMaverick(), new SyringeSASG11(), new G8A1Sideways(), new OTs11Shiny(), new LesionWorkedOut(), new GamerJackal(),
        new CaptainCapitao(), new AlibiWhitenoise(), new RetireeKaid(), new VibeNomad(), new K1ADummy(), new FellaDokkaebi(), new ElaEmo()};

        public static List<SkinElement> rareCollection = new List<SkinElement>() { new AK47MEWMEW(), new AR33ICE(), new P90Antipods(), new xi556Colours(), new Commando552Umbrella(),  new BloodsplatDoc(),
        new  ThermiteNord(), new PureTwitch(), new TwitchTwitch(), new SkullKapkan(), new OilSpitPMM(), new AlternatedMonty(), new LFP586Flag(), new IceCGCQB(), new P12Flag(), new MP5Autumn(),
        new SeptemberM4(), new RenewedCastle(), new FractureASH(), new Bearing9Crystals(), new P229Crystals(), new SleepyMonky(), new ValkyrieRaver(), new RainbowMPX(), new ITA12SIcemaiden(),
        new RG15Freezed(), new AK12ICE(), new CrocoTCSG12(), new TrueGamerFinka(), new SMG12Tears(), new MissingVigil()};

        public static List<SkinElement> epicCollection = new List<SkinElement>() { new F2Angel(), new FMGICE(), new SMG11Love(), new VSNMutagen(), new L85A2LOVECRAFT(), new GSH18Neon(), new NyanDoc(),
        new MotoRiderPulse(), new PlagueDoctorSmoke(), new MP7Axe(), new DevilMute(), new GoldMeusoc(), new FmeechTachanka(), new BshieldAngryBoi(), new TRONJager(), new WoundedPulse(),
        new EncasedHK417(), new WorkedOutT5(), new LMGE105ICE(), new GoldenD50(), new FuzeNoEvidence(), new GlazBadTime(), new Mag44Heart(),  new LFPLighting(), 
        new BloodshedBanner(), new CurcuitBanner(), new VineBanner(), new DemonsBanner(), new BannerChains(), new BannerEyes(), new BannerAnarchy()};

        public static List<SkinElement> legendaryCollection = new List<SkinElement>() { new MP5Devil(), new RP41Beetles(), new WardenRook(), new LordThatcher(), new SlavaKoroluTachanka(),
        new DP27Fmeech(), new AR1550Transisted(), new Scorpionx2(), new BurningPDW(), new BurningMP7(), new BurningMPX(), new BurningARX200(), new BanditBattery(), new JackalParty(),
        new GlazElite()};

        public static string GetRandomSkin()
        {
            string skin = allCollection[Rando.Int(allCollection.Count-1)].name;
            return skin;
        }


        public static string GetRandomCommonSkin()
        {
            string skin = commonCollection[Rando.Int(commonCollection.Count-1)].name;
            return skin;
        }

        public static string GetRandomRareSkin()
        {
            string skin = rareCollection[Rando.Int(rareCollection.Count-1)].name;
            return skin;
        }

        public static string GetRandomEpicSkin()
        {
            string skin = epicCollection[Rando.Int(epicCollection.Count-1)].name;
            return skin;
        }

        public static string GetRandomLegendarySkin()
        {
            string skin = legendaryCollection[Rando.Int(legendaryCollection.Count-1)].name;
            return skin;
        }

        public static string GetRandomLockedRareSkin()
        {
            List<SkinElement> tempCollection = new List<SkinElement>();
            foreach (SkinElement s in rareCollection)
            {
                if (!(PlayerStats.openedCustoms.Contains(s.name)))
                {
                    tempCollection.Add(s);
                }
            }
            string skin = GetRandomRareSkin();
            if (tempCollection.Count > 0)
            {
                skin = tempCollection[Rando.Int(tempCollection.Count - 1)].name;
            }
            return skin;
        }

        public static string GetRandomLockedCommonSkin()
        {
            List<SkinElement> tempCollection = new List<SkinElement>();
            foreach (SkinElement s in commonCollection)
            {
                if (!(PlayerStats.openedCustoms.Contains(s.name)))
                {
                    tempCollection.Add(s);
                }
            }
            string skin = GetRandomCommonSkin();
            if (tempCollection.Count > 0)
            {
                skin = tempCollection[Rando.Int(tempCollection.Count - 1)].name;
            }
            return skin;
        }

        public static string GetRandomLockedEpicSkin()
        {
            List<SkinElement> tempCollection = new List<SkinElement>();
            foreach (SkinElement s in epicCollection)
            {
                if (!(PlayerStats.openedCustoms.Contains(s.name)))
                {
                    tempCollection.Add(s);
                }
            }
            string skin = GetRandomEpicSkin();
            if (tempCollection.Count > 0)
            {
                skin = tempCollection[Rando.Int(tempCollection.Count - 1)].name;
            }
            return skin;
        }

        public static string GetRandomLockedLegendarySkin()
        {
            List<SkinElement> tempCollection = new List<SkinElement>();
            foreach(SkinElement s in legendaryCollection)
            {
                if (!(PlayerStats.openedCustoms.Contains(s.name)))
                {
                    tempCollection.Add(s);
                }
            }
            string skin = GetRandomLegendarySkin();
            if (tempCollection.Count > 0)
            {
                skin = tempCollection[Rando.Int(tempCollection.Count - 1)].name;
            }
            return skin;
        }
    }
}
