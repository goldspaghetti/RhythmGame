using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    //0 is empty
    //1 is harm stuff! RESIGN LOB!
    public int playerX = 0;
    public int playerY = 0;

    //how much each tile takes up in real world space
    public static int tileSpace = 1;
    public int gridHeight = 10;
    public int gridWidth = 10;
    public MusicPlayer musicPlayer;
    public CoinManager coinManager;
    public Text playerHealthText;
    public string playerMoveSound;
    public int playerHp = 4;
    public int coinCollected = 0;
    //public bool movedThisBeat = false;
    public int[,] grid;
    void Awake(){
        grid = new int[gridWidth, gridHeight];
    }
    void Start()
    {
        updatePlayer();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void movePlayer(int direction){
        Debug.Log("moving player");
        switch(direction){
            case 0:
                playerY += 1;
                break;
            case 1:
                playerX += 1;
                break;
            case 2:
                playerY -= 1;
                break;
            case 3:
                playerX -= 1;
                break;
            //check if out of bounds
        }
        checkBorder();
        updatePlayer();
        checkCoin();

        //add move sound thing
        musicPlayer.playSound(playerMoveSound);

        //checkDamaged();
    }
    public void checkCoin(){
        if (coinManager.checkOnCoin(playerX, playerY)){
            coinCollected++;
            //check win
        }
    }
    public void checkBorder(){
        if (playerY < 0){
            playerY = 0;
        }
        else if (playerY >= gridHeight){
            playerY = gridHeight - 1;
        }
        if (playerX < 0){
            playerX = 0;
        }
        else if (playerX >= gridWidth){
            playerX = gridWidth - 1;
        }
    }
    public void updatePlayer(){
        player.transform.position = new Vector3(playerX * tileSpace, playerY * tileSpace, 0);
    }
    public void updateGrid(int[,] newValues){
        if (newValues == null){
            return;
        }
        //Debug.Log(newValues.GetLength(0));
        for (int i = 0; i < newValues.GetLength(0); i++){
            //Debug.Log(newValues[i, 0]);
            //Debug.Log(newValues[i, 1]);
            grid[newValues[i, 0], newValues[i, 1]] = 1;
        }
    }
    public bool checkDamaged(){
        if (grid[playerX, playerY] == 1){
            playerHp--;
            playerHealthText.text = "HITPOINTS: " + playerHp;
            Debug.Log("player damaged");
            return true;
        }
        else{
            return false;
        }

        //update stuff

    }
    public void resetGrid(){
        //Debug.Log(grid.GetLength(0));
        //Debug.Log(grid.GetLength(1));
        for(int i = 0; i < grid.GetLength(0); i++){
            for (int j = 0; j < grid.GetLength(1); j++){
                //Debug.Log(i);
                //Debug.Log(j);
                grid[i, j] = 0;
            }
        }
    }
}
