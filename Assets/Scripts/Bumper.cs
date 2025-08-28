using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum BumperType
{
    Mushroom,
    Jellyfish,
    DesertShirt
}

public class Bumper : MonoBehaviour
{
    [Header("Bumper Info")] [SerializeField]
    private BumperType bumperType = BumperType.Mushroom;
    [Space]
    [SerializeField] private float BumpForce = 10f;
    [SerializeField] private SpriteRenderer medusaSprite;

    private void OnValidate()
    {
        switch (bumperType)
        {
            case BumperType.Mushroom:
                medusaSprite.color = new Color(168f / 255f, 156f /255f, 144f /255f, 1f);
                break;
            case BumperType.Jellyfish:
                medusaSprite.color = new Color(70f / 255f, 199f / 255f, 199f /255f, 0.7f);
                break;
            case BumperType.DesertShirt:
                medusaSprite.color = new Color(193f/255f, 154f/255f, 107f/255f, 1f);
                break;
            default:
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
        }
    }
}