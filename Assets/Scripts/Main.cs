using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public float enemyDefaultPadding = 1.5f;

    private float camWidth;
    private float camHeight;
    private DifficultySettings settings;

    void Awake()
    {
        S = this;

        settings = DifficultyManager.GetSettings();

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

        Invoke("SpawnEnemy", 1f / settings.enemySpawnRate);
    }

    public void SpawnEnemy()
    {
        if (prefabEnemies == null || prefabEnemies.Length == 0)
        {
            Debug.LogError("Main.SpawnEnemy: prefabEnemies is empty!");

            return;
        }

        if (settings.spawnInFormations)
        {
            SpawnFormation();
        }

        else
        {
            SpawnSingleEnemy();
        }

        Invoke("SpawnEnemy", 1f / settings.enemySpawnRate);
    }

    void SpawnSingleEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);

        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        ApplyDifficultyToEnemy(go);

        float padding = GetPadding(go);

        Vector3 pos = Vector3.zero;

        pos.x = Random.Range(-camWidth + padding, camWidth - padding);

        pos.y = camHeight + padding;

        go.transform.position = pos;
    }

    void SpawnFormation()
    {
        int formationType = Random.Range(0, 2);

        int count = formationType == 0 ? 3 : 5;

        float spacing = (camWidth * 1.2f) / (count + 1);

        float startX  = -camWidth * 0.6f;

        for (int i = 0; i < count; i++)
        {
            int ndx = Random.Range(0, prefabEnemies.Length);

            GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

            ApplyDifficultyToEnemy(go);

            float padding = GetPadding(go);

            Vector3 pos = Vector3.zero;

            pos.x = startX + spacing * (i + 1);

            if (formationType == 1)
            {
                float mid  = (count - 1) / 2f;

                float dist = Mathf.Abs(i - mid);

                pos.y = camHeight + padding + (dist * 1.5f);
            }

            else
            {
                pos.y = camHeight + padding;
            }

            go.transform.position = pos;
        }
    }

    void ApplyDifficultyToEnemy(GameObject go)
    {
        Enemy e = go.GetComponent<Enemy>();

        if (e != null)
        {
            e.speed  = settings.enemySpeed;

            e.health = settings.enemyHealth;

            e.score  = 100 * settings.enemyScoreMultiplier;

            e.canShoot = settings.enemiesShoot;
        }
    }

    float GetPadding(GameObject go)
    {
        BoundsCheck b = go.GetComponent<BoundsCheck>();

        return b != null ? Mathf.Abs(b.radius) : enemyDefaultPadding;
    }

    public void ShipDestroyed(Enemy e)
    {
        if (GameManager.S != null)
        {
            GameManager.S.AddScore(e.score);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}