using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.UtilSa21341.Extension.Forgotten
{
    public class NpcMechUtil_Forgotten : NpcMechUtil_Ex2
    {
        private readonly NpcMechUtilBaseModel _model;
        private readonly string _saveId;
        private BattleUnitModel _additionalUnit;
        private int _count;

        public NpcMechUtil_Forgotten(NpcMechUtilBaseModel model, string saveId) : base(model, saveId)
        {
            _model = model;
            _saveId = saveId;
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

        public override void CheckPhase()
        {
            if (_model.Phase > 3) return;
            if (_model.Phase >= 1 &&
                (_additionalUnit == null || _additionalUnit.hp >= _additionalUnit.MaxHp * 0.5f)) return;
            switch (_model.Phase)
            {
                case 0:
                case 1:
                case 2:
                    AddPhaseUnit(_model.Phase);
                    _model.Phase++;
                    break;
                case 3:
                    _count = 0;
                    AddPhaseUnits();
                    _model.Phase++;
                    break;
            }
        }

        private void AddPhaseUnit(int phase)
        {
            if (BattleObjectManager.instance.GetList(_model.Owner.faction).Exists(x => x == _additionalUnit))
            {
                if (!_additionalUnit.IsDead()) _additionalUnit.Die();
                BattleObjectManager.instance.UnregisterUnit(_additionalUnit);
            }

            switch (phase)
            {
                case 0:
                    _additionalUnit = UnitUtil.AddNewUnitEnemySide(new UnitModel
                    {
                        Id = 2,
                        Name = ModParameters.NameTexts
                            .FirstOrDefault(x => x.Key.Equals(new LorId(VortexModParameters.PackageId, 2))).Value,
                        Pos = BattleObjectManager.instance.GetList(_model.Owner.faction).Count,
                        EmotionLevel = 1
                    }, VortexModParameters.PackageId);
                    break;
                case 1:
                    _additionalUnit = UnitUtil.AddNewUnitEnemySide(new UnitModel
                    {
                        Id = 2,
                        Name = ModParameters.NameTexts
                            .FirstOrDefault(x => x.Key.Equals(new LorId(VortexModParameters.PackageId, 2))).Value,
                        Pos = BattleObjectManager.instance.GetList(_model.Owner.faction).Count,
                        EmotionLevel = 2
                    }, VortexModParameters.PackageId);
                    break;
                case 2:
                    _additionalUnit = UnitUtil.AddNewUnitEnemySide(new UnitModel
                    {
                        Id = 2,
                        Name = ModParameters.NameTexts
                            .FirstOrDefault(x => x.Key.Equals(new LorId(VortexModParameters.PackageId, 2))).Value,
                        Pos = BattleObjectManager.instance.GetList(_model.Owner.faction).Count,
                        EmotionLevel = 3
                    }, VortexModParameters.PackageId);
                    break;
            }
        }

        private void AddPhaseUnits()
        {
            if (BattleObjectManager.instance.GetList(_model.Owner.faction).Exists(x => x == _additionalUnit))
            {
                if (!_additionalUnit.IsDead()) _additionalUnit.Die();
                BattleObjectManager.instance.UnregisterUnit(_additionalUnit);
                _additionalUnit = null;
            }

            UnitUtil.AddNewUnitEnemySide(new UnitModel
            {
                Id = 2,
                Name = ModParameters.NameTexts
                    .FirstOrDefault(x => x.Key.Equals(new LorId(VortexModParameters.PackageId, 2))).Value,
                Pos = BattleObjectManager.instance.GetList(_model.Owner.faction).Count,
                EmotionLevel = 1
            }, VortexModParameters.PackageId);
            UnitUtil.AddNewUnitEnemySide(new UnitModel
            {
                Id = 2,
                Name = ModParameters.NameTexts
                    .FirstOrDefault(x => x.Key.Equals(new LorId(VortexModParameters.PackageId, 2))).Value,
                Pos = BattleObjectManager.instance.GetList(_model.Owner.faction).Count,
                EmotionLevel = 2
            }, VortexModParameters.PackageId);
            UnitUtil.AddNewUnitEnemySide(new UnitModel
            {
                Id = 2,
                Name = ModParameters.NameTexts
                    .FirstOrDefault(x => x.Key.Equals(new LorId(VortexModParameters.PackageId, 2))).Value,
                Pos = BattleObjectManager.instance.GetList(_model.Owner.faction).Count,
                EmotionLevel = 3
            }, VortexModParameters.PackageId);
        }

        public override void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData(_saveId, _model.Phase);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction).Where(x =>
                    x == _model.Owner || x.Book.BookId.packageId != VortexModParameters.PackageId)
                .Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public override void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>(_saveId, out var curPhase);
            _model.Phase = curPhase;
            _count = 0;
            if (_model.Phase <= 0) return;
            if (_model.Phase < 3) AddPhaseUnit(curPhase);
            else AddPhaseUnits();
        }
    }
}