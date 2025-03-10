using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicRush
{
    public class Rotator : MonoBehaviour
    {
        public float velocidadeRotacao = 100f; // Velocidade de rota��o em graus por segundo
        private Vector3 eixoRotacao = Vector3.back; // Eixo de rota��o (Y no caso)

        void Update()
        {
            // Gira o objeto no eixo especificado
            transform.Rotate(eixoRotacao * velocidadeRotacao * Time.deltaTime);
        }
    }
}
