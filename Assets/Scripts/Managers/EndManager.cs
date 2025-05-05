using UnityEngine.SceneManagement;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MenuGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
