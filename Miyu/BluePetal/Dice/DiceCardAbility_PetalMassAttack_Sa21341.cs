using System.Linq;
using VortexTower.Miyu.BluePetal.Buffs;

namespace VortexTower.Miyu.BluePetal.Dice
{
    public class DiceCardAbility_PetalMassAttack_Sa21341 : DiceCardAbilityBase
    {
        public override void OnSucceedAttack(BattleUnitModel target)
        {
            var buff = target.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_BluePetal_Sa21341);
            if (buff != null)
            {
                buff.OnAddBuf(1);
            }
            else
            {
                var addedBuff = new BattleUnitBuf_BluePetal_Sa21341
                {
                    stack = 3
                };
                target.bufListDetail.AddBuf(addedBuff);
            }
        }
    }
}