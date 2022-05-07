using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.UtilSa21341.Extension.BluePetal
{
    public class NpcMechUtil_BluePetal : NpcMechUtilEx
    {
        private readonly NpcMechUtilBaseModel _model;

        public NpcMechUtil_BluePetal(NpcMechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public void CheckPhase()
        {
            if (_model.Owner.hp > 271 && BattleObjectManager.instance.GetAliveList(_model.Owner.faction).Count > 1 ||
                _model.Phase > 0) return;
            _model.Phase++;
            _model.HasMechOnHp = false;
            SetMassAttack(true);
            SetCounter(3);
            foreach (var unit in BattleObjectManager.instance.GetAliveList(_model.Owner.faction)
                         .Where(x => x.Book.BookId == new LorId(VortexModParameters.PackageId, 2)))
                unit.Die();
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 99);
        }

        public void OnEndBattle()
        {
            var stageModel = Singleton<StageController>.Instance.GetStageModel();
            var currentWaveModel = Singleton<StageController>.Instance.GetCurrentWaveModel();
            if (currentWaveModel == null || currentWaveModel.IsUnavailable()) return;
            stageModel.SetStageStorgeData("PhaseBlueGuardianSa21341", _model.Phase);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction)
                .Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>("PhaseBlueGuardianSa21341", out var curPhase);
            _model.Phase = curPhase;
            if (_model.Phase < 1) return;
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 99);
            _model.HasMechOnHp = false;
            SetMassAttack(true);
            SetCounter(3);
        }

        public int GetPhase()
        {
            return _model.Phase;
        }
    }
}