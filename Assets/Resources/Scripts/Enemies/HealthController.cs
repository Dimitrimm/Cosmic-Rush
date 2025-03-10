using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Configura��es de Vida")]
    public int maxHealth = 100; // Vida m�xima do objeto
    private int currentHealth;
    ScoreCounter counter;
    public int score;// Vida atual do objeto

    private void Start()
    {
        // Inicializa a vida atual com o valor m�ximo
        currentHealth = maxHealth;
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        counter = gameManager.GetComponent<ScoreCounter>();
    }

    // M�todo para remover vida
    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida atual
        currentHealth -= damageAmount;

        // Verifica se a vida chegou a 0 ou menos
        if (currentHealth <= 0)
        {
            Die(); // Chama o m�todo para destruir o objeto
        }

        // Exibe a vida atual no console (opcional, para debug)
        Debug.Log(gameObject.name + " tomou " + damageAmount + " de dano. Vida restante: " + currentHealth);
    }

    // M�todo para destruir o objeto quando a vida acaba
    private void Die()
    {
        Debug.Log(gameObject.name + " foi destru�do.");
        counter.AddPoints(score);
        Destroy(gameObject); // Destr�i o GameObject
    }

    // M�todo opcional para curar (aumentar a vida)
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        // Garante que a vida n�o ultrapasse o valor m�ximo
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log(gameObject.name + " foi curado em " + healAmount + ". Vida atual: " + currentHealth);
    }

    // M�todo para verificar a vida atual (opcional)
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}