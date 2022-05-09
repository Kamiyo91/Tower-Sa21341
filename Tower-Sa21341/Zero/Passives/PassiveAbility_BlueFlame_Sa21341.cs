using System.Linq;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Zero.Buffs;

namespace VortexLabyrinth_Sa21341.Zero.Passives
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
            if (_buff.stack > 9) UnitUtil.ReadyCounterCard(owner, 1, VortexModParameters.PackageId);
        }
    }
}