using System;
using UnityEngine;

public class LaneSystem : MonoBehaviour
{
    [SerializeField] private int laneCount = 3;
    [SerializeField] private float laneWidth = 3f;

    private int _currentLane;
    private int CenterLane => laneCount / 2;

    private void Awake()
    {
        _currentLane = CenterLane;
    }

    public bool TryMoveLeft()
    {
        if (_currentLane <= 0) return false;
        _currentLane--;
        return true;
    }

    public bool TryMoveRight()
    {
        if (_currentLane >= laneCount - 1) return false;
        _currentLane++;
        return true;
    }

    public float GetCurrentLaneX()
    {
        float offset = (_currentLane - CenterLane) * laneWidth;
        return offset;
    }
}
