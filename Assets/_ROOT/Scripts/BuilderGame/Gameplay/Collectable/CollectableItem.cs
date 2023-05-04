using DG.Tweening;
using System;
using UnityEngine;

namespace BuilderGame.Collectable
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField]
        private float scaleTime = 0.1f;
        [SerializeField]
        private float moveTime = 0.2f;
        [SerializeField]
        private float jumpPower = 0.5f;

        public void MoveToPoint(Transform unitTransform, Action OnComplete = null) 
        {
            gameObject.SetActive(true);

            Transform startParent = transform.parent;
            transform.parent = unitTransform;
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, scaleTime);

            transform.DOLocalJump(Vector3.up, jumpPower, 1, moveTime)
                .OnComplete(() => 
                {
                    transform.DOScale(Vector3.zero, scaleTime)
                        .OnComplete(() => 
                        {
                            transform.parent = startParent;
                            gameObject.SetActive(false);
                        });

                    OnComplete?.Invoke();
                });
        }
    }
}