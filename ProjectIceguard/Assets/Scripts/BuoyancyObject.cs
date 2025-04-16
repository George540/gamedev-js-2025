using UnityEngine;
using UnityEngine.Serialization;

public class BuoyancyObject : MonoBehaviour
{
    [SerializeField] private OceanManager _oceanManager;
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private Transform[] _objectFloaters;
    
    [SerializeField] private float _underLinearWaterDrag = 3.0f;
    [SerializeField] private float _underAngularWaterDrag = 1.0f;
    
    [SerializeField] private float _airLinearWaterDrag = 0.0f;
    [SerializeField] private float _airAngularWaterDrag = 0.05f;
    
    [SerializeField] private float _floatingPower = 15.0f;

    private bool _bIsUnderWater;
    private int _floatersUnderWaterNum;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var floater in _objectFloaters)
        {
            float difference = floater.position.y - _oceanManager.GetWaterHeightAtPosition(floater.position);

            if (difference < 0)
            {
                _rigidbody.AddForceAtPosition(Vector3.up * _floatingPower * Mathf.Abs(difference), floater.position, ForceMode.Force);
                _floatersUnderWaterNum++;

                if (!_bIsUnderWater)
                {
                    _bIsUnderWater = true;
                    SwitchState(true);
                }
            }
        }
        
        if (_bIsUnderWater && _floatersUnderWaterNum == 0)
        {
            _bIsUnderWater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderWater)
    {
        if (isUnderWater)
        {
            _rigidbody.linearDamping = _underLinearWaterDrag;
            _rigidbody.angularDamping = _underAngularWaterDrag;
        }
        else
        {
            _rigidbody.linearDamping = _airLinearWaterDrag;
            _rigidbody.angularDamping = _airAngularWaterDrag;
        }
    }
}
