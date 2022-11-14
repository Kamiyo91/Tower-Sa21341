using System.Collections.Generic;
using System.Linq;
using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using BigDLL4221.Passives;
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
            SetUtil(new MechUtilBase(new MechUtilBaseModel
            {
                Owner = owner,
                Survive = true,
                RecoverToHp = 64,
                SurviveAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "ForgottenSurvive",
                        dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                            .EffectTexts["ForgottenSurvive_21341"].Desc
                    }
                },
                EgoMaps = new Dictionary<LorId, MapModel>
                    { { new LorId(VortexModParameters.PackageId, 1), VortexModParameters.ForgottenMap } },
                PersonalCards = new Dictionary<LorId, PersonalCardOptions>
                {
                    { new LorId(VortexModParameters.PackageId, 52), new PersonalCardOptions() }
                }
            }));
        }

        public override void OnWaveStart()
        {
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
    }
}