using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] Button buttonRestart;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(ReloadScene);
    }
    public void PlayerDied()
    {
        SoundManager.Instance.Play(SoundsForEvents.PlayerDeath);
        gameObject.SetActive(true);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
