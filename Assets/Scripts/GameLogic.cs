using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static float DropTime = 0.8f;
    public static float QuickDropTime = 0.05f;
    public static int width = 15, height = 30;
    public GameObject[] blocks;
    public Transform[,] grid = new Transform[width, height+2];
    public static int Lines = 0;
    public Text Linestxt;
    public GameObject gameovermenu;
    public GameObject pausemenu;
    public Text pointText;
    public Text timeToStart;
    public bool pause = false;
    void Start()
    {
        SpawnBlock();
    }

    void Update()
    {
        if (pausemenu.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            pausemenu.SetActive(true);
            BlockLogic.ispaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
        Linestxt.text = "Lines: " + Lines;
        if (IsItLost())
        {
            gameovermenu.SetActive(true);
            pointText.text = "YOU DONE " + Lines.ToString() + " LINES";
        }
        TimeToStart();
    }
    void TimeToStart()
    {
        if (Time.timeSinceLevelLoad > 3)
        {
            BlockLogic.isstarted = true;
        }
        if (Time.timeSinceLevelLoad < 1)
            timeToStart.text = "3";
        else if (Time.timeSinceLevelLoad > 1 && Time.timeSinceLevelLoad < 2)
            timeToStart.text = "2";
        else if (Time.timeSinceLevelLoad > 2 && Time.timeSinceLevelLoad < 3)
            timeToStart.text = "1";
        else
            timeToStart.text = " ";

    }
    public void Resume()
    {
        pausemenu.SetActive(false);
        BlockLogic.ispaused = false;
    }
    public void Restart()
    {
        Lines = 0;
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        Lines = 0;
        SceneManager.LoadScene(0);
        BlockLogic.ispaused = false;
    }
    public void ClearLines()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsLineCompleted(y))
            {
                if (IsLineCompleted(y+1))
                {
                    if (IsLineCompleted(y+2))
                    {
                        if (IsLineCompleted(y+3))
                        {
                            DestroyLine(y+3);
                            MoveLines(y+3);
                            Lines++;
                        }
                        DestroyLine(y+2);
                        MoveLines(y+2);
                        Lines++;
                    }
                    DestroyLine(y+1);
                    MoveLines(y+1);
                    Lines++;
                }
                DestroyLine(y);
                MoveLines(y);
                Lines++;
            }
        }
    }


    bool IsItLost()
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, height - 1] != null)
            {
                return true;
            }
        }
        return false;
    }

    void DestroyLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
                Destroy(grid[x,y].gameObject);
        }
    }

    void MoveLines(int y)
    {
        for (int i = y; i < height - 1; i++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, i + 1] != null)
                {
                    grid[x, i] = grid[x, i + 1];
                    grid[x, i].gameObject.transform.position -= new Vector3(0, 1, 0);
                    grid[x, i + 1] = null;
                }
            }
        }
    }

    bool IsLineCompleted(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    public void SpawnBlock()
    {
        if (!IsItLost())
        {
            float guess = Random.Range(0, 1f);
            guess *= blocks.Length;

            Instantiate(blocks[Mathf.FloorToInt(guess)]);
        }
    }
}
