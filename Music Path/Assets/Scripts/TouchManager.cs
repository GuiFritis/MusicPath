using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Padrao.Core.Singleton;

[DefaultExecutionOrder(-1)]
public class TouchManager : Singleton<TouchManager>
{
    public Action<int> OnMoveSwipe;
    public Action<Vector3> OnTouchStart;
    public float distanceToMove = 0.5f;
    public float timeRange = 0.5f;
    private float _timer = 0f;
    private bool _touching = false;
    private Vector2 _startTouchPosition;
    private Touch _input;

    protected override void Awake()
    {
        base.Awake();
        SetInputs();
    }

    void Update()
    {
        if(_touching)
        {
            _timer += Time.deltaTime;
            if(_timer > timeRange)
            {
                _startTouchPosition = Camera.main.ScreenToWorldPoint(_input.Gameplay.PrimaryPosition.ReadValue<Vector2>());
                _timer = 0f;
            }
        }
    }

    private void SetInputs()
    {
        _input = new Touch();

        _input.Gameplay.PrimaryContact.started += StartTouch;
        _input.Gameplay.PrimaryContact.canceled += EndTouch;
    }

    private void StartTouch(InputAction.CallbackContext ctx)
    {
        _timer = 0f;
        _touching = true;
        _startTouchPosition = Camera.main.ScreenToWorldPoint(_input.Gameplay.PrimaryPosition.ReadValue<Vector2>());
        OnTouchStart?.Invoke(_startTouchPosition);
    }

    private void EndTouch(InputAction.CallbackContext ctx)
    {
        _touching = false;
        var difference = Camera.main.ScreenToWorldPoint(_input.Gameplay.PrimaryPosition.ReadValue<Vector2>()).y - _startTouchPosition.y;
        if(Mathf.Abs(difference) > distanceToMove)
        {
            OnMoveSwipe?.Invoke((int)Mathf.Sign(difference));
        }
        _startTouchPosition = Vector2.negativeInfinity;
    }

    void OnDrawGizmos()
    {
        if(_touching)
        {
            Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(_input.Gameplay.PrimaryPosition.ReadValue<Vector2>()), 0.5f);
        }
    }

    void OnEnable() 
    {
        _input.Enable();
    }

    void OnDisable() {
        _input.Disable();
    }
}
