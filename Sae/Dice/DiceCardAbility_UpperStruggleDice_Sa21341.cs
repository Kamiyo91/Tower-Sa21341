using VortexTower.Sae.Buffs;

namespace VortexTower.Sae.Dice
{
    public class DiceCardAbility_UpperStruggleDice_Sa21341 : DiceCardAbilityBase
    {
        public override void OnWinParrying()
        {
            if (owner.hp > owner.MaxHp * 0.25f) return;
            owner.bufListDetail.AddKeywordBufByCard(
                owner.bufListDetail.HasBuf<BattleUnitBuf_AtkStance_Sa21341>() ||
                owner.bufListDetail.HasBuf<BattleUnitBuf_GeneralAtkStance_Sa21341>()
                    ? KeywordBuf.Strength
                    : KeywordBuf.Endurance, 1, owner);
        }
    }
}