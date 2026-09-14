using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] TMP_Text title, pressEnter, scoreText, gameOverText;
    [SerializeField] GameObject ships;

    float score;
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
        }
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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
