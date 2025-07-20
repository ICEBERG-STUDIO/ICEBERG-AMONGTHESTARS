using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Ames : MonoBehaviour
{
    private Renderer _selfRenderer;
    private Color _selfColor;
    //[SerializeField] private float _effectDuration = 2f;
    [SerializeField] private float _fadeDuration = 1f;

    private void Start()
    {
        _selfRenderer = GetComponent<Renderer>();
        _selfColor.a = 0f;
        _selfRenderer.material.color = _selfColor;
    }


    public void AmesActivate()
    {
        StopAllCoroutines();
        StartCoroutine(FruitsInTrigger());
    }

    public void AmesDesactivate()
    {
        StopAllCoroutines();
        StartCoroutine(FruitsExitTrigger());
    }


    //private IEnumerator FruitsInTrigger()
    //{
    //    _selfRenderer.material.color = Color.white;
    //    //yield return new WaitForSeconds(_effectDuration);
    //
    //    float _currentTimeFade = 0f;
    //    while (_currentTimeFade < _fadeDuration)
    //    {
    //        _currentTimeFade += Time.deltaTime;
    //        float t = _currentTimeFade / _fadeDuration;
    //        _selfRenderer.material.color = Color.Lerp(_selfRenderer.material.color, _selfColor, t);
    //        yield return null;
    //    }
    //
    //    //_selfRenderer.material.color = _selfColor;
    //}
    //
    //private IEnumerator FruitsExitTrigger()
    //{
    //    _selfRenderer.material.color = Color.white;
    //    //yield return new WaitForSeconds(_effectDuration);
    //
    //    float _currentTimeFade = 1f;
    //    while (_currentTimeFade > _fadeDuration)
    //    {
    //        _currentTimeFade -= Time.deltaTime;
    //        float t = _currentTimeFade / _fadeDuration;
    //        _selfRenderer.material.color = Color.Lerp(_selfRenderer.material.color, _selfColor, t);
    //        yield return null;
    //    }
    //
    //    //_selfRenderer.material.color = _selfColor;
    //}


    private IEnumerator FruitsInTrigger()
    {
        Color transparentColor = new Color(_selfColor.r, _selfColor.g, _selfColor.b, 0f);
        Color visibleColor = new Color(_selfColor.r, _selfColor.g, _selfColor.b, 1f);

        _selfRenderer.material.color = transparentColor;
        //yield return new WaitForSeconds(_effectDuration);

        float _currentTimeFade = 0f;
        while (_currentTimeFade < _fadeDuration)
        {
            _currentTimeFade += Time.deltaTime;
            float t = Mathf.Clamp01(_currentTimeFade / _fadeDuration);
            _selfRenderer.material.color = Color.Lerp(transparentColor, visibleColor, t);
            yield return null;
        }

        _selfRenderer.material.color = visibleColor; // reste visible
    }

    private IEnumerator FruitsExitTrigger()
    {
        Color visibleColor = new Color(_selfColor.r, _selfColor.g, _selfColor.b, 1f);
        Color transparentColor = new Color(_selfColor.r, _selfColor.g, _selfColor.b, 0f);

        _selfRenderer.material.color = visibleColor;
        //yield return new WaitForSeconds(_effectDuration);

        float _currentTimeFade = 0f;
        while (_currentTimeFade < _fadeDuration)
        {
            _currentTimeFade += Time.deltaTime;
            float t = Mathf.Clamp01(_currentTimeFade / _fadeDuration);
            _selfRenderer.material.color = Color.Lerp(visibleColor, transparentColor, t);
            yield return null;
        }

        _selfRenderer.material.color = transparentColor; // reste transparent
    }

}
