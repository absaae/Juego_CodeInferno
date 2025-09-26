using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObjects enemyData;
    //Stats del momento
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;
    public float despawnDistance = 20f;
    Transform player;

    void Awake(){
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void Start(){
        // Verificamos si el jugador existe al iniciar
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null) {
            player = playerStats.transform;
        }
    }

    void Update(){
        // Verificar si el jugador existe antes de intentar calcular la distancia
        if (player != null && Vector2.Distance(transform.position, player.position) >= despawnDistance){
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg){
        currentHealth -= dmg;
        if (currentHealth <= 0){
            Kill();
        }
    }

    public void Kill(){
        Destroy(gameObject);
        //Aqui podria ir lo necesario para implementar la interfaz de muerte
    }

    private void OnCollisionStay2D(Collision2D col){
        if(col.gameObject.CompareTag("Player")){
            PlayerStats playerStats = col.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null) {
                playerStats.TakeDamage(currentDamage);
            }
        }
    }

    private void OnDestroy(){
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        if (es != null) {
            es.OnEnemyKilled();
        }
    }

    void ReturnEnemy(){
        // Verificar si el jugador existe antes de reposicionar al enemigo
        if (player != null) {
            EnemySpawner es = FindObjectOfType<EnemySpawner>();
            if (es != null) {
                transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
            }
        }
    }
}
