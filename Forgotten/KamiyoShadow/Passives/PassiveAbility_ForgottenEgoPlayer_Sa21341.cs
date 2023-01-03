using System.Collections.Generic;
using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using BigDLL4221.Passives;
using LOR_DiceSystem;
using LOR_XML;
using VortexTower.Forgotten.KamiyoShadow.Buffs;

namespace VortexTower.Forgotten.KamiyoShadow.Passives
{
    public class PassiveAbility_ForgottenEgoPlayer_Sa21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        private BattleUnitBuf_Remembrance_Sa21341 _buff;

        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new MechUtilBase(new MechUtilBaseModel(survive: true, recoverToHp: 64,
                    surviveAbDialogList: new List<AbnormalityCardDialog>
                    {
                        new AbnormalityCardDialog
                        {
                            id = "ForgottenSurvive",
                            dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                                .EffectTexts["ForgottenSurvive_21341"].Desc
                        }
                    }, personalCards: new Dictionary<LorId, PersonalCardOptions>
                    {
                        { new LorId(VortexModParameters.PackageId, 52), new PersonalCardOptions() }
                    }, egoMaps: new Dictionary<LorId, MapModel>
                        { { new LorId(VortexModParameters.PackageId, 1), VortexModParameters.ForgottenMap5 } }),
                VortexModParameters.PackageId));
            Util.Model.Owner = owner;
        }

        public override void OnWaveStart()
        {
            ChangeDiceEffects(owner);
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 70));
            _buff = new BattleUnitBuf_Remembrance_Sa21341();
            owner.bufListDetail.AddBuf(_buff);
        }

        public override void OnRoundStartAfter()
        {
            base.OnRoundStartAfter();
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
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 67));
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 68));
            owner.personalEgoDetail.RemoveCard(new LorId(VortexModParameters.PackageId, 71));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 67));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 68));
            owner.personalEgoDetail.AddCard(new LorId(VortexModParameters.PackageId, 71));
        }

        public static void ChangeDiceEffects(BattleUnitModel owner)
        {
            foreach (var card in owner.allyCardDetail.GetAllDeck())
            {
                card.CopySelf();
                foreach (var dice in card.GetBehaviourList())
                    ChangeCardDiceEffect(dice);
            }
        }

        private static void ChangeCardDiceEffect(DiceBehaviour dice)
        {
            switch (dice.EffectRes)
            {
                case "KamiyoHit_Re21341":
                    dice.EffectRes = "KamiyoHitForgotten_Sa21341";
                    break;
                case "KamiyoSlash_Re21341":
                    dice.EffectRes = "KamiyoSlashForgotten_Sa21341";
                    break;
                case "PierceKamiyo_Re21341":
                    dice.EffectRes = "PierceKamiyoForgotten_Sa21341";
                    break;
            }
        }
    }
}