//100697845 Edward Cao

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSquareScript : MonoBehaviour
{
    private bool isFilled = false;
    private string tileValue;
    private int index;
    private GameObject Ochild;
    private GameObject Xchild;
    public GameObject gameManager;
    public Text winnerText;
    GameStartScript ticTactoeManager;

    private void Awake()
    {
        Ochild = this.transform.GetChild(0).gameObject;
        Xchild = this.transform.GetChild(1).gameObject;
        ticTactoeManager = (GameStartScript)gameManager.GetComponent(typeof(GameStartScript));


    }


    private void OnMouseEnter() //function to show value on tile when moused over
    {
        if(ticTactoeManager.isGameOver != true)
        {
            if (isFilled == false)
            {
                if (GameStartScript.isCPUx == true)
                {
                    Ochild.transform.localPosition = new Vector3(0.0f, 0.0f, -0.5f);
                }
                if (GameStartScript.isCPUx == false)
                {
                    Xchild.transform.localPosition = new Vector3(0.0f, 0.0f, -0.5f);
                }
            }
        }
       
    }
    private void OnMouseExit() //function to reset visual values when mouse exits tile
    {
        if (ticTactoeManager.isGameOver != true)
        {
            if (isFilled == false)
            {
                if (GameStartScript.isCPUx == true)
                {
                    Ochild.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                }
                if (GameStartScript.isCPUx == false)
                {
                    Xchild.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
        }
            
    }
    private void OnMouseDown() //function to set value when player clicks on a tile
    {
        if (ticTactoeManager.isGameOver != true)
        {
            if (isFilled == false)
            {
                if (GameStartScript.isCPUx == true)
                {
                    Ochild.transform.localPosition = new Vector3(0.0f, 0.0f, -0.5f);
                    tileValue = "O";
                    isFilled = true;

                    //update main gameboard
                    ticTactoeManager.updateGameBoard(index, tileValue);

                    if (ticTactoeManager.didWin(tileValue, ticTactoeManager.GameBoard) == tileValue) //check to see if game is won
                    {
                        winnerText.text = "You won! you were playing as" + tileValue;
                        ticTactoeManager.isGameOver = true;
                    }
                    else if (ticTactoeManager.didWin(tileValue, ticTactoeManager.GameBoard) == "draw") //check to see if game is drawn
                    {
                        winnerText.text = "Game draw";
                        ticTactoeManager.isGameOver = true;

                    }
                    else
                    {
                        ticTactoeManager.cpuTurn(); //let the cpu play
                    }

                }
                if (GameStartScript.isCPUx == false)
                {
                    Xchild.transform.localPosition = new Vector3(0.0f, 0.0f, -0.5f);
                    tileValue = "X";
                    isFilled = true;

                    //update main gameboard
                    GameStartScript ticTactoeManager = (GameStartScript)gameManager.GetComponent(typeof(GameStartScript));
                    ticTactoeManager.updateGameBoard(index, tileValue);

                    if (ticTactoeManager.didWin(tileValue, ticTactoeManager.GameBoard) == tileValue) //check to see if game is won
                    {
                        winnerText.text = "You won! you were playing as" + tileValue;
                        ticTactoeManager.isGameOver = true;

                    }
                    else if (ticTactoeManager.didWin(tileValue, ticTactoeManager.GameBoard) == "draw") //check to see if game is drawn
                    {
                        winnerText.text = "Game draw";
                        ticTactoeManager.isGameOver = true;


                    }
                    else
                    {
                        ticTactoeManager.cpuTurn(); //let the cpu play
                    }

                }
            }
        }
           
    }

    public void setValue(string player) //function used by the AI to fill values
    {
        if(player == "X")
        {
            Xchild.transform.localPosition = new Vector3(0.0f, 0.0f, -0.5f);
            tileValue = "X";
            isFilled = true;
        }

        if (player == "O")
        {
            Ochild.transform.localPosition = new Vector3(0.0f, 0.0f, -0.5f);
            tileValue = "O";
            isFilled = true;
        }
        else
        {

        }
    }

    public void setIndex(int ind) //helper function to set index of tiles
    {
        index = ind;
    }
}
