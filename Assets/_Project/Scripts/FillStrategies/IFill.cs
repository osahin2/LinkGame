using Grid;
using System;
using System.Collections.Generic;
namespace BoardSolvers
{
    public interface IFill
    {
        void Fill(IEnumerable<IGridSlot> solvedGrids = null, Action onCompleted = null);
    }
}
