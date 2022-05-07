using System.Linq;
using VortexLabyrinth_Sa21341.BluePetal.Buffs;

namespace VortexLabyrinth_Sa21341.BluePetal.Dices
{
    public class DiceCardAbility_BluePetal_Sa21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            var buff = target.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_BluePetal_Sa21341);
            if (buff != null) buff.stack++;
            else
                target.bufListDetail.AddBuf(new BattleUnitBuf_BluePetal_Sa21341());
        }
    }
}