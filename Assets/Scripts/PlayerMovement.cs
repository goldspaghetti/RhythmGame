using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool triedMovedThisBeat = false;
    public bool movedThisBeat = false;
    public TimeManager timeManager;
    public GridManager gridManager;
    public MusicPlayer musicPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //manage input
        if (Input.GetKeyDown(KeyCode.W)){
            move(0);
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            move(1);
        }
        else if (Input.GetKeyDown(KeyCode.S)){
            move(2);
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            move(3);
        }
    }
    bool move(int direction){
        //check if moved this beat, if so don't move
        //musicPlayer.playSound("closedHatTest");
        if (triedMovedThisBeat){
            return false;
        }

        Debug.Log("passed movedThisBeat");
        //check if on beat
        if (timeManager.currOnBeat){
            movedThisBeat = true;
            gridManager.movePlayer(direction);
            timeManager.beatHit();
            /*switch(direction){
                //up
                case 0:
                    
                    break;
                //right
                case 1:

                    break;
                //down
                case 2:

                    break;
                //left
                case 3:

                    break;
            }*/
        }
        else{
            Debug.Log("not currTime");
        }
        triedMovedThisBeat = true;
        return true;
    }
}
