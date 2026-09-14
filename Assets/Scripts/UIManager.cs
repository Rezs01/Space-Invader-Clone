using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public const string HIGHSCORE_KEY = "Highscore";
    [SerializeField] TMP_Text title, pressEnter, scoreText, gameOverText, endScore;
    [SerializeField] GameObject ships;
    [SerializeField] AudioClip highscoreSFX;

    float score, highscore;
    bool gameHasStarted = false;
    InputAction submit;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        submit = InputSystem.actions.FindAction("Submit");
        highscore = PlayerPrefs.GetFloat(HIGHSCORE_KEY, 0);
    }

    private void Update()
    {
        if (!gameHasStarted && submit.WasPressedThisFrame())
        {
            gameHasStarted = true;
            title.gameObject.SetActive(false);
            pressEnter.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            ships.SetActive(true);
            GameManager.Instance.SpawnEnemyCluster();
        }
    }

    [ContextMenu("Reset Highscore")]
    void ResetHighscore()
    {
        PlayerPrefs.DeleteKey(HIGHSCORE_KEY);
        highscore = 0;
    }

    public void AddScore(float score)
    {
        this.score += score;
        scoreText.text = $"Score: {this.score}";
    }

    public void GameOver() => StartCoroutine(GameOverRoutine());
    IEnumerator GameOverRoutine()
    {
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        gameOverText.gameObject.SetActive(false);

        endScore.text = score > highscore ? $"NEW HIGHSCORE!\n{score}" : $"SCORE\n{score}";
        endScore.gameObject.SetActive(true);

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetFloat(HIGHSCORE_KEY, highscore);
            AudioManager.Instance.PlaySFX(highscoreSFX);
        }
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
