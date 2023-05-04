using BuilderGame.Gameplay.InteractiveCells;
using BuilderGame.Gameplay.SeedFields;
using BuilderGame.Gameplay.Unit.Rotation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Worker
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] 
        private SeedField seedField;
        [SerializeField]
        private UnitMovement unitMovement;
        [SerializeField]
        private UnitRotation unitRotation;

        private List<Cell> allCellList = new List<Cell>();
        private List<Cell> interactableCells = new List<Cell>();

        private bool isActive;

        private void OnValidate()
        {
            unitMovement = GetComponent<UnitMovement>();
            unitRotation = GetComponent<UnitRotation>();
        }

        private void OnDisable()
        {
            allCellList.ForEach(cell => cell.OnBecameInteract -= AddInteractableCell);
            interactableCells.ForEach(cell => cell.OnBecameInteract -= InteractCell);

            isActive = false;
        }

        public void Activate() 
        {
            if (isActive)
                return;

            allCellList = seedField.GetCurrentCells();
            allCellList.ForEach(cell => cell.OnBecameInteract += AddInteractableCell);

            AddInteractableCells(allCellList.Where(cell => cell.IsInteractable).ToList());
            isActive = true;
        }

        private void AddInteractableCell(Cell cell) 
        {
            interactableCells.Add(cell);
            cell.OnInteract += InteractCell;

            MoveToFirstCell();
        }

        private void AddInteractableCells(List<Cell> cells) 
        {
            interactableCells.AddRange(cells);
            cells.ForEach(cell => cell.OnInteract += InteractCell);

            MoveToFirstCell();
        }

        private void InteractCell(Cell cell) 
        {
            interactableCells.Remove(cell);
            cell.OnInteract -= InteractCell;

            MoveToFirstCell();
        }

        private void MoveToFirstCell() 
        {
            if (interactableCells.Count > 0)
            {
                Vector3 direction = GetDirectionToPoint(interactableCells[0].transform.position);
                unitMovement.SetMovementDirection(direction);
                unitRotation.SetRotationDirection(direction);
            }
            else 
            {
                unitMovement.SetMovementDirection(Vector3.zero);
                unitRotation.SetRotationDirection(Vector3.zero);
            }
        }

        private Vector3 GetDirectionToPoint(Vector3 point) 
        {
            return (point - transform.position).normalized;
        }
    }
}