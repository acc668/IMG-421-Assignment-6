using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;

    [Header("Set in Inspector")]
    public float speed = 10f;
    public float rollMult = -45f;
    public float pitchMult = 30f;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40f;
    public float fireRate = 0.15f;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 3;

    private float lastFireTime = 0;
    private float camWidth;
    private float camHeight;
    private Rigidbody rigid;
    private float xAxis;
    private float yAxis;

    private bool invincible = false;
    public float invincibleDuration = 1.5f;

    void Awake()
    {
        if (S == null) 
        {
            S = this;
        }

        rigid = GetComponent<Rigidbody>();

        Camera cam = Camera.main;

        if (cam.orthographic)
        {
            camHeight = cam.orthographicSize;

            camWidth  = camHeight * cam.aspect;
        }

        else
        {
            float dist = Mathf.Abs(cam.transform.position.z);

            camHeight = dist * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);

            camWidth  = camHeight * cam.aspect;
        }
    }

    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");

        yAxis = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        if (Input.GetAxis("Jump") == 1 && Time.time - lastFireTime > fireRate)
        {
            FireProjectile();

            lastFireTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        Vector3 newPos = rigid.position + new Vector3(xAxis, yAxis, 0) * speed * Time.fixedDeltaTime;
        
        newPos.x = Mathf.Clamp(newPos.x, -camWidth  + 1f, camWidth  - 1f);

        newPos.y = Mathf.Clamp(newPos.y, -camHeight + 1f, camHeight - 1f);

        newPos.z = 0f;

        rigid.MovePosition(newPos);
    }

    void FireProjectile()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);

        projGO.transform.position = transform.position + new Vector3(0, 1.5f, 0);

        Rigidbody projRigid = projGO.GetComponent<Rigidbody>();

        projRigid.velocity = Vector3.up * projectileSpeed;

        projGO.tag = "ProjectileHero";

        projGO.layer = LayerMask.NameToLayer("ProjectileHero");
    }

    void OnTriggerEnter(Collider other)
    {
        if (invincible) 
        {
            return;
        }

        GameObject go = other.gameObject;

        if (go.tag == "Enemy" || go.tag == "ProjectileEnemy")
        {
            Destroy(go);

            if (_shieldLevel > 0)
            {
                shieldLevel--;

                StartCoroutine(InvincibilityFrames());

                return;
            }

            if (GameManager.S != null)
            {
                GameManager.S.PlayerHit();
            }

            StartCoroutine(InvincibilityFrames());
        }
    }

    IEnumerator InvincibilityFrames()
    {
        invincible = true;

        yield return new WaitForSeconds(invincibleDuration);

        invincible = false;
    }

    public float shieldLevel
    {
        get 
        { 
            return _shieldLevel; 
        }

        set
        {
            _shieldLevel = Mathf.Max(0, value);

            if (GameManager.S != null)
            {
                GameManager.S.UpdateHUD();
            }
        }
    }
}