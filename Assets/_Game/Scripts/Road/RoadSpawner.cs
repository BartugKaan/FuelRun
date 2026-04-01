using System;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private RoadChunk chunkPrefab;
    [SerializeField] private int visibleChunkCount = 5;
    [SerializeField] private Transform playerTransform;

    private ObjectPool<RoadChunk> _chunkPool;
    private readonly List<RoadChunk> _activeChunks = new();
    private float _nextSpawnZ = 0f;


    private void Awake()
    {
        _chunkPool = new ObjectPool<RoadChunk>(
            createFunc: () => Instantiate(chunkPrefab),
            actionOnGet: chunk => chunk.gameObject.SetActive(true),
            actionOnRelease: chunk => chunk.gameObject.SetActive(false),
            actionOnDestroy: chunk => Destroy(chunk.gameObject),
            defaultCapacity: visibleChunkCount + 2
        );
    }

    private void Start()
    {
        for (int i = 0; i < visibleChunkCount; i++)
        {
            SpawnChunk();
        }
    }

    private void Update()
    {
        RecycleChunks();
        FillAhead();
    }

    private void SpawnChunk()
    {
        RoadChunk chunk = _chunkPool.Get();
        chunk.transform.position = new Vector3(0, 0, _nextSpawnZ);
        _activeChunks.Add(chunk);
        _nextSpawnZ += chunk.ChunkLength;
        chunk.ResetChunk();
    }

    private void RecycleChunks()
    {
        if(_activeChunks.Count == 0) return;
        
        RoadChunk oldest = _activeChunks[0];
        float recyclePoint = playerTransform.position.z - oldest.ChunkLength;

        if (oldest.transform.position.z < recyclePoint)
        {
            _activeChunks.RemoveAt(0);
            _chunkPool.Release(oldest);
            SpawnChunk();
        }
    }

    private void FillAhead()
    {
        float aheadDistance = playerTransform.transform.position.z + (visibleChunkCount * chunkPrefab.ChunkLength);

        while (_nextSpawnZ < aheadDistance)
        {
            SpawnChunk();
        }
    }
}
