using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using src.Constants;
// using UnityEngine.Diagnostics;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

using System.Threading.Tasks;

public class GamePiece : MonoBehaviour
{
    public Tuple<int, int> _currentLocation;
    public string _color = "";
    public string _pieceName = "";

    public GameControl _gameControl;
   void Start()
    {
        _gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        
    }
    public GamePiece() { }
    public GamePiece(string color, string pieceName, Tuple<int, int> currentLocation)
    {
        _color = color;
        _pieceName = pieceName;
        _currentLocation = currentLocation;
    }
    public GamePiece GetGamePiece()
    {
        return this;
    }

    public void SetGamePiece(GamePiece gamePiece)
    {
        _color = gamePiece._color;
        _currentLocation = gamePiece._currentLocation;
        _pieceName = gamePiece._pieceName;

    }

    public Tuple<int, int> getCurrentLocation()
    {
        return _currentLocation;
    }

    public void setCurrentLocation(Tuple<int, int> currentLocation)
    {

        _currentLocation = currentLocation;

    }

    public string getColor()
    {
        return _color;
    }

    public void setColor(string color)
    {
        if (color != null
        && (color == Constants.BLACK || color == Constants.WHITE))
        {
            _color = color;
        }
    }

    public string getName()
    {
        return _pieceName;
    }

    public void setName(string pieceName)
    {
        _pieceName = pieceName;
    }

    /// <summary>
    /// this method should check if the move is valid and if
    /// a piece needs to be removed.
    /// Then move the piece and remove any captured pieces.
    /// </summary>
    /// <param name="pieceLocation"></param>
    public void MovePiece(string newPieceLocation, GameBoard gameBoard)
    {
        Tuple<int, int> tupleLocation = new Tuple<int, int>(src.util.Utils.getLocationFromName(newPieceLocation, gameBoard).Item1, src.util.Utils.getLocationFromName(newPieceLocation, gameBoard).Item2);
        //if moveIsValid 
        if (!tupleLocation.Equals(null))
        {
            if (isMoveValid(tupleLocation, gameBoard,false))
            {
                //if piece Location is occupied and of opposite color 
                if (gameBoard.HasPiece(tupleLocation))
                {
                    //remove the opponents piece and set players piece to new location
                    gameBoard.RemovePiece(gameBoard.GetPiece(tupleLocation));
                    Debug.Log("moving piece with enemy present in target space!" + _pieceName + " to " + newPieceLocation);
                    setPiece(tupleLocation, gameBoard);
                    _gameControl.turnOver();
                }
                else// if piece location is not occupied 
                {
                    Debug.Log("moving piece " + _pieceName + " to " + newPieceLocation);
                    setPiece(tupleLocation, gameBoard);
                     _gameControl.turnOver();
                }
            }
            else
            {
                //else log error
                Debug.LogError("Move is invalid");
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newPieceLocation"></param>
    /// <param name="gameBoard"></param>
    private void setPiece(Tuple<int, int> newPieceLocation, GameBoard gameBoard)
    {
        gameBoard.RemovePieceByLocation(_currentLocation);
        setCurrentLocation(newPieceLocation);
        gameBoard.gameBoard[newPieceLocation.Item1, newPieceLocation.Item2].GetComponent<GameSquare>().setHasPiece(true);
        gameBoard.SetPiece(newPieceLocation, gameObject);
    }

    //method to get all valid moves for a piece
    public List<Tuple<int, int>> getValidMoves(GameBoard gameBoard,bool isOpponent)
    {
        //lets do some timing to see how long it takes
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        List<Tuple<int, int>> validMoves = new List<Tuple<int, int>>();

        var w = 8;
        var h = 8;
        // for each sqaure in the board call GetPiece()
        // if piece is not null add it to the list
        for (var x = 0; x < w; ++x)
        {
            for (var y = 0; y < h; ++y)
            {
                if (isMoveValid(new Tuple<int, int>(x, y), gameBoard,isOpponent))
                {
                    validMoves.Add(new Tuple<int, int>(x, y));
                }
            }
        }
        // if valid add to list of valid moves
        stopwatch.Stop();
        Debug.Log("Time elapsed: " + stopwatch.ElapsedMilliseconds + " ms");
        return validMoves;
    }



    /// <summary>
    /// isMoveValid checks current location with requested new location and check the piece type
    /// to decide all possible moves. this checks if the new move is valid
    /// Does not consider if the spot is occupied just that the location is within the range 
    /// </summary>
    /// <param name="newLocation"></param>
    /// <returns>Boolean</returns>
    private bool isMoveValid(Tuple<int, int> newLocation, GameBoard gameBoard,bool isOpponent)
    {
        Tuple<int, int> currentlocation = getCurrentLocation();
        var isValid = false;
        List<Tuple<int, int>> validMoves = new List<Tuple<int, int>>();
        string color = getColor();
        switch (getName())
        {
            case Constants.PAWN:
                PieceEventHandlers.PawnEventHandler(newLocation, gameBoard, color, currentlocation, validMoves);
                break;
            case Constants.ROOK:
                PieceEventHandlers.RookEventHandler(newLocation, gameBoard, color, currentlocation, validMoves);
                break;
            case Constants.KNIGHT:
                PieceEventHandlers.KnightEventHandler(newLocation, gameBoard, color, currentlocation, validMoves);
                break;
            case Constants.BISHOP:
                PieceEventHandlers.BishopEventHandler(newLocation, gameBoard, color, currentlocation, validMoves);
                break;
            case Constants.KING:
                PieceEventHandlers.KingEventHandler(newLocation, gameBoard, color, currentlocation, validMoves,isOpponent);
                break;
            case Constants.QUEEN:
                PieceEventHandlers.QueenEventHandler(newLocation, gameBoard, color, currentlocation, validMoves);
                break;
            case null:
                Debug.Log("piece name is invalid");
                break;
        }
        foreach (var move in validMoves.Where(move => move.Equals(newLocation)))
        {
            isValid = true;
        }
        return isValid;
    }
}
