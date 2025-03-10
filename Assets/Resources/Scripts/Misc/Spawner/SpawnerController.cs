using UnityEngine;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{
    [Header("Configura��es Gerais")]
    public List<GameObject> prefabs; // Lista de prefabs a serem instanciados
    public Transform targetPosition; // Posi��o de destino
    public float moveSpeed = 5f; // Velocidade de movimento

    private GameObject currentInstance; // Inst�ncia atual do prefab
    private int currentIndex = 0; // �ndice do prefab atual na lista

    private void Start()
    {
        // Instancia o primeiro prefab
        SpawnNextPrefab();
    }

    private void Update()
    {
        if (currentInstance != null)
        {
            // Move o objeto em dire��o � posi��o de destino
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

    // Instancia o pr�ximo prefab da lista
    private void SpawnNextPrefab()
    {
        if (currentIndex < prefabs.Count)
        {
            // Instancia o prefab na posi��o do Spawner
            currentInstance = Instantiate(prefabs[currentIndex], transform.position, Quaternion.identity);

            // Incrementa o �ndice para o pr�ximo prefab
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
    }

    // Destr�i a inst�ncia atual e instancia o pr�ximo prefab
    public void DestroyCurrentInstance()
    {
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
            SpawnNextPrefab(); // Instancia o pr�ximo prefab
        }
    }
}