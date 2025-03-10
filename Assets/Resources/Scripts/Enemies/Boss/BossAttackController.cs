using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicRush
{
    public class BossAttackController : MonoBehaviour
    {
        [Header("Configurações de Tiro")]
        public GameObject simpleShotPrefab; // Prefab do projétil

        [Header("Firepoints")]
        public Transform firePoint1; // Primeiro firepoint
        public Transform firePoint2; // Segundo firepoint
        public Transform firePoint3; // Terceiro firepoint
        public Transform firePoint4; // Quarto firepoint

        public float simpleShotCooldown = 0.5f; // Cooldown entre os tiros

        private float lastSimpleShotTime; // Tempo do último tiro
        private int currentFirePointIndex = 0; // Índice do firepoint atual
        private bool isVisible = false; // Verifica se o objeto está visível na câmera

        private void Update()
        {
            // Verifica se o objeto está visível e se o cooldown acabou
            if (isVisible && Time.time - lastSimpleShotTime >= simpleShotCooldown)
            {
                SimpleShot();
            }
        }

        private void SimpleShot()
        {
            // Escolhe o firepoint atual com base no índice
            Transform firePoint = GetCurrentFirePoint();

            // Instancia o projétil no ponto de disparo
            CreateProjectile(firePoint, simpleShotPrefab);

            // Avança para o próximo firepoint
            currentFirePointIndex = (currentFirePointIndex + 1) % 4; // Alterna entre 0, 1, 2, 3

            // Atualiza o momento do último disparo
            lastSimpleShotTime = Time.time;
        }

        private Transform GetCurrentFirePoint()
        {
            // Retorna o firepoint correspondente ao índice atual
            switch (currentFirePointIndex)
            {
                case 0: return firePoint1;
                case 1: return firePoint2;
                case 2: return firePoint3;
                case 3: return firePoint4;
                default: return firePoint1; // Caso padrão (nunca deve acontecer)
            }
        }

        private void CreateProjectile(Transform firePoint, GameObject prefab)
        {
            // Instancia o projétil no firepoint
            GameObject projectile = Instantiate(prefab, firePoint.position, firePoint.rotation);

            // Encontra o jogador para definir a direção do tiro
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Configura a direção do projétil
            if (projectile.TryGetComponent(out EnemyShotController shotController) && player != null)
            {
                Vector2 playerPosition = player.transform.position;
                Vector2 firePointPosition = firePoint.position;
                Vector2 shootDirection = playerPosition - firePointPosition;

                shotController.SetDirection(shootDirection);
            }
        }

        // Chamado quando o objeto se torna visível para qualquer câmera
        private void OnBecameVisible()
        {
            isVisible = true;
        }

        // Chamado quando o objeto deixa de ser visível para qualquer câmera
        private void OnBecameInvisible()
        {
            isVisible = false;
        }
    }
}