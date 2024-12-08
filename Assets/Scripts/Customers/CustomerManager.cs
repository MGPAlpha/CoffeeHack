using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    public List<Customer> customers;
    public GameObject customerPrefab;
    public Transform spawnLocation;
    public Vector3 offset;

    [SerializeField] private float overallDuration = 10*60;
    private float timer = 0;

    private float customersEarned = 0;

    [SerializeField] private float baseCustomerEarnRate = .05f;
    [SerializeField] private AnimationCurve customerEarnRateCurve;

    [SerializeField] private float baseCustomerComplexity = 5;
    [SerializeField] private AnimationCurve customerComplexityCurve;

    int nextComplexity = 0;

    void Awake() {
        if (_instance == null) {
            InitializeSingleton();
        }
    }

    private void Update()
    {
        timer = Mathf.MoveTowards(timer, overallDuration, Time.deltaTime);
        float timePercent = timer/overallDuration;
        customersEarned += baseCustomerEarnRate * customerEarnRateCurve.Evaluate(timePercent) * Time.deltaTime;
        if (customersEarned >= nextComplexity + 1)
        {
            Debug.Log("spawning customer");
            customersEarned -= nextComplexity + 1;
            SpawnCustomer(nextComplexity);
            int newComplexity = (int)Mathf.Floor(Random.Range(0, baseCustomerComplexity * customerComplexityCurve.Evaluate(timePercent)));
            nextComplexity = newComplexity;
        }
    }

    public void RemoveCustomer(Customer customer)
    {
        customers.Remove(customer);
        Destroy(customer.gameObject);
        PositionCustomers();
    }

    public void SpawnCustomer(int complexity = 3)
    {
        GameObject go = Instantiate(customerPrefab, spawnLocation.position, Quaternion.identity);
        Customer c = go.GetComponent<Customer>();
        c.SetOrder(OrderGenerator.GenerateOrder(complexity));
        customers.Add(c);
        PositionCustomers();
    }

    public void PositionCustomers()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            customers[i].gameObject.transform.position = spawnLocation.position + i * offset;
        }
    }
}
