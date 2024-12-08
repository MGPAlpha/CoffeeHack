using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressoMachine : Machine
{
    [SerializeField] protected Collider2D _overlapTrigger;
    [SerializeField] protected Collider2D _rigidCollider;
    [SerializeField] protected GameObject _attachPoint;

    public float secondsToBrew = 2.0f;

    private GameObject heldCup;
    private bool brew;
    protected override void InteractMachine(Cup cup)
    {
        if (machineStatus != MachineStatus.Coffee)
        {
            //play error sound
            return;
        }
        MachineManager.SwitchMode(machineType, MachineStatus.Running);
        cup.canGrab = false;
        cup.transform.position = _attachPoint.transform.position;
        heldCup = cup.gameObject;
        brew = true;
        CoroutineUtils.ExecuteAfterDelay(() => BrewExpresso(heldCup.GetComponent<Cup>()), this, secondsToBrew);

    }

    protected override void SetMachineType()
    {
        machineType = MachineType.Espresso;
    }

    public void BrewExpresso(Cup cup)
    {
        cup.AddIngredient(Ingredient.Espresso);
        cup.canGrab = true;
        heldCup = null;
        brew = false;
        MachineManager.SwitchMode(machineType, MachineStatus.Coffee);
    }
    public void Start()
    {
        base.Start();
        brew = false;
    }

    public void Update()
    {
        if (brew)
        {
            heldCup.transform.position = _attachPoint.transform.position;
        }
    }
}
