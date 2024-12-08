using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderMachine : Machine
{
    [SerializeField] protected Collider2D _overlapTrigger;
    [SerializeField] protected Collider2D _rigidCollider;
    [SerializeField] protected GameObject _attachPoint;

    public float secondsToBlend = 6.0f;

    private GameObject heldCup;
    private bool blending;
    protected override void InteractMachine(Cup cup)
    {
        if (machineStatus != MachineStatus.Coffee)
        {
            //play error sound
            return;
        }

        if (cup.ingredients.Contains(Ingredient.Blender))
        {
            return;
        }
        MachineManager.SwitchMode(machineType, MachineStatus.Running);
        cup.canGrab = false;
        cup.transform.position = _attachPoint.transform.position;
        heldCup = cup.gameObject;
        blending = true;
        CoroutineUtils.ExecuteAfterDelay(() => Blend(heldCup.GetComponent<Cup>()), this, secondsToBlend);

    }

    protected override void SetMachineType()
    {
        machineType = MachineType.Blender;
    }

    public void Blend(Cup cup)
    {
        cup.AddIngredient(Ingredient.Blender);
        cup.canGrab = true;
        heldCup = null;
        blending = false;
        MachineManager.SwitchMode(machineType, MachineStatus.Coffee);
    }

    public void Start()
    {
        base.Start();
        blending = false;
    }

    public void Update()
    {
        if (blending)
        {
            heldCup.transform.position = _attachPoint.transform.position;
        }
    }
}
