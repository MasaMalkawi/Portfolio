using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Health healthComponent;

    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        if (healthComponent == null)
        {
            healthComponent = FindObjectOfType<Health>();
            if (healthComponent == null)
            {
                Debug.LogError("Health component is not found in the scene.");
            }
        }
        else
        {
            Debug.Log("Health component assigned successfully in the Inspector.");
        }

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        if (healthComponent != null)
        {
            healthComponent.ResetHealth();
        }
        else
        {
            Debug.LogWarning("Health component is not assigned in GameManager.");
        }

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void DecreasePlayerHealth()
    {
        if (healthComponent != null)
        {
            healthComponent.DecreaseHealth();
        }
        else
        {
            Debug.LogWarning("Health component is not assigned in GameManager.");
        }
    }
}
