using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button buttonRestart;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(ReloadScene);
    }
    public void PlayerDied()
    {
        gameObject.SetActive(true);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
