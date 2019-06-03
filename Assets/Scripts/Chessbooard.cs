﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chessbooard : MonoBehaviour {

    static Chessbooard _instacne;




    public ChessType turn = ChessType.Black;
    public int[,] grid;
    public GameObject[] prefabs;
    public float timer = 0;
    public bool gameStart = false;
    Transform parent;
    public Text Winner;
    public Stack<Transform> chessStack = new Stack<Transform>();



    public static Chessbooard Instacne
    {
        get
        {
            return _instacne;
        }
    }

    private void Awake()
    {
        if (Instacne == null)
        {
            _instacne = this;
        }
    }

    private void Start()
    {
        parent = GameObject.Find("parent").transform;
        grid = new int[15, 15];
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    public bool PlayChess(int[] pos)
    {
        if (!gameStart) return false;
        pos[0] = Mathf.Clamp(pos[0], 0, 14);
        pos[1] = Mathf.Clamp(pos[1], 0, 14);
        if (grid[pos[0], pos[1]] != 0) return false;

        if (turn == ChessType.Black)
        {
            GameObject go= Instantiate(prefabs[0], new Vector3(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            chessStack.Push(go.transform);
            go.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 1;
            //判断胜负
            if (CheckWinner(pos))
            {
                GameEnd();
            }
            turn = ChessType.White;
        }
        else if (turn == ChessType.White)
        {
            GameObject go = Instantiate(prefabs[1], new Vector3(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            chessStack.Push(go.transform);
            go.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 2;
            //判断胜负
            if (CheckWinner(pos))
            {
                GameEnd();
            }
            turn = ChessType.Black;
        }
 
     return true;
    }
   void GameEnd()
    {
        Winner.transform.parent .parent.gameObject.SetActive(true);
        switch (turn)
        {
            case ChessType.Watch:
                break;
            case ChessType.Black:
                Winner.text = "黑棋胜！";
                break;
            case ChessType.White:
                Winner.text = "白棋胜！";
                break;
            default:
                break;
        }       
        gameStart = false;
        Debug.Log(turn + "赢了");
    }

    public bool CheckWinner( int [] pos) 
    {
        if (CheckOneLine(pos, new int[2] { 1, 0 })) return true;
        if (CheckOneLine(pos, new int[2] { 0, 1 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, 1 })) return true;
        if (CheckOneLine(pos, new int[2] { 1, -1 })) return true;
        return false;
    }

    public bool CheckOneLine(int[] pos, int[] offset)
    {
        int linkNum = 1;
        //右边
        for (int i = offset[0], j = offset[1]; (pos[0] + i >= 0 && pos[0] + i < 15) &&
                pos[1] + j >= 0 && pos[1] + j < 15; i += offset[0], j += offset[1])
        {
            if (grid[pos[0] + i, pos[1] + j] == (int)turn)
            {
                linkNum++;
            }
            else
            {
                break;
            }
        }
        //左边
        for (int i = -offset[0], j = -offset[1]; (pos[0] + i >= 0 && pos[0] + i < 15) &&
                pos[1] + j >= 0 && pos[1] + j < 15; i -= offset[0], j -= offset[1])
        {
            if (grid[pos[0] + i, pos[1] + j] == (int)turn)
            {
                linkNum++;
            }
            else
            {
                break;
            }
        }
        if (linkNum > 4) return true;

        return false;
    }

    public void RetractChess()
    {
        if (chessStack.Count > 1)
        {
            Transform pos = chessStack.Pop();
            grid[(int)(pos.position.x + 7), (int)(pos.position.y + 7)] = 0;
                Destroy(pos.gameObject); 
            pos = chessStack.Pop();
            grid[(int)(pos.position.x + 7), (int)(pos.position.y + 7)] = 0;
                Destroy(pos.gameObject);
        }
    }


}

public enum ChessType
{
    Watch,
    Black,
    White
      
 }
