using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    public List<Customer> customers;
    public GameObject customerPrefab;
    public Vector3 spawnLocation;
    public Vector3 offset;

    private float timeSinceLastCustomer = 0;

    private void Update()
    {
        timeSinceLastCustomer += Time.deltaTime;
        if (timeSinceLastCustomer > 5)
        {
            timeSinceLastCustomer = 0;
            SpawnCustomer((int)Mathf.Floor(Random.Range(0, 6)));
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
        GameObject go = Instantiate(customerPrefab, spawnLocation, Quaternion.identity);
        Customer c = go.GetComponent<Customer>();
        c.SetOrder(OrderGenerator.GenerateOrder(complexity));
        customers.Add(c);
        PositionCustomers();
    }

    public void PositionCustomers()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            customers[i].gameObject.transform.position = spawnLocation + i * offset;
        }
    }
}
