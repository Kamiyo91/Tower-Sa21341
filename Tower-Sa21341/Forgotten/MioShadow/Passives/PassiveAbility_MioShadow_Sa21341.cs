﻿using System.Linq;
using VortexLabyrinth_Sa21341.Forgotten.Buffs;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Passives;

namespace VortexLabyrinth_Sa21341.Forgotten.MioShadow.Passives
{
    public class PassiveAbility_MioShadow_Sa21341 : PassiveAbilityBase
    {
        private const int Check = 1;
        private PassiveAbility_ForgottenEgo_Sa_21341 _passive;

        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_StartPoint_Sa21341());
            _passive = BattleObjectManager.instance.GetAliveList()
                    .FirstOrDefault(x => x.passiveDetail.HasPassive<PassiveAbility_ForgottenEgo_Sa_21341>())?
                    .passiveDetail.PassiveList
                    .FirstOrDefault(x => x is PassiveAbility_ForgottenEgo_Sa_21341) as
                PassiveAbility_ForgottenEgo_Sa_21341;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            var speedDiceResultValue = behavior.card.speedDiceResultValue;
            var target = behavior.card.target;
            var targetSlotOrder = behavior.card.targetSlotOrder;
            if (targetSlotOrder < 0 || targetSlotOrder >= target.speedDiceResult.Count) return;
            var speedDice = target.speedDiceResult[targetSlotOrder];
            var targetDiceBroken = target.speedDiceResult[targetSlotOrder].breaked;
            if (speedDiceResultValue - speedDice.value <= Check && !targetDiceBroken) return;
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        public override int SpeedDiceNumAdder()
        {
            return 4;
        }

        public override int ChangeTargetSlot(BattleDiceCardModel card, BattleUnitModel target, int currentSlot,
            int targetSlot, bool teamkill)
        {
            return AlwaysAimToTheSlowestDice(target);
        }

        private static int AlwaysAimToTheSlowestDice(BattleUnitModel target)
        {
            var speedValue = 999;
            var finalTarget = 0;
            foreach (var dice in target.speedDiceResult.Select((x, i) => new { i, x }))
            {
                if (speedValue <= dice.x.value) continue;
                speedValue = dice.x.value;
                finalTarget = dice.i;
            }

            return finalTarget;
        }

        public override void OnRoundStart()
        {
            if (_passive.GetPhase() < 4) return;
            if (_passive.GetCount() == 1) return;
            owner.bufListDetail.AddBuf(new BattleUnitBuf_CannotAct_Sa21341());
        }
    }
}