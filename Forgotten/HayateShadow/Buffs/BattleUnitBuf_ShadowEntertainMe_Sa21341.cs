using UnityEngine;

namespace VortexTower.Forgotten.HayateShadow.Buffs
{
    public class BattleUnitBuf_ShadowEntertainMe_Sa21341 : BattleUnitBuf
    {
        protected override string keywordId => "ShadowEntertainMe_Sa21341";
        protected override string keywordIconId => "ShadowEntertainMe_Sa21341";

        public void AddStacks(int stacks)
        {
            stack += stacks;
            stack = Mathf.Clamp(stack, 0, 50);
        }
    }
}