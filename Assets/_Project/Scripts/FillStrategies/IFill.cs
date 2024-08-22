using Grid;
using System.Collections.Generic;
namespace BoardSolvers
{
    public interface IFill
    {
        void Fill(IEnumerable<IGridSlot> solvedGrids = null);
    }
}
