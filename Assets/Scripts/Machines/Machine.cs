using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour, IInteractable
{
    protected MachineType machineType;
    protected MachineStatus machineStatus;

    public void Awake()
    {
        SetMachineType();
    }
    public void Start()
    {
        machineStatus = MachineManager._instance.machineStatusDictionary[machineType];
    }

    public void OnEnable()
    {
        CoroutineUtils.ExecuteAfterEndOfFrame(() => MachineManager._instance.machineStatusChanged += ChangeStatus, this);
    }

    public void OnDisable()
    {
        MachineManager._instance.machineStatusChanged -= ChangeStatus;
    }

    protected void ChangeStatus(object sender, StatusChangeArgs args)
    {
        if (machineType == args.machine)
        {
            machineStatus = args.machineStatus;
        }
    }
    public void Interact(DragAndDrop drag)
    {
        if (drag.GetType() != typeof(Cup))
        {
            return;
        }
        Cup cup = (Cup)drag;
        InteractMachine(cup);
    }

    protected abstract void SetMachineType();
    protected abstract void InteractMachine(Cup cup);
}
