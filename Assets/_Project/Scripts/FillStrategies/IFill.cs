using Grid;
using System;
using System.Collections.Generic;
namespace FillStrategies
{
    public interface IFill
    {
        void Fill(IEnumerable<IGridSlot> solvedGrids = null, Action onCompleted = null);
    }
}
