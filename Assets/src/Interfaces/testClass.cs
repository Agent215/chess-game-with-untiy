using System;
using System.Collections;
using System.Collections.Generic;
using src;
using UnityEngine;

public class testClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameBoard board = new GameBoard();
        board.Initialize("defaultGame.txt");
        GamePiece tempPiece = board.GetPiece(new Tuple<int, int>(1, 0));
        tempPiece.MovePiece(new Tuple<int, int>(3, 0),board);
        Debug.Log(board.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
