using System;
using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private int _timeBeforeSelfDestroy;
    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            //Reduce player life
            Debug.Log("Player Collision");
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_timeBeforeSelfDestroy);
        Destroy(gameObject);
    }
}