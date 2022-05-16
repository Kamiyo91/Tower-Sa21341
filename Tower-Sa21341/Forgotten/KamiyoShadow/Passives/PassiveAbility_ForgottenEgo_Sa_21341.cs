using System.Collections.Generic;
using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.Buffs;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;
using VortexLabyrinth_Sa21341.Maps.ForgottenMaps;
using VortexLabyrinth_Sa21341.UtilSa21341.Extension.Forgotten;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Passives
{
    public class PassiveAbility_ForgottenEgo_Sa_21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_Remembrance_Sa21341 _buff;
        private bool _singleUse;
        private NpcMechUtil_Forgotten _util;

        public override void OnWaveStart()
        {
            _singleUse = false;
            _buff = new BattleUnitBuf_Remembrance_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
            owner.bufListDetail.AddBuf(new BattleUnitBuf_0CardCost_Sa21341());
            _util = new NpcMechUtil_Forgotten(new NpcMechUtilBaseModel
            {
                Owner = owner,
                EgoMapName = "Forgotten5_Sa21341",
                EgoMapType = typeof(Forgotten5_Sa21341MapManager),
                BgY = 0.475f,
                FlY = 0.225f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(VortexModParameters.PackageId, 7), new LorId(VortexModParameters.PackageId, 8)
                },
                LorIdEgoMassAttack = new LorId(VortexModParameters.PackageId, 51),
                EgoAttackCardId = new LorId(VortexModParameters.PackageId, 51)
            }, "ForgottenPhase_Sa21341");
            _util.Restart();
        }

        public override void OnRoundStart()
        {
            if (GetPhase() < 4) owner.bufListDetail.AddBuf(new BattleUnitBuf_CannotAct_Sa21341());
        }

        public override int SpeedDiceNumAdder()
        {
            return GetPhase() > 3 ? 4 : 0;
        }

        public int GetPhase()
        {
            return _util?.GetPhase() ?? 0;
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

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (_singleUse || _buff.stack < 25) return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
            _singleUse = true;
            origin = BattleDiceCardModel.CreatePlayingCard(
                ItemXmlDataList.instance.GetCardItem(new LorId(VortexModParameters.PackageId, 51)));
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnRoundStartAfter()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction).Where(x => x != owner))
            {
                if (!unit.bufListDetail.HasBuf<BattleUnitBuf_AllyRemembrance_Sa21341>())
                    unit.bufListDetail.AddBuf(new BattleUnitBuf_AllyRemembrance_Sa21341());
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1);
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1);
            }

            if (_util.GetPhase() <= 4) return;
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1);
        }

        public override void OnRoundEndTheLast()
        {
            if (_util == null) return;
            if (_util.GetPhase() > 3) _util.IncreaseCount();
            _util.CheckPhase();
        }

        public int GetCount()
        {
            return _util.GetCount();
        }

        public override void OnDie()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction))
                unit.Die();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.card.GetID() == new LorId(VortexModParameters.PackageId, 51))
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