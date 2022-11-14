using System.Linq;
using BigDLL4221.Utils;
using VortexTower.Zero.Buffs;

namespace VortexTower.Zero.Passives
{
    public class PassiveAbility_BlueFlame_Sa21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_BlueFlame_Sa21341 _buff;

        public override void OnWaveStart()
        {
            _buff =
                owner.bufListDetail.GetActivatedBufList().FirstOrDefault(x => x is BattleUnitBuf_BlueFlame_Sa21341) as
                    BattleUnitBuf_BlueFlame_Sa21341;
        }

        public override void OnStartBattle()
        {
            if (_buff.stack > 9) UnitUtil.ReadyCounterCard(owner, 61, VortexModParameters.PackageId);
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }
    }
}