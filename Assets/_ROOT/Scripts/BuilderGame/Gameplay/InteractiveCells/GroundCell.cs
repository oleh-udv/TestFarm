using DG.Tweening;
using Structs;
using System;
using UnityEngine;

namespace BuilderGame.Gameplay.InteractiveCells
{
    public class GroundCell : Cell
    {
        [Space]
        [SerializeField]
        private Transform sprout;
        [SerializeField]
        private Transform plant;

        [Header("Growth Settings")]
        [SerializeField] 
        private FloatRangeStruct growthTime;

        [Header("Effects")]
        [SerializeField]
        private ParticleSystem collectParticle;

        [Space]
        [SerializeField]
        private float sproutScaleTime = 0.25f;
        [SerializeField]
        private float sproutPunchTime = 0.2f;
        [SerializeField]
        private float sproutPunchForce = 0.1f;

        [Space]
        [SerializeField]
        private float plantDisappearingTime = 0.5f;
        [SerializeField]
        private float plantScaleTime = 0.25f;
        [SerializeField]
        private float plantPunchTime = 0.2f;
        [SerializeField]
        private float plantPunchForce = 0.075f;

        private Tween growingTween;
        private bool isSown;

        private void OnDisable()
        {
            growingTween.Kill();
        }

        protected override void Interact()
        {
            base.Interact();
            IsInteractable = false;

            if (isSown == false)
            {
                Sown();
            }
            else 
            {
                Collect();
            }
        }

        private void Sown() 
        {
            cellType = Enums.CellType.SownGroundCell;
            IsInteractable = false;
            isSown = true;

            sprout.gameObject.SetActive(true);
            Vector3 startScale = sprout.localScale;
            sprout.localScale = Vector3.zero;

            sprout.DOScale(startScale, sproutScaleTime)
                .SetEase(Ease.Linear).OnComplete(() => 
                {
                    sprout.DOPunchScale(Vector3.one * sproutPunchForce, sproutPunchTime, 1)
                        .OnComplete(StartGrowing);
                });
        }

        private void StartGrowing() 
        {
            growingTween.Kill();
            DOVirtual.DelayedCall(UnityEngine.Random.Range(growthTime.minValue, growthTime.maxValue), GrowUp);
        }

        private void GrowUp() 
        {
            Vector3 startScale = plant.localScale;
            plant.localScale = Vector3.zero;
            plant.gameObject.SetActive(true);

            plant.DOScale(startScale, plantScaleTime)
                .SetEase(Ease.Linear).OnComplete(() => 
                {
                    plant.DOPunchScale(Vector3.one * plantPunchForce, plantPunchTime, 1)
                        .OnComplete(() => IsInteractable = true);
                });
        }

        private void Collect() 
        {
            Vector3 startScale = plant.localScale;
            plant.DOScale(Vector3.zero, plantDisappearingTime)
                .SetEase(Ease.Linear).OnComplete(() => 
                {
                    plant.gameObject.SetActive(false);
                    plant.localScale = startScale;
                });

            collectParticle.Play();

            //
            Sown();
        }
    }
}
