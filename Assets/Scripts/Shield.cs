using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.1f;

    [Header("Set Dynamically")]
    public int levelShown = 0;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Hero.S == null)
        {
            return;
        }

        int currLevel = (int)Hero.S.shieldLevel;

        if (levelShown != currLevel)
        {
            levelShown = currLevel;

            gameObject.SetActive(levelShown > 0);

            Color c = Color.white;

            switch (levelShown)
            {
                case 0:
                {
                    break;
                }

                case 1:
                {
                    c = Color.red;

                    break;
                }

                case 2:
                {
                    c = Color.yellow;

                    break;
                }

                default:
                {
                    c = Color.green;

                    break;
                }
            }

            if (rend != null)
            {
                rend.material.color = c;
            }
        }

        transform.rotation = Quaternion.Euler(
            0,
            0,
            rotationsPerSecond * Time.time * 360);
    }
}