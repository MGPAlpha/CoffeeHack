using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : Machine
{
    [SerializeField]
    private CoffeeJug _coffeeJug;

    protected override void InteractMachine(Cup cup)
    {
        cup.AddIngredient(Ingredient.Coffee);
    }

    protected override void SetMachineType()
    {
        machineType = MachineType.Coffee;
    }

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
