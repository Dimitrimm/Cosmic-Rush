using UnityEngine;
using UnityEngine.Splines;

public class SplineFollower : MonoBehaviour
{
    [Header("Configurações do Spline")]
    public SplineContainer splineContainer; // Referência ao Spline Container
    public int numberOfPrefabs = 10; // Número de prefabs a serem instanciados
    public GameObject prefab; // Prefab a ser instanciado
    public float speed = 1f; // Velocidade de movimento ao longo do spline

    private GameObject[] instances; // Array para armazenar as instâncias dos prefabs
    private float[] progress; // Progresso de cada instância ao longo do spline

    private void Start()
    {
        // Inicializa os arrays
        instances = new GameObject[numberOfPrefabs];
        progress = new float[numberOfPrefabs];

        // Instancia os prefabs ao longo do spline
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Calcula a posição normalizada ao longo do spline (0 a 1)
            float t = i / (float)numberOfPrefabs;

            // Instancia o prefab
            instances[i] = Instantiate(prefab, transform);

            // Posiciona o prefab ao longo do spline
            UpdatePosition(i, t);

            // Inicializa o progresso de cada instância
            progress[i] = t;
        }
    }

    private void Update()
    {
        int remainingInstances = 0; // Contador de instâncias restantes

        // Move cada instância ao longo do spline
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Verifica se a instância ainda existe
            if (instances[i] == null)
            {
                continue; // Pula para a próxima iteração se a instância foi destruída
            }

            // Incrementa o contador de instâncias restantes
            remainingInstances++;

            // Atualiza o progresso da instância
            progress[i] += speed * Time.deltaTime;

            // Garante que o progresso fique dentro do intervalo [0, 1]
            if (progress[i] > 1f)
            {
                progress[i] = 0f; // Reinicia no início do spline
            }

            // Atualiza a posição da instância ao longo do spline
            UpdatePosition(i, progress[i]);
        }

        // Verifica se não há mais instâncias restantes
        if (remainingInstances == 0)
        {
            DestroyParentAndSelf(); // Destrói o GameObject atual e seu pai
        }
    }

    private void UpdatePosition(int index, float t)
    {
        // Verifica se a instância ainda existe
        if (instances[index] == null)
        {
            return; // Sai do método se a instância foi destruída
        }

        // Obtém a posição e a rotação ao longo do spline
        Vector3 position = splineContainer.EvaluatePosition(t);

        // Atualiza a posição e rotação da instância
        instances[index].transform.position = position;
    }

    private void DestroyParentAndSelf()
    {
        // Verifica se o GameObject atual tem um pai
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject); // Destrói o pai
        }

        Destroy(gameObject); // Destrói o GameObject atual
    }
}