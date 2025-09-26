using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    // Lista de puntos de generación donde se colocarán los props.
    public List<GameObject> propSpawnPoints;

    // Lista de prefabs de props que pueden ser instanciados.
    public List<GameObject> propPrefabs;

    // Método que se ejecuta al inicio del juego o cuando el script es activado.
    void Start()
    {
        // Llama al método SpawnProps para generar los props en la escena.
        SpawnProps();
    }

    // Método encargado de generar los props en los puntos de generación.
    void SpawnProps()
    {
        // Recorre cada punto de generación en la lista propSpawnPoints.
        foreach (GameObject sp in propSpawnPoints)
        {
            // Genera un número aleatorio entre 0 y la cantidad de prefabs disponibles.
            int rand = Random.Range(0, propPrefabs.Count);

            // Instancia un prefab aleatorio en la posición del punto de generación.
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);

            // Establece el punto de generación como el padre del prop generado (esto es opcional, pero útil si se desea mantener la jerarquía).
            prop.transform.parent = sp.transform; 
        }
    }
}
