using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractable
{
    public void Interact(DragAndDrop drag)
    {
        if (drag.GetType() != typeof(Cup))
        {
            return;
        }
        Cup cup = (Cup)drag;
        cup.TrashCup();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
