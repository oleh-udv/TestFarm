using BuilderGame.Collectable;
using UnityEngine;

namespace BuilderGame.Pools
{
    public class PlantPoolManager : MonoBehaviour
    {
        [SerializeField] 
        private CollectableItem tomatoPrefab;
        [SerializeField]
        private int startCount;

        private Pool<CollectableItem> tomatoPool;

        private void Awake()
        {
            tomatoPool = new Pool<CollectableItem>(tomatoPrefab, transform, startCount);
        }

        public CollectableItem GetPoolElement() 
        {
            return tomatoPool.GetFreeElement();
        }
    }
}