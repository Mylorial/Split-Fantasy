using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

   public TextMeshProUGUI scoreText;
   public TextMeshProUGUI hiscoreText;
   public TextMeshProUGUI gameOverText;
   public Button retryButton;
   public Walljump player;
   private Spawner spawner;
   private float score;

    private void Awake()
    {
        if (Instance != null) {
            Instance = this;
            
        } else {
        DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
       player = FindObjectOfType<Walljump>();
       spawner = FindObjectOfType<Spawner>();
       NewGame();
   }
    

    public void NewGame()
    {
        gameSpeed = initialGameSpeed;
       enabled = true;
       player.gameObject.SetActive(true);
       spawner.gameObject.SetActive(true);
       gameOverText.gameObject.SetActive(false);
       retryButton.gameObject.SetActive(false);
       UpdateHiscore();

    }

   public void GameOver()
   {
       gameSpeed = 0f;
       enabled = false;
       player.gameObject.SetActive(false);
       spawner.gameObject.SetActive(false);
       gameOverText.gameObject.SetActive(true);
       retryButton.gameObject.SetActive(true);
       UpdateHiscore();
   }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;

    }

       private void UpdateHiscore()
   {
       float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
       if (score > hiscore)
       {
           hiscore = score;
           PlayerPrefs.SetFloat("hiscore", hiscore);
       }
       hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
   }
}

