using System;
using System.Collections;
using System.Collections.Generic;
using src.Constants;
using src;
using UnityEngine;

public class GameControl : MonoBehaviour
{


    // we want to create an array holding all the game pieces 
    public List<GamePiece> _gamePieces;
    //create a enum for player turns
    public string _playerTurn = Constants.WHITE;

    // create a bool for if the game is over
    public bool _gameOver = false;
    // create a string for the winner
    public string _winner = "";

    public GameBoard _gameBoard;
    public OutlineSelection _outlineSelection;

    // Awake is called before the first frame update
    void Awake()
    {
        _gameBoard = GameObject.Find("pivot").GetComponent<GameBoard>();
        _gamePieces = _gameBoard.getGamePieces();

        _outlineSelection = GameObject.Find("OutlineSelection").GetComponent<OutlineSelection>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_playerTurn == Constants.WHITE)
        {

            // if (_outlineSelection.selection != null)
            // {
            //     //check if the selection is a valid move
            //     bool isSquare = _outlineSelection.selection.CompareTag("SelectableSqaure");
            //     if (isSquare && _outlineSelection.previousSelection != null)
            //     {
            //         //this works but not really. we dont want to call this every frame. just when we click on a new square after already clicking our own piece
            //         // _outlineSelection.previousSelection.parent.GetComponent<GamePiece>().MovePiece(_outlineSelection.selection.GetComponent<GameSquare>().getsquareName(), _gameBoard);
            //     }

            // }
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
        else if (_playerTurn == Constants.BLACK)
        {

        }
    }


    // method to handle all the actions we only want to do once per turn. 
    // this method should be called at the end of a turn.
    public void turnOver()
    {

        //update the player turn
        if (_playerTurn == Constants.WHITE)
        {
            _playerTurn = Constants.BLACK;
        }
        else if (_playerTurn == Constants.BLACK)
        {
            _playerTurn = Constants.WHITE;
        }

        List<GamePiece> whitePieces =  PieceEventHandlers.GetPiecesByColor(Constants.WHITE, _gameBoard);
        List<GamePiece> blackPieces =  PieceEventHandlers.GetPiecesByColor(Constants.BLACK, _gameBoard);

        if(_playerTurn == Constants.WHITE) {
             foreach (GamePiece piece in whitePieces)
             {
                 piece.gameObject.transform.GetChild(0).gameObject.tag="Selectable";;
           
             }
            foreach (GamePiece piece in blackPieces)
             {
                 piece.gameObject.transform.GetChild(0).gameObject.tag="UnSelectable";;
             }
        } 
        else if( _playerTurn == Constants.BLACK) {
             foreach (GamePiece piece in whitePieces)
             {
                piece.gameObject.transform.GetChild(0).gameObject.tag="UnSelectable";;
             }
            foreach (GamePiece piece in blackPieces)
             {
               piece.gameObject.transform.GetChild(0).gameObject.tag="Selectable";;
             }
        }

        

        //get the king of the player whos turn is starting, which is the opposite of the current players turn
        GamePiece king = _gamePieces.Find(piece => piece.getName() == Constants.KING && piece.getColor() == _playerTurn);
        if (king == null)
        {
           Debug.Log("king not found");
        }
        else
        {
            // check if the players king has any valid moves
            List<Tuple<int, int>> validMoves = king.getValidMoves(_gameBoard,true);
            // if the new players king has no valid moves
            bool isThreatened = PieceEventHandlers.IsSquareThreatened(king.getCurrentLocation(), _gameBoard,king.getColor());            
            if (validMoves.Count == 0 && isThreatened)
            {
                //then the player loses
                //and the game is over 
                _winner = _playerTurn;
                _gameOver = true;
            }

        }
    }
}
