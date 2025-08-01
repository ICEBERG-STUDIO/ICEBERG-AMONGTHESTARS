using UnityEngine;

public class Fruits : MonoBehaviour
{
    [SerializeField] private float _speedImpulsion = 10f;
    [SerializeField] private float _maxRadius = 20f;
    [SerializeField] private CircleCollider2D _sphereCollider;
    private float _currentRadius;

    [SerializeField] private string _tagAmes = "AmesDamnees";

    private bool _fruitsActivate = false;

    private void Start()
    {
        _currentRadius = _sphereCollider.radius;
    }

    public void ChangeFuits()
    {
        if (_fruitsActivate)
        {
            _fruitsActivate = false;
        }
        else
        {
            _fruitsActivate = true;
        }
        Debug.Log(_fruitsActivate);
    }

    private void Update()
    {
        if (_fruitsActivate)
        {
            if (_sphereCollider.radius <= _maxRadius)
            {
                _currentRadius += _speedImpulsion * Time.deltaTime;
                _sphereCollider.radius = _currentRadius;
            }
        }
        else
        {
            if (_sphereCollider.radius >= 0)
            {
                _currentRadius -= _speedImpulsion * Time.deltaTime;
                _sphereCollider.radius = _currentRadius;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("je suis dans un trigger AmesDamnees " + collision.name);
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "AmesDamnees")
        {
            Debug.Log("je suis dans le trigger du plateforme AmesDamnees");
            collision.GetComponent<Ames>()?.AmesActivate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("je suis plus dans un trigger AmesDamnees");
        if (collision.gameObject.tag == "AmesDamnees")
        {
            Debug.Log("je suis dans le trigger du plateforme AmesDamnees");
            collision.GetComponent<Ames>()?.AmesDesactivate();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _currentRadius);
    }
}
