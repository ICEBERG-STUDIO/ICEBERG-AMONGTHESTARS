using UnityEngine;

public class SonnarImpulsion : MonoBehaviour
{
    [SerializeField] private float _speedImpulsion = 10f;
    [SerializeField] private float _maxRadius = 20f;
    private SphereCollider _sphereCollider;
    private float _currentRadius;

    [SerializeField] private string _tagSonnar = "PlateformeSonar";

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _currentRadius = _sphereCollider.radius;
    }

    private void Update()
    {
        _currentRadius += _speedImpulsion * Time.deltaTime;
        _sphereCollider.radius = _currentRadius;

        if ( _sphereCollider.radius >= _maxRadius)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hhbhv");
        if (collision.gameObject.tag == "PlateformeSonar")
        {
            Debug.Log("hhbhv");
            collision.GetComponent<SonnarEffect>()?.ActivateSonnar();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _currentRadius);
    }
}
