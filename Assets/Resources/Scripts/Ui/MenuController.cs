using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // M�todo chamado quando o bot�o � clicado
    public void OnStartButtonClicked()
    {
        Debug.Log("Bot�o");
        SceneManager.LoadScene("GameScene");
    }
}