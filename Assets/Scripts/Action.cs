using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action
{
    // Start is called before the first frame update
    public int halfBeatsStart;
    public int halfBeatsLater;
    public int quarterBeatsLater;
    public bool useHalfBeat;
    public int actionType;
    //on whole or half beat, 0 or 1
    public int beatsLater;
    public bool actionHit = false;
    public bool useQuarterBeat;

    //ONLY QUARTER NOTE RIGHT NOW, UPDATE LATER
    public int beatSinceStart;

    //kick, snare, high hat, closed hat
    static Color[] actionColors;
    static Color[] halfBeatsColor;
    public Action(int halfBeatsStart, int halfBeatsLater, int actionType){
        this.halfBeatsStart = halfBeatsStart;
        this.halfBeatsLater = halfBeatsLater;
        this.actionType = actionType;
    }
    public bool checkOnTime(int currHalfBeats, int beatCount, int inputActionType){
        if (halfBeatsLater == currHalfBeats && actionType == inputActionType){
            actionHit = true;
            return true;
        }
        return false;
    }
}
