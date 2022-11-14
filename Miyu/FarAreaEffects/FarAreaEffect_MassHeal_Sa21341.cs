using UnityEngine;

namespace VortexTower.Miyu.FarAreaEffects
{
    public class FarAreaEffect_MassHeal_Sa21341 : FarAreaEffect
    {
        private float _time;

        public override void Init(BattleUnitModel self, params object[] args)
        {
            base.Init(self, args);
            var gameObject = Util.LoadPrefab("Battle/SpecialEffect/OneBadManyGoodEffect", transform);
            if (gameObject != null)
                gameObject.AddComponent<AutoDestruct>().time = 4f;
        }

        protected override void Update()
        {
            if (!isRunning) return;
            _time += Time.deltaTime;
            if (!(_time > 3f)) return;
            Destroy(gameObject);
        }
    }
}