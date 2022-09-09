using Sound;
using UnityEngine;

namespace VortexLabyrinth_Sa21341.GreenHunter.Buffs
{
    public class BattleUnitBuf_GreenLeaf_Sa21341 : BattleUnitBuf
    {
        private GameObject _aura;
        protected override string keywordId => "GreenLeaf_Sa21341";
        protected override string keywordIconId => "GreenLeaf_Sa21341";

        public override int GetDamageReductionRate()
        {
            return stack;
        }

        public void AddStacks(int stacks)
        {
            stack += stacks;
            stack = Mathf.Clamp(stack, 0, 10);
            if (stack > 9 && _aura == null) CreateAura();
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            if (stack > 9) behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        private void CreateAura()
        {
            if (_aura != null) return;
            var @object = Resources.Load("Prefabs/Battle/SpecialEffect/IndexRelease_Aura");
            if (@object != null)
            {
                var gameObject = Object.Instantiate(@object) as GameObject;
                if (gameObject != null)
                {
                    gameObject.transform.parent = _owner.view.charAppearance.transform;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    gameObject.transform.localScale = Vector3.one;
                    var component = gameObject.GetComponent<IndexReleaseAura>();
                    if (component != null) component.Init(_owner.view);
                    _aura = gameObject;
                }

                if (_aura != null)
                    foreach (var particle in _aura.GetComponentsInChildren<ParticleSystem>())
                    {
                        var main = particle.main;
                        main.startColor = new Color(0, 1, 0, 1);
                    }
            }

            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Buf/Effect_Index_Unlock");
        }
    }
}