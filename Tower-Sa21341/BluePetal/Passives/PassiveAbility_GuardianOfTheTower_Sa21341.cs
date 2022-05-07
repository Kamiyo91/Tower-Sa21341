using System.Collections.Generic;
using System.Linq;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticUtil.Utils;
using VortexLabyrinth_Sa21341.BLL;
using VortexLabyrinth_Sa21341.Maps;
using VortexLabyrinth_Sa21341.UtilSa21341.Extension.BluePetal;

namespace VortexLabyrinth_Sa21341.BluePetal.Passives
{
    public class PassiveAbility_GuardianOfTheTower_Sa21341 : PassiveAbilityBase
    {
        private bool _startAttack;
        private NpcMechUtil_BluePetal _util;

        public override void OnWaveStart()
        {
            _startAttack = true;
            owner.view.ChangeHeight(501);
            _util = new NpcMechUtil_BluePetal(new NpcMechUtilBaseModel
            {
                Owner = owner,
                MechHp = 271,
                HasMechOnHp = true,
                Counter = 0,
                MaxCounter = 3,
                EgoMapName = "BlueGuardian_Sa21341",
                EgoMapType = typeof(BlueGuardian_Sa21341MapManager),
                BgY = 0.25f,
                FlY = 0.8f,
                OriginalMapStageIds = new List<LorId>
                {
                    new LorId(VortexModParameters.PackageId, 3), new LorId(VortexModParameters.PackageId, 4)
                },
                LorIdEgoMassAttack = new LorId(VortexModParameters.PackageId, 25),
                EgoAttackCardId = new LorId(VortexModParameters.PackageId, 25)
            });
            _util.Restart();
        }

        public override int SpeedDiceNumAdder()
        {
            return _util.GetPhase() <= 0 ? 2 : 3;
        }

        public override void OnRoundEnd()
        {
            var cards = owner.allyCardDetail.GetAllDeck()
                .Where(x => x.GetID() == new LorId(VortexModParameters.PackageId, 27));
            foreach (var card in cards) owner.allyCardDetail.ExhaustACardAnywhere(card);
            _util.ExhaustEgoAttackCards();
            _util.SetOneTurnCard(false);
            _util.RaiseCounter();
        }

        public override void OnBattleEnd()
        {
            _util.OnEndBattle();
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.MechHpCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
        }

        public override void OnStartBattle()
        {
            UnitUtil.RemoveImmortalBuff(owner);
        }

        public override BattleDiceCardModel OnSelectCardAuto(BattleDiceCardModel origin, int currentDiceSlotIdx)
        {
            if (_startAttack)
            {
                _startAttack = false;
                origin = BattleDiceCardModel.CreatePlayingCard(
                    ItemXmlDataList.instance.GetCardItem(new LorId(VortexModParameters.PackageId, 27)));
            }

            _util.OnSelectCardPutMassAttack(ref origin);
            return base.OnSelectCardAuto(origin, currentDiceSlotIdx);
        }

        public override void OnRoundStartAfter()
        {
            if (_util.GetPhase() <= 0) return;
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1);
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1);
        }

        public override void OnRoundEndTheLast()
        {
            _util.CheckPhase();
        }

        public override void OnDie()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList()
                         .Where(x => x.Book.BookId == new LorId(VortexModParameters.PackageId, 1)))
                unit.Die();
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseCardResetCount(curCard);
            _util.ChangeToEgoMap(curCard.card.GetID());
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            _util.ReturnFromEgoMap();
        }
    }
}