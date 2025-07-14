using System.Collections;
using UnityEngine;

public class SonnarEffect : MonoBehaviour
{
    private Renderer _renderer;
    [SerializeField] private Color _sonnarColor = Color.blue;
    private Color _baseColor;
    [SerializeField] private float _effectDuration = 2f;
    [SerializeField] private float _fadeDuration = 2f;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.color;
        _baseColor.a = 0f;
        _renderer.material.color = _baseColor;
    }

    public void ActivateSonnar()
    {
        StopAllCoroutines();
        StartCoroutine(SonnarTouch());
    }

    private IEnumerator SonnarTouch()
    {
        _renderer.material.color = _sonnarColor;
        yield return new WaitForSeconds(_effectDuration);
        
        float _currentTimeFade = 0f;
        while (_currentTimeFade < _fadeDuration)
        {
            _currentTimeFade += Time.deltaTime;
            float t = _currentTimeFade / _fadeDuration;
            _renderer.material.color = Color.Lerp(_renderer.material.color, _baseColor, t);
            yield return null;
        }

        _renderer.material.color = _baseColor;
    }
}
