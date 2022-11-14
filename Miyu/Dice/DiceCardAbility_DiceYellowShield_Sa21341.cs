using VortexTower.Miyu.Buffs;

namespace VortexTower.Miyu.Dice
{
    public class DiceCardAbility_DiceYellowShield_Sa21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            target.breakDetail.RecoverBreak(behavior.DiceResultValue);
            target.RecoverHP(behavior.DiceResultValue);
            if (!target.bufListDetail.HasBuf<BattleUnitBuf_YellowShield_Sa21341>())
                target.bufListDetail.AddBuf(new BattleUnitBuf_YellowShield_Sa21341());
        }
    }
}