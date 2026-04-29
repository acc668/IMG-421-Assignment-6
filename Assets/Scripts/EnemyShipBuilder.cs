using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBuilder : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Color bodyColor = new Color(0.8f, 0.1f, 0.1f);
    public Color wingColor = new Color(0.6f, 0.05f, 0.05f);
    public Color cockpitColor = new Color(1.0f, 0.6f, 0.0f);
    public Color engineColor = new Color(0.5f, 0.0f, 0.8f);

    void Awake()
    {
        BuildShip();
    }

    void BuildShip()
    {
        CreatePart("Ship_Body", PrimitiveType.Cube, new Vector3(0, 0, 0), new Vector3(0.4f, 1.0f, 0.2f), bodyColor);

        CreatePart("Ship_Nose", PrimitiveType.Cube, new Vector3(0, -0.7f, 0), new Vector3(0.2f, 0.45f, 0.15f), bodyColor);

        CreatePart("Ship_WingL", PrimitiveType.Cube, new Vector3(-0.65f, 0.1f, 0), new Vector3(0.75f, 0.2f, 0.1f), wingColor);
        
        CreatePart("Ship_WingR", PrimitiveType.Cube, new Vector3(0.65f, 0.1f, 0), new Vector3(0.75f, 0.2f, 0.1f), wingColor);

        CreatePart("Ship_TipL", PrimitiveType.Cube, new Vector3(-0.95f, -0.15f, 0), new Vector3(0.2f, 0.4f, 0.08f), wingColor);
        
        CreatePart("Ship_TipR", PrimitiveType.Cube, new Vector3(0.95f, -0.15f, 0), new Vector3(0.2f, 0.4f, 0.08f), wingColor);

        CreatePart("Ship_Cockpit", PrimitiveType.Cube, new Vector3(0, 0.2f, -0.12f), new Vector3(0.18f, 0.18f, 0.05f), cockpitColor);

        CreatePart("Ship_Engine", PrimitiveType.Cube, new Vector3(0, 0.6f, 0), new Vector3(0.2f, 0.2f, 0.2f), engineColor);
    }

    void CreatePart(string partName, PrimitiveType shape, Vector3 localPos, Vector3 localScale, Color color)
    {
        GameObject go = GameObject.CreatePrimitive(shape);

        go.name = partName;

        go.transform.SetParent(transform);

        go.transform.localPosition = localPos;

        go.transform.localScale = localScale;

        go.transform.localRotation = Quaternion.identity;

        Collider col = go.GetComponent<Collider>();

        if (col != null) 
        {
            Destroy(col);
        }

        Renderer rend = go.GetComponent<Renderer>();

        if (rend != null)
        {
            Material mat = new Material(Shader.Find("Standard"));

            mat.color = color;

            rend.sharedMaterial = mat;
        }
    }
}