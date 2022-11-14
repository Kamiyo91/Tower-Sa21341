using System;
using UnityEngine;

namespace VortexTower.Sae.Actions
{
    public class BehaviourAction_SaeMassAttackLastAtk_Sa21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var farAreaeffectMio = new GameObject().AddComponent<FarAreaEffect_SaeMassAttack_Sa21341>();
            farAreaeffectMio.Init(self, Array.Empty<object>());
            farAreaeffectMio.SetLastAttack(true);
            return farAreaeffectMio;
        }
    }
}