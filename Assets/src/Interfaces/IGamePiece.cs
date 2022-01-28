using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IGamePiece
{

    public GamePiece GetGamePiece();

    public void SetGamePiece(GamePiece gamePiece);

    public string getColor();

    public void setColor(string color);

    public string getName();
    
    public Tuple<int, int> getCurrentLocation();

    public void setCurrentLocation(Tuple<int, int> currentLocation);
    
    public void setName(string pieceName);
    

    public void MovePiece(Tuple<int, int> newPieceLocation, GameBoard gameBoard);


    // public string ToString();
}