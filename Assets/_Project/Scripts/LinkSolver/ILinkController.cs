using Grid;
using System;
using System.Collections.Generic;

namespace Link
{
    public interface ILinkController
    {
        event Action<IEnumerable<IGridSlot>> OnItemsLinked;
        void Init();
        void DeInit();
    }
}
