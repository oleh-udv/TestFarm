using BuilderGame.Gameplay.Plants;
using BuilderGame.Gameplay.Unit;
using Structs;
using UnityEngine;
using Zenject;

namespace BuilderGame.Gameplay.InteractiveCells
{
    public class GroundCell : Cell
    {
        [Header("Growth Settings")]
        [SerializeField] 
        private FloatRangeStruct growthTime;

        [SerializeField]
        private Plant plantPrefab;

        private DiContainer diContainer;
        private Plant currentPlant;
        private bool isSown;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        private void OnDisable()
        {
            currentPlant.OnGrowUp -= ActivateCell;
        }

        protected override void Interact(UnitActions unit)
        {
            base.Interact(unit);
            IsInteractable = false;

            if (isSown == false)
                Sown();
            else
                Collect(unit);
        }

        private void Sown() 
        {
            currentPlant = diContainer.InstantiatePrefab(plantPrefab, transform).GetComponent<Plant>();
            ExpectActivity();
            currentPlant.Planting();

            currentPlant.OnGrowUp += ActivateCell;

            cellType = Enums.CellType.SownGroundCell;
            isSown = true;
        }

        private void Collect(UnitActions unit) 
        {
            ExpectActivity();
            currentPlant.Collect(unit);
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