using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using MonoMod.Utils;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341
{
    public class VortexInit_Sa21341 : ModInitializer
    {
        public override void OnInitializeMod()
        {
            InitParameters();
            MapStaticUtil.GetArtWorks(new DirectoryInfo(VortexModParameters.Path + "/ArtWork"));
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance, VortexModParameters.PackageId);
            UnitUtil.ChangePassiveItem(VortexModParameters.PackageId);
            SkinUtil.LoadBookSkinsExtra(VortexModParameters.PackageId);
            LocalizeUtil.AddLocalLocalize(VortexModParameters.Path, VortexModParameters.PackageId);
            SkinUtil.PreLoadBufIcons();
            LocalizeUtil.RemoveError();
            UnitUtil.InitKeywords(Assembly.GetExecutingAssembly());
            UnitUtil.InitCustomEffects(new List<Assembly> { Assembly.GetExecutingAssembly() });
        }

        private static void InitParameters()
        {
            ModParameters.PackageIds.Add(VortexModParameters.PackageId);
            VortexModParameters.Path =
                Path.GetDirectoryName(
                    Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            ModParameters.Path.Add(VortexModParameters.Path);
            ModParameters.LocalizePackageIdAndPath.Add(VortexModParameters.PackageId, VortexModParameters.Path);
            ModParameters.SpritePreviewChange.AddRange(new Dictionary<string, List<LorId>>
            {
                { "SaeDefault_Sa21341", new List<LorId> { new LorId(VortexModParameters.PackageId, 10000001) } },
                { "MiyuDefault_Sa21341", new List<LorId> { new LorId(VortexModParameters.PackageId, 10000005) } },
                { "ZeroDefault_Sa21341", new List<LorId> { new LorId(VortexModParameters.PackageId, 10000008) } },
                { "GreenDefault_Sa21341", new List<LorId> { new LorId(VortexModParameters.PackageId, 10000009) } },
                { "KamiyoDefault_Sa21341", new List<LorId> { new LorId(VortexModParameters.PackageId, 10000012) } },
                {
                    "FragmentYellowDefault_Sa21341",
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 10000002) }
                }
            });
            ModParameters.BooksIds.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 10000001), new LorId(VortexModParameters.PackageId, 10000005),
                new LorId(VortexModParameters.PackageId, 10000008), new LorId(VortexModParameters.PackageId, 10000012)
            });
            ModParameters.SameInnerIdPassives.AddRange(new Dictionary<int, List<LorId>>
            {
                { 241001, new List<LorId> { new LorId(VortexModParameters.PackageId, 2) } }
            });
            ModParameters.OnlyCardKeywords.AddRange(new List<Tuple<List<string>, List<LorId>, LorId>>
            {
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "SaePage_Sa21341" },
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 6) },
                    new LorId(VortexModParameters.PackageId, 10000001)),
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "MiyuPage_Sa21341" },
                    new List<LorId>
                    {
                        new LorId(VortexModParameters.PackageId, 12), new LorId(VortexModParameters.PackageId, 13),
                        new LorId(VortexModParameters.PackageId, 14), new LorId(VortexModParameters.PackageId, 22),
                        new LorId(VortexModParameters.PackageId, 23), new LorId(VortexModParameters.PackageId, 24)
                    },
                    new LorId(VortexModParameters.PackageId, 10000005)),
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "ZeroPage_Sa21341" },
                    new List<LorId>
                    {
                        new LorId(VortexModParameters.PackageId, 35), new LorId(VortexModParameters.PackageId, 36),
                        new LorId(VortexModParameters.PackageId, 37), new LorId(VortexModParameters.PackageId, 38),
                        new LorId(VortexModParameters.PackageId, 39), new LorId(VortexModParameters.PackageId, 40)
                    },
                    new LorId(VortexModParameters.PackageId, 10000008)),
                new Tuple<List<string>, List<LorId>, LorId>(new List<string> { "KamiyoPage_Sa21341" },
                    new List<LorId>
                    {
                        new LorId(VortexModParameters.PackageId, 46), new LorId(VortexModParameters.PackageId, 47),
                        new LorId(VortexModParameters.PackageId, 48), new LorId("LorModPackRe21341.Mod", 19),
                        new LorId("LorModPackRe21341.Mod", 20), new LorId("LorModPackRe21341.Mod", 21)
                    },
                    new LorId(VortexModParameters.PackageId, 10000012))
            });
            ModParameters.UntransferablePassives.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 1), new LorId(VortexModParameters.PackageId, 35),
                new LorId(VortexModParameters.PackageId, 8), new LorId(VortexModParameters.PackageId, 9),
                new LorId(VortexModParameters.PackageId, 17), new LorId(VortexModParameters.PackageId, 18),
                new LorId(VortexModParameters.PackageId, 21), new LorId(VortexModParameters.PackageId, 28),
                new LorId(VortexModParameters.PackageId, 33)
            });
            ModParameters.PersonalCardList.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 1), new LorId(VortexModParameters.PackageId, 2),
                new LorId(VortexModParameters.PackageId, 10), new LorId(VortexModParameters.PackageId, 11),
                new LorId(VortexModParameters.PackageId, 33), new LorId(VortexModParameters.PackageId, 34),
                new LorId(VortexModParameters.PackageId, 49), new LorId(VortexModParameters.PackageId, 50),
                new LorId(VortexModParameters.PackageId, 53)
            });
            ModParameters.EgoPersonalCardList.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 9), new LorId(VortexModParameters.PackageId, 15),
                new LorId(VortexModParameters.PackageId, 41), new LorId(VortexModParameters.PackageId, 42),
                new LorId(VortexModParameters.PackageId, 44), new LorId(VortexModParameters.PackageId, 52)
            });
            ModParameters.PreBattleUnits.AddRange(
                new List<Tuple<LorId, List<PreBattleUnitModel>, List<SephirahType>, PreBattleUnitSpecialCases>>
                {
                    new Tuple<LorId, List<PreBattleUnitModel>, List<SephirahType>, PreBattleUnitSpecialCases>(
                        new LorId(VortexModParameters.PackageId, 1), new List<PreBattleUnitModel>
                        {
                            new PreBattleUnitModel
                            {
                                UnitId = 10000901,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 2),
                                SkinName = "Sae_Sa21341",
                                PassiveIds = new List<LorId>()
                            }
                        }, new List<SephirahType> { SephirahType.Keter }, PreBattleUnitSpecialCases.CustomUnits),
                    new Tuple<LorId, List<PreBattleUnitModel>, List<SephirahType>, PreBattleUnitSpecialCases>(
                        new LorId(VortexModParameters.PackageId, 3), new List<PreBattleUnitModel>
                        {
                            new PreBattleUnitModel
                            {
                                UnitId = 10000003,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 2),
                                SkinName = "Sae_Sa21341",
                                PassiveIds = new List<LorId>()
                            },
                            new PreBattleUnitModel
                            {
                                UnitId = 10000004,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 5),
                                SkinName = "Miyu_Sa21341",
                                PassiveIds = new List<LorId>()
                            }
                        }, new List<SephirahType> { SephirahType.Keter }, PreBattleUnitSpecialCases.CustomUnits),
                    new Tuple<LorId, List<PreBattleUnitModel>, List<SephirahType>, PreBattleUnitSpecialCases>(
                        new LorId(VortexModParameters.PackageId, 5), new List<PreBattleUnitModel>
                        {
                            new PreBattleUnitModel
                            {
                                UnitId = 10000010,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 2),
                                SkinName = "Sae_Sa21341",
                                PassiveIds = new List<LorId>()
                            },
                            new PreBattleUnitModel
                            {
                                UnitId = 10000004,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 5),
                                SkinName = "Miyu_Sa21341",
                                PassiveIds = new List<LorId>()
                            },
                            new PreBattleUnitModel
                            {
                                UnitId = 10000007,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 7),
                                SkinName = "Zero_Sa21341",
                                PassiveIds = new List<LorId>()
                            }
                        }, new List<SephirahType> { SephirahType.Keter }, PreBattleUnitSpecialCases.CustomUnits),
                    new Tuple<LorId, List<PreBattleUnitModel>, List<SephirahType>, PreBattleUnitSpecialCases>(
                        new LorId(VortexModParameters.PackageId, 7), new List<PreBattleUnitModel>
                        {
                            new PreBattleUnitModel
                            {
                                UnitId = 10000010,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 2),
                                SkinName = "Sae_Sa21341",
                                PassiveIds = new List<LorId>()
                            },
                            new PreBattleUnitModel
                            {
                                UnitId = 10000004,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 5),
                                SkinName = "Miyu_Sa21341",
                                PassiveIds = new List<LorId>()
                            },
                            new PreBattleUnitModel
                            {
                                UnitId = 10000007,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 7),
                                SkinName = "Zero_Sa21341",
                                PassiveIds = new List<LorId>()
                            },
                            new PreBattleUnitModel
                            {
                                UnitId = 10000011,
                                SephirahUnit = SephirahType.Keter,
                                UnitNameId = new LorId(VortexModParameters.PackageId, 14),
                                SkinName = "KamiyoMask_Sa21341",
                                PassiveIds = new List<LorId>()
                            }
                        }, new List<SephirahType> { SephirahType.Keter }, PreBattleUnitSpecialCases.CustomUnits)
                });
            ModParameters.DynamicNames.AddRange(new Dictionary<LorId, LorId>
            {
                { new LorId(VortexModParameters.PackageId, 10000001), new LorId(VortexModParameters.PackageId, 2) },
                { new LorId(VortexModParameters.PackageId, 10000005), new LorId(VortexModParameters.PackageId, 5) },
                { new LorId(VortexModParameters.PackageId, 10000008), new LorId(VortexModParameters.PackageId, 7) },
                { new LorId(VortexModParameters.PackageId, 10000009), new LorId(VortexModParameters.PackageId, 8) },
                { new LorId(VortexModParameters.PackageId, 10000012), new LorId(VortexModParameters.PackageId, 14) }
            });
            ModParameters.BannedEmotionStages.AddRange(new Dictionary<LorId, bool>
            {
                { new LorId(VortexModParameters.PackageId, 1), false },
                { new LorId(VortexModParameters.PackageId, 3), false },
                { new LorId(VortexModParameters.PackageId, 5), false },
                { new LorId(VortexModParameters.PackageId, 7), false }
            });
            ModParameters.ExtraReward.AddRange(new Dictionary<LorId, ExtraRewards>
            {
                {
                    new LorId(VortexModParameters.PackageId, 1),
                    new ExtraRewards
                    {
                        MessageId = "SaeDrop_Sa21341",
                        DroppedKeypages = new List<LorId> { new LorId(VortexModParameters.PackageId, 10000001) }
                    }
                },
                {
                    new LorId(VortexModParameters.PackageId, 2),
                    new ExtraRewards
                    {
                        MessageId = "SaeDrop_Sa21341",
                        DroppedKeypages = new List<LorId> { new LorId(VortexModParameters.PackageId, 10000001) }
                    }
                },
                {
                    new LorId(VortexModParameters.PackageId, 3),
                    new ExtraRewards
                    {
                        MessageId = "MiyuDrop_Sa21341",
                        DroppedKeypages = new List<LorId> { new LorId(VortexModParameters.PackageId, 10000005) }
                    }
                },
                {
                    new LorId(VortexModParameters.PackageId, 4),
                    new ExtraRewards
                    {
                        MessageId = "MiyuDrop_Sa21341",
                        DroppedKeypages = new List<LorId> { new LorId(VortexModParameters.PackageId, 10000005) }
                    }
                },
                {
                    new LorId(VortexModParameters.PackageId, 5),
                    new ExtraRewards
                    {
                        MessageId = "ZeroDrop_Sa21341",
                        DroppedKeypages = new List<LorId> { new LorId(VortexModParameters.PackageId, 10000008) }
                    }
                },
                {
                    new LorId(VortexModParameters.PackageId, 6),
                    new ExtraRewards
                    {
                        MessageId = "ZeroDrop_Sa21341",
                        DroppedKeypages = new List<LorId> { new LorId(VortexModParameters.PackageId, 10000008) }
                    }
                },
                {
                    new LorId(VortexModParameters.PackageId, 7),
                    new ExtraRewards
                    {
                        MessageId = "KamiyoDrop_Sa21341",
                        DroppedKeypages = new List<LorId> { new LorId(VortexModParameters.PackageId, 10000012) }
                    }
                },
                {
                    new LorId(VortexModParameters.PackageId, 8),
                    new ExtraRewards
                    {
                        MessageId = "KamiyoDrop_Sa21341",
                        DroppedKeypages = new List<LorId> { new LorId(VortexModParameters.PackageId, 10000012) }
                    }
                }
            });
            ModParameters.SkinParameters.AddRange(new List<SkinNames>
            {
                new SkinNames
                {
                    PackageId = VortexModParameters.PackageId,
                    Name = "Sae_Sa21341",
                    SkinParameters = new List<SkinParameters>
                    {
                        new SkinParameters
                        {
                            PivotPosX = float.Parse("-25"), PivotPosY = float.Parse("-400"),
                            Motion = ActionDetail.S1, FileName = "Slash.png"
                        },
                        new SkinParameters
                        {
                            PivotPosX = float.Parse("64"), PivotPosY = float.Parse("-340"),
                            Motion = ActionDetail.S2, FileName = "Penetrate.png"
                        },
                        new SkinParameters
                        {
                            PivotPosX = float.Parse("-60"), PivotPosY = float.Parse("-400"),
                            Motion = ActionDetail.S3, FileName = "Hit.png"
                        }
                    }
                }
            });
            ModParameters.ExtraConditionPassives.AddRange(new List<Tuple<LorId, List<LorId>>>
            {
                new Tuple<LorId, List<LorId>>(new LorId(230008),
                    new List<LorId>
                        { new LorId(VortexModParameters.PackageId, 3), new LorId(VortexModParameters.PackageId, 8) }),
                new Tuple<LorId, List<LorId>>(new LorId(241001),
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 2) }),
                new Tuple<LorId, List<LorId>>(new LorId(VortexModParameters.PackageId, 2),
                    new List<LorId> { new LorId(241001) }),
                new Tuple<LorId, List<LorId>>(new LorId(VortexModParameters.PackageId, 3),
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 8) })
            });
            ModParameters.MultiDeckPassive.AddRange(new List<Tuple<LorId, List<LorId>>>
            {
                new Tuple<LorId, List<LorId>>(new LorId(VortexModParameters.PackageId, 5),
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 5) })
            });
            ModParameters.MultiDeckPassiveIds.AddRange(new List<LorId> { new LorId(VortexModParameters.PackageId, 5) });
            ModParameters.UniquePassives.AddRange(new List<Tuple<LorId, List<LorId>>>
            {
                new Tuple<LorId, List<LorId>>(new LorId(VortexModParameters.PackageId, 3),
                    new List<LorId> { new LorId(VortexModParameters.PackageId, 5) })
            });
            ModParameters.MultiDeckUnits.AddRange(
                new List<LorId> { new LorId(VortexModParameters.PackageId, 10000001) });
            ModParameters.MultiDeckLabels.AddRange(new List<Tuple<LorId, LorId, List<string>>>
            {
                new Tuple<LorId, LorId, List<string>>(new LorId(VortexModParameters.PackageId, 5),
                    new LorId(VortexModParameters.PackageId, 10000001),
                    new List<string> { "AttackStance_Sa21341", "DefenseStance_Sa21341" })
            });
            ModParameters.DefaultKeyword.Add(VortexModParameters.PackageId, "SaeModPage_Sa21341");
            ModParameters.BookList.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 1)
            });
            ModParameters.EmotionExcludePassive.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 8)
            });
            ModParameters.SupportCharPassive.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 8)
            });
            ModParameters.BannedEmotionSelectionUnit.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 10000005)
            });
            ModParameters.OneSideCards.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 12), new LorId(VortexModParameters.PackageId, 13),
                new LorId(VortexModParameters.PackageId, 14), new LorId(VortexModParameters.PackageId, 22),
                new LorId(VortexModParameters.PackageId, 23), new LorId(VortexModParameters.PackageId, 24),
                new LorId(VortexModParameters.PackageId, 16)
            });
            ModParameters.ForceAggroPassiveIds.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 13)
            });
            ModParameters.NoEgoFloorUnit.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 10000005)
            });
            ModParameters.OnlyAllyTargetCardIds.AddRange(new List<LorId>
            {
                new LorId(VortexModParameters.PackageId, 12), new LorId(VortexModParameters.PackageId, 13),
                new LorId(VortexModParameters.PackageId, 22), new LorId(VortexModParameters.PackageId, 23),
                new LorId(VortexModParameters.PackageId, 24)
            });
        }
    }
}