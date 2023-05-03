using BuilderGame.Gameplay.InteractiveCells.Enums;
using BuilderGame.Gameplay.Unit.Interfaces;
using System;
using UnityEngine;

namespace BuilderGame.Gameplay.InteractiveCells
{
    public abstract class Cell : MonoBehaviour
    {
        [SerializeField]
        protected CellType cellType = CellType.None;

        public event Action<Cell> OnInteract;

        public bool IsInteractable { get; set; }
        public CellType CellType => cellType;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable)
                return;

            var unit = other.GetComponent<IInteractCells>();
            if (unit != null) 
            {
                unit.InteractCell(this);
                Interact();
            }
        }

        protected virtual void Interact() 
        {
            OnInteract?.Invoke(this);
        }
    }
}