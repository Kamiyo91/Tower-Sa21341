﻿using UnityEngine;

namespace VortexTower.Forgotten.KamiyoShadow.Buffs
{
    public class BattleUnitBuf_BlackFlame_Sa21341 : BattleUnitBuf
    {
        protected override string keywordId => "BlackFlame_Sa21341";
        protected override string keywordIconId => "BlackFlame_Sa21341";

        public override void OnRoundStartAfter()
        {
            _owner.TakeDamage(stack * _owner.MaxHp / 100);
            EffectUtil.BurnEffect(_owner);
            AddStacks(-1);
            if (stack == 0) _owner.bufListDetail.RemoveBuf(this);
        }

        public void AddStacks(int stacks)
        {
            stack += stacks;
            stack = Mathf.Clamp(stack, 0, 25);
        }
    }
}