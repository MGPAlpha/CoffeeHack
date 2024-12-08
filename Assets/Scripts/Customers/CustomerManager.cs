using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager>
{
    public List<Customer> customers;
    public GameObject customerPrefab;
    public Vector3 spawnLocation;
    public Vector3 offset;

    public void RemoveCustomer(Customer customer)
    {
        customers.Remove(customer);
        Destroy(customer.gameObject);
    }

    public void SpawnCustomer(int complexity = 3)
    {
        GameObject go = Instantiate(customerPrefab, spawnLocation, Quaternion.identity);
        Customer c = go.GetComponent<Customer>();
        c.SetOrder(OrderGenerator.GenerateOrder(complexity));
    }

    public void PositionCustomers()
    {

    }
}
