using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota;
        public float spawnInterval;
        public float spawnCount;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount;
    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public static bool levelCompleted = false;
    Transform player;
    public float waveInterval;
    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints;

    // Nuevas variables
    public static int enemiesRemaining; // Enemigos faltantes
    public static int levelFlag = 0; 
    private string currentScene; 

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform; // Referencia al jugador

        // Reiniciar las variables al cargar el nivel
        currentWaveCount = 0;
        enemiesAlive = 0;
        enemiesRemaining = 0;
        levelCompleted = false;
        maxEnemiesReached = false;

        // Calcular la cuota de enemigos para la primera oleada
        CalculateWaveQuota();

        // Obtener el nombre de la escena actual
        currentScene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        // Si el nivel ya ha sido completado, no hacemos nada
        if (levelCompleted)
            return;

        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }

        // Verificar si se completó el nivel (última oleada completada y todos los enemigos derrotados)
        if (currentWaveCount == waves.Count - 1 && enemiesAlive <= 0 && waves[currentWaveCount].spawnCount == waves[currentWaveCount].waveQuota)
        {
            LevelCompleted();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }


    public void OnEnemyKilled()
    {
        enemiesAlive--;
        enemiesRemaining = enemiesAlive; 
    }

    
    void LevelCompleted()
    {
        levelCompleted = true;
        Debug.Log("¡Nivel completado! Todos los enemigos han sido derrotados.");
        
        
        Time.timeScale = 1f; 


        SceneManager.LoadScene("Win");
    }
}
