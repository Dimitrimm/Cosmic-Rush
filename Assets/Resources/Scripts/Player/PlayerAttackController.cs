using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicRush
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("Configura��es de Tiro")]
        public GameObject simpleShotPrefab;
        public GameObject superShotPrefab;

        public Transform leftFirePoint;
        public Transform rightFirePoint;
        public Transform centerFirePoint;

        public float simpleShotCooldown = 0.5f;
        public float superShotCoolDown = 1.5f;

        private float lastSimpleShotTime;
        private float lastSuperShotTime;

        private bool shootFromLeft = true;

        private void Update()
        {
            // Verifica se o jogador pressionou o bot�o de atirar e se o cooldown acabou
            if (Input.GetKey(KeyCode.Mouse0) && Time.time - lastSimpleShotTime >= simpleShotCooldown)
            {
                SimpleShot();
            }
            if (Input.GetKey(KeyCode.Mouse1) && Time.time - lastSuperShotTime >= superShotCoolDown)
            {
                SuperShot();
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

        private void SuperShot()
        {
            CreateProjectile(centerFirePoint, superShotPrefab);

            lastSuperShotTime = Time.time;
        }

        private void CreateProjectile(Transform firePoint, GameObject prefab)
        {
            GameObject projectile = Instantiate(prefab, firePoint.position, firePoint.rotation);

            // Configura a dire��o do proj�til
            if (projectile.TryGetComponent(out ShotController shotController))
            {
                Vector2 mouseWorldPosition = GetMouseWorldPosition();
                Vector2 firePointPosition = firePoint.position;
                Vector2 shootDirection = mouseWorldPosition - firePointPosition;

                shotController.SetDirection(shootDirection);
            }
        }


        private Vector2 GetMouseWorldPosition()
        {
            // Obt�m a posi��o do mouse na tela (em pixels)
            Vector3 mouseScreenPosition = Input.mousePosition;

            // Converte a posi��o do mouse para coordenadas do mundo
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            // Retorna apenas as coordenadas X e Y (2D)
            return new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
        }
    }
}
