using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.UtilSa21341.Extension
{
    public class NpcMechUtil_Ex2 : NpcMechUtilEx
    {
        private readonly NpcMechUtilBaseModel _model;
        private readonly string _saveId;

        public NpcMechUtil_Ex2(NpcMechUtilBaseModel model, string saveId) : base(model)
        {
            _model = model;
            _saveId = saveId;
        }

        public void CheckPhase()
        {
            if (_model.Owner.hp > _model.MechHp || _model.Phase > 0) return;
            _model.Phase++;
            _model.HasMechOnHp = false;
            if (_model.HasEgoAttack)
            {
                SetMassAttack(true);
                SetCounter(_model.MaxCounter);
            }

            foreach (var unit in BattleObjectManager.instance.GetAliveList(_model.Owner.faction)
                         .Where(x => x.Book.BookId == new LorId(VortexModParameters.PackageId, 2)))
                unit.Die();
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 99);
        }

        public void CheckPhaseSingle()
        {
            if (_model.Owner.hp > _model.MechHp &&
                BattleObjectManager.instance.GetAliveList(_model.Owner.faction).Count > 1 ||
                _model.Phase > 0) return;
            _model.Phase++;
            _model.HasMechOnHp = false;
            if (_model.HasEgoAttack)
            {
                SetMassAttack(true);
                SetCounter(_model.MaxCounter);
            }

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
            stageModel.SetStageStorgeData(_saveId, _model.Phase);
            var list = BattleObjectManager.instance.GetAliveList(_model.Owner.faction)
                .Select(unit => unit.UnitData)
                .ToList();
            currentWaveModel.ResetUnitBattleDataList(list);
        }

        public void Restart()
        {
            Singleton<StageController>.Instance.GetStageModel()
                .GetStageStorageData<int>(_saveId, out var curPhase);
            _model.Phase = curPhase;
            if (_model.Phase < 1) return;
            UnitUtil.ChangeCardCostByValue(_model.Owner, -2, 99);
            _model.HasMechOnHp = false;
            if (!_model.HasEgoAttack) return;
            SetMassAttack(true);
            SetCounter(_model.MaxCounter);
        }

        public int GetPhase()
        {
            return _model.Phase;
        }
    }
}