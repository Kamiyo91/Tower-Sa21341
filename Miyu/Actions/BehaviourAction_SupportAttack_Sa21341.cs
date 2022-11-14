using System.Collections.Generic;

namespace VortexTower.Miyu.Actions
{
    public class BehaviourAction_SupportAttack_Sa21341 : BehaviourActionBase
    {
        public override bool IsMovable(BattleCardBehaviourResult self, BattleCardBehaviourResult opponent)
        {
            return true;
        }

        public override bool IsOpponentMovable(BattleCardBehaviourResult self, BattleCardBehaviourResult opponent)
        {
            return false;
        }

        public override List<RencounterManager.MovingAction> GetMovingAction(
            ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            opponent.infoList = new List<RencounterManager.MovingAction>();
            var item = new RencounterManager.MovingAction(ActionDetail.Default, CharMoveState.Stop, 0f, true, 0.3f);
            opponent.infoList.Add(item);
            return base.GetMovingAction(ref self, ref opponent);
        }
    }
}