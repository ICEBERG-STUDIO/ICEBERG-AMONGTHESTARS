using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class SunDetection : MonoBehaviour
{
    [SerializeField] Health _health;

    [Header("Sun Dmg")]
    [SerializeField] float _laps = 1;
    [SerializeField] int _burnDmg = 10;

    Transform _sun;

    bool isSafe;

    private void Awake()
    {
        if (_health == null) this.enabled = false;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sun = GameObject.FindGameObjectWithTag("Sun").transform;
    }


    void FixedUpdate()
    {
        if (_health._state == STATE.DEAD) return;

        // launch a multi raycast from me to sun
        Vector2 AB = _sun.position - transform.position;
        float distance = AB.magnitude * 40;
        Vector2 dir = AB.normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, distance);

        // looking for a gameObject that produces a shadow 
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            GameObject gameObject = hit.transform.gameObject;
            if (gameObject.TryGetComponent<ShadowCaster2D>(out ShadowCaster2D s))
            {
                Debug.Log("à l ombre safe " + gameObject);
                isSafe = true;
                StopCoroutine("Burn");
                break;
            }

            isSafe = false;
        }

        if (!isSafe)
        {
            Debug.Log("visible");

            StartCoroutine("Burn");
        }
    }

    private IEnumerator Burn()
    {
        yield return new WaitForSeconds(_laps);

        Debug.Log("je me brule");
        _health.TakeDamage(_burnDmg);
    }


}
