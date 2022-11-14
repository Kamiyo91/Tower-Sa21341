using System.Linq;
using VortexTower.Zero.Buffs;

namespace VortexTower.Zero.Cards
{
    public class DiceCardSelfAbility_BlueFlameEgo_Sa21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.bufListDetail.GetActivatedBufList()
                .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341)?.stack > 9;
        }

        public override void OnUseCard()
        {
            if (!(owner.bufListDetail.GetActivatedBufList()
                    .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341) is BattleUnitBuf_BlueFlame_Sa21341 buff))
                return;
            buff.AddStacks(-10);
        }
    }
}