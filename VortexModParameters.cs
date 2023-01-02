using System.Collections.Generic;
using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using LOR_XML;
using UnityEngine;
using VortexTower.Forgotten.ForgottenMaps;
using VortexTower.Miyu;
using VortexTower.Miyu.Buffs;
using VortexTower.Sae;
using VortexTower.Sae.Buffs;
using VortexTower.Zero;
using VortexTower.Zero.GreenHunter;
using VortexTower.Zero.GreenHunter.Buffs;

namespace VortexTower
{
    public class VortexModParameters
    {
        public const string PackageId = "VortexTowerModSa21341.Mod";
        public const string KamiyoModPackageId = "LorModPackRe21341.Mod";
        public const string MaryPackageId = "MaryIb21341.Mod";
        public static string Path;

        public static List<LorId> SaeKeypageIds = new List<LorId>
            { new LorId(PackageId, 10000901), new LorId(PackageId, 10000010), new LorId(PackageId, 10000003) };

        public static List<LorId> DarkSaeAttackDeck = new List<LorId>
        {
            new LorId(PackageId, 22), new LorId(PackageId, 22), new LorId(PackageId, 22), new LorId(PackageId, 17),
            new LorId(PackageId, 17), new LorId(PackageId, 21), new LorId(PackageId, 21), new LorId(PackageId, 18),
            new LorId(PackageId, 18)
        };

        public static List<LorId> DarkSaeDefDeck = new List<LorId>
        {
            new LorId(PackageId, 15), new LorId(PackageId, 15), new LorId(PackageId, 15), new LorId(PackageId, 16),
            new LorId(PackageId, 16), new LorId(PackageId, 16), new LorId(PackageId, 18), new LorId(PackageId, 18),
            new LorId(PackageId, 18)
        };

        public static List<LorId> SaeAttackDeck = new List<LorId>
        {
            new LorId(PackageId, 13), new LorId(PackageId, 13), new LorId(PackageId, 13), new LorId(PackageId, 5),
            new LorId(PackageId, 5), new LorId(PackageId, 12), new LorId(PackageId, 12), new LorId(PackageId, 6),
            new LorId(PackageId, 6)
        };

        public static List<LorId> SaeDefDeck = new List<LorId>
        {
            new LorId(PackageId, 3), new LorId(PackageId, 3), new LorId(PackageId, 3), new LorId(PackageId, 4),
            new LorId(PackageId, 4), new LorId(PackageId, 4), new LorId(PackageId, 6), new LorId(PackageId, 6),
            new LorId(PackageId, 6)
        };

