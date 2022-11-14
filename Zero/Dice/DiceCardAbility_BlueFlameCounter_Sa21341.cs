using System.Linq;
using VortexTower.Zero.Buffs;

namespace VortexTower.Zero.Dice
{
    public class DiceCardAbility_BlueFlameCounter_Sa21341 : DiceCardAbilityBase
    {
        private BattleUnitBuf_BlueFlame_Sa21341 _buff;

        public override void BeforeRollDice()
        {
            if (_buff == null)
                _buff = owner.bufListDetail.GetActivatedBufList()
                    .FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341) as BattleUnitBuf_BlueFlame_Sa21341;
            if (_buff != null && _buff.stack > 19)
                behavior.ApplyDiceStatBonus(new DiceStatBonus
                {
                    power = 1
                });
        }
    }
}