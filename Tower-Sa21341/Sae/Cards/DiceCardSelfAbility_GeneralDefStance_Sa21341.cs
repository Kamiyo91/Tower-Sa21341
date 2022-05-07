using VortexLabyrinth_Sa21341.Sae.Buffs;
using VortexLabyrinth_Sa21341.Sae.Passives;
using VortexLabyrinth_Sa21341.UtilSa21341;

namespace VortexLabyrinth_Sa21341.Sae.Cards
{
    public class DiceCardSelfAbility_GeneralDefStance_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        public static void Activate(BattleUnitModel unit)
        {
            StanceUtil.RemoveGeneralStanceBuffs(unit);
            StanceUtil.RemoveGeneralCards(unit);
            var addBuff = KeywordBuf.None;
            var removeBuff = KeywordBuf.None;
            if (unit.passiveDetail.HasPassive<PassiveAbility_LoneWarrior_Sa21341>())
            {
                addBuff = KeywordBuf.Endurance;
                removeBuff = KeywordBuf.Strength;
            }

            StanceUtil.ChangeStance(unit, typeof(BattleUnitBuf_GeneralDefStance_Sa21341), "Def", removeBuff,
                addBuff, 1);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return !owner.bufListDetail.HasBuf<BattleUnitBuf_GeneralDefStance_Sa21341>();
        }
    }
}