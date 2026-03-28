using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float laneChangeSpeed = 8f;
    [SerializeField] private LaneSystem _laneSystem;

    private float _targetX;
    private bool _isMovingLane;


    private void Start()
    {
        _targetX = _laneSystem.GetCurrentLaneX();
    }

    private void Update()
    {
        MoveForward();
        MoveLateral();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    private void MoveLateral()
    {
        float currentX = transform.position.x;
        _targetX = _laneSystem.GetCurrentLaneX();

        float newX = Mathf.MoveTowards(currentX, _targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        _isMovingLane = !Mathf.Approximately(currentX, _targetX);
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        if(_isMovingLane) return;
        _laneSystem.TryMoveLeft();
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        if(_isMovingLane) return;
        _laneSystem.TryMoveRight();
    }
}
