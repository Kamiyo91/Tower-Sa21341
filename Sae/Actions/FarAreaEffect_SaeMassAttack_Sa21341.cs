using System.Collections.Generic;
using Battle.DiceAttackEffect;
using BigDLL4221.Models;
using BigDLL4221.Utils;
using LOR_DiceSystem;
using Sound;
using UnityEngine;

namespace VortexTower.Sae.Actions
{
    public class FarAreaEffect_SaeMassAttack_Sa21341 : FarAreaEffect
    {
        public enum Phase
        {
            Start,
            SetTarget,
            MoveToTarget,
            ChangeMotion,
            GiveDamage,
            End
        }

        private readonly float _DELAY_AFTER_ARRIVED = 0.25f;

        private readonly float _DELAY_AFTER_GIVING_DMG = 0.25f;

        private readonly float _DELAY_DESTROY = 0.5f;

        private readonly float _DELAY_END_ACTION = 0.5f;

        private readonly float _DELAY_FINAL_MOTION = 0.5f;

        private readonly float _DELAY_LAST_MOTION_CHANGE = 0.2f;

        private readonly int _LAST_ATK_COUNT = 4;

        private readonly AnimationCurve _moveRatioCurve = new AnimationCurve(
            new Keyframe(0, 0.001708984f, 0.001759519f, 0.001759519f, 0, 0.7804829f),
            new Keyframe(0.5f, 0.5f, 3.17064f, 3.17064f, 0.2216982f, 0.2122641f),
            new Keyframe(1, 1, -0.004208297f, -0.004208297f, 0.9757937f, 0));

        private readonly float _moveSpeed = 8;

        private readonly bool _RANDOM_MOTION = true;

        private readonly AudioClip _soundS1 = UnitUtil.GetSound("Purple_Slash_Hori", true);

        private readonly AudioClip _soundS2 = UnitUtil.GetSound("Purple_Stab_Stab2", true);

        private readonly AudioClip _soundS3 = UnitUtil.GetSound("Purple_Slash_VertDown", true);

        private List<GameObject> _atkParticleInstanceList;

        private bool _bCamReturning;

        private bool _bDoneGivingDmg;

        private bool _bLastAtk;

        private bool _bLastBehaviour;

        private bool _bLastTarget;

        private BattleFarAreaPlayManager.VictimInfo _curVictim;

        private List<BattleFarAreaPlayManager.VictimInfo> _defensedList;

        private Vector3 _dstPos;

        private float _elapsedAfterArrived;

        private float _elapsedAfterGiveDmg;

        private float _elapsedCamReturn;

        private float _elapsedDestroy;

        private float _elapsedEnd;

        private float _elapsedFinalPenetrateMotion;

        private float _elapsedMovingToTarget;

        private int _lastMotionCount;

        private int _motionIdx;

        private Phase _phase;

        private Vector3 _srcPos;

        private float _timerLastMotion;

        private List<BattleFarAreaPlayManager.VictimInfo> _victimList;
        public override bool HasIndependentAction => true;

        public void SetLastAttack(bool value)
        {
            _bLastAtk = value;
        }

        public override void Init(BattleUnitModel self, params object[] args)
        {
            base.Init(self, args);
            _atkParticleInstanceList = new List<GameObject>();
            _victimList =
                new List<BattleFarAreaPlayManager.VictimInfo>(Singleton<BattleFarAreaPlayManager>.Instance.victims);
            if (!_victimList.Exists(x => !x.unitModel.IsDead()))
            {
                DestroyForcely();
                return;
            }

            _self = self;
            isRunning = false;
            state = EffectState.Start;
            if (self.currentDiceAction.currentBehavior.Index == 0) self.moveDetail.Move(Vector3.zero, 150f);
            if (self.currentDiceAction.cardBehaviorQueue.Count == 0)
                _bLastBehaviour = true;
            else
                _bLastBehaviour = false;
            _defensedList = new List<BattleFarAreaPlayManager.VictimInfo>();
            _phase = Phase.Start;
            _bLastTarget = false;
            _curVictim = null;
            _bDoneGivingDmg = false;
            _elapsedAfterGiveDmg = 0f;
            _elapsedEnd = 0f;
            _elapsedDestroy = 0f;
            _timerLastMotion = 0f;
            _lastMotionCount = 0;
            _motionIdx = Random.Range(0, 2);
            _self.view.charAppearance.ChangeMotion(ActionDetail.Default);
            FocusCam(true);
            _elapsedFinalPenetrateMotion = 0f;
            _elapsedCamReturn = 0f;
            _bCamReturning = false;
        }

