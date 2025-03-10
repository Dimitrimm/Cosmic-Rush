using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicRush
{
    public class BossAttackController : MonoBehaviour
    {
        [Header("Configura��es de Tiro")]
        public GameObject simpleShotPrefab; // Prefab do proj�til

        [Header("Firepoints")]
        public Transform firePoint1; // Primeiro firepoint
        public Transform firePoint2; // Segundo firepoint
        public Transform firePoint3; // Terceiro firepoint
        public Transform firePoint4; // Quarto firepoint

        public float simpleShotCooldown = 0.5f; // Cooldown entre os tiros

        private float lastSimpleShotTime; // Tempo do �ltimo tiro
        private int currentFirePointIndex = 0; // �ndice do firepoint atual
        private bool isVisible = false; // Verifica se o objeto est� vis�vel na c�mera

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
            // Escolhe o firepoint atual com base no �ndice
            Transform firePoint = GetCurrentFirePoint();

            // Instancia o proj�til no ponto de disparo
            CreateProjectile(firePoint, simpleShotPrefab);

            // Avan�a para o pr�ximo firepoint
            currentFirePointIndex = (currentFirePointIndex + 1) % 4; // Alterna entre 0, 1, 2, 3

            // Atualiza o momento do �ltimo disparo
            lastSimpleShotTime = Time.time;
        }

        private Transform GetCurrentFirePoint()
        {
            // Retorna o firepoint correspondente ao �ndice atual
            switch (currentFirePointIndex)
            {
                case 0: return firePoint1;
                case 1: return firePoint2;
                case 2: return firePoint3;
                case 3: return firePoint4;
                default: return firePoint1; // Caso padr�o (nunca deve acontecer)
            }
        }

        private void CreateProjectile(Transform firePoint, GameObject prefab)
        {
            // Instancia o proj�til no firepoint
            GameObject projectile = Instantiate(prefab, firePoint.position, firePoint.rotation);

            // Encontra o jogador para definir a dire��o do tiro
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Configura a dire��o do proj�til
            if (projectile.TryGetComponent(out EnemyShotController shotController) && player != null)
            {
                Vector2 playerPosition = player.transform.position;
                Vector2 firePointPosition = firePoint.position;
                Vector2 shootDirection = playerPosition - firePointPosition;

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