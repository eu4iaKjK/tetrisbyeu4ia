using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject levelChanger;
    public GameObject ExitPanel;
    public GameObject Play;
    public GameObject Exit;
    public static int LevelOfDifficulty = 1;

    private void Update()
    {
        if(levelChanger.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            levelChanger.SetActive(false);
            Play.SetActive(true);
            Exit.SetActive(true);

        }
        else if (ExitPanel.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanel.SetActive(true);
            Play.SetActive(false);
            Exit.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            NoExit();
        }
    }
    public void NoExit()
    {
        ExitPanel.SetActive(false);
        Play.SetActive(true);
        Exit.SetActive(true);
    }

    public void Level(int level)
    {
        SceneManager.LoadScene(1);
        LevelOfDifficulty = level;
    }
    public void ClickPlay()
    {
        levelChanger.SetActive(true);
        Play.SetActive(false);
        Exit.SetActive(false);
    }
    public void ClickExit()
    {
        Application.Quit();
    }
}
