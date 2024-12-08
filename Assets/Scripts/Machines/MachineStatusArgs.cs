using System;
using UnityEngine.Events;

public class StatusChangeArgs : EventArgs
{
    public MachineType machine;
    public MachineStatus machineStatus;

    public StatusChangeArgs(MachineType m, MachineStatus status)
    {
        this.machine = m;
        this.machineStatus = status;
    }
}
