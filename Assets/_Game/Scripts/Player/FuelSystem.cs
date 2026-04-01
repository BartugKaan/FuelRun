using System;
using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float fuelDrainRate = 10f;

    private float _currentFuel;
    private bool _isActive = true;
    
    public float FuelPercentage => _currentFuel / maxFuel;

    private void OnEnable()
    {
        GameEvents.OnGameOver += StopSystem;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= StopSystem;
    }

    private void Start()
    {
        _currentFuel = maxFuel;
        GameEvents.TriggerFuelChanged(FuelPercentage);
    }

    private void Update()
    {
        if(!_isActive) return;
        
        DrainFuel();
    }

    private void DrainFuel()
    {
        _currentFuel -= fuelDrainRate * Time.deltaTime;
        _currentFuel = Mathf.Clamp(_currentFuel, 0f, maxFuel);
        
        GameEvents.TriggerFuelChanged(FuelPercentage);

        if (_currentFuel <= 0)
            TriggerGameOver();
    }

    public void AddFuel(float amount)
    {
        if(_isActive) return;
        
        _currentFuel = Mathf.Clamp(_currentFuel + amount, 0f, maxFuel);
        GameEvents.TriggerFuelChanged(FuelPercentage);
    }

    private void TriggerGameOver()
    {
        _isActive = false;
        GameEvents.TriggerGameOver();
    }

    private void StopSystem()
    {
        _isActive = false;
    }
}
