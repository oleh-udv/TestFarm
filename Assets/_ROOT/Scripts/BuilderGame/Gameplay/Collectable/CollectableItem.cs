using DG.Tweening;
using System;
using UnityEngine;

namespace BuilderGame.Gameplay.Collectable
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

            //AndriiCodeReview: Added move sequence
            var moveSequence = DOTween.Sequence();
            moveSequence
                .Join(transform.DOScale(Vector3.one, scaleTime))
                .Join(transform.DOLocalJump(Vector3.up, jumpPower, 1, moveTime))
                .AppendCallback(() => OnComplete?.Invoke())
                .Append(transform.DOScale(Vector3.zero, scaleTime))
                .OnComplete(() => ResetCollectable(startParent));
        }

        private void ResetCollectable(Transform startParent)
        {
            transform.parent = startParent;
            gameObject.SetActive(false);
        }
    }
}