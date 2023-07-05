using BuilderGame.Gameplay.Plants;
using BuilderGame.Gameplay.Unit;
using Structs;
using UnityEngine;
using Zenject;
using BuilderGame.Gameplay.InteractiveCells.Enums;

namespace BuilderGame.Gameplay.InteractiveCells
{
    public class GroundCell : Cell
    {
        [Header("Growth Settings")]
        [SerializeField] 
        private FloatRangeStruct growthTime;

        [Header("Plant")]
        [SerializeField]
        private PlantType plantType;

        private Plant currentPlant;
        private PlantsFactory plantsFactory;

        [Inject]
        public void Construct(PlantsFactory plantsFactory)
        {
            this.plantsFactory = plantsFactory;
        }

        private void OnDisable()
        {
            currentPlant.OnGrowUp -= ActivateCell;
        }

        protected override void Interact(UnitActions unit)
        {
            base.Interact(unit);
            IsInteractable = false;

            //AndriiCodeReview: Removed unnecessary "isSown" flag
            if (cellType != CellType.SownGroundCell)
                Sown();
            else
                Collect(unit);
        }

        private void Sown()
        {

            currentPlant = plantsFactory.Create(plantType, transform);
            ExpectActivity();
            currentPlant.Planting();

            currentPlant.OnGrowUp += ActivateCell;

            cellType = CellType.SownGroundCell;
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