using BuilderGame.Gameplay.InteractiveCells;
using BuilderGame.Gameplay.InteractiveCells.Enums;
using System;
using System.Collections.Generic;

namespace BuilderGame.Gameplay.SeedFields.Structs
{
    [Serializable]
    public struct CellLayers
    {
        public CellType type;
        public List<Cell> cells; 
    }
}
