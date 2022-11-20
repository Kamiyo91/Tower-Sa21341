using System.Collections.Generic;
using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Enum;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using LOR_XML;

namespace VortexTower.Forgotten
{
    public class NpcMechUtil_Forgotten : NpcMechUtilBase
    {
        private BattleUnitModel _additionalUnit;
        private int _count;

        public NpcMechUtil_Forgotten(NpcMechUtilBaseModel model) : base(model)
        {
        }

        public void SetCount(int value)
        {
            _count = value;
        }

        public void IncreaseCount()
        {
            if (_count < 2) _count++;
            else _count = 0;
        }

        public int GetCount()
        {
            return _count;
        }

        public void CheckPhase()
        {
            if (Model.Phase > 3) return;
            if (Model.Phase >= 1 &&
                (_additionalUnit == null || _additionalUnit.hp >= _additionalUnit.MaxHp * 0.5f)) return;
            UnitUtil.BattleAbDialog(Model.Owner.view.dialogUI, new List<AbnormalityCardDialog>
            {
                new AbnormalityCardDialog
                {
                    id = "ForgottenPhase",
                    dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId].EffectTexts
                        .FirstOrDefault(x => x.Key.Contains($"ForgottenPhase{Model.Phase + 1}_Sa21341")).Value.Desc
                }
            }, AbColorType.Negative);
            UnitUtil.LevelUpEmotion(Model.Owner, 1);
            switch (Model.Phase)
            {
                case 0:
                case 1:
                case 2:
                    AddPhaseUnit(Model.Phase);
                    Model.Phase++;
                    break;
                case 3:
                    _count = 0;
                    AddPhaseUnits();
                    Model.Phase++;
                    break;
            }
        }

        private void AddPhaseUnit(int phase)
        {
            if (BattleObjectManager.instance.GetList(Model.Owner.faction).Exists(x => x == _additionalUnit))
                BattleObjectManager.instance.UnregisterUnit(_additionalUnit);
            switch (phase)
            {
                case 0:
                    _additionalUnit = UnitUtil.AddNewUnitWithDefaultData(
                        new UnitModel(7, VortexModParameters.PackageId, 7),
                        BattleObjectManager.instance.GetList(Model.Owner.faction).Count, unitSide: Model.Owner.faction,
                        emotionLevel: 1);
                    UnitUtil.RefreshCombatUI();
                    break;
                case 1:
                    _additionalUnit = UnitUtil.AddNewUnitWithDefaultData(
                        new UnitModel(8, VortexModParameters.PackageId, 8),
                        BattleObjectManager.instance.GetList(Model.Owner.faction).Count, unitSide: Model.Owner.faction,
                        emotionLevel: 2);
                    UnitUtil.RefreshCombatUI();
                    break;
                case 2:
                    _additionalUnit = UnitUtil.AddNewUnitWithDefaultData(
                        new UnitModel(9, VortexModParameters.PackageId, 9),
                        BattleObjectManager.instance.GetList(Model.Owner.faction).Count, unitSide: Model.Owner.faction,
                        emotionLevel: 3);
                    UnitUtil.RefreshCombatUI();
                    break;
            }
        }

        private void AddPhaseUnits()
        {
            if (BattleObjectManager.instance.GetList(Model.Owner.faction).Exists(x => x == _additionalUnit))
            {
                BattleObjectManager.instance.UnregisterUnit(_additionalUnit);
                _additionalUnit = null;
            }

            UnitUtil.AddNewUnitWithDefaultData(new UnitModel(7, VortexModParameters.PackageId, 7),
                BattleObjectManager.instance.GetList(Model.Owner.faction).Count, unitSide: Model.Owner.faction,
                emotionLevel: 5);
            UnitUtil.AddNewUnitWithDefaultData(new UnitModel(8, VortexModParameters.PackageId, 8),
                BattleObjectManager.instance.GetList(Model.Owner.faction).Count, unitSide: Model.Owner.faction,
                emotionLevel: 5);
            UnitUtil.AddNewUnitWithDefaultData(new UnitModel(9, VortexModParameters.PackageId, 9),
                BattleObjectManager.instance.GetList(Model.Owner.faction).Count, unitSide: Model.Owner.faction,
                emotionLevel: 5);
            UnitUtil.RefreshCombatUI();
        }

        public override void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData(Model.SaveDataId, Model.Phase);
            var list = BattleObjectManager.instance.GetAliveList(Model.Owner.faction).Where(x =>
                    x == Model.Owner || x.Book.BookId.packageId != VortexModParameters.PackageId)
                .Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public override void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>(Model.SaveDataId, out var curPhase);
            Model.Phase = curPhase;
            _count = 0;
            if (Model.Phase <= 0) return;
            if (Model.Phase < 3) AddPhaseUnit(curPhase);
            else AddPhaseUnits();
        }
    }
}