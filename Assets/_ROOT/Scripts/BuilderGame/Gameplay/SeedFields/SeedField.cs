using BuilderGame.Gameplay.InteractiveCells;
using BuilderGame.Gameplay.SeedFields.Structs;
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
        private List<CellLayers> cellLayers;

        private int currentLayer = 0;

        private void Start()
        {
            if (isActiveByStart)
                ActivateLayer(0);
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
            cellLayers[currentLayer].cells.Remove(cell);

            if (cellLayers[currentLayer].cells.Count == 0) 
            {
                currentLayer++;
                if (cellLayers.Count > currentLayer)
                    ActivateLayer(currentLayer);
                else
                    CompleteField();
            }
        }

        private void CompleteField() 
        {
        
        }
    }
}