using System.Linq;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Dice
{
    public class DiceCardAbility_BlackFlame3_Sa21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            if (target?.bufListDetail.GetActivatedBufList()
                    .FirstOrDefault(x => x is BattleUnitBuf_BlackFlame_Sa21341) is BattleUnitBuf_BlackFlame_Sa21341
                buff)
                buff.AddStacks(3);
            else
            {
                var newBuff = new BattleUnitBuf_BlackFlame_Sa21341
                {
                    stack = 3
                };
                target?.bufListDetail.AddBuf(newBuff);
            }
        }
	}
}
