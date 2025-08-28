using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int _initHealth;
    [SerializeField] int _maxHealth;

    [Header("Events")]
    [SerializeField] UnityEvent _OnTakeDamage;
    [SerializeField] UnityEvent _OnDie;

    int _minHealth = 0;
    int _currentHealth;
    [HideInInspector] public STATE _state;


    void Awake()
    {
        Init();
    }

    private void Init()
    {
        _currentHealth = _initHealth;
        _state = STATE.ALIVE;
    }

    public void TakeDamage(int dmg)
    {
        //conditions
        if (_state == STATE.DEAD || dmg <= 0) return;

        // Decrease Health
        _currentHealth = Mathf.Clamp(_currentHealth - dmg, _minHealth, _maxHealth);
        _OnTakeDamage?.Invoke();

        //Check Death
        if (_currentHealth <= _minHealth)
            Die();
    }

    public void Heal(int heal)
    {
        //conditions
        if (_state == STATE.DEAD) return;

        //Increase Health
        _currentHealth = Mathf.Clamp(_currentHealth + heal, _minHealth,_maxHealth);
    }

    public void Die()
    {
        _state = STATE.DEAD;

        _OnDie?.Invoke();

        // call respawn + init
    }

}

public enum STATE
{
    NONE = 0,
    ALIVE = 1,
    INVINCIBLE = 2,
    DEAD = 3,
}

