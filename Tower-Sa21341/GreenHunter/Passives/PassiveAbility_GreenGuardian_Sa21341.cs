using System.Collections.Generic;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.GreenHunter.Buffs;
using VortexLabyrinth_Sa21341.Maps;
using VortexLabyrinth_Sa21341.UtilSa21341.Extension;

namespace VortexLabyrinth_Sa21341.GreenHunter.Passives
{
    public class PassiveAbility_GreenGuardian_Sa21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_GreenLeafNpc_Sa21341 _buff;
        private bool _singleUse;
        private NpcMechUtil_Ex2 _util;

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            _buff.AddStacks(1);
        }

        public override void OnWaveStart()
        {
            var passive = owner.passiveDetail.AddPassive(new PassiveAbility_251201());
            passive.Hide();
            _singleUse = false;
            _buff = new BattleUnitBuf_GreenLeafNpc_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
            _util = new NpcMechUtil_Ex2(new NpcMechUtilBaseModel
            {
                Owner = owner,
                MechHp = 311,
                HasMechOnHp = true,
                EgoMapName = "GreenHunterPhase2_Sa21341",
                EgoMapType = typeof(GreenGuardian2_Sa21341MapManager),
                BgY = 0.2f,
                FlY = 0.25f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(VortexModParameters.PackageId, 5), new LorId(VortexModParameters.PackageId, 6)
                },
                LorIdEgoMassAttack = new LorId(VortexModParameters.PackageId, 32),
                EgoAttackCardId = new LorId(VortexModParameters.PackageId, 32)
            }, "GreenGuardianPhase_Sa21341");
            _util.Restart();
        }

        public override int SpeedDiceNumAdder()
        {
            return _util.GetPhase() <= 0 ? 3 : 5;
        }

        public int GetPhase()
        {
            return _util.GetPhase();
        }

        public override void OnRoundEnd()
        {
            _util.ExhaustEgoAttackCards();
            _singleUse = false;
        }

        public override void OnBattleEnd()
        {
            _util.OnEndBattle();
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.MechHpCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnStartBattle()
        {
            UnitUtil.RemoveImmortalBuff(owner);
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (_singleUse || _buff.stack <= 9) return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
            _singleUse = true;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(new LorId(VortexModParameters.PackageId, 32)));
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnRoundStartAfter()
        {
            if (_util.GetPhase() <= 0) return;
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1);
        }

        public override void OnRoundEndTheLast()
        {
            _util.CheckPhase();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.card.GetID() == new LorId(VortexModParameters.PackageId, 32))
            {
                _buff.stack = 0;
                owner.allyCardDetail.ExhaustACardAnywhere(curCard.card);
            }

            _util.ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }
    }
}