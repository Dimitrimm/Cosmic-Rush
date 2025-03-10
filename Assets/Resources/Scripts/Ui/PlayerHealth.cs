using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [Header("Corações (Vida)")]
    public GameObject[] hearts; // Array de GameObjects que representam os corações

    [Header("Botão de Reinício")]
    public Button restartButton; // Botão de UI para reiniciar o jogo

    private int currentHealth; // Vida atual do jogador

    private void Start()
    {
        // Inicializa a vida com o número máximo de corações
        currentHealth = hearts.Length;

        // Garante que todos os corações estejam visíveis no início
        UpdateHeartsDisplay();

        // Esconde o botão de reinício no início
        restartButton.gameObject.SetActive(false);
    }

    // Método para reduzir a vida
    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida atual
        currentHealth -= damageAmount;

        // Garante que a vida não fique negativa
        currentHealth = Mathf.Max(currentHealth, 0);

        // Atualiza a exibição dos corações
        UpdateHeartsDisplay();

        // Verifica se a vida chegou a zero
        if (currentHealth <= 0)
        {
            OnPlayerDeath(); // Chama o método de morte do jogador
        }
    }

    // Método para atualizar a exibição dos corações
    private void UpdateHeartsDisplay()
    {
        // Percorre todos os corações
        for (int i = 0; i < hearts.Length; i++)
        {
            // Ativa ou desativa o coração com base na vida atual
            if (i < currentHealth)
            {
                hearts[i].SetActive(true); // Mostra o coração
            }
            else
            {
                hearts[i].SetActive(false); // Esconde o coração
            }
        }
    }

    // Método chamado quando a vida chega a zero
    private void OnPlayerDeath()
    {
        Debug.Log("Jogador morreu!");

        // Congela o jogo
        Time.timeScale = 0;

        // Mostra o botão de reinício
        restartButton.gameObject.SetActive(true);

        // Configura o botão para reiniciar o jogo ao ser clicado
        restartButton.onClick.AddListener(RestartGame);
    }

    // Método para reiniciar o jogo
    private void RestartGame()
    {
        // Restaura o tempo normal
        Time.timeScale = 1;

        // Recarrega a cena atual
        SceneManager.LoadScene("MenuInicial");
    }

    // Método para restaurar a vida ao máximo
    public void RestoreHealth()
    {
        currentHealth = hearts.Length; // Restaura a vida ao máximo
        UpdateHeartsDisplay(); // Atualiza a exibição dos corações
    }
}