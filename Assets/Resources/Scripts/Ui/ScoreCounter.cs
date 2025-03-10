using UnityEngine;
using UnityEngine.UI; // Para usar Text (UI)
using TMPro; // Para usar TextMeshPro (opcional)

public class ScoreCounter : MonoBehaviour
{
    [Header("Configura��es do Contador")]
    public int currentScore = 0; // Pontua��o atual
    public Text scoreText; // Refer�ncia ao componente Text (UI)
    public TextMeshProUGUI scoreTextTMP; // Refer�ncia ao componente TextMeshPro (opcional)

    private void Start()
    {
        // Inicializa o contador na tela
        UpdateScoreDisplay();
    }

    // M�todo para adicionar pontos ao contador
    public void AddPoints(int points)
    {
        currentScore += points; // Adiciona os pontos � pontua��o atual
        UpdateScoreDisplay(); // Atualiza o texto na tela
    }

    // M�todo para atualizar o texto do contador
    private void UpdateScoreDisplay()
    {
        // Atualiza o texto com a pontua��o atual
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }

        // Se estiver usando TextMeshPro
        if (scoreTextTMP != null)
        {
            scoreTextTMP.text =currentScore.ToString();
        }
    }
}