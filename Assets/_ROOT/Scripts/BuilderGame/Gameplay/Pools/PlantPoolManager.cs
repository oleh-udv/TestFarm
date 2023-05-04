using BuilderGame.Gameplay.Collectable;
using UnityEngine;

namespace BuilderGame.Gameplay.Pools
{
    public class PlantPoolManager : MonoBehaviour
    {
        [SerializeField] 
        private CollectableItem tomatoPrefab;
        [SerializeField]
        private int tomatoCount;

        [Space]
        [SerializeField] 
        private CollectableItem cornPrefab;
        [SerializeField]
        private int cornCount;

        private Pool<CollectableItem> tomatoPool;
        private Pool<CollectableItem> cornPool;

        private void Awake()
        {
            tomatoPool = new Pool<CollectableItem>(tomatoPrefab, transform, tomatoCount);
            cornPool = new Pool<CollectableItem>(cornPrefab, transform, cornCount);
        }

        public CollectableItem GetPoolElement(CollectableItemsEnum elementType) 
        {
            switch (elementType)
            {
                case CollectableItemsEnum.Tomato:
                    return tomatoPool.GetFreeElement();
                case CollectableItemsEnum.Corn:
                    return cornPool.GetFreeElement();
                default:
                    return tomatoPool.GetFreeElement();
            }
        }
    }
}