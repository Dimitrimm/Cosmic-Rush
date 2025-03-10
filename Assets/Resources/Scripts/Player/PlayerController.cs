using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float moveSpeed = 5f; // Velocidade de movimento do player

    [Header("Limites de Movimento")]
    public float minX = -10f; // Limite mínimo no eixo X
    public float maxX = 10f;  // Limite máximo no eixo X
    public float minY = -10f; // Limite mínimo no eixo Y
    public float maxY = 10f;  // Limite máximo no eixo Y

    private Animator animator; // Referência ao componente Animator (opcional)
    private Vector2 movement;  // Vetor de movimento (direção e magnitude)

    private void Start()
    {
        // Tenta obter o componente Animator automaticamente
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Captura a entrada do jogador (será substituída por integração com Input System)
        float moveX = 0f;
        float moveY = 0f;

        // Exemplo de movimento (substitua pela integração com Input System)
        if (Input.GetKey(KeyCode.W)) moveY = 1f; // Frente
        if (Input.GetKey(KeyCode.S)) moveY = -1f; // Trás
        if (Input.GetKey(KeyCode.D)) moveX = 1f; // Direita
        if (Input.GetKey(KeyCode.A)) moveX = -1f; // Esquerda

        movement = new Vector2(moveX, moveY).normalized;

        // Move o player
        MovePlayer(movement);

        // Atualiza parâmetros do Animator (se houver animação)
        //UpdateAnimator(movement);
    }

    private void MovePlayer(Vector2 direction)
    {
        // Calcula a nova posição do player
        Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;

        // Aplica os limites de movimento
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        UpdateAnimator((newPosition - transform.position)*100);

        // Move o player para a nova posição
        transform.position = newPosition;
    }

    private void UpdateAnimator(Vector2 direction)
    {
        // Se houver um Animator, atualiza os parâmetros de movimento
        if (animator != null)
        {
            // Define a direção do movimento (para animações específicas de direção)
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }
}