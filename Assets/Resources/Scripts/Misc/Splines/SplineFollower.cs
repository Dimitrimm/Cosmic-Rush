using UnityEngine;
using UnityEngine.Splines;

public class SplineFollower : MonoBehaviour
{
    [Header("Configura��es do Spline")]
    public SplineContainer splineContainer; // Refer�ncia ao Spline Container
    public int numberOfPrefabs = 10; // N�mero de prefabs a serem instanciados
    public GameObject prefab; // Prefab a ser instanciado
    public float speed = 1f; // Velocidade de movimento ao longo do spline

    private GameObject[] instances; // Array para armazenar as inst�ncias dos prefabs
    private float[] progress; // Progresso de cada inst�ncia ao longo do spline

    private void Start()
    {
        // Inicializa os arrays
        instances = new GameObject[numberOfPrefabs];
        progress = new float[numberOfPrefabs];

        // Instancia os prefabs ao longo do spline
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Calcula a posi��o normalizada ao longo do spline (0 a 1)
            float t = i / (float)numberOfPrefabs;

            // Instancia o prefab
            instances[i] = Instantiate(prefab, transform);

            // Posiciona o prefab ao longo do spline
            UpdatePosition(i, t);

            // Inicializa o progresso de cada inst�ncia
            progress[i] = t;
        }
    }

    private void Update()
    {
        int remainingInstances = 0; // Contador de inst�ncias restantes

        // Move cada inst�ncia ao longo do spline
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Verifica se a inst�ncia ainda existe
            if (instances[i] == null)
            {
                continue; // Pula para a pr�xima itera��o se a inst�ncia foi destru�da
            }

            // Incrementa o contador de inst�ncias restantes
            remainingInstances++;

            // Atualiza o progresso da inst�ncia
            progress[i] += speed * Time.deltaTime;

            // Garante que o progresso fique dentro do intervalo [0, 1]
            if (progress[i] > 1f)
            {
                progress[i] = 0f; // Reinicia no in�cio do spline
            }

            // Atualiza a posi��o da inst�ncia ao longo do spline
            UpdatePosition(i, progress[i]);
        }

        // Verifica se n�o h� mais inst�ncias restantes
        if (remainingInstances == 0)
        {
            DestroyParentAndSelf(); // Destr�i o GameObject atual e seu pai
        }
    }

    private void UpdatePosition(int index, float t)
    {
        // Verifica se a inst�ncia ainda existe
        if (instances[index] == null)
        {
            return; // Sai do m�todo se a inst�ncia foi destru�da
        }

        // Obt�m a posi��o e a rota��o ao longo do spline
        Vector3 position = splineContainer.EvaluatePosition(t);

        // Atualiza a posi��o e rota��o da inst�ncia
        instances[index].transform.position = position;
    }

    private void DestroyParentAndSelf()
    {
        // Verifica se o GameObject atual tem um pai
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject); // Destr�i o pai
        }

        Destroy(gameObject); // Destr�i o GameObject atual
    }
}