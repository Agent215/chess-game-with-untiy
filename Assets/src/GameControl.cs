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

    // Start is called before the first frame update
    void Start()
    {
        _gameBoard = GameObject.Find("pivot").GetComponent<GameBoard>();
        _gamePieces = _gameBoard.getGamePieces();

    }

    // Update is called once per frame
    void Update()
    {

        if (_playerTurn == Constants.WHITE)
        {
          

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
          
            _playerTurn = Constants.BLACK;
        }
        else if (_playerTurn == Constants.BLACK)
        {
      
            _playerTurn = Constants.WHITE;
        }
    }


    // method to handle all the actions we only want to do once per turn
    public void turnOver(){
 
            //get the king of the player whos turn is starting, which is the opposite of the current players turn
             GamePiece king = _gamePieces.Find(x => x.getName() == Constants.KING && x.getColor() != _playerTurn);
            // check if the players king has any valid moves
            List<Tuple<int, int>> validMoves = king.getValidMoves(_gameBoard);
            // if the players king has no valid moves
            if(validMoves.Count == 0)
            {
                //then the player loses
                //and the game is over 
                _winner = _playerTurn;
                _gameOver = true;
            }  

            //update the player turn
            if(_playerTurn == Constants.WHITE)
            {
                _playerTurn = Constants.BLACK;
            }
            else if(_playerTurn == Constants.BLACK)
            {
                _playerTurn = Constants.WHITE;
            }
    }
}
