using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIFollow : MonoBehaviour
{


    void Update()
    {
        if (Chessbooard.Instacne.chessStack.Count > 0)
            transform.position = Chessbooard.Instacne.chessStack.Peek().position;
    }

    public void OnRelayBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnBtn()
    {
        SceneManager.LoadScene(0);

    }
}