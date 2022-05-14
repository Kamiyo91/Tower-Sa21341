

namespace VortexLabyrinth_Sa21341.Forgotten.MioShadow.Cards
{
    public class DiceCardSelfAbility_SakuraBloom_Sa21341 : DiceCardSelfAbilityBase
    {
        private const int Check = 3;

        public override void OnUseCard()
        {
            var speedDiceResultValue = card.speedDiceResultValue;
            var target = card.target;
            var targetSlotOrder = card.targetSlotOrder;
            if (targetSlotOrder < 0 || targetSlotOrder >= target.speedDiceResult.Count) return;
            var speedDice = target.speedDiceResult[targetSlotOrder];
            var targetDiceBroken = target.speedDiceResult[targetSlotOrder].breaked;
            if (speedDiceResultValue - speedDice.value <= Check && !targetDiceBroken) return;
            owner.TakeDamage(9, DamageType.Card_Ability, owner);
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus
            {
                power = 1
            });
        }
    }
}