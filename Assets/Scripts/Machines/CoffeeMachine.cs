using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : Machine
{
    [SerializeField] private CoffeeJug _coffeeJug;

    public float secondsToMakeCoffee = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public void OnEnable()
    {
        base.OnEnable();
        CoroutineUtils.ExecuteAfterEndOfFrame(() => DragController.ClickAction += Click, this);
    }

    public void OnDisable()
    {
        base.OnDisable();
    }

    protected override void InteractMachine(Cup cup)
    {
        if (_coffeeJug.NumUses <= 0)
        {
            //play error noise
            return;
        }

        if (cup.ingredients.Contains(Ingredient.Coffee))
        {
            return;
        }
        cup.AddIngredient(Ingredient.Coffee);
        _coffeeJug.UseCoffee();
    }

    protected override void SetMachineType()
    {
        machineType = MachineType.Coffee;
    }

    private void Click(GameObject gO)
    {
        if (gO.Equals(gameObject))
        {
            StartMakeCoffee();
        }
    }

    public void StartMakeCoffee()
    {
        Debug.Log("Start Make Coffee");
        if (machineStatus != MachineStatus.Coffee)
        {
            //play error sound
            return;
        }
        MachineManager.SwitchMode(machineType, MachineStatus.Waiting);
        CoroutineUtils.ExecuteAfterDelay(() => MakeCoffee(), this, secondsToMakeCoffee);
    }

    private void MakeCoffee()
    {
        Debug.Log("Make Coffee!");
        _coffeeJug.FillJug();
    }
}
