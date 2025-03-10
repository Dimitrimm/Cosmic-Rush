using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public int maxHealth = 100; // Vida máxima do objeto
    private int currentHealth;
    ScoreCounter counter;
    public int score;// Vida atual do objeto

    private void Start()
    {
        // Inicializa a vida atual com o valor máximo
        currentHealth = maxHealth;
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        counter = gameManager.GetComponent<ScoreCounter>();
    }

    // Método para remover vida
    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida atual
        currentHealth -= damageAmount;

        // Verifica se a vida chegou a 0 ou menos
        if (currentHealth <= 0)
        {
            Die(); // Chama o método para destruir o objeto
        }

        // Exibe a vida atual no console (opcional, para debug)
        Debug.Log(gameObject.name + " tomou " + damageAmount + " de dano. Vida restante: " + currentHealth);
    }

    // Método para destruir o objeto quando a vida acaba
    private void Die()
    {
        Debug.Log(gameObject.name + " foi destruído.");
        counter.AddPoints(score);
        Destroy(gameObject); // Destrói o GameObject
    }

    // Método opcional para curar (aumentar a vida)
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Garante que a vida não ultrapasse o valor máximo
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log(gameObject.name + " foi curado em " + healAmount + ". Vida atual: " + currentHealth);
    }

    // Método para verificar a vida atual (opcional)
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}