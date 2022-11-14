using System.Linq;
using VortexTower.Miyu.BluePetal.Buffs;

namespace VortexTower.Miyu.BluePetal.Dice
{
    public class DiceCardAbility_BluePetal_Sa21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            var buff = target.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_BluePetal_Sa21341);
            if (buff != null) buff.OnAddBuf(1);
            else
                target.bufListDetail.AddBuf(new BattleUnitBuf_BluePetal_Sa21341());
        }
    }
}