using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicRush
{
    public class BasicEnemyAttackController : MonoBehaviour
    {
        [Header("Configura��es de Tiro")]
        public GameObject simpleShotPrefab;

        public Transform leftFirePoint;
        public Transform rightFirePoint;

        public float simpleShotCooldown = 0.5f;

        private float lastSimpleShotTime;

        private bool shootFromLeft = true;

        private bool isVisible = false;

        private void Update()
        {
            // Verifica se o objeto est� vis�vel e se o cooldown acabou
            if (isVisible && Time.time - lastSimpleShotTime >= simpleShotCooldown)
            {
                SimpleShot();
            }
        }

        private void SimpleShot()
        {
            // Define o ponto de disparo com base na altern�ncia
            Transform firePoint = shootFromLeft ? leftFirePoint : rightFirePoint;

            // Instancia o proj�til no ponto de disparo
            CreateProjectile(firePoint, simpleShotPrefab);

            // Alterna o ponto de disparo para o pr�ximo tiro
            shootFromLeft = !shootFromLeft;

            // Atualiza o momento do �ltimo disparo
            lastSimpleShotTime = Time.time;
        }

        private void CreateProjectile(Transform firePoint, GameObject prefab)
        {
            GameObject projectile = Instantiate(prefab, firePoint.position, firePoint.rotation);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // Configura a dire��o do proj�til
            if (projectile.TryGetComponent(out EnemyShotController shotController) && player != null)
            {
                Vector2 mouseWorldPosition = player.transform.position;
                Vector2 firePointPosition = firePoint.position;
                Vector2 shootDirection = mouseWorldPosition - firePointPosition;

                shotController.SetDirection(shootDirection);
            }
        }

        // Chamado quando o objeto se torna vis�vel para qualquer c�mera
        private void OnBecameVisible()
        {
            isVisible = true;
        }

        // Chamado quando o objeto deixa de ser vis�vel para qualquer c�mera
        private void OnBecameInvisible()
        {
            isVisible = false;
        }
    }
}