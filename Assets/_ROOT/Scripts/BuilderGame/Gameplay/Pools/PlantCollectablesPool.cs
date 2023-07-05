using BuilderGame.Gameplay.Collectable;
using BuilderGame.Gameplay.Plants;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace BuilderGame.Gameplay.Pools
{
    public class PlantCollectablesPool : MonoBehaviour
    {
        [Inject]
        private PlantsSettings Settings { get; set; }

        private Dictionary<PlantType, Pool<CollectableItem>> pools;

        private void Awake() => Initialize();

        private void Initialize()
        {
            pools = new Dictionary<PlantType, Pool<CollectableItem>>();
            foreach (var plantData in Settings.PlantData)
            {
                var pool = new Pool<CollectableItem>(plantData.PlantCollectable, transform, plantData.PoolInitialCount);
                pools.Add(plantData.Type, pool);
            }
        }

        public CollectableItem GetPoolElement(PlantType elementType) => pools[elementType].GetFreeElement();
    }
}