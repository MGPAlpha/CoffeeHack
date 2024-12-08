using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : Machine
{
    [SerializeField] private CoffeeJug _coffeeJug;
    [SerializeField] private GameObject _coffeeButton;

    public bool brewingCoffee;

    public float secondsToMakeCoffee = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        brewingCoffee = false;
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
        if (gO.Equals(_coffeeButton))
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
        
        _coffeeJug.canGrab = false;
        MachineManager.SwitchMode(machineType, MachineStatus.Waiting);
        CoroutineUtils.ExecuteAfterDelay(() => MakeCoffee(), this, secondsToMakeCoffee/_coffeeJug.MaxUses);
    }

    private void MakeCoffee()
    {
        _coffeeJug.NumUses++;
        if (_coffeeJug.NumUses < _coffeeJug.MaxUses)
        {
            CoroutineUtils.ExecuteAfterDelay(() => MakeCoffee(), this, secondsToMakeCoffee / _coffeeJug.MaxUses);
        } else
        {
            MachineManager.SwitchMode(machineType, MachineStatus.Coffee);
            _coffeeJug.canGrab = true;
        }
    }
}