        public static MapModel SaePhase1Map = new MapModel(typeof(SaePhase1_Sa21341MapManager), "SaePhase1_Sa21341",
            bgy: 0.25f, fy: 0.8f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 1), new LorId(PackageId, 2) });

        public static MapModel SaePhase2Map = new MapModel(typeof(SaePhase2_Sa21341MapManager), "SaePhase2_Sa21341",
            bgy: 0.25f, fy: 0.8f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 1), new LorId(PackageId, 2) });

        public static MapModel MiyuMap =
            new MapModel(typeof(BlueGuardian_Sa21341MapManager), "BlueGuardian_Sa21341", bgy: 0.30f,
                originalMapStageIds: new List<LorId> { new LorId(PackageId, 3), new LorId(PackageId, 4) });

        public static MapModel ZeroMap = new MapModel(typeof(GreenGuardian_Sa21341MapManager), "GreenHunter_Sa21341",
            bgy: 0.2f, fy: 0.25f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 5), new LorId(PackageId, 6) });

        public static MapModel ForgottenMap = new MapModel(typeof(Forgotten5_Sa21341MapManager), "Forgotten5_Sa21341",
            bgy: 0.475f, fy: 0.225f,
            originalMapStageIds: new List<LorId> { new LorId(PackageId, 7), new LorId(PackageId, 8) });

        public static UnitModel SaeStoryModel = new UnitModel(10000901, PackageId, 2);

        public static UnitModel SaeStoryModelMiyuStage = new UnitModel(10000901, PackageId, 2,
            additionalPassiveIds: new List<LorId> { new LorId(PackageId, 14) });

        public static UnitModel SaeStoryModelZeroStage = new UnitModel(10000904, PackageId, 2,
            additionalPassiveIds: new List<LorId> { new LorId(PackageId, 14) });

        public static UnitModel ForgottenStoryModel = new UnitModel(10000905, PackageId, 10);
        public static UnitModel ZeroStoryModel = new UnitModel(10000903, PackageId, 11);
        public static UnitModel MiyuStoryModel = new UnitModel(10000902, PackageId, 4);
        public static Color Blue = new Color(0.4f, 0.69f, 1f);
    }

    public class SaeUtil
    {
        public NpcSaeUtil SaeNpcUtil = new NpcSaeUtil(new NpcMechUtilBaseModel("PhaseSae21341",
            new Dictionary<int, EgoOptions> { { 0, new EgoOptions(egoSkinName: "SaeRage_Sa21341", refreshUI: true) } },
            permanentBuffList: new List<PermanentBuffOptions>
                { new PermanentBuffOptions(new BattleUnitBuf_SaeNpcCostReduce_Sa21341()) },
            addBuffsOnPlayerUnitsAtStart: new List<BattleUnitBuf> { new BattleUnitBuf_Vip_Sa21341() },
            originalSkinName: "SaeShadow_Sa21341", mechOptions: new Dictionary<int, MechPhaseOptions>
            {
                {
                    0, new MechPhaseOptions(1, 128, hasCustomMap: true,
                        onPhaseChangeDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "ShadowSae1",
                                dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                                    .EffectTexts["ShadowSaePhase1_21341"].Desc
                            }
                        })
                },
                {
                    1,
                    new MechPhaseOptions(forceEgo: true, hasExtraFunctionRoundPreEnd: true,
                        onPhaseChangeDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "ShadowSae2",
                                dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                                    .EffectTexts["ShadowSaePhase2_21341"].Desc
                            }
                        },
                        buffOptions: new MechBuffOptions(
                            new List<BattleUnitBuf> { new BattleUnitBuf_RedAura_Sa21341() },
                            otherSideBuffs: new List<OtherSideBuffOption>
                                { new OtherSideBuffOption(new BattleUnitBuf_SaeSecondPhase_Sa21341(), index: 0) }),
                        speedDieAdder: 2, hasCustomMap: true, mapOrderIndex: 1)
                }
            }));
    }

    public class MiyuUtil
    {
        public NpcMiyuUtil MiyuNpcUtil = new NpcMiyuUtil(new NpcMechUtilBaseModel("MiyuPhaseSa21341",
            new Dictionary<int, EgoOptions>
                { { 0, new EgoOptions(egoSkinName: "BluePetal_Sa21341", refreshUI: true) } },
            originalSkinName: "MiyuBlue_Sa21341", permanentBuffList: new List<PermanentBuffOptions>
            {
                new PermanentBuffOptions(new BattleUnitBuf_MiyuNpcCostReduce_Sa21341())
            }, addBuffsOnPlayerUnitsAtStart: new List<BattleUnitBuf>
            {
                new BattleUnitBuf_Vip_Sa21341()
            }, mechOptions: new Dictionary<int, MechPhaseOptions>
            {
                {
                    0, new MechPhaseOptions(2, hasCustomMap: true, mechHp: 201, startMassAttack: true,
                        setCounterToMax: true, egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                        {
                            new SpecialAttackCardOptions(new LorId(VortexModParameters.PackageId, 44))
                        }, onPhaseChangeDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "ShadowMiyu1",
                                dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                                    .EffectTexts["ShadowMiyuPhase1_21341"].Desc
                            }
                        })
                },
                {
                    1, new MechPhaseOptions(2, forceEgo: true, hasExtraFunctionRoundStart: true, hasCustomMap: true,
                        buffOptions: new MechBuffOptions
                        {
                            EachRoundStartKeywordBuffs = new Dictionary<KeywordBuf, int>
                            {
                                { KeywordBuf.Strength, 2 }, { KeywordBuf.Endurance, 2 }
                            }
                        }, startMassAttack: true,
                        musicOptions: new MusicOptions("BlueGuardianPhase2_Sa21341.ogg", "BlueGuardian_Sa21341"),
                        setCounterToMax: true, egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                        {
                            new SpecialAttackCardOptions(new LorId(VortexModParameters.PackageId, 34))
                        }, onPhaseChangeDialogList: new List<AbnormalityCardDialog>
                        {
                            new AbnormalityCardDialog
                            {
                                id = "ShadowMiyu2",
                                dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                                    .EffectTexts["ShadowMiyuPhase2_21341"].Desc
                            }
                        })
                }
            }));
    }

    public class GreenGuardianUtil
    {
        public NpcMechUtil_GreenGuardian GreenGuardianNpcUtil =
            new NpcMechUtil_GreenGuardian(new NpcMechUtilBaseModel("GreenGuardianPhase21341",
                permanentBuffList: new List<PermanentBuffOptions>
                    { new PermanentBuffOptions(new BattleUnitBuf_GreenLeafNpc_Sa21341()) },
                mechOptions: new Dictionary<int, MechPhaseOptions>
                {
                    {
                        0, new MechPhaseOptions(3, 223, hasCustomMap: true, loweredCost: 1, changeCardCost: true,
                            onPhaseChangeDialogList: new List<AbnormalityCardDialog>
                            {
                                new AbnormalityCardDialog
                                {
                                    id = "GreenGuardian1",
                                    dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                                        .EffectTexts["GreenGuardianPhase1_21341"].Desc
                                }
                            })
                    },
                    {
                        1, new MechPhaseOptions(hasExtraFunctionRoundStart: true, startMassAttack: true,
                            hasCustomMap: true, setCounterToMax: true,
                            egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                            {
                                new SpecialAttackCardOptions(new LorId(VortexModParameters.PackageId, 60))
                            }, musicOptions: new MusicOptions("GreenGuardian2_Sa21341.ogg", "GreenHunter_Sa21341"),
                            speedDieAdder: 4, loweredCost: 1, buffOptions: new MechBuffOptions
                            {
                                EachRoundStartKeywordBuffs = new Dictionary<KeywordBuf, int>
                                {
                                    { KeywordBuf.Strength, 2 }, { KeywordBuf.Endurance, 2 }
                                }
                            }, changeCardCost: true, onPhaseChangeDialogList: new List<AbnormalityCardDialog>
                            {
                                new AbnormalityCardDialog
                                {
                                    id = "ShadowMiyu2",
                                    dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                                        .EffectTexts["GreenGuardianPhase2_21341"].Desc
                                }
                            })
                    }
                }, egoMaps: new Dictionary<LorId, MapModel>
                {
                    { new LorId(VortexModParameters.PackageId, 60), VortexModParameters.ZeroMap }
                }));

        public MechUtilBase GreenGuardianPlayerUtil = new MechUtilBase(new MechUtilBaseModel(survive: true,
            egoMaps: new Dictionary<LorId, MapModel>
                { { new LorId(VortexModParameters.PackageId, 87), VortexModParameters.ZeroMap } },
            personalCards: new Dictionary<LorId, PersonalCardOptions>
            {
                { new LorId(VortexModParameters.PackageId, 87), new PersonalCardOptions() }
            },
            additionalStartDraw: 2,
            recoverToHp: 41,
            permanentBuffList: new List<PermanentBuffOptions>
                { new PermanentBuffOptions(new BattleUnitBuf_GreenLeaf_Sa21341()) }), VortexModParameters.PackageId);
    }
}