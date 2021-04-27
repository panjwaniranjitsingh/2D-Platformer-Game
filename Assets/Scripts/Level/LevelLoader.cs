using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button m_button;

    [SerializeField] private string m_LevelName;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        SoundManager.Instance.Play(SoundsForEvents.ButtonClick);
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(m_LevelName);
        //Debug.Log(m_LevelName+" is "+levelStatus);
        switch(levelStatus)
        {
            default:
                //Debug.Log("Default Case");
                SceneManager.LoadScene(m_LevelName);
                break;

            case LevelStatus.Locked:
                Debug.Log("Can't play this level till you unlock it");
                break;


                /*case LevelStatus.Unlocked:
                  case LevelStatus.Completed:
                    SceneManager.LoadScene(LevelName);
                    break;*/
        }
    }
}
