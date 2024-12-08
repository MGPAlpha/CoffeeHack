using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{

    public static Fridge Instance {
        get; private set;
    }

    void Awake() {
        Instance = this;
    }
    
    [SerializeField] public float maxUnpluggedTime = 15;

    public float timeUnplugged = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MachineManager._instance.machineStatusDictionary[MachineType.Fridge] != MachineStatus.Hacking) {
            timeUnplugged = 0;
        } else {
            timeUnplugged += Time.deltaTime;
            if (timeUnplugged > maxUnpluggedTime) {
                timeUnplugged = 0;
                StrikeManger._instance.AddStrike();
                StrikeManger._instance.AddStrike();
                MachineManager.SwitchMode(MachineType.Fridge, MachineStatus.Coffee);
            }
        }
    }
}
