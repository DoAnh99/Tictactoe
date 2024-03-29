using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Player
{
   public Image panel;   
   public Text text;
   public Button button;
}
[System.Serializable]
public class PlayerColor
{
   public Color panelColor;   
   public Color textColor;
}



public class GameController : MonoBehaviour
{
    [SerializeField] private AudioSource wineffect;
    [SerializeField] private AudioSource equaleffect;
    [SerializeField] private AudioSource btclickeffect;

    public Text[] buttonList;
    private string playerSide;

    public GameObject gameOverPanel;
    public Text gameOverText;

    private int moveCount;

    public GameObject restartButton;

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;

    private string computerSide;
    public bool playerMove;
    public float delay;
    private int value;

    void Awake()
    {   
        SetGameControllerReferenceOnButtons();
       // playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);// thuoc tinh SetActive(thuoc tinh) true can view in scren false do'nt view in scren
                                       // SetPlayerColors(playerX, playerO);
        playerMove = true;//khoi tao player choi truoc
    }

    private void Update()
    {
        if (playerMove == false)
        {
            delay += delay * Time.deltaTime;
            if (delay >= 100)
            {
                value = Random.Range(0, 8);
                if (buttonList [value].GetComponentInParent<Button> ().interactable == true)
                {
                    btclickeffect.Play();
                    buttonList [value].text = GetComputerSide();
                    buttonList [value].GetComponentInParent<Button> ().interactable = false;
                    EndTurn();

                }
            }
        }
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i=0;i<buttonList.Length;i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);

        }
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            computerSide = "O";
            SetPlayerColors(playerX, playerO);
        }
        else{
            computerSide = "X";
            SetPlayerColors(playerO, playerX);
        }
        StartGame();
    }

    void StartGame()
    {
       SetBoardInteractable(true);
       SetPlayerButtons(false);//start game thi nut chose interactable
       startInfo.SetActive(false);
     //  playerX.button.SetActive(false);// setactive() is menthod,interactable is properties 
       //playerO.button.SetActive(false);//
    }

    public string GetPlayerSide()
    {
        return playerSide;

    }
    public string GetComputerSide()
    {
        return computerSide;
    }
    public void EndTurn()
    {   moveCount++;
    
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
       else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
               GameOver(playerSide);
        }
       else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
               GameOver(playerSide);
        }
       else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
               GameOver(playerSide);
        }
       else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
               GameOver(playerSide);
        }
       else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
              GameOver(playerSide);
        }
       else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
              GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
              GameOver(playerSide);
        }


       else if (buttonList[0].text == computerSide && buttonList[1].text == computerSide && buttonList[2].text == computerSide)
        {
            GameOver(computerSide);
        }
        else if (buttonList[3].text == computerSide && buttonList[4].text == computerSide && buttonList[5].text == computerSide)
        {
            GameOver(computerSide);
        }
        else if (buttonList[6].text == computerSide && buttonList[7].text == computerSide && buttonList[8].text == computerSide)
        {
            GameOver(computerSide);
        }
        else if (buttonList[0].text == computerSide && buttonList[3].text == computerSide && buttonList[6].text == computerSide)
        {
            GameOver(computerSide);
        }
        else if (buttonList[1].text == computerSide && buttonList[4].text == computerSide && buttonList[7].text == computerSide)
        {
            GameOver(computerSide);
        }
        else if (buttonList[2].text == computerSide && buttonList[5].text == computerSide && buttonList[8].text == computerSide)
        {
            GameOver(computerSide);
        }
        else if (buttonList[0].text == computerSide && buttonList[4].text == computerSide && buttonList[8].text == computerSide)
        {
            GameOver(computerSide);
        }
        else if (buttonList[2].text == computerSide && buttonList[4].text == computerSide && buttonList[6].text == computerSide)
        {
            GameOver(computerSide);
        }

        else if ( moveCount >= 9)  
           {
            GameOver("draw");
           }

        else
        {
           ChangeSides();
            delay = 10;
        }
           
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;

    }     

    void GameOver(string winningPlayer)
    {

        SetBoardInteractable(false);
        //SetPlayerColorsInactive();
       /* for (int i=0;i<buttonList.Length;i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        */
       //  gameOverPanel.SetActive(true);
         //gameOverText.text = playerSide + " Wins!";

          if (winningPlayer == "draw")
          {
            equaleffect.Play();
            SetGameOverText("It's a Draw");   
                SetPlayerColorsInactive();
          }
          else{
            wineffect.Play();
            SetGameOverText(winningPlayer + " Wins!");
                
          }

         //SetGameOverText(playerSide + " Wins!");
         restartButton.SetActive(true);
    }
    void ChangeSides()
    {
        //   playerSide = (playerSide =="X") ? "O" : "X";
        playerMove = (playerMove == true) ? false : true;
        //   if (playerSide == "X")
        if (playerMove == true)
        {
          SetPlayerColors(playerX, playerO);    
        }
        else
        {
           SetPlayerColors(playerO, playerX);  
        }
    }
    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }
    public void RestartGame()
    {
       // playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
         SetPlayerButtons(true);
         SetPlayerColorsInactive();
         startInfo.SetActive(true);
         playerMove = true;
         delay = 10;

       //   playerX.button.SetActive(true);//i add no setactive inster of interactive
      //  playerO.button.SetActive(true);//
       // SetBoardInteractable(true);
      //  SetPlayerColors(playerX, playerO);
        
        for ( int i = 0; i < buttonList.Length; i++)
        {
          //  buttonList[i].GetComponentInParent<Button>().interactable = true;
             buttonList[i].text = "";
        }
        
    }

    void SetBoardInteractable(bool toggle)
    {
         for ( int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;// in unity not check interctable to remove by code
                                             //   playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }
    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

}
