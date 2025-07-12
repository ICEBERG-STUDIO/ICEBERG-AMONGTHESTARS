using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MedusaType
{
    Bumper,
    Platform
}

public class Jellyfish : MonoBehaviour
{
    [Header("Medusa Info")]
    [SerializeField] private MedusaType medusaType = MedusaType.Bumper;
    [SerializeField] private float BumpForce = 10f;
    [SerializeField] private SpriteRenderer medusaSprite;

    private void Awake()
    {
        if (medusaType == MedusaType.Bumper)
        {
            medusaSprite.color = new Color(54f / 255f, 248f / 255f, 255f / 255f, 1f);
        }
        else
        {
            medusaSprite.color = new Color(231f / 255f, 107f / 255f, 249f / 255f, 1f);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (medusaType == MedusaType.Bumper)
            {
                Rigidbody2D playerRgbd2d = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRgbd2d != null)
                {
                    // Get the collision point
                    ContactPoint2D contact = collision.GetContact(0);

                    // Check with the normal if the player is under or over the medusa
                    if (contact.normal.y > 0f)
                    {
                        // Do nothing is the player is from under the medusa
                        return;
                    }
                    
                    playerRgbd2d.AddForce(Vector2.up * BumpForce, ForceMode2D.Impulse);
                }
                else
                {
                    Debug.Log("No player rigidbody found");
                }
            }
        }
    }
}