using BuilderGame.Gameplay.Plants;
using DG.Tweening;
using Structs;
using System;
using UnityEngine;

namespace BuilderGame.Gameplay.InteractiveCells
{
    public class GroundCell : Cell
    {
        [Header("Growth Settings")]
        [SerializeField] 
        private FloatRangeStruct growthTime;

        [SerializeField]
        private Plant plantPrefab;

        private Plant currentPlant;
        private bool isSown;

        private void OnDisable()
        {
            currentPlant.OnGrowUp -= ActivateCell;
        }

        protected override void Interact()
        {
            base.Interact();
            IsInteractable = false;

            if (isSown == false)
                Sown();
            else
                Collect();
        }

        private void Sown() 
        {
            currentPlant = Instantiate(plantPrefab, transform);
            ExpectActivity();
            currentPlant.Planting();

            currentPlant.OnGrowUp += ActivateCell;

            cellType = Enums.CellType.SownGroundCell;
            isSown = true;
        }

        private void Collect() 
        {
            ExpectActivity();
            currentPlant.Collect();
        }

        private void ExpectActivity()
        {
            IsInteractable = false;
            currentPlant.SetGrowingTime(UnityEngine.Random.Range(growthTime.minValue, growthTime.maxValue));
        }

        private void ActivateCell() 
        {
            IsInteractable = true;
        }
    }
}