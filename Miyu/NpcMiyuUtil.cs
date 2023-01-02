using BigDLL4221.BaseClass;
using BigDLL4221.Models;
using BigDLL4221.Utils;

namespace VortexTower.Miyu
{
    public class NpcMiyuUtil : NpcMechUtilBase
    {
        public NpcMiyuUtil(NpcMechUtilBaseModel model) : base(model, VortexModParameters.PackageId)
        {
        }

        public override void ExtraMethodOnPhaseChangeRoundStart(MechPhaseOptions mechOptions)
        {
            CameraFilterUtil.EarthQuake(0.08f, 0.02f, 50f, 0.3f);
            ArtUtil.BaseGameLoadPrefabEffect(Model.Owner,
                "Battle/CreatureEffect/New_IllusionCardFX/6_G/FX_IllusionCard_6_G_Shout", "Creature/Danggo_Lv2_Shout");
            ChangeDeck();
        }

        public void ChangeDeck()
        {
            Model.Owner.allyCardDetail.ExhaustAllCards();
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 29));
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 30));
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 31));
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 29));
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 30));
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 31));
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 29));
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 30));
            Model.Owner.allyCardDetail.AddNewCard(new LorId(VortexModParameters.PackageId, 31));
        }
    }
}