using Sound;

namespace VortexLabyrinth_Sa21341.Miyu.Buffs
{
    public class BattleUnitBuf_RedShield_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_RedShield_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "RedShield_Sa21341";
        protected override string keywordIconId => "RedShield_Sa21341";

        public override void OnRoundEnd()
        {
            Destroy();
        }

        public override void BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            attacker.TakeDamage(dmg);
            if (_owner.battleCardResultLog == null) return;
            SingletonBehavior<DiceEffectManager>.Instance.CreateBehaviourEffect("RedShield_Sa21341", 1f,
                _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
        }
    }
}