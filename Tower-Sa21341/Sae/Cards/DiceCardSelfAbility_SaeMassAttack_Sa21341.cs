using VortexLabyrinth_Sa21341.Sae.Buffs;

namespace VortexLabyrinth_Sa21341.Sae.Cards
{
    public class DiceCardSelfAbility_SaeMassAttack_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.emotionDetail.EmotionLevel > 2;
        }

        public override void OnUseCard()
        {
            if (owner.hp > owner.MaxHp * 0.25f) return;
            card.ignorePower = true;
            card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus { min = 1, max = 2 });
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction == Faction.Player
                         ? Faction.Enemy
                         : Faction.Player))
                unit.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_SpecialPowerNull_Sa21341());
        }

        public override void OnEndAreaAttack()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction == Faction.Player
                         ? Faction.Enemy
                         : Faction.Player))
                unit.bufListDetail.RemoveBufAll(typeof(BattleUnitBuf_SpecialPowerNull_Sa21341));
        }
    }
}