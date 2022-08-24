//100697845 Edward Cao
//https://www.freecodecamp.org/news/how-to-make-your-tic-tac-toe-game-unbeatable-by-using-the-minimax-algorithm-9d690bad4b37/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    public string[] GameBoard = new string[] { " ", " ", " ", " ", " ", " ", " ", " ", " "};
    List<int> moves = new List<int>();
    List<int> playableSpots = new List<int>();
    public bool isGameOver = false;
    int bestAIMove = -1;
    public GameObject menuUI;
    public static bool isCPUx;
    public string cpuPlayer;
    public string humanPlayer;
    public Text winnerText;


    public void PlayerisX() //Function on button to set human player as X
    {
        //disable menu stuff
        menuUI.SetActive(false);
        GameObject menuBackground = GameObject.Find("Menu Background");
        menuBackground.SetActive(false);

        //set variables
        Debug.Log("PLAYER IS X");
        humanPlayer = "X";
        cpuPlayer = "O";
        isCPUx = false;

        for(int i = 0; i < 9; i++) //finding and setting values to the game tiles
        {
            GameObject currentTile = GameObject.Find("Game Square (" + i + ")");
            GameSquareScript other = (GameSquareScript)currentTile.GetComponent(typeof(GameSquareScript));
            other.setIndex(i);
            other.setValue(GameBoard[i]); //setting tile values for testing purposes
        }
    }



    public void CPUisX() //function to set CPU player as X
    {
        //disable menus stuff
        menuUI.SetActive(false);
        GameObject menuBackground = GameObject.Find("Menu Background");
        menuBackground.SetActive(false);

        //set variables
        Debug.Log("CPU IS  X");
        cpuPlayer = "X";
        humanPlayer = "O";
        isCPUx = true;

        for (int i = 0; i < 9; i++)
        {
            GameObject currentTile = GameObject.Find("Game Square (" + i + ")");
            GameSquareScript other = (GameSquareScript)currentTile.GetComponent(typeof(GameSquareScript));
            other.setIndex(i);
            other.setValue(GameBoard[i]); //setting tile values for testing purposes
        }
        cpuTurn();

    }


    public void cpuTurn() //function that is called for AI to take it's turn
    {
        Debug.Log("NEW AI TURN STARTS HERE");
        bestAIMove = findBestMove(GameBoard); //find best move for AI and play it
        GameObject tile = GameObject.Find("Game Square (" + bestAIMove + ")");
        GameSquareScript playTile = (GameSquareScript)tile.GetComponent(typeof(GameSquareScript));
        playTile.setValue(cpuPlayer); //setting tile value of that gameObject
        GameBoard[bestAIMove] = cpuPlayer; //setting value of the internal gameboard

        if(didWin(cpuPlayer, GameBoard) == cpuPlayer) //check to see if computer won
        {
            winnerText.text = "CPU wins";
            isGameOver = true;
        }
        else if (didWin(cpuPlayer, GameBoard) == "draw") //check to see if game is drawn
        {
            winnerText.text = "draw";
            isGameOver = true;
        }
        else { }
    }

    public void updateGameBoard(int index, string tileValue) //function for tiles to call to update the game board
    {
        GameBoard[index] = tileValue;
    }


int minMax(string[] possibleBoard, bool isCPUTurn, int alpha, int beta) //MinMaxAlgorithm
    {
        List<int> availSpots = availableSpots(possibleBoard); //check for available spots on the board

        if(didWin(humanPlayer, possibleBoard) == humanPlayer) //checks to see if the player won
        {
            return -10;
        }
        else if(didWin(cpuPlayer, possibleBoard) == cpuPlayer) //checks to see if the CPU won
        {
            return 10;
        }
        else if(didWin(cpuPlayer, possibleBoard) == "draw") //check to see if the entire board is filled
        {
            return 0;
        }

        if(isCPUTurn) //Maximizing if is CPU turn
        {
            int bestScore = -10000000;
            for (int i = 0; i < possibleBoard.Length; i++) //loop through the current available spots
            {
                if(possibleBoard[i] == " ")
                {
                    possibleBoard[i] = cpuPlayer; //set current empty position to the cpu
                    bestScore = Mathf.Max(minMax(possibleBoard, false, alpha, beta), bestScore); //run minmax and get the highest score
                    possibleBoard[i] = " "; //reset that position
                    alpha = Mathf.Max(alpha, bestScore);
                    if(beta <= alpha) //alpha pruning
                    {
                        break;
                    }
                }
                

            }
            return bestScore;
        }
        else //Minimizing if is player turn
        {
            int bestScore = 100000000;
            for (int i = 0; i < possibleBoard.Length; i++) //loop through the current available spots
            {
                if(possibleBoard[i] == " ")
                {
                    possibleBoard[i] = humanPlayer; //set current empty position to the human
                    bestScore = Mathf.Min(minMax(possibleBoard, true, alpha, beta), bestScore); //run minmax and get the lowest score
                    possibleBoard[i] = " "; //reset that position
                    beta = Mathf.Min(beta, bestScore);
                    if (beta <= alpha) //beta pruning
                    {
                        break;
                    }

                }
              
               
            }
            return bestScore;
        }
    }






    int findBestMove(string[] board) //function for finding the best move using minimax
    {
        int bestValue = -1000;
        int bestMove = -1;

        for(int i = 0; i < board.Length; i++) //loop through board
        {
            if(board[i] == " ") //check to see if slot is empty
            {
                board[i] = cpuPlayer; //make the move
                int moveValue = minMax(board, false, -999999999, 999999999); //if the slot is empty, run minmax on it as the opponent
                Debug.Log("Value at index " + i + " is: " + moveValue);
                board[i] = " "; //undo the move

                if(moveValue > bestValue) //if the move has a higher value than the previous best, replace it
                {
                    bestMove = i;
                    bestValue = moveValue;
                }
            }
        }
        Debug.Log("index " + bestMove + " is the best move");   
        return bestMove;
    }







    public List<int> availableSpots(string[] board) //Function to find empty spots on board

    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i].Equals(" "))
            {
                playableSpots.Add(i);
            }
        }
        return playableSpots;
    }








    public string didWin(string player, string[] board) //Function to check for winner

    {
        //check winner
        if (board[0] == player && board[1] == player && board[2] == player)
        {
            return player;
        }
        if (board[3] == player && board[4] == player && board[5] == player)
        {
            return player;
        }
        if (board[6] == player && board[7] == player && board[8] == player)
        {
            return player;
        }
        if (board[0] == player && board[3] == player && board[6] == player)
        {
            return player;
        }
        if (board[1] == player && board[4] == player && board[7] == player)
        {
            return player; 
        }
        if (board[2] == player && board[5] == player && board[8] == player)
        {
            return player;
        }
        if (board[0] == player && board[4] == player && board[8] == player)
        {
            return player;
        }
        if (board[2] == player && board[4] == player && board[6] == player)
        {
            return player;
        }
        
        // check to see if there are still empty slots
        int emptySpots = 0;
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i].Equals(" "))
            {
                emptySpots++;
            }
        }
        if(emptySpots > 0)
        {
            return "continue";
        }

        //returns draw if everything else fails
        return "draw";
    }
}
