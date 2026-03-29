using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnGameOver;
    public static event Action<int> OnScoreChanged;
    public static event Action<float> OnFuelChanged;
    public static event Action<int> OnNearMiss;


    public static void TriggerGameOver() => OnGameOver?.Invoke();
    public static void TriggerScoreChanged(int score) => OnScoreChanged?.Invoke(score);
    public static void TriggerFuelChanged(float fuel) => OnFuelChanged?.Invoke(fuel);
    public static void TriggerNearMiss(int combo) => OnNearMiss?.Invoke(combo);
}
