using UnityEngine;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class DifficultyManager : MonoBehaviour
{
    public static Difficulty difficulty = Difficulty.Easy;

    public static DifficultySettings GetSettings()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
            {
                return new DifficultySettings
                {
                    enemySpeed = 4f,
                    enemyHealth = 5f,
                    enemySpawnRate = 0.4f,
                    playerStartLives = 3,
                    playerStartShield = 3,
                    enemiesShoot = false,
                    spawnInFormations = false,
                    enemyScoreMultiplier = 1
                };
            }

            case Difficulty.Medium:
            {
                return new DifficultySettings
                {
                    enemySpeed = 7f,
                    enemyHealth = 10f,
                    enemySpawnRate = 0.7f,
                    playerStartLives = 3,
                    playerStartShield = 2,
                    enemiesShoot = false,
                    spawnInFormations = true,
                    enemyScoreMultiplier = 2
                };
            }

            case Difficulty.Hard:
            {
                return new DifficultySettings
                {
                    enemySpeed = 10f,
                    enemyHealth = 15f,
                    enemySpawnRate = 1.2f,
                    playerStartLives = 2,
                    playerStartShield = 1,
                    enemiesShoot = true,
                    spawnInFormations = true,
                    enemyScoreMultiplier = 3
                };
            }

            default:
            {
                return new DifficultySettings();
            }
        }
    }
}

[System.Serializable]
public class DifficultySettings
{
    public float enemySpeed;
    public float enemyHealth;
    public float enemySpawnRate;
    public int playerStartLives;
    public int playerStartShield;
    public bool enemiesShoot;
    public bool spawnInFormations;
    public int enemyScoreMultiplier;
}