
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
    public GameObject settingsBtn;
    public GameObject backBtn;
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

    // Transitions the player to the win screen.
    public void ToWin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
    }

    // Transitions the player to the main menu.
    public void ToMainMenu()
    {
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
    }

    // Unlocks levels based on how far the player has gotten in the game.
    public void LevelUnlock()
    {
        if (globalManager.howFar >= 2)
        {
            levelTwoBtn.SetActive(true);
        }
        if (globalManager.howFar >= 3)
        {
            levelThreeBtn.SetActive(true);
        }
        if (globalManager.howFar >= 4)
        {
            levelFourBtn.SetActive(true);
        }
        if (globalManager.howFar >= 5)
        {
            levelFiveBtn.SetActive(true);
        }
        if (globalManager.howFar >= 6)
        {
            levelSixBtn.SetActive(true);
        }
    }

    // Exits the application.
    public void Quit()
    {
        Application.Quit();
    }
}