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
        private BattleUnitBuf_Remembrance_Sa21341 _buff;
        private MechUtilEx _util;


        public override void OnWaveStart()
        {
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 52));
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

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }

        public override void OnRoundStartAfter()
        {
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
    }
}