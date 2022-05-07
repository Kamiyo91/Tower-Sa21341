using Sound;

namespace VortexLabyrinth_Sa21341.Miyu.Buffs
{
    public class BattleUnitBuf_BlueShield_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_BlueShield_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "BlueShield_Sa21341";
        protected override string keywordIconId => "BlueShield_Sa21341";

        public override int GetDamageReduction(BattleDiceBehavior behavior)
        {
            _owner.bufListDetail.RemoveBuf(this);
            if (_owner.battleCardResultLog == null) return 9999;
            SingletonBehavior<DiceEffectManager>.Instance.CreateBehaviourEffect("BlueShield_Sa21341", 1f,
                _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
            return 9999;
        }

        public override void OnRoundEnd()
        {
            Destroy();
        }
    }
}