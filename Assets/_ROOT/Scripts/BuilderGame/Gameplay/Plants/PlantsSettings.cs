using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuilderGame.Gameplay.Plants
{
    [CreateAssetMenu(fileName = nameof(PlantsSettings), menuName = "TestFarm/PlantsSettings", order = 0)]
    public class PlantsSettings : ScriptableObject
    {
        [field: SerializeField]
        public List<PlantData> PlantData { get; private set; }

        private Dictionary<PlantType, PlantData> plantDataDictionary;

        public PlantData DataFor(PlantType type)
        {
            TryInitializeDictionary();
            return plantDataDictionary[type];
        }

        private void TryInitializeDictionary()
        {
            if (DictionaryNotInitialized())
            {
                InitializeDictionary();
            }
        }

        private void InitializeDictionary() => 
            plantDataDictionary = PlantData.ToDictionary(x => x.Type, x => x);

        private bool DictionaryNotInitialized() => 
            plantDataDictionary == null || plantDataDictionary.Count == 0;
    }
}