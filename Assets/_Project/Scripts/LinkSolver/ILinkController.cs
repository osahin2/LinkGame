using Grid;
using System;
using System.Collections.Generic;

namespace Link
{
    public interface ILinkController
    {
        event Action<IEnumerable<IGridSlot>> OnItemsLinked;
        public int MinLinkCount { get; }
        void Init();
        void DeInit();
    }
}