        public override bool ActionPhase(float deltaTime, BattleUnitModel attacker,
            List<BattleFarAreaPlayManager.VictimInfo> victims,
            ref List<BattleFarAreaPlayManager.VictimInfo> defenseVictims)
        {
            var flag = false;
            if (_phase == Phase.Start)
            {
                StartPhase(deltaTime);
            }
            else if (_phase == Phase.SetTarget)
            {
                SetTargetPhase(deltaTime);
            }
            else if (_phase == Phase.MoveToTarget)
            {
                MoveToTargetPhase(deltaTime);
            }
            else if (_phase == Phase.ChangeMotion)
            {
                ChangeMotionPhase(deltaTime);
            }
            else if (_phase == Phase.GiveDamage)
            {
                GiveDamagePhase(deltaTime);
            }
            else if (_phase == Phase.End)
            {
                flag = EndPhase(deltaTime);
                if (flag) defenseVictims = _defensedList;
            }

            return flag;
        }

        private void StartPhase(float deltaTime)
        {
            if (_self.moveDetail.isArrived) _phase = Phase.SetTarget;
        }

        private void SetTargetPhase(float deltaTime)
        {
            _victimList.RemoveAll(x => x.unitModel.IsDead());
            if (_victimList.Count > 0)
            {
                if (_victimList.Count == 1) _bLastTarget = true;
                _curVictim = _victimList[Random.Range(0, _victimList.Count)];
                var worldPosition = _curVictim.unitModel.view.WorldPosition;
                var x2 = _curVictim.unitModel.view.transform.localScale.x;
                var num = SingletonBehavior<HexagonalMapManager>.Instance.tileSize * x2 + 4f;
                var num2 = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
                var dstPos = worldPosition + new Vector3(num2 * num, 0f, 0f);
                _dstPos = dstPos;
                _srcPos = _self.view.WorldPosition;
                _phase = Phase.MoveToTarget;
                return;
            }

            _phase = Phase.End;
        }

        private void MoveToTargetPhase(float deltaTime)
        {
            if (_curVictim == null)
            {
                _phase = Phase.SetTarget;
                return;
            }

            if (_elapsedMovingToTarget < 1f)
            {
                _elapsedMovingToTarget += deltaTime * _moveSpeed;
                var t = _moveRatioCurve.Evaluate(_elapsedMovingToTarget);
                _self.view.WorldPosition = Vector3.Lerp(_srcPos, _dstPos, t);
                return;
            }

            _elapsedAfterArrived += deltaTime;
            if (_elapsedAfterArrived >= _DELAY_AFTER_ARRIVED)
            {
                _elapsedAfterArrived = 0f;
                _elapsedMovingToTarget = 0f;
                _phase = Phase.ChangeMotion;
            }
        }

