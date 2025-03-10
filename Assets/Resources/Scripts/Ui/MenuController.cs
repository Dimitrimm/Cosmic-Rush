using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Método chamado quando o botão é clicado
    public void OnStartButtonClicked()
    {
        Debug.Log("Botão");
        SceneManager.LoadScene("GameScene");
    }
}