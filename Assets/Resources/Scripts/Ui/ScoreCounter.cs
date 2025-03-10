using UnityEngine;
using UnityEngine.UI; // Para usar Text (UI)
using TMPro; // Para usar TextMeshPro (opcional)

public class ScoreCounter : MonoBehaviour
{
    [Header("Configurações do Contador")]
    public int currentScore = 0; // Pontuação atual
    public Text scoreText; // Referência ao componente Text (UI)
    public TextMeshProUGUI scoreTextTMP; // Referência ao componente TextMeshPro (opcional)

    private void Start()
    {
        // Inicializa o contador na tela
        UpdateScoreDisplay();
    }

    // Método para adicionar pontos ao contador
    public void AddPoints(int points)
    {
        currentScore += points; // Adiciona os pontos à pontuação atual
        UpdateScoreDisplay(); // Atualiza o texto na tela
    }

    // Método para atualizar o texto do contador
    private void UpdateScoreDisplay()
    {
        // Atualiza o texto com a pontuação atual
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