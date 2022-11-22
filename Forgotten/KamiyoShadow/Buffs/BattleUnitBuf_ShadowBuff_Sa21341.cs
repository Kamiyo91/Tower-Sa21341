using Sound;
using UnityEngine;

namespace VortexTower.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_ShadowBuff_Sa21341 : BattleUnitBuf
    {
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
            owner.view.charAppearance.ChangeMotion(ActionDetail.Default);
            var aura = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "2_Y/FX_IllusionCard_2_Y_Charge", 1f, _owner.view, _owner.view);
            foreach (var particle in aura.gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (particle.gameObject.name.Contains("Burn"))
                    particle.gameObject.AddComponent<AuraColor>();
                if (!particle.gameObject.name.Equals("Main") && !particle.gameObject.name.Contains("Charge") &&
                    !particle.gameObject.name.Contains("Scaner_holo_distortion")) continue;
                particle.gameObject.SetActive(false);
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