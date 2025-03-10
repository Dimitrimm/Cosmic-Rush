using UnityEngine;

public class ShotController : MonoBehaviour
{
    [Header("Configurações do Projétil")]
    public float speed = 10f; // Velocidade do projétil
    public int damage = 1; // Dano causado pelo projétil
    private Animator animator;
    private bool isMoving = true;
    private string target = "";


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isMoving)
        {
            MoveProjectile();
        }
    }

    // Move o projétil para frente (eixo Y local)
    private void MoveProjectile()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
    }

    // Rotaciona o projétil para a direção especificada
    public void SetDirection(Vector2 newDirection)
    {
        if (newDirection != Vector2.zero)
        {
            RotateTowardsDirection(newDirection.normalized);
        }
    }

    private void RotateTowardsDirection(Vector2 direction)
    {
        // Calcula o ângulo de rotação em radianos
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica a rotação ao projétil (no eixo Z)
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Subtrai 90 graus para alinhar com o eixo Y
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Tenta obter o componente HealthController do objeto colidido
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();

            // Verifica se o componente foi encontrado antes de chamar TakeDamage
            if (healthController != null)
            {
                healthController.TakeDamage(damage);
                isMoving = false;
                animator.SetBool("destroy", true);
                Destroy(gameObject, 0.2f);// Aplica o dano
            }
        }
    }
}