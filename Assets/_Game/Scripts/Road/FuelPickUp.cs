using System;
using UnityEngine;

public class FuelPickUp : MonoBehaviour
{
    [SerializeField] private float fuelAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        FuelSystem fuelSystem = other.GetComponent<FuelSystem>();
        ComboSystem comboSystem = other.GetComponent<ComboSystem>();
    
        if (fuelSystem == null) return;

        int combo = comboSystem != null ? comboSystem.CurrentCombo : 0;
        float multiplier = 1f + (combo * 0.5f);
    
        fuelSystem.AddFuel(fuelAmount * multiplier);
        gameObject.SetActive(false);
    }
}
