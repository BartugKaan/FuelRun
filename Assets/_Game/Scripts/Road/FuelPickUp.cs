using System;
using UnityEngine;

public class FuelPickUp : MonoBehaviour
{
    [SerializeField] private float fuelAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        
        FuelSystem fuelSystem = other.GetComponent<FuelSystem>();
        if(fuelSystem == null) return;
        
        fuelSystem.AddFuel(fuelAmount);
        gameObject.SetActive(false);
    }
}
