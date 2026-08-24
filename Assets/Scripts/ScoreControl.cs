using UnityEngine;
using TMPro;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] GameObject scoreBox;
    public static int totalScore = 0;
    private TMP_Text scoreTextComponent;

    void Start()
    {
        FindScoreText();
    }

    void FindScoreText()
    {
        if (scoreBox != null)
        {
            scoreTextComponent = scoreBox.GetComponent<TMP_Text>();
            if (scoreTextComponent == null)
            {
                scoreTextComponent = scoreBox.GetComponentInChildren<TMP_Text>();
            }
        }

        if (scoreTextComponent == null)
        {
            scoreTextComponent = GetComponent<TMP_Text>();
        }

        if (scoreTextComponent == null)
        {
            scoreTextComponent = GetComponentInChildren<TMP_Text>();
        }

        if (scoreTextComponent == null)
        {
            scoreTextComponent = Object.FindFirstObjectByType<TMP_Text>();
        }
    }

    void Update()
    {
        if (scoreTextComponent == null)
        {
            FindScoreText();
        }

        if (scoreTextComponent != null)
        {
            scoreTextComponent.text = "SCORE: " + totalScore;
            if (!scoreTextComponent.enabled) scoreTextComponent.enabled = true;
        }
    }

    public static void AddScore(int amount)
    {
        totalScore += amount;
        Debug.Log("Score Updated: " + totalScore);
    }
}
