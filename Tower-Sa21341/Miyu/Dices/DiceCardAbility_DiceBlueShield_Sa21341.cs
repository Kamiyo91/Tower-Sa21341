using VortexLabyrinth_Sa21341.Miyu.Buffs;

namespace VortexLabyrinth_Sa21341.Miyu.Dices
{
    public class DiceCardAbility_DiceBlueShield_Sa21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            target.breakDetail.RecoverBreak(behavior.DiceResultValue);
            target.RecoverHP(behavior.DiceResultValue);
            if(!target.bufListDetail.HasBuf<BattleUnitBuf_BlueShield_Sa21341>()) target.bufListDetail.AddBuf(new BattleUnitBuf_BlueShield_Sa21341());
        }
    }
}