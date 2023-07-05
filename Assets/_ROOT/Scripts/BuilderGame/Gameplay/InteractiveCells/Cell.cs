using BuilderGame.Gameplay.InteractiveCells.Enums;
using BuilderGame.Gameplay.Unit;
using System;
using UnityEngine;

namespace BuilderGame.Gameplay.InteractiveCells
{
    public abstract class Cell : MonoBehaviour
    {
        [SerializeField]
        protected CellType cellType = CellType.None;

        public event Action<Cell> OnInteract;
        public event Action<Cell> OnBecameInteract;

        private bool isInteractable;

        public bool IsInteractable 
        {
            get 
            { 
                return isInteractable; 
            } 
            set 
            { 
                if (value)
                    OnBecameInteract?.Invoke(this);

                isInteractable = value;
            } 
        }

        public CellType CellType => cellType;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable)
                return;
            
            //AndriiCodeReview: Changed GetComponent to TryGetComponent
            if (other.TryGetComponent<UnitActions>(out var unit)) 
            {
                unit.InteractCell(this);
                Interact(unit);
            }
        }

        protected virtual void Interact(UnitActions unit) 
        {
            OnInteract?.Invoke(this);
        }
    }
}