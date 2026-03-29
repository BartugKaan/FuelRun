using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class TrafficSpawner : MonoBehaviour
{
    [SerializeField] private TrafficCar carPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnDistance = 40f;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private int laneCount = 3;
    [SerializeField] private float laneWidth = 3f;

    private ObjectPool<TrafficCar> _carPool;
    private float _spawnTimer;
    private bool _isActive = true;

    private void Awake()
    {
        _carPool = new ObjectPool<TrafficCar>(
            createFunc: () =>
            {
                TrafficCar car = Instantiate(carPrefab);
                car.Initialize(this);
                return car;
            },
            actionOnGet: car => car.gameObject.SetActive(true),
            actionOnRelease: car => car.gameObject.SetActive(false),
            actionOnDestroy: car => Destroy(car.gameObject),
            defaultCapacity: 10
            );
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= StopSpawning;
    }

    private void Update()
    {
        if(!_isActive) return;

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= spawnInterval)
        {
            _spawnTimer = 0f;
            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        int lane = Random.Range(0, laneCount);
        float laneX = (lane - laneCount / 2) * laneWidth;
        Vector3 spawnPos = new Vector3(laneX, 0, playerTransform.position.z + spawnDistance);

        TrafficCar car = _carPool.Get();
        car.transform.position = spawnPos;
    }

    public void ReturnCar(TrafficCar car)
    {
        _carPool.Release(car);
    }

    private void StopSpawning()
    {
        _isActive = false;
    }
}
