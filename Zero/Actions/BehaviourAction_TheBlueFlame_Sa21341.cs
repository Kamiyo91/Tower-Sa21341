﻿using System;
using UnityEngine;
using VortexTower.Zero.Effects;

namespace VortexTower.Zero.Actions
{
    public class BehaviourAction_TheBlueFlame_Sa21341 : BehaviourActionBase
    {
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            _self = self;
            var effect = new GameObject().AddComponent<FarAreaEffect_TheBlueFlame_Sa21341>();
            effect.Init(self, Array.Empty<object>());
            return effect;
        }
    }
}