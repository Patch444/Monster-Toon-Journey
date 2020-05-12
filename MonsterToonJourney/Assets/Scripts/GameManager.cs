using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused;
    public bool hasUnpaused;
    public bool hasUsedSlime;
    public bool isInFinal;
    public bool isBossLevel;
    public GameObject pauseMenu;
    public int lives = 4;
    public Sprite[] fearSprites = new Sprite[5];
    public PlayerMove player;
    public string currentLevel = "Level 1";
    public SpriteRenderer playerSR;

    public Image mamaMeter;
    public Text countdownTxt;

    public PlayerShieldSlime pSS;


    public float flickerDuration;

    public float flickerTimer = 0f;

    //public GlobalManager globalManager;

    public ArrowSpawn asp;

    public AudioClip PlayerHurt;

    public AudioSource Audio;
    public AudioSource LevelMusic;

    public GameObject HurtSoundObject;


    public Scene currentScene;
    public string sceneName;

    [SerializeField]
    Sprite fearMeter;


    // Start is called before the first frame update
    void Start()
    {

        

        // Assigns the Global Manager.
        //globalManager = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();

        hasUsedSlime = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        fearMeter = fearSprites[lives];

        pSS = GameObject.Find("PlayerShieldSlime").GetComponent<PlayerShieldSlime>();

        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName != "Intro")
        {
            GameObject.Find("Fear Meter").GetComponent<Image>().sprite = fearMeter;
            pauseMenu = GameObject.Find("PauseMenu");
            pauseMenu.SetActive(false);
            playerSR = GameObject.Find("Player").GetComponent<SpriteRenderer>();
            HurtSoundObject = GameObject.Find("HurtSounds");


            Audio = HurtSoundObject.GetComponent<AudioSource>();
            //Audio.volume = PlayerPrefs.GetFloat("FxVolume");
            //LevelMusic = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
            //LevelMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
            flickerDuration = 2f;
        }


        if (currentLevel == "Boss Level 1")
        {
            isInFinal = true;
        }
        else
        {
            isInFinal = false;
        }

        if (isBossLevel == false || sceneName == "Intro")
        {
            countdownTxt = GameObject.Find("Countdown Text").GetComponent<Text>();
            countdownTxt.text = null;
            mamaMeter = GameObject.Find("Mama Meter").GetComponent<Image>();
            mamaMeter.enabled = false;
        }
        else 
        {
            mamaMeter = GameObject.Find("Mama Meter").GetComponent<Image>();
            mamaMeter.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            HitFlicker();
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = true;
                Time.timeScale = 0;
            }
        }
        else
        {
            pauseMenu.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = false;
                hasUnpaused = true;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }

    }

    public void StartFlickerTimer()
    {
        flickerTimer = flickerTimer + Time.deltaTime;
    }

    public void HitFlicker()
    {
        if (player.beenHit == true)
        {
            StartFlickerTimer();
            if (flickerTimer >= 0.0f && flickerTimer <= 0.1f)
            {
                playerSR.enabled = false;
            }
            if (flickerTimer >= 0.2f && flickerTimer <= 0.3f)
            {
                playerSR.enabled = true;
            }
            if (flickerTimer >= 0.4f && flickerTimer <= 0.5f)
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
        
        if (player.hasShieldSlime == true)
        {
            pSS.Disperse();
            if (player.immune == false)
            {
                player.immune = true;
                StartCoroutine(ImmunityFrames());
            }
            return;
        }
        else
        {
            player.beenHit = true;
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
    }

    public void TakeHit()
    {
        if (player.hasShieldSlime == true)
        {
            pSS.Disperse();
            player.Audio.clip = player.shieldSlimeDown;
            player.Audio.Play();
            if (player.immune == false)
            {
                player.immune = true;
                StartCoroutine(ImmunityFrames());
            }
            return;
        }
        player.beenHit = true;
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

    public IEnumerator ImmunityFrames()
    {
        yield return new WaitForSeconds(1.5f);
        if (player.immune == true)
        {
            player.hasShieldSlime = false;
            pSS.anim.SetBool("wasUsed", false);
            player.immune = false;
        }
    }

    public void GameOver()
    {
        // Checks if the player is on the BlanketArrow test level.
        if (currentLevel == "Demo_BlanketArrows")
        {
            // Loads the Slimes test game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_BATest");
        }
        // Checks if the player is on the Slimes test level.
        if (currentLevel == "Demo_Slimes")
        {
            // Loads the Slimes test game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_STest");
        }
        //Checks if the player is on the KeyButton test level.
        if (currentLevel == "Demo_KeyButton")
        {
            // Loads the KeyButton test game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_KBTest");
        }
        // Checks if the player is on the first level.
        if (currentLevel == "Level 1")
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

        // Checks if the player is on the seventh level.
        if (currentLevel == "Level 7")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_L7");
        }

        // Checks if the player is on the eighth level.
        if (currentLevel == "Level 8")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_L8");
        }

        // Checks if the player is on the first boss level.
        if (currentLevel == "Boss Level 1")
        {
            // Loads the game over screen.
            UnityEngine.SceneManagement.SceneManager.LoadScene("GO_BL1");
        }
    }

    public void AdvanceLevel()
    {
        //Debug.Log(PlayerPrefs.GetInt("HowFar"));
        switch (currentLevel)
        {
            case "Level 1":
                if (PlayerPrefs.GetInt("HowFar") < 1)
                {
                    PlayerPrefs.SetInt("HowFar", 2);
                }
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Level_2");
                break;
            case "Level 2":
                if (PlayerPrefs.GetInt("HowFar") < 3)
                {
                    PlayerPrefs.SetInt("HowFar", 3);
                }
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Level_3");
                break;
            case "Level 3":
                if (PlayerPrefs.GetInt("HowFar") < 4)
                {
                    PlayerPrefs.SetInt("HowFar", 4);
                }
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Level_4");
                break;
            case "Level 4":
                if (PlayerPrefs.GetInt("HowFar") < 5)
                {
                    PlayerPrefs.SetInt("HowFar", 5);
                }
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Level_5");
                break;
            case "Level 5":
                if (PlayerPrefs.GetInt("HowFar") < 6)
                {
                    PlayerPrefs.SetInt("HowFar", 6);
                }
                break;
            case "Shield Demo":
                if (PlayerPrefs.GetInt("HowFar") < 7)
                {
                    PlayerPrefs.SetInt("HowFar", 7);
                }
                break;
            case "Level 7":
                if (PlayerPrefs.GetInt("HowFar") < 8)
                {
                    PlayerPrefs.SetInt("HowFar", 8);
                }
                break;
            case "Level 8":
                if (PlayerPrefs.GetInt("HowFar") < 9)
                {
                    PlayerPrefs.SetInt("HowFar", 9);
                }
                break;
        }
        if (PlayerPrefs.GetInt("HowFar") == 9 && isInFinal == true)
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
