using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackFreezer : MonoBehaviour
{

    private SpriteRenderer sp;
    private Collider2D col;
    private LineRenderer ln;
    
    [SerializeField] private float speedReductionFactor = .15f;
    [SerializeField] private float freezeDamage = .1f;
    [SerializeField] private float freezeDamageRate = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        ln = GetComponent<LineRenderer>();

        ln.positionCount = 0;
        int positionCount = 16;
        ln.positionCount = positionCount;
        for (int i = 0; i < positionCount; i++) {
            float degrees = (float)i/positionCount * 360f;
            Vector3 pos = (Quaternion.Euler(0, 0, degrees) * Vector3.left) * 0.5f;
            ln.SetPosition(i, pos);
        }

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
            ln.enabled = true;
        }
        else {
            sp.enabled = false;
            col.enabled = false;
            ln.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("freeze zone triggered");
        if (other.gameObject.layer == LayerMask.NameToLayer("HackEnemy")) {
            other.GetComponent<HackEnemy>().ApplyFreeze(speedReductionFactor, freezeDamage, freezeDamageRate);
        }
    }
}
