using System.Collections.Generic;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.GreenHunter.Buffs;
using VortexLabyrinth_Sa21341.Maps;
using VortexLabyrinth_Sa21341.UtilSa21341.Extension;

namespace VortexLabyrinth_Sa21341.GreenHunter.Passives
{
    public class PassiveAbility_GreenGuardianPlayer_Sa21341 : PassiveAbilityBase
    {
        private BattleUnitBuf_GreenLeaf_Sa21341 _buff;
        private MechUtilEx _util;

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            _buff.AddStacks(1);
        }

        public override void OnWaveStart()
        {
            var passive = owner.passiveDetail.AddPassive(new PassiveAbility_251201());
            passive.Hide();
            _buff = new BattleUnitBuf_GreenLeaf_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 42));
            _util = new MechUtilEx(new MechUtilBaseModel
            {
                Owner = owner,
                EgoMapName = "GreenHunterPhase2_Sa21341",
                EgoMapType = typeof(GreenGuardian2_Sa21341MapManager),
                BgY = 0.2f,
                FlY = 0.25f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(VortexModParameters.PackageId, 5), new LorId(VortexModParameters.PackageId, 6)
                },
                EgoAttackCardId = new LorId(VortexModParameters.PackageId, 42)
            });
            UnitUtil.CheckSkinProjection(owner);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.card.GetID() == new LorId(VortexModParameters.PackageId, 42))
                _buff.stack = 0;
            _util.ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }
    }
}