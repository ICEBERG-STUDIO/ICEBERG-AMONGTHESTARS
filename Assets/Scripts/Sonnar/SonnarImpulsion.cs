using UnityEngine;

public class SonnarImpulsion : MonoBehaviour
{
    [SerializeField] private float _speedImpulsion = 10f;
    [SerializeField] private float _maxRadius = 20f;
    private CircleCollider2D _sphereCollider;
    private float _currentRadius;

    [SerializeField] private string _tagSonnar = "PlateformeSonnar";

    [SerializeField] private SonnarVisual _sonnarVisual;

    private void Start()
    {
        _sphereCollider = GetComponent<CircleCollider2D>();
        _currentRadius = _sphereCollider.radius;
    }

    private void Update()
    {
        _currentRadius += _speedImpulsion * Time.deltaTime;
        _sphereCollider.radius = _currentRadius;
        _sonnarVisual.DrawCircle(_currentRadius);

        if ( _sphereCollider.radius >= _maxRadius)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("je suis dans un trigger");
        if (collision.gameObject.tag == "PlateformeSonnar")
        {
            Debug.Log("je suis dans le trigger du plateforme sonnar");
            collision.GetComponent<SonnarEffect>()?.ActivateSonnar();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _currentRadius);
    }
}
