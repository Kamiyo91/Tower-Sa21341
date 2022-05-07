namespace VortexLabyrinth_Sa21341.Sae.Buffs
{
    public class BattleUnitBuf_SpecialPowerNull_Sa21341 : BattleUnitBuf
    {
        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            _owner.currentDiceAction.ignorePower = true;
            base.BeforeRollDice(behavior);
        }
    }
}