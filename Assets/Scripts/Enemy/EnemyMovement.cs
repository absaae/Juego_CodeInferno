using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy; // Obtenemos la referencia a EnemyStats
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyStats>(); // Inicializamos la referencia a EnemyStats
        player = FindObjectOfType<PlayerMovement>().transform; // Encontramos al jugador
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null && player != null) // Verificamos que enemy y player no sean nulos
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemy.currentMoveSpeed * Time.deltaTime); // Movemos al enemigo hacia el jugador
        }
    }
}

