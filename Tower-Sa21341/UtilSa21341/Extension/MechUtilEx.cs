using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.BaseClass;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;

namespace VortexLabyrinth_Sa21341.UtilSa21341.Extension
{
    public class MechUtilEx : MechUtilBase
    {
        private readonly MechUtilBaseModel _model;
        private bool _specialUsed;

        public MechUtilEx(MechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public virtual void ChangeToEgoMap(LorId cardId)
        {
            if (cardId != _model.EgoAttackCardId ||
                SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject.isEgo) return;
            _model.MapUsed = true;
            MapUtil.ChangeMap(new MapModel
            {
                Stage = _model.EgoMapName,
                StageIds = _model.OriginalMapStageIds,
                OneTurnEgo = true,
                IsPlayer = true,
                Component = _model.EgoMapType,
                Bgy = _model.BgY ?? 0.5f,
                Fy = _model.FlY ?? 407.5f / 1080f
            });
        }

        public virtual void ReturnFromEgoMap()
        {
            if (!_model.MapUsed) return;
            _model.MapUsed = false;
            MapUtil.ReturnFromEgoMap(_model.EgoMapName, _model.OriginalMapStageIds);
        }

        public virtual void SpecialCardUseOn()
        {
            _specialUsed = true;
        }

        public virtual bool CheckSpecialUsed()
        {
            return _specialUsed;
        }
        public virtual void SummonSpecialUnit(StageLibraryFloorModel floor,int unitId,LorId unitNameId,int emotionLevel)
        {
            if (!_specialUsed) return;
            _specialUsed = false;
            UnitUtil.AddNewUnitPlayerSide(floor, new UnitModel
            {
                Id = unitId,
                Name = ModParameters.NameTexts
                    .FirstOrDefault(x => x.Key.Equals(unitNameId)).Value,
                EmotionLevel = emotionLevel,
                Pos = BattleObjectManager.instance.GetAliveList(Faction.Player).Count,
                Sephirah = floor.Sephirah,
                CustomPos = new XmlVector2 { x = 4, y = 0 }
            }, VortexModParameters.PackageId);
        }
    }
}