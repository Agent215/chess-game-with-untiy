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


        //below is test code, will remove later
        GameBoard board = gameboard.GetComponent<GameBoard>();
        board.Initialize("defaultGame.txt");
        GamePiece pawn = board.GetPiece(new Tuple<int, int>(1, 0));
        GamePiece pawn2 = board.GetPiece(new Tuple<int, int>(1, 1));
        GamePiece pawn3 = board.GetPiece(new Tuple<int, int>(1, 4));

        GamePiece rook = board.GetPiece(new Tuple<int, int>(0, 0));
        GamePiece knight = board.GetPiece(new Tuple<int, int>(0, 1));
        GamePiece bishop = board.GetPiece(new Tuple<int, int>(0, 2));
        GamePiece queen = board.GetPiece(new Tuple<int, int>(0, 4));
        GamePiece king = board.GetPiece(new Tuple<int, int>(0, 3));
        pawn3.MovePiece("D4", board);

        knight.MovePiece("F3", board);
        bishop.MovePiece("F6", board); // invalid move
        pawn2.MovePiece("G3", board);
        pawn.MovePiece("H3", board);
        pawn.MovePiece("H4", board);
        bishop.MovePiece("H3", board);
        pawn.MovePiece("H5", board);
        pawn.MovePiece("H6", board);
        pawn.MovePiece("G7", board);

        Debug.Log(board.ToString());
        rook.MovePiece("H4", board); // all rook moves are now invalid blocked by bishop
        rook.MovePiece("A4", board);
        rook.MovePiece("A7", board);
        queen.MovePiece("D3", board);
        queen.MovePiece("A6", board);
        king.MovePiece("D2", board);
        king.MovePiece("D3", board);
        king.MovePiece("D6", board); //invalid move
        king.MovePiece("G6", board); //invalid move
        king.MovePiece("G6", board); //invalid move
        king.MovePiece("C3", board);
        king.MovePiece("C4", board);
        king.MovePiece("C5", board);
        king.MovePiece("C6", board); // should be invalid due to king moving in to check

        Debug.Log(board.ToString());
    }


}
