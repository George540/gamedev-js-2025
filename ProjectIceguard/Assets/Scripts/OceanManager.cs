using System;
using Unity.VisualScripting;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    [SerializeField] private Material _oceanMaterial;
    [SerializeField] private Texture2D _displacementWavesTexture;
    
    [SerializeField] private float _waveHeight = 0.5f;
    [SerializeField] private float _waveFrequency = 1.0f;
    [SerializeField] private float _waveSpeed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_oceanMaterial)
        {
            _displacementWavesTexture = (Texture2D) _oceanMaterial.GetTexture("_WavesDisplacement");
        }
    }

    public float GetWaterHeightAtPosition(Vector3 position)
    {
        return transform.position.y + _displacementWavesTexture.GetPixelBilinear(position.x * _waveFrequency / 100.0f, position.z * _waveFrequency / 100.0f + Time.time * _waveSpeed / 100.0f).g * _waveHeight / 100.0f * transform.localScale.x;
    }

    private void OnValidate()
    {
        if (!_oceanMaterial)
        {
            _oceanMaterial = _oceanMaterial.GetComponent<Renderer>().sharedMaterial;
            _displacementWavesTexture = (Texture2D) _oceanMaterial.GetTexture("_WavesDisplacement");
        }
        
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        _oceanMaterial.SetFloat("_WavesFrequency", _waveFrequency);
        _oceanMaterial.SetFloat("_WavesSpeed", _waveSpeed);
        _oceanMaterial.SetFloat("_WavesHeight", _waveHeight);
    }
}
