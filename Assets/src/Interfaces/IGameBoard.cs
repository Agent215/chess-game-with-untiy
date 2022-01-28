using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using src;


public interface IGameBoard {


    public GameBoard GetGameBoard();

    public void SetGameBoard(GameBoard gameBoard);

    public GamePiece GetPiece(Tuple<int,int> pieceLocation);

    public Boolean HasPiece(Tuple<int,int> pieceLocation);

    public void SetPiece(Tuple<int,int> pieceLocation ,GamePiece gamePiece);

    public void RemovePiece(GamePiece gamePiece);

    public void Initialize(String filename);

    // public String ToString();
}