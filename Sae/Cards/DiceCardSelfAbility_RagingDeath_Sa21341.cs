using VortexTower.Sae.Buffs;

namespace VortexTower.Sae.Cards
{
    public class DiceCardSelfAbility_RagingDeath_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnEndBattle()
        {
            if (card.target != null && card.target.IsDead())
                owner.bufListDetail.AddBuf(new BattleUnitBuf_RagingDeath_Sa21341());
        }
    }
}