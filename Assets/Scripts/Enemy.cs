using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = 2f;
    public float health = 10;
    public int score = 100;
    public float showDamageDuration = 0.1f;
    public float powerUpDropChance = 0.25f;
    public bool canShoot = false;

    [Header("Set in Inspector: Projectile")]
    public GameObject enemyProjectilePrefab;

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestruction = false;

    protected BoundsCheck bndCheck;
    private float lastShotTime = 0;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();

        materials = Utils.GetAllMaterials(gameObject);

        originalColors = new Color[materials.Length];

        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    public Vector3 pos
    {
        get 
        { 
            return transform.position; 
        }

        set 
        { 
            transform.position = value; 
        }
    }

    void Update()
    {
        Move();

        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }

        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }

        if (canShoot && bndCheck != null && bndCheck.isOnScreen)
        {
            TryShoot();
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;

        tempPos.y -= speed * Time.deltaTime;

        pos = tempPos;
    }

    void TryShoot()
    {
        if (enemyProjectilePrefab == null) 
        {
            return;
        }

        if (Time.time - lastShotTime < fireRate) 
        {
            return;
        }

        if (Hero.S == null) 
        {
            return;
        }

        lastShotTime = Time.time;

        GameObject projGO = Instantiate<GameObject>(enemyProjectilePrefab);

        projGO.transform.position = transform.position + new Vector3(0, -1f, 0);

        projGO.tag = "ProjectileEnemy";

        projGO.layer = LayerMask.NameToLayer("ProjectileEnemy");

        Rigidbody projRigid = projGO.GetComponent<Rigidbody>();

        if (projRigid != null)
        {
            Vector3 dir = (Hero.S.transform.position - projGO.transform.position).normalized;
            
            dir.x += Random.Range(-0.3f, 0.3f);
            
            projRigid.velocity = dir.normalized * 15f;
        }
    }

    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }

        showingDamage = true;

        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }

        showingDamage = false;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;

        switch (otherGO.tag)
        {
            case "ProjectileHero":
            {
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);

                    break;
                }

                health -= 1;

                ShowDamage();

                if (health <= 0)
                {
                    if (!notifiedOfDestruction)
                    {
                        Main.S.ShipDestroyed(this);
                    }

                    notifiedOfDestruction = true;

                    Destroy(this.gameObject);
                }

                Destroy(otherGO);

                break;
            }

            default:
            {
                break;
            }
        }
    }
}