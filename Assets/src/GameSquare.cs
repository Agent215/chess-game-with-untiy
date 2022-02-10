using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class GameSquare : MonoBehaviour
{

    public string color;

    public string squareName;

    public Boolean hasPiece;

    public GamePiece _gamePiece;
    
    public GameSquare() { }

    public GameSquare(string color, string squareName, Boolean hasPiece, GamePiece gamePiece)
    {
        
        this.color = color;
        this.squareName = squareName;
        this.hasPiece = hasPiece;
        this._gamePiece = gamePiece;
    }
    public GameSquare(string color, string squareName, Boolean hasPiece)
    {
        this.color = color;
        this.squareName = squareName;
        this.hasPiece = hasPiece;
    }


    public void setColor(string color){
        this.color = color;
    }

    public string getColor(){
        return this.color;
    }


   public void setSquareName(string squareName){
        this.squareName = squareName;
    }

    public string getsquareName(){
        return this.squareName;
    }

      public void setHasPiece(Boolean hasPiece){
        this.hasPiece = hasPiece;
    }

    public Boolean getHasPiece(){
        return this.hasPiece;
    }


      public void setGamePiece(GamePiece gamePiece)
      {
          _gamePiece = gamePiece;
      }

    public GamePiece getGamePiece()
    {
        return _gamePiece;
    }

}
