using VortexLabyrinth_Sa21341.Sae.Buffs;
using VortexLabyrinth_Sa21341.UtilSa21341;

namespace VortexLabyrinth_Sa21341.Sae.Cards
{
    public class DiceCardSelfAbility_AtkStance_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        public static void Activate(BattleUnitModel unit)
        {
            StanceUtil.RemoveStanceBuffs(unit);
            StanceUtil.RemoveCards(unit);
            StanceUtil.ChangeStance(unit, typeof(BattleUnitBuf_AtkStance_Sa21341), "Atk", KeywordBuf.Endurance,
                KeywordBuf.Strength, 0);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return !owner.bufListDetail.HasBuf<BattleUnitBuf_AtkStance_Sa21341>();
        }
    }
}