using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] Button buttonPlay;
    [SerializeField] GameObject LevelSelection;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        SoundManager.Instance.Play(SoundsForEvents.ButtonClick);
        //SceneManager.LoadScene(1);
        LevelSelection.SetActive(true);
    }
}
