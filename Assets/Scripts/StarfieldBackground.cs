using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldBackground : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int   starCount = 150;
    public float minSize = 0.02f;
    public float maxSize = 0.12f;
    public float minSpeed = 0.3f;
    public float maxSpeed = 1.2f;
    public float minBrightness = 0.4f;
    public float maxBrightness = 1.0f;
    public float zDepth = 5f;

    private float spawnW;
    private float spawnH;

    private class StarData
    {
        public GameObject go;
        public float speed;
        public float brightness;
        public float twinkleSpeed;
        public float twinkleOffset;
    }

    private List<StarData> stars = new List<StarData>();

    void Awake()
    {
        Camera cam = Camera.main;

        if (cam.orthographic)
        {
            spawnH = cam.orthographicSize * 2.2f;

            spawnW = spawnH * cam.aspect;
        }

        else
        {
            float dist = Mathf.Abs(cam.transform.position.z) + zDepth;

            spawnH = dist * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) * 2.2f;

            spawnW = spawnH * cam.aspect;
        }

        SpawnStars();
    }

    void SpawnStars()
    {
        for (int i = 0; i < starCount; i++)
        {
            GameObject star = GameObject.CreatePrimitive(PrimitiveType.Quad);

            star.name = "Star_" + i;

            star.transform.SetParent(transform);

            Destroy(star.GetComponent<MeshCollider>());

            float x = Random.Range(-spawnW * 0.5f, spawnW * 0.5f);

            float y = Random.Range(-spawnH * 0.5f, spawnH * 0.5f);

            star.transform.position = new Vector3(x, y, zDepth);

            float size = Random.Range(minSize, maxSize);

            star.transform.localScale = new Vector3(size, size, 1f);

            Renderer rend = star.GetComponent<Renderer>();

            Material mat = new Material(Shader.Find("Unlit/Color"));

            float brightness = Random.Range(minBrightness, maxBrightness);

            float blueTint = Random.Range(0.85f, 1.0f);

            mat.color = new Color(blueTint, blueTint, 1f) * brightness;

            rend.material = mat;

            stars.Add(new StarData
            {
                go = star,
                speed = Random.Range(minSpeed, maxSpeed),
                brightness = brightness,
                twinkleSpeed = Random.Range(1f, 4f),
                twinkleOffset = Random.Range(0f, Mathf.PI * 2f)
            });
        }
    }

    void Update()
    {
        foreach (StarData s in stars)
        {
            if (s.go == null) 
            {
                continue;
            }

            Vector3 pos = s.go.transform.position;

            pos.y -= s.speed * Time.deltaTime;

            if (pos.y < -spawnH * 0.5f)
            {
                pos.y = spawnH * 0.5f;

                pos.x = Random.Range(-spawnW * 0.5f, spawnW * 0.5f);
            }

            s.go.transform.position = pos;

            Renderer rend = s.go.GetComponent<Renderer>();

            if (rend != null)
            {
                float twinkle = Mathf.Sin(Time.time * s.twinkleSpeed + s.twinkleOffset);

                float bright = Mathf.Clamp01(s.brightness + twinkle * 0.15f);

                float blue = Mathf.Clamp01(bright + 0.1f);

                rend.material.color = new Color(blue * 0.9f, blue * 0.9f, 1f) * bright;
            }
        }
    }
}