using VortexLabyrinth_Sa21341.Sae.Buffs;
using VortexLabyrinth_Sa21341.Sae.Passives;
using VortexLabyrinth_Sa21341.UtilSa21341;

namespace VortexLabyrinth_Sa21341.Sae.Cards
{
    public class DiceCardSelfAbility_GeneralAtkStance_Sa21341 : DiceCardSelfAbilityBase
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
                addBuff = KeywordBuf.Strength;
                removeBuff = KeywordBuf.Endurance;
            }

            StanceUtil.ChangeStance(unit, typeof(BattleUnitBuf_GeneralAtkStance_Sa21341), "Atk", removeBuff,
                addBuff, 0);
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }

        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return !owner.bufListDetail.HasBuf<BattleUnitBuf_GeneralAtkStance_Sa21341>();
        }
    }
}