        private void ChangeMotionPhase(float deltaTime)
        {
            if (_bLastBehaviour && _bLastTarget)
            {
                if (_lastMotionCount > _LAST_ATK_COUNT)
                {
                    _phase = Phase.GiveDamage;
                    return;
                }

                _timerLastMotion -= deltaTime;
                if (_timerLastMotion <= 0f)
                {
                    _timerLastMotion = _DELAY_LAST_MOTION_CHANGE;
                    ActionDetail detail;
                    if (_lastMotionCount < _LAST_ATK_COUNT)
                    {
                        _motionIdx = (_motionIdx + 1) % 2;
                        if (_motionIdx == 0)
                        {
                            detail = ActionDetail.Hit;
                            var componentType = ModParameters.CustomEffects["SaeHit_Sa21341"];
                            var diceAttackEffect =
                                new GameObject("SaeHit_Sa21341").AddComponent(componentType) as
                                    DiceAttackEffect;
                            diceAttackEffect.Initialize(_self.view, _curVictim.unitModel.view, 0.5f);
                            diceAttackEffect.SetScale(1f);
                            SingletonBehavior<SoundEffectManager>.Instance.PlayClip(_soundS1);
                        }
                        else
                        {
                            detail = ActionDetail.Penetrate;
                            var componentType = ModParameters.CustomEffects["SaePierce_Sa21341"];
                            var diceAttackEffect =
                                new GameObject("SaePierce_Sa21341").AddComponent(componentType) as
                                    DiceAttackEffect;
                            diceAttackEffect.Initialize(_self.view, _curVictim.unitModel.view, 0.5f);
                            diceAttackEffect.SetScale(1f);
                            SingletonBehavior<SoundEffectManager>.Instance.PlayClip(_soundS2);
                        }
                    }
                    else
                    {
                        _elapsedFinalPenetrateMotion += Time.deltaTime;
                        if (_elapsedFinalPenetrateMotion < _DELAY_FINAL_MOTION)
                        {
                            _timerLastMotion = 0f;
                            return;
                        }

                        _timerLastMotion = _DELAY_LAST_MOTION_CHANGE;
                        _elapsedFinalPenetrateMotion = 0f;
                        var b = _curVictim.unitModel.view.WorldPosition - _self.view.WorldPosition;
                        var worldPosition = _curVictim.unitModel.view.WorldPosition + b;
                        _self.view.WorldPosition = worldPosition;
                        detail = ActionDetail.Slash;
                        var componentType = ModParameters.CustomEffects["SaeSlash_Sa21341"];
                        var diceAttackEffect =
                            new GameObject("SaeSlash_Sa21341").AddComponent(componentType) as
                                DiceAttackEffect;
                        diceAttackEffect.Initialize(_self.view, _curVictim.unitModel.view, 0.5f);
                        diceAttackEffect.SetScale(1f);
                        SingletonBehavior<SoundEffectManager>.Instance.PlayClip(_soundS3);
                    }

                    _self.view.charAppearance.ChangeMotion(detail);
                    _lastMotionCount++;
                    CreateBloodEffect();
                }
            }
            else
            {
                ActionDetail detail2;
                if (_RANDOM_MOTION)
                {
                    detail2 = Random.Range(0f, 1f) > 0.5f ? ActionDetail.Hit : ActionDetail.Penetrate;
                    if (detail2 == ActionDetail.Hit)
                    {
                        var componentType = ModParameters.CustomEffects["SaeHit_Sa21341"];
                        var diceAttackEffect =
                            new GameObject("SaeHit_Sa21341").AddComponent(componentType) as
                                DiceAttackEffect;
                        diceAttackEffect.Initialize(_self.view, _curVictim.unitModel.view, 0.5f);
                        diceAttackEffect.SetScale(1f);
                        SingletonBehavior<SoundEffectManager>.Instance.PlayClip(_soundS1);
                    }
                    else
                    {
                        var componentType = ModParameters.CustomEffects["SaePierce_Sa21341"];
                        var diceAttackEffect =
                            new GameObject("SaePierce_Sa21341").AddComponent(componentType) as
                                DiceAttackEffect;
                        diceAttackEffect.Initialize(_self.view, _curVictim.unitModel.view, 0.5f);
                        diceAttackEffect.SetScale(1f);
                        SingletonBehavior<SoundEffectManager>.Instance.PlayClip(_soundS2);
                    }
                }
                else
                {
                    _motionIdx = (_motionIdx + 1) % 2;
                    if (_motionIdx == 0)
                    {
                        detail2 = ActionDetail.S1;
                        SingletonBehavior<SoundEffectManager>.Instance.PlayClip(_soundS1);
                    }
                    else
                    {
                        detail2 = ActionDetail.S2;
                        SingletonBehavior<SoundEffectManager>.Instance.PlayClip(_soundS2);
                    }
                }

                _self.view.charAppearance.ChangeMotion(detail2);
                _phase = Phase.GiveDamage;
                CreateBloodEffect();
            }
        }

        private void GiveDamagePhase(float deltaTime)
        {
            if (_bDoneGivingDmg)
            {
                _elapsedAfterGiveDmg += deltaTime;
                if (_elapsedAfterGiveDmg >= _DELAY_AFTER_GIVING_DMG)
                {
                    _phase = Phase.SetTarget;
                    _elapsedAfterGiveDmg = 0f;
                    _bDoneGivingDmg = false;
                }

                return;
            }

            if (_curVictim == null)
            {
                _phase = Phase.SetTarget;
                return;
            }

            var playingCard = _curVictim.playingCard;
            if (playingCard != null)
            {
                var ranged = _self.currentDiceAction.card.GetSpec().Ranged;
                if (ranged == CardRange.FarArea)
                {
                    var sum = 0;
                    playingCard.GetDiceBehaviorList().ForEach(delegate(BattleDiceBehavior x)
                    {
                        sum += x.DiceResultValue;
                    });
                    if (_self.currentDiceAction.currentBehavior.DiceResultValue > sum)
                    {
                        SuccessAtk();
                        _curVictim.cardDestroyed = true;
                    }
                    else
                    {
                        FailAtk();
                    }
                }
                else if (ranged == CardRange.FarAreaEach)
                {
                    var currentBehavior = playingCard.currentBehavior;
                    if (currentBehavior != null)
                    {
                        if (_self.currentDiceAction.currentBehavior.DiceResultValue > currentBehavior.DiceResultValue)
                        {
                            SuccessAtk();
                            _curVictim.destroyedDicesIndex.Add(currentBehavior.Index);
                        }
                        else
                        {
                            FailAtk();
                        }
                    }
                    else
                    {
                        SuccessAtk();
                    }
                }
            }
            else
            {
                SuccessAtk();
            }

            SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(
                _curVictim.unitModel, _curVictim.unitModel.faction, _curVictim.unitModel.hp,
                _curVictim.unitModel.breakDetail.breakGauge);
            SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(_self,
                _self.faction, _self.hp, _self.breakDetail.breakGauge);
            _bDoneGivingDmg = true;
            if (_curVictim != null)
            {
                _victimList.Remove(_curVictim);
                FocusCam(true);
            }
        }

