using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackEnemy : MonoBehaviour
{
    [SerializeField] private float speed = .5f;
    [SerializeField] private float health = 1;
    [SerializeField] private float damage = 1;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("HackPlayer")) {
            HackPlayer player = other.GetComponent<HackPlayer>();
            player.Damage(damage);
            Destroy(this.gameObject);
        }
    }

    public void Damage(float d) {
        health -= d;
        if (health <= 0) Destroy(this.gameObject);
    }
}
