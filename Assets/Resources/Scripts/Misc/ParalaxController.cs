using UnityEngine;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform[] sprites; // Sprites da camada
        [Range(0, 1)] public float scrollSpeedPercent; // Velocidade de scroll em porcentagem (0% a 100%)
        public float spriteHeight;  // Altura da sprite
    }

    public float globalScrollSpeed = 5f; // Velocidade global de scroll
    public List<ParallaxLayer> layers;   // Lista de camadas

    private void Update()
    {
        foreach (ParallaxLayer layer in layers)
        {
            // Calcula a velocidade da camada com base na porcentagem e na velocidade global
            float layerScrollSpeed = globalScrollSpeed * layer.scrollSpeedPercent;

            foreach (Transform sprite in layer.sprites)
            {
                // Move a sprite para baixo
                sprite.Translate(Vector3.down * layerScrollSpeed * Time.deltaTime);

                // Verifica se a sprite saiu da tela
                if (sprite.position.y <= -layer.spriteHeight)
                {
                    // Reposiciona a sprite no topo
                    Vector3 newPosition = new Vector3(sprite.position.x, layer.spriteHeight, sprite.position.z);
                    sprite.position = newPosition;
                }
            }
        }
    }
}