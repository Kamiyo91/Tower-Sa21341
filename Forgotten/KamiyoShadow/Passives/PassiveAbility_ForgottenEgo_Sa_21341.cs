using System.Collections.Generic;
using System.Linq;
using BigDLL4221.Models;
using LOR_DiceSystem;
using VortexTower.Forgotten.Buffs;
using VortexTower.Forgotten.KamiyoShadow.Buffs;

namespace VortexTower.Forgotten.KamiyoShadow.Passives
{
    public class PassiveAbility_ForgottenEgo_Sa_21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_Remembrance_Sa21341 _buff;
        private NpcMechUtil_Forgotten _util;
        public override bool isImmortal => GetPhase() <= 3;

        public override void OnWaveStart()
        {
            ChangeDiceEffects(owner);
            owner.ignoreBloodyEffect = true;
            _buff = new BattleUnitBuf_Remembrance_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
            owner.bufListDetail.AddBuf(new BattleUnitBuf_0CardCost_Sa21341());
            _util = new NpcMechUtil_Forgotten(new NpcMechUtilBaseModel("ForgottenPhase_Sa21341",
                massAttackStartCount: true, mechOptions: new Dictionary<int, MechPhaseOptions>
                {
                    {
                        4,
                        new MechPhaseOptions(egoMassAttackCardsOptions: new List<SpecialAttackCardOptions>
                            { new SpecialAttackCardOptions(new LorId(VortexModParameters.PackageId, 69)) })
                    }
                }, egoMaps: new Dictionary<LorId, MapModel>
                {
                    { new LorId(VortexModParameters.PackageId, 69), VortexModParameters.ForgottenMap5 }
                }));
            _util.Model.Owner = owner;
            _util.Model.ThisPassiveId = id;
            var tryMech = _util.Model.MechOptions.TryGetValue(4, out var mechOptions);
            if (tryMech) mechOptions.Counter = 4;
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
            return _util.Model.Phase;
        }

        public override void OnRoundEnd()
        {
            _util.ExhaustEgoAttackCards();
        }

        public override void OnBattleEnd()
        {
            _util.OnEndBattle();
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            _util.OnSelectCardPutMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_Remembrance_Sa21341>())
            {
                _buff = new BattleUnitBuf_Remembrance_Sa21341();
                owner.bufListDetail.AddBuf(_buff);
            }

            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction).Where(x => x != owner))
            {
                if (!unit.bufListDetail.HasBuf<BattleUnitBuf_AllyRemembrance_Sa21341>())
                    unit.bufListDetail.AddBuf(new BattleUnitBuf_AllyRemembrance_Sa21341());
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 2);
                unit.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 2);
            }

            if (_util.Model.Phase <= 4) return;
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 2);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 2);
        }

        public override void OnRoundEndTheLast()
        {
            if (_util == null) return;
            if (_util.Model.Phase > 3) _util.IncreaseCount();
            _util.CheckPhase();
        }

        public int GetCount()
        {
            return _util.GetCount();
        }

        public override void OnDie()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction))
                unit.DieFake();
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

        public static void ChangeDiceEffects(BattleUnitModel owner)
        {
            foreach (var card in owner.allyCardDetail.GetAllDeck())
            {
                card.CopySelf();
                foreach (var dice in card.GetBehaviourList())
                    ChangeCardDiceEffect(dice);
            }
        }

        private static void ChangeCardDiceEffect(DiceBehaviour dice)
        {
            switch (dice.EffectRes)
            {
                case "KamiyoHitForgotten_Sa21341":
                    dice.EffectRes = "KamiyoHitForgottenEnemy_Sa21341";
                    break;
                case "KamiyoSlashForgotten_Sa21341":
                    dice.EffectRes = "KamiyoSlashForgottenEnemy_Sa21341";
                    break;
                case "PierceKamiyoForgotten_Sa21341":
                    dice.EffectRes = "PierceKamiyoForgottenEnemy_Sa21341";
                    break;
            }
        }
    }
}