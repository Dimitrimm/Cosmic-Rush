using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicRush
{
    public class BasicEnemyAttackController : MonoBehaviour
    {
        [Header("Configurações de Tiro")]
        public GameObject simpleShotPrefab;

        public Transform leftFirePoint;
        public Transform rightFirePoint;

        public float simpleShotCooldown = 0.5f;

        private float lastSimpleShotTime;

        private bool shootFromLeft = true;

        private bool isVisible = false;

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
            // Define o ponto de disparo com base na alternância
            Transform firePoint = shootFromLeft ? leftFirePoint : rightFirePoint;

            // Instancia o projétil no ponto de disparo
            CreateProjectile(firePoint, simpleShotPrefab);

            // Alterna o ponto de disparo para o próximo tiro
            shootFromLeft = !shootFromLeft;

            // Atualiza o momento do último disparo
            lastSimpleShotTime = Time.time;
        }

        private void CreateProjectile(Transform firePoint, GameObject prefab)
        {
            GameObject projectile = Instantiate(prefab, firePoint.position, firePoint.rotation);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // Configura a direção do projétil
            if (projectile.TryGetComponent(out EnemyShotController shotController) && player != null)
            {
                Vector2 mouseWorldPosition = player.transform.position;
                Vector2 firePointPosition = firePoint.position;
                Vector2 shootDirection = mouseWorldPosition - firePointPosition;

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