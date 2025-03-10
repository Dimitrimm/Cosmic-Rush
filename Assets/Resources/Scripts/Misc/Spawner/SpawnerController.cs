using UnityEngine;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{
    [Header("Configurações Gerais")]
    public List<GameObject> prefabs; // Lista de prefabs a serem instanciados
    public Transform targetPosition; // Posição de destino
    public float moveSpeed = 5f; // Velocidade de movimento

    private GameObject currentInstance; // Instância atual do prefab
    private int currentIndex = 0; // Índice do prefab atual na lista

    private void Start()
    {
        // Instancia o primeiro prefab
        SpawnNextPrefab();
    }

    private void Update()
    {
        if (currentInstance != null)
        {
            // Move o objeto em direção à posição de destino
            currentInstance.transform.position = Vector3.MoveTowards(
                currentInstance.transform.position,
                targetPosition.position,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            SpawnNextPrefab();
        }
    }

    // Instancia o próximo prefab da lista
    private void SpawnNextPrefab()
    {
        if (currentIndex < prefabs.Count)
        {
            // Instancia o prefab na posição do Spawner
            currentInstance = Instantiate(prefabs[currentIndex], transform.position, Quaternion.identity);

            // Incrementa o índice para o próximo prefab
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
    }

    // Destrói a instância atual e instancia o próximo prefab
    public void DestroyCurrentInstance()
    {
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
            SpawnNextPrefab(); // Instancia o próximo prefab
        }
    }
}