using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineStatusItem : MonoBehaviour
{
    public MachineType machineType;
    public MachineStatus currStatus = MachineStatus.Coffee;
    public Color activeColor;
    public Color inactiveColor;
    public Color waitingColor;
    public Image coffeeStatusImage;
    public Image hackingStatusImage;

    public TextMeshProUGUI statusText;
    public Button toggleButton;

    public void OnStatusChange(MachineStatus status)
    {
        currStatus = status;
        statusText.text = status.ToString();
        toggleButton.interactable = status != MachineStatus.Waiting;
    }

    public void ToggleStatus()
    {
        print("toggle");
        if (currStatus == MachineStatus.Hacking)
        {
            SwitchMode(MachineStatus.Coffee);
        }

        if (currStatus == MachineStatus.Coffee)
        {
            SwitchMode(MachineStatus.Hacking);
        }

        if (currStatus == MachineStatus.Waiting)
        {
            //play error sound
        }
    }

    public void SwitchMode(MachineStatus status)
    {
        MachineManager.SwitchMode(machineType, status);
    }
}
