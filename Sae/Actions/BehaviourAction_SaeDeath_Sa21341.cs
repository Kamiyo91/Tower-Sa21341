using System.Collections.Generic;

namespace VortexTower.Sae.Actions
{
    public class BehaviourAction_SaeDeath_Sa21341 : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(
            ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            var list = new List<RencounterManager.MovingAction>();
            if (self.result == Result.Win && self.data.actionType == ActionType.Atk &&
                !opponent.behaviourResultData.IsFarAtk())
            {
                var movingAction =
                    new RencounterManager.MovingAction(ActionDetail.Slash, CharMoveState.MoveForward, 30f, false, 0f);
                movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.NONE, EffectTiming.NONE);
                movingAction.customEffectRes = "BorderOfDeath";
                list.Add(movingAction);
                var item = new RencounterManager.MovingAction(ActionDetail.Slash, CharMoveState.MoveForward, 0.2f,
                    false,
                    1f);
                list.Add(item);
                var movingAction2 =
                    new RencounterManager.MovingAction(ActionDetail.Hit, CharMoveState.Stop, 1f, false, 1f);
                movingAction2.SetEffectTiming(EffectTiming.NONE, EffectTiming.PRE, EffectTiming.PRE);
                list.Add(movingAction2);
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f,
                    false, 0f));
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f,
                    false, 0f));
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f,
                    false, 1f));
            }
            else
            {
                list = base.GetMovingAction(ref self, ref opponent);
            }

            return list;
        }
    }
}