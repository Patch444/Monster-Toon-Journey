﻿
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{


    // Sets up a string to keep track of what level the player was on.
    public string currentLevel;

    // Sets up the buttons.
    public GameObject startBtn;
    public GameObject levelSelectBtn;
    public GameObject quitBtn;
    public GameObject levelOneBtn;
    public GameObject levelTwoBtn;
    public GameObject levelThreeBtn;
    public GameObject levelFourBtn;
    public GameObject levelFiveBtn;
    public GameObject levelSixBtn;
    public GameObject levelSevenBtn;
    public GameObject levelEightBtn;
    public GameObject settingsBtn;
    public GameObject backBtn;
    public GameObject resetBtn;
    public GameObject resetCheckBtn1;
    public GameObject resetCheckBtn2;

    public Text resetText;
    public VolumeManager vm;
    public Scene currentScene;
    public string sceneName;
    public GlobalManager globalManager;

    void Start()
    {
        // Assigns the Global Manager.
        globalManager = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();
        // Checks which scene the scene manager is in.
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        // Checks if the player is in level select.
        if (sceneName == "LevelSelect")
        {
            // Deactivates all of the level buttons besides the first.
            levelTwoBtn.SetActive(false);
            levelThreeBtn.SetActive(false);
            levelFourBtn.SetActive(false);
            levelFiveBtn.SetActive(false);
            levelSixBtn.SetActive(false);
            levelSevenBtn.SetActive(false);
            levelEightBtn.SetActive(false);
        }
        if (sceneName == "Settings")
        {
            vm = GameObject.Find("VolumeManager").GetComponent<VolumeManager>();
            resetBtn = GameObject.Find("Reset_Btn");
            resetCheckBtn1 = GameObject.Find("ResetCheck_Btn_1");
            resetCheckBtn1.SetActive(false);
            resetCheckBtn2 = GameObject.Find("ResetCheck_Btn_2");
            resetCheckBtn2.SetActive(false);
            resetText = GameObject.Find("Reset_Txt").GetComponent<Text>();
            resetText.text = "";
        }
    }

    void Update()
    {
        // Checks if the player is in level select.
        if (sceneName == "LevelSelect")
        {
            LevelUnlock();
        }


    }

    // Transitions player to the Blanket Arrow Test Level.
    public void ToBlanketArrows()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Demo_BlanketArrows");
    }

    // Transitions player to the Slimes Test Level.
    public void ToSlimes()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Demo_Slimes");
    }

    // Transitions player to the KeyButton Test Level.
    public void ToKeyButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Demo_KeyButton");
    }

    // Transitions the player to the level select.
    public void ToLevelSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }

    // Transitions the player to the settings.
    public void ToSettings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }

    // Transitions the player to the first level.
    public void ToLevelOne()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
    }

    // Transitions the player to the second level.
    public void ToLevelTwo()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_2");
    }

    // Transitions the player to the third level.
    public void ToLevelThree()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_3");
    }

    // Transitions the player to the fourth level.
    public void ToLevelFour()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_4");
    }

    // Transitions the player to the fifth level.
    public void ToLevelFive()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_5");
    }

    // Transitions the player to the sixth level.
    public void ToLevelSix()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Demo_Shield");
    }

    // Transitions the player to the seventh level.
    public void ToLevelSeven()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_7");
    }

    // Transitions the player to the eighth level.
    public void ToLevelEight()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_8");
    }

    // Transitions the player to the first boss level.
    public void ToBossOne()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BossLevel_1");
    }

    /*
    // Transitions the player to the game over screen for the first level.
    public void ToGameOverOne()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GO_1");
    }

    // Transitions the player to the game over screen for the second level.
    public void ToGameOverTwo()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GO_2");
    }

    // Transitions the player to the game over screen for the third level.
    public void ToGameOverThree()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GO_3");
    }

    // Transitions the player to the game over screen for the fourth level.
    public void ToGameOverFour()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GO_4");
    }

    // Transitions the player to the game over screen for the fifth level.
    public void ToGameOverFive()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GO_5");
    }

    // Transitions the player to the game over screen for the sixth level.
    public void ToGameOverSix()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GO_6");
    }
    */
    // Transitions the player to the win screen.
    public void ToWin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
    }

    // Transitions the player to the main menu.
    public void ToMainMenu()
    {
        if (sceneName == "Settings")
        {
            vm.VolumePrefs();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }

    // Lets the player try the previous level again.
    public void TryAgain()
    {
        // Checks if the player was on the BlanketArrow test level.
        if (currentLevel == "Demo_BlanketArrows")
        {
            // Loads Slimes test level.
            ToBlanketArrows();
        }
        // Checks if the player was on the Slime test level.
        if (currentLevel == "Demo_Slimes")
        {
            // Loads Slimes test level.
            ToSlimes();
        }
        // Checks if the player was on the KeyButton test level.
        if (currentLevel == "Demo_KeyButton")
        {
            // Loads KeyButton test level.
            ToKeyButton();
        }
        // Checks if the player was on level one.
        if (currentLevel == "Level_1")
        {
            // Loads the first level.
            ToLevelOne();
        }

        // Checks if the player was on level two.
        if (currentLevel == "Level_2")
        {
            // Loads the second level.
            ToLevelTwo();
        }

        // Checks if the player was on level three.
        if (currentLevel == "Level_3")
        {
            // Loads the third level.
            ToLevelThree();
        }

        // Checks if the player was on level four.
        if (currentLevel == "Level_4")
        {
            // Loads the fourth level.
            ToLevelFour();
        }

        // Checks if the player was on level five.
        if (currentLevel == "Level_5")
        {
            // Loads the fifth level.
            ToLevelFive();
        }

        // Checks if the player was on the shield demo level.
        if (currentLevel == "Shield_Demo")
        {
            // Loads the shield demo level.
            ToLevelSix();
        }

        // Checks if the player was on level seven.
        if (currentLevel == "Level_7")
        {
            // Loads the seventh level.
            ToLevelSeven();
        }

        // Checks if the player was on level eight.
        if (currentLevel == "Level_8")
        {
            // Loads the eighth level.
            ToLevelEight();
        }

        // Checks if the player was on the first boss level.
        if (currentLevel == "BossLevel_1")
        {
            ToBossOne();
        }
    }

    // Unlocks levels based on how far the player has gotten in the game.
    public void LevelUnlock()
    {
        if (PlayerPrefs.GetInt("HowFar") >= 2)
        {
            levelTwoBtn.SetActive(true);
        }
        if (PlayerPrefs.GetInt("HowFar") >= 3)
        {
            levelThreeBtn.SetActive(true);
        }
        if (PlayerPrefs.GetInt("HowFar") >= 4)
        {
            levelFourBtn.SetActive(true);
        }
        if (PlayerPrefs.GetInt("HowFar") >= 5)
        {
            levelFiveBtn.SetActive(true);
        }
        if (PlayerPrefs.GetInt("HowFar") >= 6)
        {
            levelSixBtn.SetActive(true);
        }
        if (PlayerPrefs.GetInt("HowFar") >= 7)
        {
            levelSevenBtn.SetActive(true);
        }
        if (PlayerPrefs.GetInt("HowFar") >= 8)
        {
            levelEightBtn.SetActive(true);
        }

    }

    public void ResetCheck()
    {
        resetBtn.SetActive(false);
        resetCheckBtn1.SetActive(true);
        resetCheckBtn2.SetActive(true);
        resetText.text = "Are you sure you want to reset your progress?";
    }

    public void ResetCancel()
    {
        resetBtn.SetActive(true);
        resetCheckBtn1.SetActive(false);
        resetCheckBtn2.SetActive(false);
        resetText.text = "";
    }

    public void ResetConfirm()
    {
        resetBtn.SetActive(true);
        resetCheckBtn1.SetActive(false);
        resetCheckBtn2.SetActive(false);
        PlayerPrefs.SetInt("HowFar", 0);
        resetText.text = "Your progress has been reset!";
    }

    // Exits the application.
    public void Quit()
    {
        Application.Quit();
    }
}