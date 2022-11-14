using Sound;
using UnityEngine;
using VortexTower.Miyu.FarAreaEffects;

namespace VortexTower.Miyu.Actions
{
    public class BehaviourAction_MassHeal_Sa21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var effect = new GameObject().AddComponent<FarAreaEffect_MassHeal_Sa21341>();
            effect.Init(self, new[]
            {
                2
            });
            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Creature/WhiteNight_OneBad_AfterKill");
            return effect;
        }
    }
}