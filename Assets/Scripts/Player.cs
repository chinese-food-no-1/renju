using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public ChessType chessColor = ChessType.Black;
    public bool isDoibleMode = false;
    Button btn;

    protected virtual void Start()
    {
        btn = GameObject.Find("RetractBtn").GetComponent<Button>();
        //print(PlayerPrefs.GetInt("Double"));
        if (PlayerPrefs.GetInt("Double") == 10)
            isDoibleMode = true;
    }

    protected virtual void FixedUpdate()
    {
        if (chessColor == Chessbooard.Instacne.turn && Chessbooard.Instacne.timer > 0.3f)
            PlayeChess();
        if (!isDoibleMode)
            ChangeBtnColor();
    }

    public virtual void PlayeChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //print((int)(pos.x + 7.5f)+ " " + (int)(pos.y + 7.5f));
            if (Chessbooard.Instacne.PlayChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) }))
                Chessbooard.Instacne.timer = 0;
        }
    }


    protected virtual void ChangeBtnColor()
    {
        if (chessColor == ChessType.Watch)
            return;
        if (Chessbooard.Instacne.turn == chessColor)
            btn.interactable = true;
        else
            btn.interactable = false;
    }
}
