using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveTextSlot : TextSlot, ISelectHandler, IDeselectHandler
{

    public Action<Move> updateMPcost;

    public Move currentMove;

    [SerializeField] Button button;

    public void SetCurrentMove(Move currentMove)
    {
        SetText(currentMove.Base.name);
        this.currentMove = currentMove;
    }


       
   
    public Button GetButtonComponent
    {
        get { return button; }
    }

    public new void OnSelect(BaseEventData eventData)
    {
        updateMPcost?.Invoke(currentMove);

    }


}
