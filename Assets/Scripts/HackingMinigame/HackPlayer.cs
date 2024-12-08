using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackPlayer : MonoBehaviour
{

    public static HackPlayer Instance {
        get; private set;
    }

    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float baseFirePeriod = 1;
    [SerializeField] private float firePeriodSpeedFactor = 5;

    [SerializeField] private bool tempFastFire;
    
    private bool atkHeld = false;


    public float MaxHealth {
        get {return maxHealth;}
        private set {maxHealth = value;}
    }
    [SerializeField] private float maxHealth = 50;

    public float Health {
        get {return health;}
        private set {health = value;}
    }
    [SerializeField] private float health;

    [SerializeField] private GameObject bulletPrefab;

    private PlayerInput m_playerInput;
    private InputAction m_turnAction;

    private float fireCooldown = 0;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_turnAction = m_playerInput.actions.FindAction("Turn");

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float turn = m_turnAction.ReadValue<float>();

        transform.rotation = Quaternion.Euler(0, 0, -turn * turnSpeed * Time.deltaTime) * transform.rotation;

        fireCooldown = Mathf.MoveTowards(fireCooldown, 0, Time.deltaTime * (tempFastFire ? firePeriodSpeedFactor : 1));

        if (tempFastFire && atkHeld && fireCooldown <= 0) {
            OnAttack();
        }
    }

    void OnAttack() {
        if (fireCooldown == 0) {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            fireCooldown = baseFirePeriod;
        }
    }

    void OnAttackHold(InputValue v) {
        Debug.Log("AttackHold");
        atkHeld = v.isPressed;

    }

    public void Damage(float val) {
        health -= val;
        if (health <= 0) {
            Debug.Log("Dead");
        }
    }
}
