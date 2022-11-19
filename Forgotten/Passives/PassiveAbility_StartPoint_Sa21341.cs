using BigDLL4221.Models;
using BigDLL4221.Utils;
using VortexTower.Forgotten.Buffs;

namespace VortexTower.Forgotten.Passives
{
    public class PassiveAbility_StartPoint_Sa21341 : PassiveAbilityBase
    {
        public override bool isImmortal => true;

        public override void OnRoundEndTheLast()
        {
            BattleObjectManager.instance.UnregisterUnit(owner);
            UnitUtil.AddNewUnitWithDefaultData(new UnitModel(10, VortexModParameters.PackageId, 10), 0,
                unitSide: owner.faction, emotionLevel: 1);
            UnitUtil.RefreshCombatUI();
        }

        public override void OnWaveStart()
        {
            owner.bufListDetail.AddBuf(new BattleUnitBuf_CannotAct_Sa21341());
            owner.bufListDetail.AddBuf(new BattleUnitBuf_StartPoint_Sa21341());
        }
    }
}