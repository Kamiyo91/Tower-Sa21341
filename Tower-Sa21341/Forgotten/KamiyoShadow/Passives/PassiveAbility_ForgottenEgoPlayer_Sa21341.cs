using System.Collections.Generic;
using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Buffs;
using VortexLabyrinth_Sa21341.Maps.ForgottenMaps;
using VortexLabyrinth_Sa21341.UtilSa21341.Extension;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow.Passives
{
    public class PassiveAbility_ForgottenEgoPlayer_Sa21341 : PassiveAbilityBase
    {
        private readonly StageLibraryFloorModel
            _floor = Singleton<StageController>.Instance.GetCurrentStageFloorModel();

        private BattleUnitBuf_Remembrance_Sa21341 _buff;
        private BattleUnitModel _summonedUnit;
        private bool _unitSummoned;
        private MechUtilEx _util;


        public override void OnWaveStart()
        {
            _summonedUnit = null;
            _unitSummoned = false;
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 52));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 69));
            _buff = new BattleUnitBuf_Remembrance_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
            _util = new MechUtilEx(new MechUtilBaseModel
            {
                Owner = owner,
                EgoMapName = "Forgotten5_Sa21341",
                EgoMapType = typeof(Forgotten5_Sa21341MapManager),
                BgY = 0.475f,
                FlY = 0.225f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(VortexModParameters.PackageId, 7), new LorId(VortexModParameters.PackageId, 8)
                },
                EgoAttackCardId = new LorId(VortexModParameters.PackageId, 52)
            });
        }

        public override void OnRoundEndTheLast()
        {
            if (!_util.CheckSpecialUsed()) return;
            _unitSummoned = true;
            _summonedUnit = _util.SummonSpecialUnit(_floor, 10000013, new LorId(VortexModParameters.PackageId, 11),
                owner.emotionDetail.EmotionLevel);
        }

        public bool GetSummonedStatus()
        {
            return _unitSummoned;
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            var cardId = curCard.card.GetID();
            _util.ChangeToEgoMap(cardId);
            if (cardId != new LorId(VortexModParameters.PackageId, 69)) return;
            _util.SpecialCardUseOn();
            owner.personalEgoDetail.RemoveCard(cardId);
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }

        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_Remembrance_Sa21341>())
            {
                _buff = new BattleUnitBuf_Remembrance_Sa21341();
                owner.bufListDetail.AddBuf(_buff);
            }

            foreach (var unit in BattleObjectManager.instance.GetAliveList(owner.faction).Where(x => x != owner))
                if (!unit.bufListDetail.HasBuf<BattleUnitBuf_AllyRemembrance_Sa21341>())
                    unit.bufListDetail.AddBuf(new BattleUnitBuf_AllyRemembrance_Sa21341());
            AddCards();
        }

        private void AddCards()
        {
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 49));
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 50));
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 53));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 49));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 50));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 53));
        }

        public override void OnBattleEnd()
        {
            if (_summonedUnit != null) _summonedUnit.Book.owner = null;
        }
    }
}