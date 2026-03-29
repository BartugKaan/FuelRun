using System;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    [SerializeField] private float speed = 15f;

    private TrafficSpawner _spawner;

    public void Initialize(TrafficSpawner spawner)
    {
        _spawner = spawner;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.TriggerGameOver();
            ReturnToPool();
            return;
        }

        if (other.CompareTag("Recycler"))
            ReturnToPool();
    }

    private void ReturnToPool()
    {
        _spawner.ReturnCar(this);
    }
}
