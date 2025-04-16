using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    [SerializeField] private VirtualMouseInput _virtualMouseInput;
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private RectTransform _cursorRectTransform;
    [SerializeField] private Image _cursorImage;

    public bool _isGamepadActive;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.localScale = Vector3.one * 1.0f / _canvasRectTransform.localScale.x;
        transform.SetAsFirstSibling();
        
        _cursorImage.color = Color.white;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit) && !EventSystem.current.IsPointerOverGameObject())
        {
            /*if (hit.transform.gameObject.GetComponentInParent<PointOfInterest>() 
                || hit.transform.gameObject.TryGetComponent<PointOfInterest>(out var poi)
                    || hit.transform.gameObject.tag == "Pickable"
                    || hit.transform.gameObject.TryGetComponent<Player>(out var player))
            {
                _cursorImage.color = Color.cyan;
            }
            else if (hit.transform.gameObject.GetComponentInParent<NavMeshSurface>() && hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                _cursorImage.color = Color.green;
            }*/
        }
    }
    
    private void LateUpdate()
    {
        var virtualMousePosition = _virtualMouseInput.virtualMouse.position.value;
        virtualMousePosition.x = Mathf.Clamp(virtualMousePosition.x, 0.0f, Screen.width);
        virtualMousePosition.y = Mathf.Clamp(virtualMousePosition.y, 0.0f, Screen.height);
        InputState.Change(_virtualMouseInput.virtualMouse.position, virtualMousePosition);
        if (_isGamepadActive)
        {
            Mouse.current.WarpCursorPosition(new Vector2(_cursorRectTransform.position.x, _cursorRectTransform.position.y));
        }
        else
        {
            var mousePosition = Mouse.current.position;
            _cursorRectTransform.position = new Vector3(mousePosition.x.ReadValue(), mousePosition.y.ReadValue(), 0.0f);
        }
    }

    public void OnPointGamepad(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        _isGamepadActive = input != Vector2.zero;
    }
}
