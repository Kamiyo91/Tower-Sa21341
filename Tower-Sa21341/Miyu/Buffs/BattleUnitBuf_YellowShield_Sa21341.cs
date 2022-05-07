using Sound;

namespace VortexLabyrinth_Sa21341.Miyu.Buffs
{
    public class BattleUnitBuf_YellowShield_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_YellowShield_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        protected override string keywordId => "YellowShield_Sa21341";
        protected override string keywordIconId => "YellowShield_Sa21341";

        public override void OnRoundEnd()
        {
            Destroy();
        }

        public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
        {
            _owner.bufListDetail.RemoveBuf(this);
            _owner.cardSlotDetail.RecoverPlayPoint(1);
            _owner.allyCardDetail.DrawCards(1);
            if (_owner.battleCardResultLog == null) return;
            SingletonBehavior<DiceEffectManager>.Instance.CreateBehaviourEffect("YellowShield_Sa21341", 1f,
                _owner.view, _owner.view);
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
        }
    }
}