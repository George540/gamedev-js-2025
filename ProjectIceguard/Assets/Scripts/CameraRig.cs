using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRig : MonoBehaviour
{
    [SerializeField] private InputActionReference _mouseInputReference;
    [SerializeField] private InputActionReference _gamepadInputReference;
    [SerializeField] private CinemachineInputAxisController _cinemachineInputProvider;
    private bool _isCameraActivated;

    private void Update()
    {
        foreach (var controller in _cinemachineInputProvider.Controllers)
        {
            controller.Input.InputAction = _isCameraActivated ? _mouseInputReference : _gamepadInputReference;
        }
    }

    public void OnCameraActivate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isCameraActivated = true;
        }
        if (context.canceled)
        {
            _isCameraActivated = false;
        }
    }
}
