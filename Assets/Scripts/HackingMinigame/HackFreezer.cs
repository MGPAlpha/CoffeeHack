using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackFreezer : MonoBehaviour
{
    
    [SerializeField] private float speedReductionFactor = .15f;
    [SerializeField] private float freezeDamage = .1f;
    [SerializeField] private float freezeDamageRate = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("freeze zone triggered");
        if (other.gameObject.layer == LayerMask.NameToLayer("HackEnemy")) {
            other.GetComponent<HackEnemy>().ApplyFreeze(speedReductionFactor, freezeDamage, freezeDamageRate);
        }
    }
}
