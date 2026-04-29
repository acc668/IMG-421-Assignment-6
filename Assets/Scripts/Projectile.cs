using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;

    [Header("Set Dynamically")]
    public Rigidbody rigid;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Destroy the projectile when it goes off the top of the screen
        if (bndCheck != null && bndCheck.offUp)
        {
            Destroy(gameObject);
        }
        // Also destroy if it goes off the bottom (for enemy projectiles)
        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }
}