using BuilderGame.Gameplay.Collectable;
using UnityEngine;
using Zenject;

namespace BuilderGame.Gameplay.Plants
{
    public class PlantsFactory : IFactory<PlantType,Transform,Plant>
    {
        private readonly IInstantiator instantiator;
        private readonly PlantsSettings settings;

        public PlantsFactory(IInstantiator instantiator,PlantsSettings settings)
        {
            this.instantiator = instantiator;
            this.settings = settings;
        }
        
        public Plant Create(PlantType plantType, Transform parent)
        {
            var data = settings.DataFor(plantType);

            return instantiator.InstantiatePrefabForComponent<Plant>(data.PlantPrefab, parent);
        }
    }
}