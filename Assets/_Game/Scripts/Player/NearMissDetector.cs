using System;
using UnityEngine;

public class NearMissDetector : MonoBehaviour
{
    private ComboSystem _comboSystem;

    public void Initialize(ComboSystem comboSystem)
    {
        _comboSystem = comboSystem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Traffic"))
        {
            _comboSystem.RegisterNearMiss();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Traffic"))
        {
            _comboSystem.RegisterNearMissExit();
        }
    }
}
