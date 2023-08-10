using UnityEngine;
using System.Collections;

namespace Hitcode_RoomEscape.UI
{
  
    public interface IListScrollerDelegate
    {
       
        int GetNumberOfCells(ListScroller scroller);

      
        float GetCellViewSize(ListScroller scroller, int dataIndex);

       
        ListScrollerCellView GetCellView(ListScroller scroller, int dataIndex, int cellIndex);
    }
}