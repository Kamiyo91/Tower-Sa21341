using Battle.CreatureEffect;
using Sound;
using UnityEngine;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_ShadowBuff_Sa21341 : BattleUnitBuf
    {
        private const string Path = "6/RedHood_Emotion_Aura";
        private CreatureEffect _aura;

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
                _aura = SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect(Path, 1f, owner.view,
                    owner.view);
            var original = Resources.Load("Prefabs/Battle/SpecialEffect/RedMistRelease_ActivateParticle");
            if (original != null)
            {
                var gameObject = Object.Instantiate(original) as GameObject;
                if (gameObject != null)
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.gray;
                    gameObject.transform.parent = owner.view.charAppearance.transform;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    gameObject.transform.localScale = Vector3.one;
                }
            }

            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Battle/Kali_Change");
        }

        public override int GetCardCostAdder(BattleDiceCardModel card)
        {
            return -99;
        }

        public override void OnRoundEnd()
        {
            DestoryAura();
            _owner.bufListDetail.RemoveBuf(this);
        }

        private void DestoryAura()
        {
            if (_aura == null) return;
            Object.Destroy(_aura.gameObject);
            _aura = null;
        }
    }
}