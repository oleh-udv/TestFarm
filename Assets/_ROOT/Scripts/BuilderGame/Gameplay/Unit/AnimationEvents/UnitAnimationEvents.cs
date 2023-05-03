using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit
{
    public class UnitAnimationEvents : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem plantingParticle;

        public void PlayPlantingParticle()
        {
            if (plantingParticle)
                plantingParticle.Play();
        }
    }
}
