using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionVisulizer : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer beat1;
    public SpriteRenderer beat2;
    public SpriteRenderer beat3;
    public SpriteRenderer beat4;

    public Color onBeat;
    public Color offBeat;

    public TimeManager timeManager;

    //public Action[] activeActions = new Action[4];
    //ACTIVE ACTIONS STORES THE BEAT LATER
    public int[] activeActions = new int[4];

    //public ArrayList activeActions = new ArrayList();
    
    void Start()
    {
        /*for (int i = 0; i < activeActions.Length; i++){
            Debug.Log(activeActions[i]);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < activeActions.)
    }
    public void addBeat(int currAction){
        /*for (int i = 0; i < activeActions.Length; i++){
            if (activeActions[i] == null){
                activeActions[i] = newAction;
                break;
            }
        }*/
        for (int i = 0; i < activeActions.Length; i++){
            if (activeActions[i] == 0){
                activeActions[i] = currAction;
                break;
            }
        }
    }
    public void updateVisulizer(){
        beat1.color = offBeat;
        beat2.color = offBeat;
        beat3.color = offBeat;
        beat4.color = offBeat;
        for (int i = 0; i < activeActions.Length; i++){
            int currAction = activeActions[i];
            if (currAction == 0){
                continue;
            }
            //Debug.Log(currAction);
            switch(currAction - timeManager.beatCount){
                case 0:
                    beat1.color = onBeat;
                    activeActions[i] = 0;
                    break;
                case 1:
                    beat2.color = onBeat;
                    break;
                case 2:
                    beat3.color = onBeat;
                    break;
                case 3:
                    beat4.color = onBeat;
                    break;
            }
        }
    }
}
