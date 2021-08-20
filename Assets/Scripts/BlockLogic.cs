using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    public bool movable = true;
    float timer = 0f;
    public GameObject rig;
    public static bool isstarted = false;
    public static bool ispaused = false;

    GameLogic gamelogic;
    void Start()
    {
        isstarted = false;
        gamelogic = FindObjectOfType<GameLogic>();         
    }
    void RegisterBlock()
    {
        foreach(Transform subBlock in rig.transform)
        {
            gamelogic.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] = subBlock;
        }
    }
    //Playzone(Playground)
    bool CheckValid()
    {
        foreach (Transform subBlock in rig.transform)
        {
            if(subBlock.transform.position.x >= GameLogic.width || subBlock.transform.position.x < 0 || subBlock.transform.position.y < 0)
            {
                return false; 
            }
            if (subBlock.position.y < GameLogic.height && gamelogic.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] != null)
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        if (isstarted)
        {
            if (!ispaused)
            {
                if (movable)
                {
                    //The timer begining
                    timer += 1 * Time.deltaTime;
                    //Fall
                    if (Input.GetKey(KeyCode.DownArrow) && timer > GameLogic.QuickDropTime)
                    {
                        gameObject.transform.position -= new Vector3(0, 1, 0);
                        timer = 0;
                        if (!CheckValid())
                        {
                            movable = false;
                            gameObject.transform.position += new Vector3(0, 1, 0);
                            RegisterBlock();
                            gamelogic.ClearLines();
                            gamelogic.SpawnBlock();
                        }
                    }
                    else if (timer > GameLogic.DropTime / (2 * MenuScript.LevelOfDifficulty))
                    {
                        gameObject.transform.position -= new Vector3(0, 1, 0);
                        timer = 0;
                        if (!CheckValid())
                        {
                            movable = false;
                            gameObject.transform.position += new Vector3(0, 1, 0);
                            RegisterBlock();
                            gamelogic.ClearLines();
                            gamelogic.SpawnBlock();
                        }
                    }
                    //Moving right and left
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        gameObject.transform.position -= new Vector3(1, 0, 0);
                        if (!CheckValid())
                        {
                            gameObject.transform.position += new Vector3(1, 0, 0);
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        gameObject.transform.position += new Vector3(1, 0, 0);
                        if (!CheckValid())
                        {
                            gameObject.transform.position -= new Vector3(1, 0, 0);
                        }
                    }
                    //Rotate
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        rig.transform.eulerAngles -= new Vector3(0, 0, 90);
                        if (!CheckValid())
                        {
                            rig.transform.eulerAngles += new Vector3(0, 0, 90);
                        }
                    }
                }
            }
        }
    }
}
