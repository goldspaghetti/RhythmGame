using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TimeManager timeManager;
    public EnemyControl enemyControl;
    public SpriteRenderer square;
    public MusicPlayer musicPlayer;
    public Color offTimeColor;
    public Color onTimeColor;
    public Color onHalfBeatColor;
    public SpriteRenderer inputSquare;
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            checkOnTime(0);
        }
        if (Input.GetKeyDown(KeyCode.S)){
            checkOnTime(0);
            musicPlayer.playSound("kickTest");
        }
        if (Input.GetKeyDown(KeyCode.D)){
            checkOnTime(1);
            musicPlayer.playSound("snareTest");
        }
        //half beat test
        if (Input.GetKeyDown(KeyCode.G)){
            checkOnTime(2);
            musicPlayer.playSound("closedHatTest");
        }
        if (Input.GetKeyDown(KeyCode.H)){
            checkOnTime(3);
            musicPlayer.playSound("openHatTest");
        }
    }
    public bool checkOnTime(int actionType){
        if (inputSquare.color == onTimeColor){
            inputSquare.color = offTimeColor;
        }
        else{
            inputSquare.color = onTimeColor;
        }

        if (timeManager.activeAction && enemyControl.currAction.actionType == actionType && !enemyControl.currAction.actionHit){
            
        
            square.color = onTimeColor;
            enemyControl.currAction.actionHit = true;
            enemyControl.beatHit();
            return true;
        }
        enemyControl.beatMissed();
        square.color = offTimeColor;
        return false;
    }*/
}
