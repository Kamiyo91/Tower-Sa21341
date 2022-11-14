using System.Collections.Generic;
using System.Linq;
using BigDLL4221.Enum;
using BigDLL4221.Models;
using BigDLL4221.Passives;
using LOR_XML;
using VortexTower.Zero.Buffs;

namespace VortexTower.Zero.Passives
{
    public class PassiveAbility_Zero_Sa21341 : PassiveAbility_PlayerMechBase_DLL4221
    {
        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            SetUtil(new MechUtil_Zero(new MechUtilBaseModel
            {
                Owner = owner,
                ThisPassiveId = id,
                Survive = true,
                SurviveHp = 0,
                RecoverToHp = 10,
                EgoOptions = new Dictionary<int, EgoOptions>
                {
                    {
                        0, new EgoOptions(new BattleUnitBuf_BlueFlameEgo_Sa21341(),
                            egoAbColorColor: AbColorType.Positive, egoAbDialogList: new List<AbnormalityCardDialog>
                            {
                                new AbnormalityCardDialog
                                {
                                    id = "Zero",
                                    dialog = ModParameters.LocalizedItems[VortexModParameters.PackageId]
                                        .EffectTexts["ZeroEgoActive1_Sa21341"].Desc
                                }
                            })
                    }
                },
                FirstEgoFormCard = new LorId(VortexModParameters.PackageId, 51),
                EgoMaps = new Dictionary<LorId, MapModel>
                    { { new LorId(VortexModParameters.PackageId, 59), VortexModParameters.ZeroMap } },
                PersonalCards = new Dictionary<LorId, PersonalCardOptions>
                {
                    {
                        new LorId(VortexModParameters.PackageId, 51), new PersonalCardOptions(true, activeEgoCard: true)
                    },
                    { new LorId(VortexModParameters.PackageId, 59), new PersonalCardOptions(true) },
                    { new LorId(VortexModParameters.PackageId, 52), new PersonalCardOptions(onPlayCard: true) }
                },
                PermanentBuffList = new List<PermanentBuffOptions>
                    { new PermanentBuffOptions(new BattleUnitBuf_BlueFlame_Sa21341()) }
            }));
        }

        public override void OnWaveStart()
        {
            InitDialog();
            base.OnWaveStart();
        }

        public override bool CanAddBuf(BattleUnitBuf buf)
        {
            if (buf.bufType != KeywordBuf.Burn) return true;
            Util.Model.PermanentBuffList.FirstOrDefault()?.Buff?.OnAddBuf(buf.stack);
            return false;
        }

        public override void OnSucceedAttack(BattleDiceBehavior behavior)
        {
            Util.Model.PermanentBuffList.FirstOrDefault()?.Buff?.OnAddBuf(1);
            var target = behavior.card.target;
            if (target == null) return;
            if (target.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_BlueBurn_Sa21341) is
                BattleUnitBuf_BlueBurn_Sa21341 buff)
                buff.OnAddBuf(1);
            else
                target.bufListDetail.AddBuf(new BattleUnitBuf_BlueBurn_Sa21341());
        }

        public override void OnRoundStartAfter()
        {
            ConvertBurnForAll();
            base.OnRoundStartAfter();
        }

        private void InitDialog()
        {
            if (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.packageId !=
                VortexModParameters.PackageId) return;
            switch (Singleton<StageController>.Instance.GetStageModel().ClassInfo.id.id)
            {
                case 5:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000007));
                    break;
                case 7:
                    owner.UnitData.unitData.InitBattleDialogByDefaultBook(new LorId(VortexModParameters.PackageId,
                        10000009));
                    break;
            }
        }

        public void ConvertBurnForAll()
        {
            foreach (var unit in BattleObjectManager.instance.GetAliveList().Where(x =>
                         x != owner && !x.passiveDetail.HasPassive<PassiveAbility_BlueBurn_Sa21341>()))
            {
                var existCheck = false;
                var burnBuff = unit.bufListDetail.GetActivatedBufList().FirstOrDefault(x =>
                    x.bufType == KeywordBuf.Burn && !(x is BattleUnitBuf_BlueBurn_Sa21341));
                var burnNextBuff = unit.bufListDetail.GetReadyBufList().FirstOrDefault(x =>
                    x.bufType == KeywordBuf.Burn && !(x is BattleUnitBuf_BlueBurn_Sa21341));
                var blueBuff = unit.bufListDetail.GetActivatedBufList().FirstOrDefault(x =>
                    x is BattleUnitBuf_BlueBurn_Sa21341);
                if (blueBuff == null)
                {
                    existCheck = true;
                    blueBuff = new BattleUnitBuf_BlueBurn_Sa21341
                    {
                        stack = 0
                    };
                }

                if (burnBuff != null)
                {
                    blueBuff.stack += burnBuff.stack;
                    unit.bufListDetail.RemoveBuf(burnBuff);
                }

                if (burnNextBuff != null)
                {
                    blueBuff.stack += burnNextBuff.stack;
                    unit.bufListDetail.RemoveReadyBuf(burnNextBuff);
                }

                if (existCheck && blueBuff.stack != 0) unit.bufListDetail.AddBuf(blueBuff);
                var passive = unit.passiveDetail.AddPassive(new LorId(VortexModParameters.PackageId, 41));
                passive.Hide();
                unit.passiveDetail.OnCreated();
                passive.OnRoundStartAfter();
            }
        }
    }
}