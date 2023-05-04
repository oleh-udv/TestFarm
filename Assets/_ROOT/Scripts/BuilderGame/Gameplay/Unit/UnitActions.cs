using BuilderGame.Gameplay.InteractiveCells;
using BuilderGame.Gameplay.InteractiveCells.Enums;
using BuilderGame.Gameplay.Unit.Animation;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit
{
    public class UnitActions : MonoBehaviour
    {
        [SerializeField] 
        private UnitActionsAnimation actionsAnimation;

        [Space]
        [SerializeField]
        private Transform shovel;
        [SerializeField]
        private Transform seeds;
        [SerializeField]
        private Transform visual;

        [Header("Settings")]
        [SerializeField]
        private float activityTime = 1f;

        [Header("Effects")]
        [SerializeField]
        private float scaleItemTime = 0.5f;

        [Space]
        [SerializeField]
        private float punchTime = 0.15f;
        [SerializeField]
        private float punchForce = 0.1f;

        private Transform activeItem;
        private Tween punchTween;
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

        public void CollectItem() 
        {
            punchTween.Kill(true);
            punchTween = visual.DOPunchScale(Vector3.one * punchForce, punchTime);
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
            item.DOScale(startScale, scaleItemTime);
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