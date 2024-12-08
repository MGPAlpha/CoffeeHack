using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStatusTablet : MonoBehaviour
{
    public List<MachineStatusItem> machineStatusItems;

    private void Start()
    {
        CoroutineUtils.ExecuteAfterEndOfFrame(() => MachineManager._instance.machineStatusChanged += OnStatusChange, this);
    }

    private void OnDisable()
    {
        MachineManager._instance.machineStatusChanged -= OnStatusChange;
    }

    public void OnStatusChange(object sender, StatusChangeArgs args)
    {
        foreach (MachineStatusItem item in machineStatusItems)
        {
            if (item.machineType == args.machine)
            {
                item.OnStatusChange(args.machineStatus);
            }
        }
    }
}
