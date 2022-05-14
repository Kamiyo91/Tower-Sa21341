using UnityEngine;

namespace VortexLabyrinth_Sa21341.Forgotten.HayateShadow.Buffs
{
    public class BattleUnitBuf_ShadowEntertainMe_Sa21341 : BattleUnitBuf
    {
        public void AddStacks(int stacks)
        {
            stack += stacks;
            stack = Mathf.Clamp(stack, 0, 50);
        }
    }
}