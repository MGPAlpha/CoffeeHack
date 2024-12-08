using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeJug : DragAndDrop, IInteractable
{
    [SerializeField]
    private CoffeeMachine _coffeeMachine;

    public int NumUses { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        NumUses = 0;
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

    public void FillJug()
    {
        NumUses = 3;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = Color.blue;
    }

    public void UseCoffee()
    {
        NumUses--;
    }
}
