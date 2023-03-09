using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GridManager gridManager;
    public int[,] coinPos;
    public GameObject[,] coins;
    public GameObject coin;
    public int coinNum = 0;
    //private Random random;
    void Awake(){
        coinPos = new int[gridManager.gridWidth,gridManager.gridHeight];
        coins = new GameObject[gridManager.gridWidth, gridManager.gridHeight];
        //Random.InitState(32);
        generateCoins();
    }
    public bool checkOnCoin(int xPos, int yPos){
        /*if (coinPos[xPos, yPos] == 1){
            coinPos[xPos, yPos] = 0;
            return true;
        }
        return false;*/
        if (coins[xPos, yPos] != null){
            if (coins[xPos, yPos].activeSelf){
                coins[xPos, yPos].SetActive(false);
                return true;
            }
        }
        return false;
    }
    public void generateCoins(){
        Debug.Log("generating coins");
        for (int i = 0; i < gridManager.gridWidth; i++){
            for (int j = 0; j < gridManager.gridHeight; j++){
                if (Random.Range(0, 4) == 1){
                    coinNum++;
                    coinPos[i, j] = 1;
                    GameObject currCoin = Instantiate(coin);
                    coins[i, j] = currCoin;
                    currCoin.transform.position = new Vector3(i * GridManager.tileSpace, j * GridManager.tileSpace, 0);
                    //currCoin.SetActive(true);

                    //COIN INACTIVE!
                    currCoin.SetActive(false);
                }
                //INIT SPRITE LATER!
            }
        }
    }
}
