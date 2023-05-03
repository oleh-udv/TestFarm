using BuilderGame.Gameplay.InteractiveCells;
using BuilderGame.Gameplay.InteractiveCells.Enums;
using BuilderGame.Gameplay.Unit.Animation;
using BuilderGame.Gameplay.Unit.Interfaces;
using DG.Tweening;
using UnityEngine;
using static UnityEditor.Progress;

namespace BuilderGame.Gameplay.Unit
{
    public class UnitActions : MonoBehaviour, IInteractCells
    {
        [SerializeField] 
        private UnitActionsAnimation actionsAnimation;

        [Space]
        [SerializeField]
        private Transform shovel;
        [SerializeField]
        private Transform seeds;

        [Header("Settings")]
        [SerializeField]
        private float activityTime = 1f;

        [Header("Effects")]
        [SerializeField]
        private float scaleTime;

        private Transform activeItem;
        private Tween activityTween;

        public void InteractCell(Cell cell)
        {
            switch (cell.CellType) 
            {
                case CellType.GrassCell:
                    Dig();
                    break;
                
                case CellType.GroundCell:
                    Planting();
                    break;
            }
        }

        private void Dig() 
        {
            StartActivityTimer();
            if (activeItem == shovel)
                return;

            StopAction();
            ActivateItem(shovel);
            actionsAnimation.SetDigAnimation();
        }

        private void Planting() 
        {
            StartActivityTimer();
            if (activeItem == seeds)
                return;

            StopAction();
            ActivateItem(seeds);
            actionsAnimation.SetPlantingAnimation();
        }

        private void ActivateItem(Transform item) 
        {
            activeItem = item;

            item.gameObject.SetActive(true);
            var startScale = item.localScale;
            item.localScale = Vector3.zero;
            item.DOScale(startScale, scaleTime);
        }

        private void StartActivityTimer() 
        {
            activityTween.Kill(false);
            activityTween = DOVirtual.DelayedCall(activityTime, StopAction);
        }

        private void StopAction() 
        {
            if (activeItem)
            {
                activeItem.gameObject.SetActive(false);
                actionsAnimation.ResetLayer();
                activeItem = null;
            }
        }
    }
}