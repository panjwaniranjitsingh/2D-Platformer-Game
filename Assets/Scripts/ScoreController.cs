using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int m_score = 0;
    const string SCORE ="Score: ";

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
            m_score = LevelManager.Instance.score;
        else
            m_score = 0;
        RefreshUI();
    }

    public void IncreaseScore(int increment)
    {
        m_score = LevelManager.Instance.score;
        m_score += increment;
        RefreshUI();
    }

    public int GetScore()
    {
        return m_score;
    }

    public void SetScore(int value)
    {
        m_score=value;
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoreText.text = SCORE + m_score;
        LevelManager.Instance.score = m_score;
    }
}
