﻿using System.IO;
using Battle.DiceAttackEffect;
using UnityEngine;

namespace VortexTower.Miyu.DiceEffects
{
    public class DiceAttackEffect_YellowShield_Sa21341 : DiceAttackEffect
    {
        private float _duration;

        public override void Initialize(BattleUnitView self, BattleUnitView target, float destroyTime)
        {
            base.Initialize(self, target, destroyTime);
            _self = self.model;
            _selfTransform = self.atkEffectRoot;
            _targetTransform = self.atkEffectRoot;
            transform.parent = self.charAppearance.transform;
            _duration = destroyTime;
            var texture2D = new Texture2D(1, 1);
            texture2D.LoadImage(File.ReadAllBytes(VortexModParameters.Path + "/CustomEffect/" +
                                                  GetType().Name.Replace("DiceAttackEffect_", "") + ".png"));
            spr.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height),
                new Vector2(0.50f, 0.20f));
            gameObject.layer = LayerMask.NameToLayer("Effect");
            ResetLocalTransform(transform);
        }


        protected override void Update()
        {
            base.Update();
            _duration -= Time.deltaTime;
            spr.color = new Color(1f, 1f, 1f, _duration * 2f);
        }


        public override void SetScale(float scaleFactor)
        {
            scaleFactor *= 0.5f;
            base.SetScale(scaleFactor);
        }
    }
}