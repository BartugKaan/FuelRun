using UnityEngine;

public class RoadChunk : MonoBehaviour
{
    [SerializeField] private float chunkLength = 20f;
    [SerializeField] private Transform[] pickupPoints;
    [SerializeField] private GameObject fuelPickupPrefab;
    [SerializeField] private float spawnChance = 0.6f;

    public float ChunkLength => chunkLength;

    public void ResetChunk()
    {
        SpawnPickups();
    }

    private void SpawnPickups()
    {
        if (pickupPoints == null || fuelPickupPrefab == null) return;

        foreach (Transform point in pickupPoints)
        {
            if (point == null) continue;

            foreach (Transform child in point)
                child.gameObject.SetActive(false);

            if (Random.value <= spawnChance)
            {
                GameObject pickup = Instantiate(fuelPickupPrefab, point.position,
                    Quaternion.identity, point);
                pickup.SetActive(true);
            }
        }
    }
}