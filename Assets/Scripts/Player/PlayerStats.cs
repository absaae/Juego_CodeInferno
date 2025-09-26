using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Añadido para la gestión de escenas

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObjects characterData;

    // Stats actuales
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;

    // Esto para la experiencia y nivel 
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap = 100;
    public int experienceCapIncrease;

    // I-Frames (marco de invulnerabilidad)
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    // Desvanecimiento (Fade-out)
    [Header("Death Fade Animation")]
    public float fadeDuration = 1f; // Duración del fade-out en segundos

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Obtener referencia al SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializar stats del jugador con datos del objeto de personaje
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;
    }

    void Update()
    {
        // Contador para los I-Frames (marco de invulnerabilidad)
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }

        Recover(); // Llamar a la función de recuperación de vida
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            experienceCap += experienceCapIncrease;
        }
    }

    // Función que reduce la salud del jugador cuando recibe daño
    public void TakeDamage(float dmg)
    {
        if (!isInvincible) // Solo recibir daño si no está invulnerable
        {
            currentHealth -= dmg;
        
            // Asegurarse de que la vida nunca sea menor a 0
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            invincibilityTimer = invincibilityDuration; // Activar el temporizador de invulnerabilidad
            isInvincible = true;

            // Verificar si la salud es 0 o menos, lo que significa que el jugador muere
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    // Función que maneja la muerte del jugador
    public void Kill()
    {
        Debug.Log("El jugador ha muerto");
        StartCoroutine(FadeAndDestroy()); // Iniciar el fade-out y luego destruir al jugador
    }

    // Corutina para hacer el fade-out y luego destruir el GameObject del jugador
    IEnumerator FadeAndDestroy()
    {
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Finalmente destruir el GameObject
        Destroy(gameObject);

        // Restablecer el Time.timeScale ANTES de cargar la escena de "Fail"
        Time.timeScale = 1f; 

        // Cargar la escena de "Fail" al morir
        if (SceneManager.GetActiveScene().name == "Nivel 1")
        {
            EnemySpawner.levelFlag = 1; // Perdiste en el Nivel 1
        }
        else if (SceneManager.GetActiveScene().name == "Nivel 2")
        {
            EnemySpawner.levelFlag = 2; // Perdiste en el Nivel 2
        }

        SceneManager.LoadScene("Fail");
    }

    void DisableAllScripts()
    {
        CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();
        if (cameraMovement != null)
        {
            cameraMovement.enabled = false;
        }
    }

    // Función para restaurar la salud del jugador
    public void RestoreHealth(float amount)
    {
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    // Función que permite la recuperación automática de salud a lo largo del tiempo
    void Recover()
    {
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }
}
