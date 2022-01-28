using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using src.Constants;

public class GamePiece : MonoBehaviour, IGamePiece
{
    private Tuple<int, int> _currentLocation;
    private string _color = "";
    private string _pieceName = "";
    
    public GamePiece() { }
    public GamePiece(string color, string pieceName, Tuple<int, int> currentLocation)
    {
        this._color = color;
        this._pieceName = pieceName;
        this._currentLocation = currentLocation;
    }
    public GamePiece GetGamePiece()
    {
        return this;
    }

    public void SetGamePiece(GamePiece gamePiece)
    {

    }
    
    public Tuple<int, int> getCurrentLocation()
    {
        return this._currentLocation;
    }

    public void setCurrentLocation(Tuple<int, int> currentLocation)
    {

        this._currentLocation = currentLocation;

    }

    public string getColor()
    {
        return this._color;
    }

    public void setColor(string color)
    {
        if (color != null
        && (color == Constants.BLACK || color == Constants.WHITE))
        {
            this._color = color;
        }
    }

    public string getName()
    {
        return this._pieceName;
    }

    public void setName(string pieceName)
    {
        this._pieceName = pieceName;
    }

    /// <summary>
    /// this method should check if the move is valid and if
    /// a piece needs to be removed.
    /// Then move the piece and remove any captured pieces.
    /// </summary>
    /// <param name="pieceLocation"></param>
    public void MovePiece(Tuple<int, int> newPieceLocation, GameBoard gameBoard)
    {
        //if moveIsValid 
        if (isMoveValid(newPieceLocation,gameBoard))
        {
            //if piece Location is occupied and of opposite color 
            if (gameBoard.HasPiece(newPieceLocation))
            {
                //remove the opponents piece and set players piece to new location
                gameBoard.RemovePiece(gameBoard.GetPiece(newPieceLocation));
                Debug.Log("moving piece " + _pieceName +" to " + newPieceLocation);
                //remove the piece from current location
                gameBoard.RemovePieceByLocation(_currentLocation);
                setCurrentLocation(newPieceLocation);
                gameBoard.gameBoard[newPieceLocation.Item1,newPieceLocation.Item2].setHasPiece(true);
                gameBoard.SetPiece(newPieceLocation,this);

            }
            else// if piece location is not occupied 
            {
                Debug.Log("moving piece " + _pieceName +" to " + newPieceLocation);
                gameBoard.RemovePieceByLocation(_currentLocation);
                setCurrentLocation(newPieceLocation);
                gameBoard.gameBoard[newPieceLocation.Item1,newPieceLocation.Item2].setHasPiece(true);
                gameBoard.SetPiece(newPieceLocation,this);
            }
        }
        else
        {
            //else log error
            Debug.LogError("Move is invalid");
        }
    }

    /// <summary>
    /// isMoveValid checks current location with requested new location and check the piece type
    /// to decide all possible moves. this checks if the new move is valid
    /// Does not consider if the spot is occupied just that the location is within the range 
    /// </summary>
    /// <param name="newLocation"></param>
    /// <returns>Boolean</returns>
    private bool isMoveValid(Tuple<int, int> newLocation,GameBoard gameBoard)
    {
        Tuple<int, int> currentlocation = getCurrentLocation();
        bool isValid;
        isValid = false;
        List<Tuple<int, int>> validMoves = new List<Tuple<int,int>>(); 
        string color = getColor();
        switch (getName())
        {
            case Constants.PAWN:
                if (color == Constants.WHITE)
                {
                    //if pawn is on starting position it can move up +2 spaces
                    if (currentlocation.Item1 == 1)
                    {
                        Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1 + 1, currentlocation.Item2);
                        Tuple<int, int> move2 = new Tuple<int, int>(currentlocation.Item1 + 2, currentlocation.Item2);
                        validMoves.Add(move1);
                        validMoves.Add(move2);
                    }
                    else if(currentlocation.Item1 !=7) // we are not at the end of the board
                    {
                        //pawn can move +1 up if there is nothing in that spot
                        if(!gameBoard.HasPiece(newLocation))
                        { 
                            Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1 + 1, currentlocation.Item2);
                            validMoves.Add(move1);
                        }
                    }
                    
                }else if (color == Constants.BLACK)
                {
                    //if pawn is on starting position it can move up +2 spaces
                    if (currentlocation.Item1 == 6)
                    {
                        Tuple<int, int> move1 = new Tuple<int, int>(currentlocation.Item1 + -1, currentlocation.Item2);
                        Tuple<int, int> move2 = new Tuple<int, int>(currentlocation.Item1 + -2, currentlocation.Item2);
                        validMoves.Add(move1);
                        validMoves.Add(move2);
                    }
                }
                //if a pawn is diaganaly adjacient to an opposing piece then they can move +1 up and +1 left or right
                // if an opponents pawn has passed next to this pawn then current pawn get move +1 up and left or right behind that pawn to capture it
                break;
            case Constants.ROOK:
                // can move in straight line horizontaly and vertically until they hit opposing piece
                break;
            case Constants.KNIGHT:
                // can move in 8 directions if all degrees of freedom are open
                //+1 up and +2 left or +2 up and +1 left
                //+1 up and +2 right or +2 up and +1 right
                //-1 down and +2 right or -2 down and +1 right
                //-1 down and +2 left or -2 down and +1 left
                break;
            case Constants.BISHOP:
                //can move diagnocally until they hit an oppenents piece
                break;
            case Constants.KING:
                //can move in any direction +1
                break;
            case Constants.QUEEN:
                // can move in any direction until they hit an opponents piece
                break;
            case null:
                Debug.Log("piece name is invalid");
                break;
        }

        foreach (Tuple<int,int > move in validMoves)
        {
            if (move.Equals(newLocation))
            {
                isValid = true;
            } 
        }
        
        return isValid;
    }
    
    public static IGamePiece GamePieceFactory()
    {

        return new GamePiece();
    }
    
}
