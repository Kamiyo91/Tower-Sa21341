using Battle.CreatureEffect;
using Sound;

namespace VortexTower.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_ShadowBuff_Sa21341 : BattleUnitBuf
    {
        private const string Path = "6/RedHood_Emotion_Aura";
        private CreatureEffect _aura;

        public BattleUnitBuf_ShadowBuff_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        public override bool isAssimilation => true;
        protected override string keywordId => "ForgottenAura_Sa21341";
        protected override string keywordIconId => "Light_Sa21341";

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            owner.cardSlotDetail.RecoverPlayPoint(owner.MaxPlayPoint);
            PlayChangingEffect(owner);
        }

        private void PlayChangingEffect(BattleUnitModel owner)
        {
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            if (_aura == null)
            {
                _aura = SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect(Path, 1f, owner.view,
                    owner.view);
                _aura.gameObject.AddComponent<AuraColor>();
            }

            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }

        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return -1;
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }
    }
}