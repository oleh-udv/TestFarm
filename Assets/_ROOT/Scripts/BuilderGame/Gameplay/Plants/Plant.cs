using BuilderGame.Gameplay.Unit;
using BuilderGame.Gameplay.Pools;
using DG.Tweening;
using System;
using UnityEngine;
using Zenject;
using BuilderGame.Gameplay.Collectable;

namespace BuilderGame.Gameplay.Plants
{
    public class Plant : MonoBehaviour
    {
        [SerializeField]
        private CollectableItemsEnum plantType;
        [Space]
        [SerializeField]
        private Transform sprout;
        [SerializeField]
        private Transform grownPlant;

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
        private float grownDisappearingTime = 0.5f;
        [SerializeField]
        private float grownScaleTime = 0.25f;
        [SerializeField]
        private float grownPunchTime = 0.2f;
        [SerializeField]
        private float grownPunchForce = 0.075f;

        private PlantPoolManager plantPoolManager;
        private Tween growingTween;
        private float growingTime;

        public event Action OnGrowUp;

        [Inject]
        public void Construct(PlantPoolManager plantPoolManager)
        {
            this.plantPoolManager = plantPoolManager;
        }

        private void OnDisable()
        {
            growingTween.Kill();
        }

        public void SetGrowingTime(float time)
        {
            growingTime = time;
        }

        public void Planting()
        {
            Appear(sprout, sproutScaleTime, sproutPunchTime, sproutPunchForce, StartGrowing);
            Disappear(grownPlant, grownDisappearingTime);
        }

        public void Collect(UnitActions unit)
        {
            Planting();

            var collectItim = plantPoolManager.GetPoolElement(plantType);
            collectItim.transform.position = transform.position;
            collectItim.MoveToPoint(unit.transform, () => unit.CollectItem());

            collectParticle.Play();
        }

        private void GrowUp()
        {
            Appear(grownPlant, grownScaleTime, grownPunchTime, grownPunchForce, () => OnGrowUp?.Invoke());
            Disappear(sprout, sproutScaleTime);
        }

        private void StartGrowing()
        {
            growingTween.Kill();
            growingTween = DOVirtual.DelayedCall(growingTime, GrowUp);
        }

        private void Appear(Transform plant, float scaleTime, float punchTime, float punchForce, Action OnComplete = null)
        {
            plant.gameObject.SetActive(true);
            Vector3 startScale = plant.localScale;
            plant.localScale = Vector3.zero;

            plant.DOScale(startScale, scaleTime)
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    plant.DOPunchScale(Vector3.one * punchForce, punchTime, 1)
                        .OnComplete(() => OnComplete?.Invoke());
                });
        }

        private void Disappear(Transform plant, float disappearingTime) 
        {
            Vector3 startScale = plant.localScale;
            plant.DOScale(Vector3.zero, disappearingTime)
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    plant.gameObject.SetActive(false);
                    plant.localScale = startScale;
                });
        }
    }
}