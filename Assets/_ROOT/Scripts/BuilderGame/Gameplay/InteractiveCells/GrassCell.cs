using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.InteractiveCells
{
    public class GrassCell : Cell
    {
        [Space]
        [SerializeField]
        private Transform visual;

        [Header("Effects")]
        [SerializeField]
        private ParticleSystem _interactParticle;

        [SerializeField]
        private float scaleTime = 0.25f;
        [SerializeField]
        private float existTime = 1f;

        private void Start()
        {
            IsInteractable = true;
        }

        protected override void Interact()
        {
            base.Interact();
            IsInteractable = false;

            _interactParticle.Play();

            visual.DOScale(Vector3.up, scaleTime)
                .OnComplete(() => 
                {
                    DOVirtual.DelayedCall(existTime, () => gameObject.SetActive(false));
                });
        }
    }
}
