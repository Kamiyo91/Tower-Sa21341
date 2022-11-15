using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BigDLL4221.Enum;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using LOR_DiceSystem;
using MonoMod.Utils;
using UnityEngine;

namespace VortexTower
{
    public class VortexInit : ModInitializer
    {
        public override void OnInitializeMod()
        {
            OnInitParameters();
            ArtUtil.GetArtWorks(new DirectoryInfo(VortexModParameters.Path + "/ArtWork"));
            CardUtil.ChangeCardItem(ItemXmlDataList.instance, VortexModParameters.PackageId);
            PassiveUtil.ChangePassiveItem(VortexModParameters.PackageId);
            LocalizeUtil.AddGlobalLocalize(VortexModParameters.PackageId);
            ArtUtil.PreLoadBufIcons();
            LocalizeUtil.RemoveError();
            CardUtil.InitKeywordsList(new List<Assembly> { Assembly.GetExecutingAssembly() });
            ArtUtil.InitCustomEffects(new List<Assembly> { Assembly.GetExecutingAssembly() });
            CustomMapHandler.ModResources.CacheInit.InitCustomMapFiles(Assembly.GetExecutingAssembly());
        }

        private static void OnInitParameters()
        {
            ModParameters.PackageIds.Add(VortexModParameters.PackageId);
            VortexModParameters.Path = Path.GetDirectoryName(
                Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            ModParameters.Path.Add(VortexModParameters.PackageId, VortexModParameters.Path);
            ModParameters.DefaultKeyword.Add(VortexModParameters.PackageId, "SaeModPage_Sa21341");
            OnInitSprites();
            OnInitSkins();
            OnInitKeypages();
            OnInitCards();
            OnInitDropBooks();
            OnInitPassives();
            OnInitRewards();
            OnInitStages();
            OnInitCredenza();
        }

        private static void OnInitRewards()
        {
            ModParameters.StartUpRewardOptions.Add(new RewardOptions(new Dictionary<LorId, int>
                {
                    { new LorId(VortexModParameters.PackageId, 1), 0 }
                }
            ));
        }

        private static void OnInitCards()
        {
            ModParameters.CardOptions.Add(VortexModParameters.PackageId, new List<CardOptions>
            {
                new CardOptions(37, CardOption.OnlyPage, new List<string> { "MiyuPage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000003) }, true, true,
                    cardColorOptions: new CardColorOptions(Color.yellow, customIconColor: Color.yellow,
                        useHSVFilter: false)),
                new CardOptions(27, onlyAllyTargetCard: true, oneSideOnlyCard: true),
                new CardOptions(1, CardOption.Personal),
                new CardOptions(2, CardOption.Personal),
                new CardOptions(24, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.yellow, customIconColor: Color.yellow,
                        useHSVFilter: false)),
                new CardOptions(33, CardOption.Personal, onlyAllyTargetCard: true, oneSideOnlyCard: true,
                    cardColorOptions: new CardColorOptions(Color.yellow, customIconColor: Color.yellow,
                        useHSVFilter: false)),
                new CardOptions(9, CardOption.EgoPersonal),
                new CardOptions(26, CardOption.EgoPersonal),
                new CardOptions(14, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.red, customIconColor: Color.red, useHSVFilter: false)),
                new CardOptions(6, CardOption.OnlyPage, new List<string> { "SaePage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000001) }),
                new CardOptions(53, CardOption.OnlyPage, new List<string> { "ZeroPage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000004) }),
                new CardOptions(54, CardOption.OnlyPage, new List<string> { "ZeroPage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000004) }),
                new CardOptions(55, CardOption.OnlyPage, new List<string> { "ZeroPage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000004) }),
                new CardOptions(56, CardOption.OnlyPage, new List<string> { "ZeroPage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000004) }),
                new CardOptions(57, CardOption.OnlyPage, new List<string> { "ZeroPage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000004) }),
                new CardOptions(58, CardOption.OnlyPage, new List<string> { "ZeroPage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000004) }),
                new CardOptions(51, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(VortexModParameters.Blue,
                        customIconColor: VortexModParameters.Blue, useHSVFilter: false)),
                new CardOptions(52, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(VortexModParameters.Blue,
                        customIconColor: VortexModParameters.Blue, useHSVFilter: false)),
                new CardOptions(59, CardOption.EgoPersonal),
                new CardOptions(70, CardOption.EgoPersonal),
                new CardOptions(62, CardOption.EgoPersonal),
                new CardOptions(71, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.gray, iconColor: HSVColors.Black)),
                new CardOptions(67, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.gray, iconColor: HSVColors.Black)),
                new CardOptions(68, CardOption.Personal,
                    cardColorOptions: new CardColorOptions(Color.gray, iconColor: HSVColors.Black))
            });
        }

        private static void OnInitSkins()
        {
            ModParameters.SkinOptions.AddRange(new Dictionary<string, SkinOptions>
            {
                { "Sae_Sa21341", new SkinOptions(VortexModParameters.PackageId) },
                { "SaeShadow_Sa21341", new SkinOptions(VortexModParameters.PackageId) },
                { "SaeRage_Sa21341", new SkinOptions(VortexModParameters.PackageId) },
                { "MiyuBlue_Sa21341", new SkinOptions(VortexModParameters.PackageId) },
                { "BluePetal_Sa21341", new SkinOptions(VortexModParameters.PackageId, 501) }
            });
        }

        private static void OnInitKeypages()
        {
            ModParameters.KeypageOptions.Add(VortexModParameters.PackageId, new List<KeypageOptions>
            {
                new KeypageOptions(10000001, bookCustomOptions: new BookCustomOptions(nameTextId: 2), isMultiDeck: true,
                    multiDeckOptions: new MultiDeckOptions(new List<string>
                        { "AttackStance_Sa21341", "DefenseStance_Sa21341" }),
                    keypageColorOptions: new KeypageColorOptions(Color.red, Color.red)),
                new KeypageOptions(10000901, bookCustomOptions: new BookCustomOptions(nameTextId: 2), isMultiDeck: true,
                    multiDeckOptions: new MultiDeckOptions(new List<string>
                        { "AttackStance_Sa21341", "DefenseStance_Sa21341" }),
                    keypageColorOptions: new KeypageColorOptions(Color.red, Color.red)),
                new KeypageOptions(2,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 2, originalSkin: "SaeShadow_Sa21341",
                        egoSkin: new List<string> { "SaeRage_Sa21341" }), isMultiDeck: true,
                    multiDeckOptions: new MultiDeckOptions(new List<string>
                        { "AttackStance_Sa21341", "DefenseStance_Sa21341" }),
                    keypageColorOptions: new KeypageColorOptions(Color.red, Color.red)),
                new KeypageOptions(4,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 4, originalSkin: "MiyuBlue_Sa21341",
                        egoSkin: new List<string> { "BluePetal_Sa21341" }),
                    keypageColorOptions: new KeypageColorOptions(VortexModParameters.Blue, VortexModParameters.Blue)),
                new KeypageOptions(5,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 5),
                    keypageColorOptions: new KeypageColorOptions(Color.green, Color.green)),
                new KeypageOptions(6,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 6),
                    keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray)),
                new KeypageOptions(7,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 7),
                    keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray)),
                new KeypageOptions(8,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 8),
                    keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray)),
                new KeypageOptions(9,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 9),
                    keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray)),
                new KeypageOptions(10,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 10),
                    keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray)),
                new KeypageOptions(10000003,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 4),
                    keypageColorOptions: new KeypageColorOptions(Color.yellow, Color.yellow)),
                new KeypageOptions(10000004,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 11),
                    keypageColorOptions: new KeypageColorOptions(VortexModParameters.Blue, VortexModParameters.Blue)),
                new KeypageOptions(10000005,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 10),
                    keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray)),
                new KeypageOptions(10000902,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 4),
                    keypageColorOptions: new KeypageColorOptions(Color.yellow, Color.yellow)),
                new KeypageOptions(10000903,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 11),
                    keypageColorOptions: new KeypageColorOptions(VortexModParameters.Blue, VortexModParameters.Blue)),
                new KeypageOptions(10000904,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 2),
                    keypageColorOptions: new KeypageColorOptions(Color.red, Color.red)),
                new KeypageOptions(10000905,
                    bookCustomOptions: new BookCustomOptions(nameTextId: 10),
                    keypageColorOptions: new KeypageColorOptions(Color.gray, Color.gray))
            });
        }

        private static void OnInitCredenza()
        {
            ModParameters.CredenzaOptions.Add(VortexModParameters.PackageId,
                new CredenzaOptions(CredenzaEnum.ModifiedCredenza, credenzaNameId: VortexModParameters.PackageId,
                    customIconSpriteId: VortexModParameters.PackageId, credenzaBooksId: new List<int>
                    {
                        10000001, 10000003, 10000004, 10000005
                    }));
        }

        private static void OnInitSprites()
        {
            ModParameters.SpriteOptions.Add(VortexModParameters.PackageId, new List<SpriteOptions>
            {
                new SpriteOptions(SpriteEnum.Custom, 10000001, "SaeDefault_Sa21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000003, "MiyuDefault_Sa21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000004, "ZeroDefault_Sa21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000005, "KamiyoDefault_Sa21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000010, "FragmentYellowDefault_Sa21341"),
                new SpriteOptions(SpriteEnum.Custom, 10000011, "FragmentYellowDefault_Sa21341")
            });
        }

        private static void OnInitStages()
        {
            ModParameters.StageOptions.Add(VortexModParameters.PackageId, new List<StageOptions>
            {
                new StageOptions(1,
                    preBattleOptions: new PreBattleOptions(new List<SephirahType> { SephirahType.Keter },
                        battleType: PreBattleType.CustomUnits,
                        unitModels: new List<UnitModel> { VortexModParameters.SaeStoryModel }),
                    bannedEmotionLevel: true, stageColorOptions: new StageColorOptions(Color.red, Color.red)),
                new StageOptions(2, stageColorOptions: new StageColorOptions(Color.red, Color.red)),
                new StageOptions(3,
                    preBattleOptions: new PreBattleOptions(new List<SephirahType> { SephirahType.Keter },
                        battleType: PreBattleType.CustomUnits,
                        unitModels: new List<UnitModel>
                            { VortexModParameters.SaeStoryModelMiyuStage, VortexModParameters.MiyuStoryModel }),
                    bannedEmotionLevel: true, stageColorOptions: new StageColorOptions(Color.yellow, Color.yellow)),
                new StageOptions(4, stageColorOptions: new StageColorOptions(Color.yellow, Color.yellow)),
                new StageOptions(5, preBattleOptions: new PreBattleOptions(
                        new List<SephirahType> { SephirahType.Keter },
                        battleType: PreBattleType.CustomUnits,
                        unitModels: new List<UnitModel>
                        {
                            VortexModParameters.SaeStoryModelZeroStage, VortexModParameters.MiyuStoryModel,
                            VortexModParameters.ZeroStoryModel
                        }),
                    bannedEmotionLevel: true,
                    stageColorOptions: new StageColorOptions(VortexModParameters.Blue, VortexModParameters.Blue)),
                new StageOptions(6,
                    stageColorOptions: new StageColorOptions(VortexModParameters.Blue, VortexModParameters.Blue)),
                new StageOptions(7, preBattleOptions: new PreBattleOptions(
                        new List<SephirahType> { SephirahType.Keter },
                        battleType: PreBattleType.CustomUnits,
                        unitModels: new List<UnitModel>
                        {
                            VortexModParameters.SaeStoryModelZeroStage, VortexModParameters.MiyuStoryModel,
                            VortexModParameters.ZeroStoryModel, VortexModParameters.ForgottenStoryModel
                        }),
                    bannedEmotionLevel: true,
                    stageColorOptions: new StageColorOptions(Color.gray, Color.gray)),
                new StageOptions(8,
                    stageColorOptions: new StageColorOptions(Color.gray, Color.gray),
                    stageRewardOptions: new RewardOptions(new Dictionary<LorId, int>
                    {
                        { new LorId(VortexModParameters.PackageId, 5), 5 }
                    }, messageId: "KamiyoDrop_Sa21341"))
            });
        }

        private static void OnInitPassives()
        {
            ModParameters.PassiveOptions.Add(VortexModParameters.PackageId, new List<PassiveOptions>
            {
                new PassiveOptions(1, false, passiveColorOptions: new PassiveColorOptions(Color.red, Color.red)),
                new PassiveOptions(9, false, passiveColorOptions: new PassiveColorOptions(Color.yellow, Color.yellow)),
                new PassiveOptions(33, false, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(17, false,
                    passiveColorOptions: new PassiveColorOptions(VortexModParameters.Blue, VortexModParameters.Blue)),
                new PassiveOptions(2, false,
                    cannotBeUsedWithPassives: new List<LorId> { new LorId(241301), new LorId(241001) }),
                new PassiveOptions(5, isMultiDeck: true,
                    multiDeckOptions: new MultiDeckOptions(new List<string>
                        { "AttackStance_Sa21341", "DefenseStance_Sa21341" })),
                new PassiveOptions(35, false),
                new PassiveOptions(8, false,
                    cannotBeUsedWithPassives: new List<LorId> { new LorId(VortexModParameters.PackageId, 5) }),
                new PassiveOptions(18, false),
                new PassiveOptions(28, false),
                new PassiveOptions(12,
                    passiveColorOptions: new PassiveColorOptions(VortexModParameters.Blue, VortexModParameters.Blue)),
                new PassiveOptions(19, passiveColorOptions: new PassiveColorOptions(Color.green, Color.green)),
                new PassiveOptions(31, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(30, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(27, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(26, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(32, passiveColorOptions: new PassiveColorOptions(Color.gray, Color.gray)),
                new PassiveOptions(3,
                    canBeUsedWithPassivesOne: new List<LorId> { new LorId(VortexModParameters.PackageId, 5) },
                    cannotBeUsedWithPassives: new List<LorId>
                    {
                        new LorId(230008), new LorId(VortexModParameters.MaryPackageId, 3),
                        new LorId(VortexModParameters.KamiyoModPackageId, 22)
                    })
            });
        }

        private static void OnInitDropBooks()
        {
            ModParameters.DropBookOptions.Add(VortexModParameters.PackageId, new List<DropBookOptions>
            {
                new DropBookOptions(1, new DropBookColorOptions(Color.red, Color.red)),
                new DropBookOptions(2, new DropBookColorOptions(Color.yellow, Color.yellow)),
                new DropBookOptions(3, new DropBookColorOptions(VortexModParameters.Blue, VortexModParameters.Blue)),
                new DropBookOptions(4, new DropBookColorOptions(Color.gray, Color.gray)),
                new DropBookOptions(5, new DropBookColorOptions(Color.gray, Color.gray))
            });
        }
    }
}