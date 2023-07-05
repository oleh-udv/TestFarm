using System;
using BuilderGame.Gameplay.Collectable;

namespace BuilderGame.Gameplay.Plants
{
    [Serializable]
    public class PlantData
    {
        public PlantType Type;
        public Plant PlantPrefab;
        public CollectableItem PlantCollectable;
        public int PoolInitialCount = 10;
    }
}