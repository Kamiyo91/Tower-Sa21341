using Battle.CreatureEffect;
using Sound;
using UnityEngine;

namespace VortexTower.Sae.Buffs
{
    public class BattleUnitBuf_RedAura_Sa21341 : BattleUnitBuf
    {
        private const string Path = "6/RedHood_Emotion_Aura";
        private CreatureEffect _aura;

        public BattleUnitBuf_RedAura_Sa21341()
        {
            stack = 0;
        }

        public override int paramInBufDesc => 0;
        public override bool isAssimilation => true;
        protected override string keywordId => "RedAura_Sa21341";
        protected override string keywordIconId => "RedHood_Rage";

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (_owner.hp > _owner.MaxHp * 0.25f)
                behavior.ApplyDiceStatBonus(
                    new DiceStatBonus
                    {
                        power = 1
                    });
        }

        public override void Init(BattleUnitModel owner)
        {
            base.Init(owner);
            PlayChangingEffect(owner);
        }

        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            if (_owner.hp <= _owner.MaxHp * 0.25f) return -1;
            return base.GetCardCostAdder(card);
        }

        private void PlayChangingEffect(BattleUnitModel owner)
        {
            if (_aura == null)
                _aura = SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect(Path, 1f, owner.view,
                    owner.view);
            var original = Resources.Load("Prefabs/Battle/SpecialEffect/RedMistRelease_ActivateParticle");
            if (original != null)
            {
                var gameObject = Object.Instantiate(original) as GameObject;
                gameObject.transform.parent = owner.view.charAppearance.transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
                gameObject.transform.localScale = Vector3.one;
            }

            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }
    }
}