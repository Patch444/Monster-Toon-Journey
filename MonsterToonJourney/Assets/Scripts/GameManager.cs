using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused;
    public bool hasUnpaused;
    public GameObject pauseMenu;
    public int lives = 3;
    public Sprite[] fearSprites = new Sprite[4];
    public PlayerMove player;
    public string currentLevel = "Level 1";
    public SpriteRenderer playerSR;

    public float flickerDuration;

    public float flickerTimer = 0f;

    public GlobalManager globalManager;

    public ArrowSpawn asp;

    public AudioClip PlayerHurt;

    public AudioSource Audio;

    public GameObject HurtSoundObject;

    [SerializeField]
    Sprite fearMeter;

    // Start is called before the first frame update
    void Start()
    {
        // Assigns the Global Manager.
        globalManager = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        fearMeter = fearSprites[lives];
        GameObject.Find("Fear Meter").GetComponent<Image>().sprite = fearMeter;
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        playerSR = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        HurtSoundObject = GameObject.Find("HurtSounds");

        Audio = HurtSoundObject.GetComponent<AudioSource>();

        flickerDuration = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            HitFlicker();
            if (Input.GetKeyDown(KeyCode.R))
            {
                isPaused = true;
            }
        }
        else
        {
            pauseMenu.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                isPaused = false;
                hasUnpaused = true;
                pauseMenu.SetActive(false);
            }
        }
        
    }

    public void StartFlickerTimer()
    {
        flickerTimer = flickerTimer + Time.deltaTime;
    }

    public void HitFlicker()
    {
        if(player.beenHit == true)
        {
            StartFlickerTimer();
            if (flickerTimer >= 0.0f && flickerTimer <= 0.1f)
            {
                playerSR.enabled = false;
            }
            if(flickerTimer >= 0.2f && flickerTimer <=0.3f)
            {
                playerSR.enabled = true;
            }
            if(flickerTimer >= 0.4f && flickerTimer <= 0.5f)
            {
                playerSR.enabled = false;
            }
            if (flickerTimer >= 0.6f && flickerTimer <= 0.7f)
            {
                playerSR.enabled = true;
            }
            if (flickerTimer >= 0.8f && flickerTimer <= 0.9f)
            {
                playerSR.enabled = false;
            }
            if (flickerTimer >= 1.0f && flickerTimer <= 1.1f)
            {
                playerSR.enabled = true;
            }
            if (flickerTimer >= 1.2f && flickerTimer <= 1.3f)
            {
                playerSR.enabled = false;
            }
            if (flickerTimer >= 1.4f && flickerTimer <= 1.5f)
            {
                playerSR.enabled = true;
                flickerTimer = 0;
                player.beenHit = false;
            }
        }

    }

    public void LoseLife()
    {
        Audio.Play();
        lives--;
        fearMeter = fearSprites[lives];

        player.transform.position = player.spawnPosition;
        if (lives > 0)
        {
            player.isDead = false;

            GameObject.Find("Fear Meter").GetComponent<Image>().sprite = fearMeter;
        }
        else
        {
            GameOver();
        }
    }

    public void TakeHit()
    {
        Audio.Play();
        lives--;
        fearMeter = fearSprites[lives];
        if (lives > 0)
        {
            HitFlicker();
            player.isDead = false;

            GameObject.Find("Fear Meter").GetComponent<Image>().sprite = fearMeter;

        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        // Checks if the player is on the first level.
        if(currentLevel == "Level 1")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_L1");
            
        }

        // Checks if the player is on the second level.
        if (currentLevel == "Level 2")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_L2");
        }

        // Checks if the player is on the third level.
        if (currentLevel == "Level 3")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_L3");
        }

        // Checks if the player is on the fourth level.
        if (currentLevel == "Level 4")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_L4");
        }

        // Checks if the player is on the fifth level.
        if (currentLevel == "Level 5")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_L5");
        }

        // Checks if the player is on the sixth level.
        if (currentLevel == "Shield Demo")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_L6");
        }
    }

    public void AdvanceLevel()
    {
        switch (currentLevel)
        {
            case "Level 1":
                if (globalManager.howFar < 2)
                {
                    globalManager.howFar = 2;
                }
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Level_2");
                break;
            case "Level 2":
                if (globalManager.howFar < 3)
                {
                    globalManager.howFar = 3;
                }
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Level_3");
                break;
            case "Level 3":
                if (globalManager.howFar < 4)
                {
                    globalManager.howFar = 4;
                }
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Level_4");
                break;
            case "Level 4":
                if (globalManager.howFar < 5)
                {
                    globalManager.howFar = 5;
                }
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Level_5");
                break;
            case "Level 5":
                if (globalManager.howFar < 6)
                {
                    globalManager.howFar = 6;
                }
                break;
            case "Shield Demo":
                if (globalManager.howFar < 7)
                {
                    globalManager.howFar = 7;
                }
                break;
        }
        if (globalManager.howFar == 7)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
        }
        

    }

    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevelSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }
    public void Continue()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }
}
