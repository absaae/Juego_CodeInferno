using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    public Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {

        // Comprobamos la dirección de movimiento del jugador
        if (pm.moveDir.x > 0 && pm.moveDir.y == 0) // Movimiento a la derecha
        {
            Transform rightPoint = currentChunk.transform.Find("Right");
            if (rightPoint != null && !Physics2D.OverlapCircle(rightPoint.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = rightPoint.position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0) // Movimiento a la izquierda
        {
            Transform leftPoint = currentChunk.transform.Find("Left");
            if (leftPoint != null && !Physics2D.OverlapCircle(leftPoint.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = leftPoint.position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.y > 0 && pm.moveDir.x == 0) // Movimiento hacia arriba
        {
            Transform upPoint = currentChunk.transform.Find("Up");
            if (upPoint != null && !Physics2D.OverlapCircle(upPoint.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = upPoint.position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.y < 0 && pm.moveDir.x == 0) // Movimiento hacia abajo
        {
            Transform downPoint = currentChunk.transform.Find("Down");
            if (downPoint != null && !Physics2D.OverlapCircle(downPoint.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = downPoint.position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) // Movimiento hacia arriba a la derecha
        {
            Transform rightUpPoint = currentChunk.transform.Find("Right Up");
            if (rightUpPoint != null && !Physics2D.OverlapCircle(rightUpPoint.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = rightUpPoint.position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) // Movimiento hacia abajo a la derecha
        {
            Transform rightDownPoint = currentChunk.transform.Find("Right Down");
            if (rightDownPoint != null && !Physics2D.OverlapCircle(rightDownPoint.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = rightDownPoint.position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0) // Movimiento hacia arriba a la izquierda
        {
            Transform leftUpPoint = currentChunk.transform.Find("Left Up");
            if (leftUpPoint != null && !Physics2D.OverlapCircle(leftUpPoint.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = leftUpPoint.position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0) // Movimiento hacia abajo a la izquierda
        {
            Transform leftDownPoint = currentChunk.transform.Find("Left Down");
            if (leftDownPoint != null && !Physics2D.OverlapCircle(leftDownPoint.position, checkerRadius, terrainMask))
            {
                noTerrainPosition = leftDownPoint.position;
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
