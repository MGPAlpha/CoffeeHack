using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackPlayer : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float baseFirePeriod = 1;
    [SerializeField] private float firePeriodSpeedFactor = 5;

    [SerializeField] private GameObject bulletPrefab;

    private PlayerInput m_playerInput;
    private InputAction m_turnAction;

    private float fireCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_turnAction = m_playerInput.actions.FindAction("Turn");
    }

    // Update is called once per frame
    void Update()
    {
        float turn = m_turnAction.ReadValue<float>();

        transform.rotation = Quaternion.Euler(0, 0, -turn * turnSpeed * Time.deltaTime) * transform.rotation;

        fireCooldown = Mathf.MoveTowards(fireCooldown, 0, Time.deltaTime);
    }

    void OnAttack() {
        if (fireCooldown == 0) {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            fireCooldown = baseFirePeriod;
        }
    }
}
