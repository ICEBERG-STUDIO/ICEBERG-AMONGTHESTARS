using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tunnel : MonoBehaviour
{
    [SerializeField] GameObject _objectDepart;
    [SerializeField] GameObject _objectArrive;

    [SerializeField] Image _imgTransition;
    Color _transparence;

    float _timer = 1.5f;

    [SerializeField] AnimationCurve _animCurve;

    bool _startTunnel;
    bool _stopTunnel;

    GameObject _player;

    private void Start()
    {
        _transparence = _imgTransition.color;
        _transparence.a = 0f;
        _imgTransition.color = _transparence;
        _startTunnel = false;
        _stopTunnel = false;

        UnityEngine.Debug.Log(_timer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _timer = 1.5f;
            _startTunnel = true;
            _player = collision.gameObject;
            _player.GetComponent<PlayerInput>().enabled = false;

            UnityEngine.Debug.Log("start tunnel dans trigger : " + _startTunnel);
            UnityEngine.Debug.Log("stop tunnel dans trigger : " + _stopTunnel);
        }
    }

    private void Update()
    {
        if (_startTunnel)
        {
            _timer -= Time.deltaTime;

            float pourcentageTransparence = (1.5f - _timer) / 1.5f;

            _transparence.a = _animCurve.Evaluate(pourcentageTransparence);
            _imgTransition.color = _transparence;

            if (pourcentageTransparence >= 1)
            {
                _timer = 1.5f;
                _stopTunnel = true;
                _player.transform.position = _objectArrive.transform.position;
                _startTunnel = false;
            }
        }
        if (_stopTunnel)
        {
            _timer -= Time.deltaTime;

            float pourcentageTransparence = _timer / 1.5f;

            _transparence.a = _animCurve.Evaluate(pourcentageTransparence);
            _imgTransition.color = _transparence;

            if (pourcentageTransparence <= 0)
            {
                _player.GetComponent<PlayerInput>().enabled = true;
                _stopTunnel = false;
            }
        }
    }
}
