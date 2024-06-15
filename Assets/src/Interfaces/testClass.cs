using System; 
using System.Collections;
using System.Collections.Generic;
using src;
using UnityEngine;

public class testClass : MonoBehaviour
{

    public GameObject gameboard;
    // Start is called before the first frame update
    void Start()
    {
        GameBoard board = gameboard.GetComponent<GameBoard>();
        board.Initialize("defaultGame.txt");
        GamePiece pawn = board.GetPiece(new Tuple<int, int>(1, 0));
        GamePiece rook = board.GetPiece(new Tuple<int, int>(0, 0));
        GamePiece knight = board.GetPiece(new Tuple<int, int>(0, 1));

        knight.MovePiece("F3",board);

        pawn.MovePiece("H3",board);
        pawn.MovePiece("H4",board);
        pawn.MovePiece("H5",board);
        pawn.MovePiece("H6",board);
        pawn.MovePiece("G7",board);

        Debug.Log(board.ToString());
        rook.MovePiece("H4",board);
        rook.MovePiece("A4",board);
        rook.MovePiece("A7",board);


        Debug.Log(board.ToString());
    }


}
