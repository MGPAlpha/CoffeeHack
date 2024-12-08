using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackFreezer : MonoBehaviour
{

    private SpriteRenderer sp;
    private Collider2D col;
    
    [SerializeField] private float speedReductionFactor = .15f;
    [SerializeField] private float freezeDamage = .1f;
    [SerializeField] private float freezeDamageRate = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        SetVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        SetVisibility();
    }

    void SetVisibility() {
        if (MachineManager._instance.machineStatusDictionary[MachineType.Fridge] == MachineStatus.Hacking) {
            sp.enabled = true;
            col.enabled = true;
        }
        else {
            sp.enabled = false;
            col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("freeze zone triggered");
        if (other.gameObject.layer == LayerMask.NameToLayer("HackEnemy")) {
            other.GetComponent<HackEnemy>().ApplyFreeze(speedReductionFactor, freezeDamage, freezeDamageRate);
        }
    }
}
