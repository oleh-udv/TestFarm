using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Pools
{
    public class Pool<T> where T : MonoBehaviour
    {
        private List<T> pool;
        private T Prefab { get; }
        private Transform Container { get; }

        public Pool(T prefab, Transform container, int count)
        {
            Prefab = prefab;
            Container = container;
            CreatePool(count);
        }
        
        public bool HasFreeElement(out T element)
        {
            foreach (var mono in pool)
            {
                if (!mono.gameObject.activeSelf)
                {
                    element = mono;
                    return true;
                }
            }

            element = null;
            return false;
        }
        
        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;
            else
                return CreateObject();
        }

        private void CreatePool(int count)
        {
            pool = new List<T>(count);
            for (int i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(Prefab, Container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            pool.Add(createdObject);
            return createdObject;
        }
    }
}
