using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EnemyAction : MonoBehaviour
{
    // Start is called before the first frame update
    public static int actionType;
    public int beatsLater;
    public bool currActive = false;

    //ONLY QUARTER NOTE RIGHT NOW, UPDATE LATER
    public int beatsSinceStart;
    public int beatsSinceActivation = 0;
    public int[,] updateGridValue;

    //kick, snare, high hat, closed hat
    /*public EnemyAction(int beatsLater, int beatsSinceStart, int actionType){
        this.beatsLater = beatsLater;
        this.beatsSinceStart = beatsSinceStart;
        this.actionType = actionType;
    }*/
    public abstract void activate();
    public abstract void endOfBeat();
    public abstract void endOfAnimation();
}
