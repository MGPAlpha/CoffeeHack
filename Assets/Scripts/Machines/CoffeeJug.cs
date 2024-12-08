using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeJug : DragAndDrop, IInteractable
{
    [SerializeField]
    private CoffeeMachine _coffeeMachine;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void Interact(DragAndDrop drag)
    {
        _coffeeMachine.Interact(drag);
    }
}