        private void SuccessAtk()
        {
            _self.currentDiceAction.currentBehavior.GiveDamage(_curVictim.unitModel);
            _curVictim.unitModel.view.charAppearance.ChangeMotion(ActionDetail.Damaged);
        }

        private void FailAtk()
        {
            _curVictim.unitModel.view.charAppearance.ChangeMotion(ActionDetail.Guard);
            if (!_defensedList.Contains(_curVictim)) _defensedList.Add(_curVictim);
        }

        private bool EndPhase(float deltaTime)
        {
            var result = false;
            _elapsedEnd += deltaTime;
            if (_elapsedEnd >= _DELAY_END_ACTION)
            {
                if (!_bLastAtk)
                {
                    FocusCam(false);
                    _isDoneEffect = true;
                    result = true;
                }
                else if (!_bCamReturning)
                {
                    FocusCam(false);
                    _bCamReturning = true;
                }
            }
            else
            {
                result = false;
            }

            if (_bLastAtk && _bCamReturning)
            {
                if (SingletonBehavior<BattleCamManager>.Instance.IsCamIsReturning)
                {
                    result = false;
                    _elapsedCamReturn += deltaTime;
                }
                else
                {
                    result = true;
                    _isDoneEffect = true;
                }

                _elapsedCamReturn += deltaTime;
                if (_elapsedCamReturn >= 5f)
                {
                    result = true;
                    _isDoneEffect = true;
                }
            }

            return result;
        }

        private void FocusCam(bool b)
        {
            if (b)
            {
                var list = new List<BattleUnitModel>();
                list.Add(_self);
                foreach (var victimInfo in _victimList) list.Add(victimInfo.unitModel);
                SingletonBehavior<BattleCamManager>.Instance.FollowUnits(false, list);
                return;
            }

            if (_bLastAtk)
            {
                StartCoroutine(SingletonBehavior<BattleCamManager>.Instance.ReturnCam());
                return;
            }

            SingletonBehavior<BattleCamManager>.Instance.FollowUnits(false,
                BattleObjectManager.instance.GetAliveList());
        }

        private void DestroyForcely()
        {
            Destroy(gameObject);
        }

        private void CreateBloodEffect()
        {
        }

        protected override void Update()
        {
            if (_isDoneEffect)
            {
                _elapsedDestroy += Time.deltaTime;
                if (_elapsedDestroy >= _DELAY_DESTROY) Destroy(gameObject);
            }

            if (_curVictim != null)
            {
                if (_lastMotionCount < _LAST_ATK_COUNT) _self.UpdateDirection(_curVictim.unitModel.view.WorldPosition);
                foreach (var victimInfo in _victimList) victimInfo.unitModel.UpdateDirection(_self.view.WorldPosition);
            }

            if (_atkParticleInstanceList != null && _atkParticleInstanceList.Count > 0)
            {
                var currentMapObject = SingletonBehavior<BattleSceneRoot>.Instance.currentMapObject;
                if (currentMapObject != null)
                    foreach (var gameObject in _atkParticleInstanceList)
                        currentMapObject.ReviseFilterTransform(gameObject.GetComponent<SpriteRenderer>());
            }
        }

        private void OnDestroy()
        {
            if (_atkParticleInstanceList != null && _atkParticleInstanceList.Count > 0)
                foreach (var obj in _atkParticleInstanceList)
                    Destroy(obj);
        }
    }
}