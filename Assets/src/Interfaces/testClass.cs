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
//TODO: we want to create an array holding all the game pieces 
//TODO: create a enum for player turns
//TODO: create a bool for if the game is over
//TODO: create a string for the winner

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
        pawn3.MovePiece("D4",board);

        knight.MovePiece("F3",board);
        bishop.MovePiece("F6",board); // invalid move
        pawn2.MovePiece("G3",board);
        pawn.MovePiece("H3",board);
        pawn.MovePiece("H4",board);
        bishop.MovePiece("H3",board);
        pawn.MovePiece("H5",board);
        pawn.MovePiece("H6",board);
        pawn.MovePiece("G7",board);

        Debug.Log(board.ToString());
        rook.MovePiece("H4",board); // all rook moves are now invalid blocked by bishop
        rook.MovePiece("A4",board);
        rook.MovePiece("A7",board);
        queen.MovePiece("D3",board);
        queen.MovePiece("A6",board);
        king.MovePiece("D2",board);
        king.MovePiece("D3",board);
        king.MovePiece("D6",board); //invalid move
        king.MovePiece("G6",board); //invalid move
        king.MovePiece("G6",board); //invalid move
        king.MovePiece("C3",board);
        king.MovePiece("C4",board);
        king.MovePiece("C5",board);
        king.MovePiece("C6",board); // should be invalid due to king moving in to check





        Debug.Log(board.ToString());
    }

void Update()
    {
// TODO: add method to check if players king is under attack
// check if players king is under attack
//if players king is under attack
    // check if the players king has any valid moves
    // if the players king has no valid moves
    //then the player lose
    //and the game is over so break the loop
     
// when its my turn and a piece is selected,
    //find that piece that is selected in the array
    // then when i hover my mouse over another square we should check if its a valid move
        //if move is valid
            // then color the square green
            //if the player then clicks on the valid square
            //then move the piece
        //if move is invalid
            //then color the square red   
            //if the player then clicks on the invalid square
            //then do nothing and tell user its invalid move
    }

}
