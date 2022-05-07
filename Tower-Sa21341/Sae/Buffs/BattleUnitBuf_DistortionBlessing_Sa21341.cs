namespace VortexLabyrinth_Sa21341.Sae.Buffs
{
    public class BattleUnitBuf_DistortionBlessing_Sa21341 : BattleUnitBuf
    {
        public override bool CanRecoverHp(int amount)
        {
            if (_owner.hp + amount < _owner.MaxHp * 0.25f) return true;
            _owner.SetHp((int)(_owner.MaxHp * 0.25f));
            return false;
        }
    }
}