using System.Collections.Generic;
using System.Linq;
using KamiyoStaticBLL.Models;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Maps;
using VortexLabyrinth_Sa21341.UtilSa21341;

namespace VortexLabyrinth_Sa21341.Miyu.Passives
{
    public class PassiveAbility_Miyu_Sa21341 : PassiveAbilityBase
    {
        private bool _used;

        public override void OnWaveStart()
        {
            _used = false;
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id ==
                new LorId(VortexModParameters.PackageId, 3))
                owner.UnitData.unitData.InitBattleDialogByDefaultBook(
                    new LorId(VortexModParameters.PackageId, 10000004));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 15));
        }

        public override void OnRoundStart()
        {
            owner.allyCardDetail.DrawCards(1);
            owner.cardSlotDetail.RecoverPlayPoint(1);
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            if (curCard.card.GetID() != new LorId(VortexModParameters.PackageId, 15)) return;
            owner.personalEgoDetail.RemoveCard(curCard.card.GetID());
            _used = true;
            ChangeToMiyuEgoMap();
        }

        public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
        {
            if (!VortexModParameters.HealerCardsId.Contains(card.GetID())) return base.ChangeAttackTarget(card, idx);
            var units = BattleObjectManager.instance.GetAliveList(owner.faction).Where(x => x.IsTargetable(owner))
                .ToList();
            return units.Any() ? RandomUtil.SelectOne(units) : base.ChangeAttackTarget(card, idx);
        }

        private static void ChangeToMiyuEgoMap()
        {
            MapUtil.ChangeMap(new MapModel
            {
                Stage = "BlueGuardian_Sa21341",
                StageIds = new List<LorId>
                    { new LorId(VortexModParameters.PackageId, 3), new LorId(VortexModParameters.PackageId, 4) },
                OneTurnEgo = true,
                IsPlayer = true,
                Component = typeof(BlueGuardian_Sa21341MapManager),
                Bgy = 0.25f,
                Fy = 0.8f
            });
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!_used) return;
            _used = false;
            MapUtil.ReturnFromEgoMap("BlueGuardian_Sa21341",
                new List<LorId>
                    { new LorId(VortexModParameters.PackageId, 3), new LorId(VortexModParameters.PackageId, 4) });
        }
    }
}