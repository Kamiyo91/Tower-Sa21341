using System.Collections.Generic;
using System.Linq;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using LOR_XML;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Maps;
using VortexLabyrinth_Sa21341.UtilSa21341.Extension.Zero;
using VortexLabyrinth_Sa21341.Zero.Buffs;

namespace VortexLabyrinth_Sa21341.Zero.Passives
{
    public class PassiveAbility_Zero_Sa21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_BlueFlame_Sa21341 _buff;
        private MechUtil_Zero _util;

        public override void OnWaveStart()
        {
            InitDialog();
            _buff = new BattleUnitBuf_BlueFlame_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
            _util = new MechUtil_Zero(new MechUtilBaseModel
            {
                Owner = owner,
                Survive = true,
                Hp = 0,
                SetHp = 10,
                HasEgo = true,
                EgoType = typeof(BattleUnitBuf_BlueFlameEgo_Sa21341),
                EgoCardId = new LorId(VortexModParameters.PackageId, 33),
                HasEgoAbDialog = true,
                HasEgoAttack = true,
                EgoAttackCardId = new LorId(VortexModParameters.PackageId, 41),
                EgoAbColorColor = AbColorType.Positive,
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Zero",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("ZeroEgoActive1_Sa21341"))
                            .Value.Desc
                    }
                },
                EgoMapName = "GreenHunter_Sa21341",
                EgoMapType = typeof(GreenGuardian_Sa21341MapManager),
                BgY = 0.25f,
                FlY = 0.8f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(VortexModParameters.PackageId, 5), new LorId(VortexModParameters.PackageId, 6)
                }
            }, _buff);
            UnitUtil.CheckSkinProjection(owner);
            if (owner.faction != Faction.Enemy) return;
            if (UnitUtil.SpecialCaseEgo(owner.faction, new LorId(VortexModParameters.PackageId, 17),
                    typeof(BattleUnitBuf_BlueFlameEgo_Sa21341))) _util.ForcedEgo();
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.SurviveCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override bool CanAddBuf(BattleUnitBuf buf)
        {
            if (buf.bufType != KeywordBuf.Burn) return true;
            _buff.AddStacks(buf.stack);
            return false;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            _buff.AddStacks(1);
            var target = behavior.card.target;
            if (target == null) return;
            if (target.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_BlueBurn_Sa21341) is
                BattleUnitBuf_BlueBurn_Sa21341 buff)
                buff.OnAddBuf(1);
            else
                target.bufListDetail.AddBuf(new BattleUnitBuf_BlueBurn_Sa21341());
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
        }

        public override void OnRoundStartAfter()
        {
            ConvertBurnForAll();
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 34));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 34));
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
            _util.ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEnd()
        {
            if (owner.faction != Faction.Enemy) return;
            if (UnitUtil.SpecialCaseEgo(owner.faction, new LorId(VortexModParameters.PackageId, 17),
                    typeof(BattleUnitBuf_BlueFlameEgo_Sa21341))) _util.ForcedEgo();
        }

        private void InitDialog()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId !=
                VortexModParameters.PackageId) return;
            switch (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id)
            {
                case 5:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000007));
                    break;
                case 7:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000009));
                    break;
            }
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }

        public void ConvertBurnForAll()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList().Where(x =>
                         x != owner && !x.passiveDetail.HasPassive<PassiveAbility_BlueBurn_Sa21341>()))
            {
                var existCheck = false;
                var burnBuff = unit.bufListDetail.GetActivatedBufList().FirstOrDefault(x =>
                    x.bufType == KeywordBuf.Burn && !(x is BattleUnitBuf_BlueBurn_Sa21341));
                var burnNextBuff = unit.bufListDetail.GetReadyBufList().FirstOrDefault(x =>
                    x.bufType == KeywordBuf.Burn && !(x is BattleUnitBuf_BlueBurn_Sa21341));
                var blueBuff = unit.bufListDetail.GetActivatedBufList().FirstOrDefault(x =>
                    x is BattleUnitBuf_BlueBurn_Sa21341);
                if (blueBuff == null)
                {
                    existCheck = true;
                    blueBuff = new BattleUnitBuf_BlueBurn_Sa21341
                    {
                        stack = 0
                    };
                }

                if (burnBuff != null)
                {
                    blueBuff.stack += burnBuff.stack;
                    unit.bufListDetail.RemoveBuf(burnBuff);
                }

                if (burnNextBuff != null)
                {
                    blueBuff.stack += burnNextBuff.stack;
                    unit.bufListDetail.RemoveReadyBuf(burnNextBuff);
                }

                if (existCheck && blueBuff.stack != 0) unit.bufListDetail.AddBuf(blueBuff);
                var passive = unit.passiveDetail.AddPassive(new LorId(VortexModParameters.PackageId, 41));
                passive.Hide();
                unit.passiveDetail.OnCreated();
                passive.OnRoundStartAfter();
            }
        }
    }
}