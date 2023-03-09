using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : EnemyAction
{
    /*public Laser(int beatsLater, int beatsSinceStart, int actionType, int posX, int posY) 
    : base(beatsLater, beatsSinceStart, actionType){
        
    }*/
    public GridManager gridManager;

    public GameObject attackSquare;
    public SpriteRenderer attackSquareRenderer;
    public GameObject laserObject;
    public SpriteRenderer laserRenderer;

    public Color squareOffColor = Color.black;
    public Color laserOffColor = Color.gray;
    public Color almostOnColor = Color.blue;
    public Color onColor = Color.red;

    int delay = 2;
    private int[,] activeReturnValue;
    
    public void init(bool orientation, int position, int beatsLater){
        gridManager = GameObject.FindObjectOfType<GridManager>();

        //orientation is row or column, true is row, false is column
        //position is the row/column num
        this.beatsLater = beatsLater;
        //init object STUFF

        //set activeReturnValue
        if (orientation){
            attackSquare.transform.position = new Vector3(position * GridManager.tileSpace, -1 * GridManager.tileSpace, 0);
            laserObject.transform.localScale = new Vector3(0.5f * GridManager.tileSpace, 20 * GridManager.tileSpace, 1);
            laserObject.transform.position = new Vector3(position * GridManager.tileSpace, 9.5f * GridManager.tileSpace, 0);

            activeReturnValue = new int[gridManager.gridHeight, 2];
            for (int i = 0; i < gridManager.gridHeight; i++){
                activeReturnValue[i, 0] = position;
                activeReturnValue[i, 1] = i;
            }
        }
        else{
            attackSquare.transform.position = new Vector3(-1 * GridManager.tileSpace, position * GridManager.tileSpace, 0);
            laserObject.transform.localScale = new Vector3(20 * GridManager.tileSpace, 0.5f * GridManager.tileSpace, 1);
            laserObject.transform.position = new Vector3(9.5f * GridManager.tileSpace, position * GridManager.tileSpace, 0);

            activeReturnValue = new int[gridManager.gridWidth, 2];
            for (int i = 0; i < gridManager.gridWidth; i++){
                activeReturnValue[i, 0] =  i;
                activeReturnValue[i, 1] = position;
            }
        }
        gameObject.SetActive(false);
    }
    public override void endOfBeat()
    {
        beatsSinceStart--;
        if (beatsSinceStart == 0){
            currActive = true;
            activate();
        }
        if (currActive){
            if (delay == 1){
                laserRenderer.color = almostOnColor;
                updateGridValue = null;
            }
            else if (delay == 0){
                laserRenderer.color = onColor;
                updateGridValue = activeReturnValue;
            }
            //PLACEHOLDER FOR NOW UNTIL ANIMATION
            /*else if (delay == -1){
                updateGridValue = null;
                gameObject.SetActive(false);
                currActive = false;
                //remove from EnemyControl?
            }*/
            delay--;
        }
    }
    public override void activate()
    {
        attackSquareRenderer.color = squareOffColor;
        laserRenderer.color = laserOffColor;
        gameObject.SetActive(true);
        updateGridValue = null;
    }
    public override void endOfAnimation()
    {
        if (delay == -1){
            updateGridValue = null;
            gameObject.SetActive(false);
            currActive = false;
        }
    }
}
