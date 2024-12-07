using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MachineManager : MonoBehaviour
{
  public Dictionary<MachineType, MachineStatus> machineStatusDictionary = new Dictionary<MachineType, MachineStatus>();
  public static MachineManager _instance;

  public System.EventHandler<StatusChangeArgs> machineStatusChanged;

  private void Awake()
  {
    _instance = this;
    machineStatusDictionary = new Dictionary<MachineType, MachineStatus>();
    foreach (MachineType m in Enum.GetValues(typeof(MachineType)))
    {
      machineStatusDictionary.Add(m, MachineStatus.Coffee);
    }
  }

  private void SetStatus(MachineType m, MachineStatus status)
  {
    machineStatusDictionary[m] = status;
    machineStatusChanged?.Invoke(this, new StatusChangeArgs(m, status));
  }

  protected void SwitchModeInternal(MachineType m, MachineStatus status)
  {
    MachineStatus currStatus = machineStatusDictionary[m];
    if (currStatus == status || currStatus == MachineStatus.Waiting)
    {
      //play error sound
      return;
    }
    if (currStatus == MachineStatus.Hacking && status == MachineStatus.Coffee)
    {
      SetStatus(m, MachineStatus.Waiting);
      CoroutineUtils.ExecuteAfterDelay(() => SetStatus(m, status), this, 5);
    }
    if (currStatus == MachineStatus.Coffee && status == MachineStatus.Hacking)
    {
      SetStatus(m, status);
    }
  }

  public static void SwitchMode(MachineType m, MachineStatus status)
  {
    _instance.SwitchModeInternal(m, status);
  }
}