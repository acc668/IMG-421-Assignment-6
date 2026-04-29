using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBuilder : MonoBehaviour
{
    [Header("Set in Inspector")]
    public bool  isEnemyProjectile = false;
    public Color heroColor = new Color(0.0f, 1.0f, 0.9f);
    public Color enemyColor = new Color(1.0f, 0.3f, 0.0f);

    void Awake()
    {
        MeshRenderer rootRend = GetComponent<MeshRenderer>();

        if (rootRend != null) 
        {
            rootRend.enabled = false;
        }

        BuildProjectile();
    }

    void BuildProjectile()
    {
        Color col = isEnemyProjectile ? enemyColor : heroColor;

        GameObject bolt = GameObject.CreatePrimitive(PrimitiveType.Cube);

        bolt.name = "Proj_Bolt";

        bolt.transform.SetParent(transform);

        bolt.transform.localPosition = Vector3.zero;

        bolt.transform.localScale = new Vector3(0.08f, 0.5f, 0.08f);

        bolt.transform.localRotation = Quaternion.identity;

        Collider c1 = bolt.GetComponent<Collider>();

        if (c1 != null) 
        {
            Destroy(c1);
        }

        Renderer r1 = bolt.GetComponent<Renderer>();

        if (r1 != null)
        {
            Material mat = new Material(Shader.Find("Unlit/Color"));

            mat.color = col;

            r1.sharedMaterial = mat;
        }

        GameObject core = GameObject.CreatePrimitive(PrimitiveType.Cube);

        core.name = "Proj_Core";

        core.transform.SetParent(transform);

        core.transform.localPosition = Vector3.zero;

        core.transform.localScale = new Vector3(0.04f, 0.38f, 0.04f);

        core.transform.localRotation = Quaternion.identity;

        Collider c2 = core.GetComponent<Collider>();

        if (c2 != null) 
        {
            Destroy(c2);
        }

        Renderer r2 = core.GetComponent<Renderer>();

        if (r2 != null)
        {
            Material mat = new Material(Shader.Find("Unlit/Color"));
            mat.color = Color.white;
            r2.sharedMaterial = mat;
        }
    }
}