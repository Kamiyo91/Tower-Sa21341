using System;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.BaseClass;
using KamiyoStaticUtil.CommonBuffs;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.Zero.Buffs;

namespace VortexLabyrinth_Sa21341.UtilSa21341.Extension.Zero
{
    public class MechUtil_Zero : MechUtilBase
    {
        private readonly BattleUnitBuf_BlueFlame_Sa21341 _buff;
        private readonly MechUtilBaseModel _model;

        public MechUtil_Zero(MechUtilBaseModel model, BattleUnitBuf_BlueFlame_Sa21341 buff) : base(model)
        {
            _model = model;
            _buff = buff;
        }

        public override void SurviveCheck(int dmg)
        {
            if (_model.Owner.hp - dmg > _model.Hp || !_model.Survive) return;
            _model.SetHp = 10 + _buff.stack * 3;
            _model.Survive = false;
            UnitUtil.UnitReviveAndRecovery(_model.Owner, 0, _model.RecoverLightOnSurvive);
            if (_model.HasSurviveAbDialog)
                UnitUtil.BattleAbDialog(_model.Owner.view.dialogUI, _model.SurviveAbDialogList,
                    _model.SurviveAbDialogColor);
            _model.Owner.SetHp(_model.SetHp);
            if (_model.NearDeathBuffExist)
                _model.Owner.bufListDetail.AddBufWithoutDuplication(
                    (BattleUnitBuf)Activator.CreateInstance(_model.NearDeathBuffType));
            _model.Owner.bufListDetail.AddBufWithoutDuplication(
                new BattleUnitBuf_KamiyoImmunityToStatusAlimentUntilRoundEnd());
            _model.Owner.bufListDetail.AddBufWithoutDuplication(new BattleUnitBuf_KamiyoImmortalUntilRoundEnd());
            _buff.stack = 0;
        }
    }
}