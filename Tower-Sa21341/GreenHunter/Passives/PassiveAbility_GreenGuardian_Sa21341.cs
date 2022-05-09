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
        private BattleUnitBuf_GreenLeaf_Sa21341 _buff;
        private bool _singleUse;
        private NpcMechUtil_Ex2 _util;

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            if (_buff.stack < 10) _buff.stack++;
            behavior.card.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Decay, 1, owner);
        }

        public override void OnWaveStart()
        {
            _singleUse = false;
            _buff = new BattleUnitBuf_GreenLeaf_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
            _util = new NpcMechUtil_Ex2(new NpcMechUtilBaseModel
            {
                Owner = owner,
                MechHp = 311,
                HasMechOnHp = true,
                EgoMapName = "GreenGuardian_Sa21341",
                EgoMapType = typeof(BlueGuardian_Sa21341MapManager),
                BgY = 0.25f,
                FlY = 0.8f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(VortexModParameters.PackageId, 5), new LorId(VortexModParameters.PackageId, 6)
                },
                LorIdEgoMassAttack = new LorId(VortexModParameters.PackageId, 25),
                EgoAttackCardId = new LorId(VortexModParameters.PackageId, 25)
            }, "GreenGuardianPhase_Sa21341");
            _util.Restart();
        }

        public override int SpeedDiceNumAdder()
        {
            return _util.GetPhase() <= 0 ? 3 : 6;
        }

        public override void OnRoundEnd()
        {
            _util.ExhaustEgoAttackCards();
            _util.SetOneTurnCard(false);
            _util.RaiseCounter();
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
                ItemXmlDataList.instance.GetCardItem(new LorId(VortexModParameters.PackageId, 27)));
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
            if (curCard.card.GetID() == new LorId(VortexModParameters.PackageId, 1))
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