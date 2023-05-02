using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuilderGame.Gameplay.InteractiveCells
{
    public class GrassCell : Cell
    {
        [Space]
        [SerializeField]
        private Transform visual;

        [Header("Effects")]
        [SerializeField]
        private float scaleTime = 0.25f;

        private void Start()
        {
            IsInteractable = true;
        }

        protected override void Interact()
        {
            base.Interact();

            visual.DOScale(Vector3.zero, scaleTime)
                .OnComplete(() => 
                {
                    gameObject.SetActive(false);
                });
        }
    }
}
