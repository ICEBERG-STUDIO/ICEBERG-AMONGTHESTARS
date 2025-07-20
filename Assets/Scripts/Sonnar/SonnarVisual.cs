using UnityEngine;

public class SonnarVisual : MonoBehaviour
{
    //Pour le rendu visuel du sonnar
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private int _subdivision = 10;
    //[SerializeField] private float _radius = 5f;

    //"dessine un cercle avec le radius donner
    public void DrawCircle(float radius)

    {
        float angleStep = 2f * Mathf.PI / _subdivision;
        _lineRenderer.positionCount = _subdivision;

        for (int i = 0; i < _subdivision; i++)
        {
            float xPosition = radius * Mathf.Cos(angleStep * i);
            float yPosition = radius * Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(xPosition, yPosition, 0f);
            _lineRenderer.SetPosition(i, pointInCircle);
        }
    }
}
