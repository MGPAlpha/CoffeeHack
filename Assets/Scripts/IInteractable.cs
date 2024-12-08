using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public int Priority { get => 1; }
    public void Interact(DragAndDrop drag);
}