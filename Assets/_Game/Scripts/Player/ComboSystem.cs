using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] private float comboResetTime = 2f;

    private int _currentCombo = 0;
    private float _comboTimer = 0f;
    private bool _isActive = true;
    private int _activNearMisses = 0;

    private void OnEnable()
    {
        GameEvents.OnGameOver += StopSystem;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= StopSystem;
    }

    private void Update()
    {
        if (!_isActive || _currentCombo == 0) return;

        _comboTimer += Time.deltaTime;

        if (_comboTimer >= comboResetTime)
            ResetCombo();
    }

    public void RegisterNearMiss()
    {
        if (!_isActive) return;

        _activNearMisses++;
        _currentCombo++;
        _comboTimer = 0f;

        GameEvents.TriggerNearMiss(_currentCombo);
    }

    public void RegisterNearMissExit()
    {
        _activNearMisses = Mathf.Max(0, _activNearMisses - 1);
    }

    private void ResetCombo()
    {
        _currentCombo = 0;
        _comboTimer = 0f;
        GameEvents.TriggerNearMiss(0);
    }

    private void StopSystem()
    {
        _isActive = false;
        ResetCombo();
    }

    public int CurrentCombo => _currentCombo;
}