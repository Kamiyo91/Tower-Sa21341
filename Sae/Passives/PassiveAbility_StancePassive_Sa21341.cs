using System.Collections.Generic;
using System.Linq;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using CustomMapUtility;
using VortexTower.Sae.Buffs;
using VortexTower.Sae.Cards;
using VortexTower.Sae.Dice;

namespace VortexTower.Sae.Passives
{
    public class PassiveAbility_StancePassive_Sa21341 : PassiveAbilityBase
    {
        private bool _counterReload;
        private bool _enemyKillCheck;
        private bool _mapUsed;
        private bool _start;

        public override int GetSpeedDiceAdder(int speedDiceResult)
        {
            if (owner.bufListDetail.HasBuf<BattleUnitBuf_DefStance_Sa21341>())
                return -100;
            return base.GetSpeedDiceAdder(speedDiceResult);
        }

        public override void OnRoundStartAfter()
        {
            if (_enemyKillCheck && BattleObjectManager.instance
                    .GetAliveList(UnitUtil.ReturnOtherSideFaction(owner.faction)).Any()) UnitUtil.VipDeath(owner);
            if (owner.faction == Faction.Player) return;
            if (_start)
            {
                _start = false;
                return;
            }

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
            InitDialog();
            _start = true;
            _enemyKillCheck = false;
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 9));
            if (StageController.Instance.GetStageModel().ClassInfo.id != new LorId(VortexModParameters.PackageId, 1))
                owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 14));
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
            if (curCard.card.GetID() == new LorId(VortexModParameters.PackageId, 14))
            {
                owner.personalEgoDetail.RemoveCard(curCard.card.GetID());
                if (StageController.Instance.GetStageModel().ClassInfo.id ==
                    new LorId(VortexModParameters.PackageId, 1)) _enemyKillCheck = true;
            }

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
            MapUtil.ChangeMap(CustomMapHandler.GetCMU(VortexModParameters.PackageId), new MapModel
            {
                Stage = "SaePhase2_Sa21341",
                OriginalMapStageIds = new List<LorId>
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
            MapUtil.ReturnFromEgoMap(CustomMapHandler.GetCMU(VortexModParameters.PackageId), "SaePhase2_Sa21341",
                new List<LorId>
                    { new LorId(VortexModParameters.PackageId, 1), new LorId(VortexModParameters.PackageId, 2) });
        }

        private void InitDialog()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId !=
                VortexModParameters.PackageId) return;
            switch (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id)
            {
                case 1:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000901));
                    break;
                case 3:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000003));
                    break;
                case 5:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000010));
                    break;
                case 7:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000012));
                    break;
            }
        }
    }
}