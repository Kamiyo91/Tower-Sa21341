using System.Linq;
using BigDLL4221.Utils;
using Sound;

namespace VortexTower.Zero.Buffs
{
    public class BattleUnitBuf_BlueFlameEgo_Sa21341 : BattleUnitBuf
    {
        private BattleUnitBuf_BlueFlame_Sa21341 _buff;

        public BattleUnitBuf_BlueFlameEgo_Sa21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "BlueFlameEgo_Sa21341";
        protected override string keywordIconId => "BlueFlameEgo_Sa21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            InitAuraAndPlaySound();
            _buff =
                owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341) as
                    BattleUnitBuf_BlueFlame_Sa21341;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus
            {
                power = 1
            });
        }

        private void InitAuraAndPlaySound()
        {
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
            ArtUtil.MakeEffect(_owner, "6/BigBadWolf_Emotion_Aura", 1f, _owner);
        }

        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return -1;
        }

        public override int GetDamageReductionRate()
        {
            return _buff.stack;
        }
    }
}