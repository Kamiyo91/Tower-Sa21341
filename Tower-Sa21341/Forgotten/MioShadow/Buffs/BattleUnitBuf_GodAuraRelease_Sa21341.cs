using Sound;
using UnityEngine;

namespace VortexLabyrinth_Sa21341.Forgotten.MioShadow.Buffs
{
    public class BattleUnitBuf_GodAuraRelease_Sa21341 : BattleUnitBuf
    {
        public BattleUnitBuf_GodAuraRelease_Sa21341()
        {
            stack = 0;
        }

        public override bool isAssimilation => true;
        public override int paramInBufDesc => 0;
        protected override string keywordId => "GodAura_Sa21341";
        protected override string keywordIconId => "Light_Sa21341";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(
                new DiceStatBonus
                {
                    power = 1
                });
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            InitAuraAndPlaySound();
        }

        private void InitAuraAndPlaySound()
        {
            var component = SingletonBehavior<DiceEffectManager>.Instance.CreateNewFXCreatureEffect(
                "5_T/FX_IllusionCard_5_T_Happiness", 1f, _owner.view, _owner.view);
            var gameObject = component.gameObject;
            var particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach (var particleSystem in particleSystems)
            {
                if (particleSystem.gameObject.name.Equals("Force"))
                {
                    particleSystem.gameObject.SetActive(false);
                    continue;
                }
                var main = particleSystem.main;
                main.startColor = new Color(0, 0, 0, 1);
            }
            SoundEffectPlayer.PlaySound("Creature/Greed_MakeDiamond");
        }

        public override void OnRoundEnd()
        {
            RecoverHpAndStagger();
        }

        private void RecoverHpAndStagger()
        {
            _owner.RecoverHP(3);
            _owner.breakDetail.RecoverBreak(3);
        }
    }
}
