using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int _initHealth;
    [SerializeField] int _maxHealth;
    [SerializeField] UnityEvent _OnTakeDamage;
    [SerializeField] UnityEvent _OnDie;

    int _minHealth = 0;
    int _currentHealth;
    [HideInInspector] public STATE _state;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _initHealth;

        _state = STATE.ALIVE;

    }

    public void TakeDamage(int dmg)
    {
        if (_state == STATE.DEAD) return;

        if (dmg <= 0) return;

        //Debug.Log("is taking dmg");

        _currentHealth = Mathf.Clamp(_currentHealth - dmg, _minHealth, _maxHealth);
        _OnTakeDamage?.Invoke();

        if (_currentHealth <= _minHealth)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        if (_state == STATE.DEAD) return;

        //Debug.Log("healing");
        _currentHealth = Mathf.Clamp(_currentHealth + heal, _minHealth,_maxHealth);
    }

    public void Die()
    {
        _OnDie?.Invoke();
        _state = STATE.DEAD;
        //Debug.Log("is dead");
    }

}

public enum STATE
{
    ALIVE = 0,
    DEAD = 1,
}

