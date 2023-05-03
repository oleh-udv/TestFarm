using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitActionsAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float transitionTime = 0.2f;

        private Tween waightTween;
        private float weight;
        private bool isActiveLayer;

        private const string DigTrigger = "Dig";
        private const string PlantingTrigger = "Planting";
        private const int LayerIndex = 1;


        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void SetDigAnimation()
        {
            if(isActiveLayer == false)
                SetLayerWaight(1f, transitionTime);

            isActiveLayer = true;
            ResetTriggers();
            animator.SetTrigger(DigTrigger);
        }
        
        public void SetPlantingAnimation()
        {
            if (isActiveLayer == false)
                SetLayerWaight(1f, transitionTime);

            isActiveLayer = true;
            ResetTriggers();
            animator.SetTrigger(PlantingTrigger);
        }

        public void ResetLayer() 
        {
            if (isActiveLayer == true)
                SetLayerWaight(0f, transitionTime);

            isActiveLayer = false;
        }

        private void SetLayerWaight(float value, float time) 
        {
            waightTween.Kill();

            waightTween = DOVirtual.Float(weight, value, time, (f) =>
            {
                weight = f;
                animator.SetLayerWeight(LayerIndex, weight);
            }).SetEase(Ease.Linear);
        }

        private void ResetTriggers()
        {
            animator.ResetTrigger(DigTrigger);
            animator.ResetTrigger(PlantingTrigger);
        }
    }
}