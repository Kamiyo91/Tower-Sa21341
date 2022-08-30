using UnityEngine;

namespace VortexLabyrinth_Sa21341.Forgotten.KamiyoShadow
{
    public class AuraColor : MonoBehaviour
    {
        private void Start()
        {
            var ps = GetComponentInChildren<ParticleSystem>();
            var col = ps.colorOverLifetime;
            col.enabled = true;
            var grad = new Gradient();
            grad.SetKeys(new[] { new GradientColorKey(new Color(0,0,0,1), 0.0f), new GradientColorKey(new Color(0, 0, 0, 1), 1.0f) }, new[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
            col.color = grad;
        }
    }
}
