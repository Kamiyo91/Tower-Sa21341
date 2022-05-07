using System.Collections.Generic;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Maps;
using VortexLabyrinth_Sa21341.Sae.Buffs;
using VortexLabyrinth_Sa21341.Sae.Cards;
using VortexLabyrinth_Sa21341.Sae.Dices;
using VortexLabyrinth_Sa21341.UtilSa21341;

namespace VortexLabyrinth_Sa21341.Sae.Passives
{
    public class PassiveAbility_StancePassive_Sa21341 : PassiveAbilityBase
    {
        private bool _counterReload;
        private bool _mapUsed;

        public override int GetSpeedDiceAdder(int speedDiceResult)
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_DefStance_Sa21341>())
                return -100;
            return base.GetSpeedDiceAdder(speedDiceResult);
        }

        public override void OnRoundStartAfter()
        {
            if (owner.faction == Faction.Player) return;
            var randomStance = RandomUtil.Range(0, 1);
            switch (randomStance)
            {
                case 0:
                    DiceCardSelfAbility_AtkStance_Sa21341.Activate(owner);
                    break;
                case 1:
                    DiceCardSelfAbility_DefStance_Sa21341.Activate(owner);
                    break;
            }
        }

        public override void OnWaveStart()
        {
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 9));
            UnitUtil.CheckSkinProjection(owner);
            DiceCardSelfAbility_AtkStance_Sa21341.Activate(owner);
        }

        public override void OnStartBattle()
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_DefStance_Sa21341>())
                UnitUtil.ReadyCounterCard(owner, 8, VortexModParameters.PackageId);
            else if (owner.bufListDetail.HasBuf<BattleUnitBuf_AtkStance_Sa21341>())
                UnitUtil.ReadyCounterCard(owner, 7, VortexModParameters.PackageId);
        }

        public override void OnRoundStart()
        {
            StanceUtil.AddCards(owner);
        }

        public override void OnLoseParrying(BattleDiceBehavior behavior)
        {
            if (!_counterReload)
                _counterReload = behavior.abilityList.Exists(x => x is DiceCardAbility_SaeBlock_Sa21341);
        }

        public override void OnDrawParrying(BattleDiceBehavior behavior)
        {
            if (!_counterReload)
                _counterReload = behavior.abilityList.Exists(x => x is DiceCardAbility_SaeBlock_Sa21341);
        }

        public override void OnWinParrying(BattleDiceBehavior behavior)
        {
            if (!_counterReload)
                _counterReload = behavior.abilityList.Exists(x => x is DiceCardAbility_SaeBlock_Sa21341);
        }

        public override void OnEndBattle(BattlePlayingCardDataInUnitModel curCard)
        {
            if (!_counterReload || owner.hp > owner.MaxHp * 0.25f) return;
            _counterReload = false;
            UnitUtil.SetPassiveCombatLog(this, owner);
            UnitUtil.ReadyCounterCard(owner, 8, VortexModParameters.PackageId);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            ReturnFromEgoMap();
        }

        private void ChangeToEgoMap(LorId cardId)
        {
            if (cardId != new LorId(VortexModParameters.PackageId, 9) ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            _mapUsed = true;
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "Sae_Sa21341",
                StageIds = new List<LorId>
                    { new LorId(VortexModParameters.PackageId, 1), new LorId(VortexModParameters.PackageId, 2) },
                OneTurnEgo = true,
                IsPlayer = true,
                Component = typeof(Sae_Sa21341MapManager),
                Bgy = 0.25f,
                Fy = 0.8f
            });
        }

        private void ReturnFromEgoMap()
        {
            if (!_mapUsed) return;
            _mapUsed = false;
            MapUtil.ReturnFromEgoMap("Sae_Sa21341",
                new List<LorId>
                    { new LorId(VortexModParameters.PackageId, 1), new LorId(VortexModParameters.PackageId, 2) });
        }
    }
}