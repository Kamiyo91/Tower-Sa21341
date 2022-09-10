using System.Linq;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Miyu.Passives;

namespace VortexLabyrinth_Sa21341.Miyu.Cards
{
    public class DiceCardSelfAbility_SummonMiyu_Sa21341 : DiceCardSelfAbilityBase
    {
        public override void OnUseInstance(BattleUnitModel unit, BattleDiceCardModel self, BattleUnitModel targetUnit)
        {
            Activate(unit);
            self.exhaust = true;
        }

        private static void Activate(BattleUnitModel unit)
        {
            var summonedUnit = SummonSpecialUnit(Singleton<StageController>.Instance.GetCurrentStageFloorModel(), 10000005,
                new LorId(VortexModParameters.PackageId, 5), unit.emotionDetail.EmotionLevel);
            UnitUtil.RefreshCombatUI();
            if (!(unit.passiveDetail.PassiveList.FirstOrDefault(x => x is PassiveAbility_SummonMiyu_Sa21341) is
                    PassiveAbility_SummonMiyu_Sa21341 passive)) return;
            passive.Unit = summonedUnit;
        }

        public override bool IsTargetableSelf()
        {
            return true;
        }

        public static BattleUnitModel SummonSpecialUnit(StageLibraryFloorModel floor, int unitId, LorId unitNameId,
            int emotionLevel)
        {
            return UnitUtil.AddNewUnitPlayerSideCustomDataOnPlay(floor, new UnitModel
            {
                Id = unitId,
                Name = ModParameters.NameTexts
                    .FirstOrDefault(x => x.Key.Equals(unitNameId)).Value,
                EmotionLevel = emotionLevel,
                Pos = BattleObjectManager.instance.GetList(Faction.Player).Count,
                Sephirah = floor.Sephirah
            }, VortexModParameters.PackageId,false);
        }
    }
}
