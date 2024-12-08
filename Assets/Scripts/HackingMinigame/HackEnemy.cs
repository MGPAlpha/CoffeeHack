using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackEnemy : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;
    private SpriteRenderer sprite;
    
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject hitPrefab;

    [SerializeField] private float speed = .5f;
    [SerializeField] private float health = 1;
    [SerializeField] private float damage = 1;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        CheckVisibility();
    }

    void CheckVisibility() {
        if (MachineManager._instance.machineStatusDictionary[MachineType.Coffee] == MachineStatus.Hacking) {
            sprite.enabled = true;
        } else {
            sprite.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckVisibility();
        if (target) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("HackPlayer")) {
            HackPlayer player = other.GetComponent<HackPlayer>();
            player.Damage(damage);
            float rotAngle = Vector3.Angle(target.position - transform.position, Vector3.down);
            if (target.position.x < transform.position.x ) rotAngle = -rotAngle;
            Instantiate(hitPrefab, (transform.position + 3*target.position) / 4, Quaternion.Euler(0, 0, rotAngle));
            DetachParticleTrail();
            Destroy(this.gameObject);
        }
    }

    public void Damage(float d) {
        health -= d;
        if (health <= 0) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            DetachParticleTrail();
            Destroy(this.gameObject);
        }
    }

    void DetachParticleTrail() {
            particleSystem.transform.parent = null;
            particleSystem.Stop();
    }
}
