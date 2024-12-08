using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 1; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() {
        transform.position += transform.up * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collided");
        Destroy(gameObject);

        if (other.gameObject.layer == LayerMask.NameToLayer("HackEnemy")) {
            other.GetComponent<HackEnemy>().Damage(1);
        }
    }


}
