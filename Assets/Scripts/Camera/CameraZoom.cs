using NaughtyAttributes;
using Unity.Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour,IOnTrigger
{

    [SerializeField] CinemachineCamera _cam;
    [SerializeField] Transform _player;

    [SerializeField] bool _onShotZoom = true;

    [SerializeField,ShowIf("_onShotZoom")] float _cameraSize = 10f;
    [SerializeField,ShowIf("_onShotZoom")] float _zoomSpeed = 0.8f;


    [Header("Gradual Increase")]
    [SerializeField] float _leftCameraSize = 9f;
    [SerializeField] float _rightCameraSize = 15f;


    bool inTrigger;
    bool _go;

    //gradual
    float _minXPos;
    float _maxXPos;
    float _distance;
    float _sizeGap;

    float t;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _minXPos = transform.position.x - transform.localScale.x /2 ;
        _maxXPos = transform.position.x + transform.localScale.x/2 ;

        _distance = _maxXPos - _minXPos;
        _sizeGap = Mathf.Abs(_rightCameraSize - _leftCameraSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (_onShotZoom)
        {
            if(_go) PlayOneShotZoom();
        }
        else
        {
            if (inTrigger) PlayGradualZoom(); ;
        }
    }

    #region Play Zoom
    private void PlayOneShotZoom()
    {
        t += Time.deltaTime * _zoomSpeed;
        _cam.Lens.OrthographicSize = Mathf.Lerp(_cam.Lens.OrthographicSize, _cameraSize, t);

        if (Mathf.Approximately(_cam.Lens.OrthographicSize, _cameraSize))
        {
            t = 0;
        }

    }

    private void PlayGradualZoom()
    {
        float playerPos = _player.position.x;
        float coeff = (playerPos - _minXPos) / _distance;
        _cam.Lens.OrthographicSize = Mathf.Lerp(_leftCameraSize, _rightCameraSize, coeff); 
    }
    #endregion

    #region Trigger
    public void OnEnter()
    {
        inTrigger = true;
        _go = true;
    }

    public void OnExit()
    {
        inTrigger = false;
    }
    #endregion
}
