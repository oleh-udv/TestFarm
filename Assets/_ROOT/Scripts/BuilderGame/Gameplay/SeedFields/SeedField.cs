using BuilderGame.Gameplay.InteractiveCells;
using BuilderGame.Gameplay.SeedFields.Structs;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace BuilderGame.Gameplay.SeedFields
{
    public class SeedField : MonoBehaviour
    {
        [SerializeField]
        private bool isActiveByStart = true;

        [Space]
        [SerializeField]
        private GameObject workerField;
        [SerializeField]
        private float scaleTime = 0.2f;

        [Space]
        [SerializeField]
        private List<CellLayers> cellLayers;

        private int currentLayer = 0;
        private int interactCount = 0;

        private void Start()
        {
            if (isActiveByStart)
                ActivateLayer(0);

            workerField.SetActive(false);
        }

        public void ActivateLayer(int layer) 
        {
            if (cellLayers.Count > 0) 
            {
                foreach (var cell in cellLayers[layer].cells) 
                {
                    cell.IsInteractable = true;
                    cell.OnInteract += InteractCell;
                }
            }
        }

        public List<Cell> GetCurrentCells() 
        {
            return currentLayer == cellLayers.Count ? 
                cellLayers[cellLayers.Count - 1].cells : 
                cellLayers[currentLayer].cells;
        }

        //Only in edit mode
        public void FillLists() 
        {
            foreach (var layer in cellLayers) 
            {
                layer.cells.Clear();
                layer.cells.AddRange(GetComponentsInChildren<Cell>().Where(cell => cell.CellType == layer.type));
            }
        }

        private void InteractCell(Cell cell) 
        {
            cell.OnInteract -= InteractCell;
            interactCount++;

            if (cellLayers[currentLayer].cells.Count == interactCount) 
            {
                currentLayer++;
                interactCount = 0;

                if (cellLayers.Count > currentLayer)
                    ActivateLayer(currentLayer);
                else
                    CompleteField();
            }
        }

        private void CompleteField() 
        {
            if (workerField)
            {
                workerField.SetActive(true);
                workerField.transform.localScale = Vector3.zero;
                workerField.transform.DOScale(Vector3.one, scaleTime);
            }
        }
    }
}
