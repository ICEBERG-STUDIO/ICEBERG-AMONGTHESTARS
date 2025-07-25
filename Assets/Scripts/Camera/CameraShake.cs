using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance { get; private set; }
    private CinemachineCamera _cineCam;
    CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;
    [SerializeField] private float _shakeDuration = 3f;
    private float _shakeTimer = 0f;
    [SerializeField] private float _intensity = 2f;
    private bool _isShaking = false;

    private void Awake()
    {
        instance = this;
        _cineCam = GetComponent<CinemachineCamera>();
        basicMultiChannelPerlin = _cineCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }


    private void Start()
    {

        basicMultiChannelPerlin.AmplitudeGain = 0f;
    }

    private void Update()
    {

        if (_isShaking)
        {

            _shakeTimer -= Time.deltaTime;


            if (_shakeTimer <= 0)
            {
                _isShaking = false;

                basicMultiChannelPerlin.AmplitudeGain = 0f;
            }
        }
        else
        {
            _shakeTimer = 0;
        }
    }

    public void ShakeCamera()
    {
        _isShaking = true;

        basicMultiChannelPerlin.AmplitudeGain = _intensity;

        _shakeTimer = _shakeDuration;
    }


}
