using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class TimeManager : MonoBehaviour
{
    //currTime, secPerBeat, and errorWindow are all in seconds
    private double currTime;
    private double timeSinceBeat;
    private double timeSinceHalfBeat;
    private double timeSinceQuarterBeat;
    public int bpm;
    //should I have sec per beat 
    public double secPerBeat;
    //error window entered as % in unity
    //early error window
    private double secPerHalfBeat;
    private double secPerQuarterBeat;
    public int beatCount = 0;
    private int halfBeatCount = 0;
    private int quarterBeatCount = 0;

    public int quarterBeatsSinceAction = 0;

    public int halfBeatSinceAction = 0;
    public int beatsSinceAction = 0;
    public bool activeAction = false;

    public double errorWindowE;
    public double errorWindowL;
    public double animationEndTime;

    public double errorWindowHalfE;
    public double errorWindowHalfL;
    public double errorWindowQuarterE;
    public double errorWindowQuarterL;

    public bool thisBeatEntered = false;
    public bool thisBeatExited = true;
    public bool animationExited = true;

    public bool thisHalfBeatEntered = false;
    public bool thisHalfBeatExited = true;

    public bool thisQuarterBeatEntered = false;
    public bool thisQuarterBeatExited = true;


    //test
    public SpriteRenderer square;
    public SpriteRenderer halfBeatSquare;
    public SpriteRenderer actionSquare;
    public PostProcessVolume postProcess;
    public ChromaticAberration chromaticAberration;
    public Animator animator;
    public Text comboCount;
    

    //public ActionVisulizer[] actionVisulizers;

    public Color offTimeColor;
    public Color onTimeColor;
    public Color onHalfBeatColor;
    public bool currOnBeat;
    public bool currOnHalfBeat;
    public bool currOnQuarterBeat;
    //public bool currOnAction;

    //public Text currQuarterBeatText;

    //public EnemyControl enemyControl;
    public PlayerMovement playerMovement;
    public MusicPlayer musicPlayer;
    public EnemyControl enemyControl;
    public GridManager gridManager;

    int nextIntenseIndex = 0;
    bool intenseFinished = false;
    bool currIntense = false;

    bool thisBeatHit = false;
    int beatHitCount = 0;
    public float chromaticIntenseModifier = 0.2f;

    // Update is called once per frame
    void Awake(){
        comboCount.text = "Beat Hit " + beatHitCount.ToString();
        //chromaticAberration = postProcess.GetComponent<ChromaticAberration>();
        chromaticAberration = postProcess.profile.GetSetting<ChromaticAberration>();
        secPerBeat = 1d/(bpm/60d);
        Debug.Log("secPerBeat: " + secPerBeat);
        secPerHalfBeat = secPerBeat/2d;
        secPerQuarterBeat = secPerBeat/4d;
        //error window shite
        /*errorWindowE *= (80d/Mathf.Sqrt(bpm));
        errorWindowL *= (80d/Mathf.Sqrt(bpm));*/
        errorWindowE *= secPerBeat;
        errorWindowL *= secPerBeat;
        animationEndTime *= secPerBeat;
        Debug.Log("errorWindowE: " + errorWindowE);
        Debug.Log("errorWindowL: " + errorWindowL);
        
        /*
        //for now
        errorWindowHalfE = errorWindowE/2d;
        errorWindowHalfL = errorWindowL/2d;

        //errorWindowHalfE = errorWindowE;
        //errorWindowHalfL = errorWindowL;
        errorWindowQuarterE = errorWindowE/4d;
        errorWindowQuarterL = errorWindowE/4d;*/
    }
    void exitPostFx(){
        chromaticAberration.intensity.value = 0.05f;
    }
    void Update()
    {
        //RADICAL CHANGE, GO BACK IF NEEDED, USING AUDIOSOURCE.TIME FOR CURRTIME NOW

        //Debug.Log("currTime: " + currTime);
        //maybe have all depend on quarter beat and have variable error windows somehow?
        currTime = musicPlayer.currMusic.time;
        //for now!
        /*
        if (currTime > secPerBeat){
            exitPostFx();
        }*/

        timeSinceBeat = currTime % secPerBeat;
        //timeSinceHalfBeat += Time.deltaTime;
        //timeSinceQuarterBeat += Time.deltaTime;
        currOnBeat = onTime();
        //currOnHalfBeat = onHalfBeatTime();
        //currOnQuarterBeat = onQuarterBeatTime();


        
        //OLD DEPRECIATED STUFF!

        /*
        Debug.Log("currTime: " + currTime);
        //maybe have all depend on quarter beat and have variable error windows somehow?
        currTime += Time.deltaTime;
        timeSinceBeat += Time.deltaTime;
        //timeSinceHalfBeat += Time.deltaTime;
        //timeSinceQuarterBeat += Time.deltaTime;
        currOnBeat = onTime();
        //currOnHalfBeat = onHalfBeatTime();
        //currOnQuarterBeat = onQuarterBeatTime();
        if (timeSinceBeat >= secPerBeat){
            //may lead to some problems, but do this for now I guess
            //timeSinceBeat -= secPerBeat;

            timeSinceBeat = currTime % secPerBeat;
        }
        /*if (timeSinceHalfBeat >= secPerHalfBeat){
            timeSinceHalfBeat = currTime % secPerHalfBeat;
        }
        if (timeSinceQuarterBeat >= secPerQuarterBeat){
            timeSinceQuarterBeat = currTime%secPerQuarterBeat;
        }*/
        
    }
    public bool onTime(){
        //double secSinceLastBeat = currTime % secPerBeat;
        //double secSinceLastHalfBeat = currTime % secPerHalfBeat;
        /*if (timeSinceHalfBeat > (secPerHalfBeat - errorWindowHalfE) || timeSinceHalfBeat < errorWindowHalfL){
            if (!thisHalfBeatEntered) enterHalfBeat();
            return true;
        }
        else{
            if (!thisHalfBeatExited) exitHalfBeat();
        }*/

        //if it's in the time window

        if (!animationExited && timeSinceBeat > animationEndTime && !thisBeatEntered){
            animationEnd();
        }

        if (timeSinceBeat > (secPerBeat - errorWindowE) || timeSinceBeat < errorWindowL){
            if (!thisBeatEntered) enterBeat();
            return true;
        }
        else{
            if (!thisBeatExited) exitBeat();
        }

        //check if intense
        if (currTime > enemyControl.intensities[nextIntenseIndex] && !intenseFinished){
            currIntense = !currIntense;
            if (currIntense){
                chromaticIntenseModifier = 0.5f;
                updateChromatic();
            }
            else{
                chromaticIntenseModifier = 0.2f;
                updateChromatic();
            }
            if (nextIntenseIndex >= enemyControl.intensities.Length - 1){
                intenseFinished = true;
            }
            else{
                nextIntenseIndex++;
             }
        }


        //double secSinceLastBeat = currTime % secPerBeat;
        //double secSinceLastHalfBeat = currTime % secPerHalfBeat;
        /*if (secSinceLastBeat > (secPerBeat / 2d) && secSinceLastBeat - (secPerBeat/2d) < errorWindow){
            this.timeSinceBeat = 0;
            return true;
        }
        else if (secSinceLastBeat < (secPerBeat /2d) && secSinceLastBeat < errorWindow){
            this.timeSinceBeat = 0;
            return true;
        }*/


        return false;
    }
    public void animationEnd(){
        animationExited = true;
        //chromaticAberration.intensity.value = 0.05f;
        enemyControl.manageEndOfAnimation();
        animator.SetBool("AnimationEnded", true);
        Debug.Log("animator end");
    }
    public void enterBeat(){
        thisBeatEntered = true;
        thisBeatExited = false;
        animationExited = false;
        square.color = onTimeColor;
        //Debug.Log("ENTERED");
        beatCount++;
        beatsSinceAction++;

        //musicPlayer.playSound("closedHatTest");

        //check for incoming beats
        /*foreach(Action currAction in enemyControl.actions){
            //Debug.Log(currAction.beatSinceStart);
            //Debug.Log(beatCount);
            if (currAction.beatSinceStart - beatCount== 3){
                //Debug.Log("added beat " + currAction.beatSinceStart);
                if (currAction.actionType == 0){
                    //kick
                    actionVisulizers[0].addBeat(currAction.beatSinceStart);
                }
                else if (currAction.actionType == 1){
                    //snare
                    actionVisulizers[1].addBeat(currAction.beatSinceStart);
                }
            }
            else if (beatCount - currAction.beatsLater > 4){
                break;
            }
        }

        //update actionVisulizers
        foreach(ActionVisulizer actionVisulizer in actionVisulizers){
            actionVisulizer.updateVisulizer();
        }

        //square.transform.position = new Vector3(5, 0, 0);
        if (!enemyControl.currAction.useHalfBeat && enemyControl.currAction.beatsLater == beatsSinceAction){
            //Debug.Log("active action");
            actionSquare.color = Color.green;
            activeAction = true;

        }
        if (!enemyControl.currAction.useHalfBeat && enemyControl.currAction.beatsLater >= 4){

        }*/
    }
    public void updateChromatic(){
        comboCount.text = "Beat Hit " + beatHitCount.ToString();
        float newChromatic = beatHitCount * 0.1f + chromaticIntenseModifier;
        if (currIntense){
            if (newChromatic > 1) newChromatic = 1f;
        }
        else{
            if (newChromatic > 0.75) newChromatic = 0.75f;
        }
        chromaticAberration.intensity.value = newChromatic;
    }
    public void beatHit(){
        beatHitCount++;
        thisBeatHit = true;
        updateChromatic();
    }
    public void exitBeat(){
        //beat management
        thisBeatExited = true;
        thisBeatEntered = false;
        square.color = offTimeColor;
        playerMovement.triedMovedThisBeat = false;
        /*if (!playerMovement.movedThisBeat){
            gridManager.checkDamaged();
        }*/
        enemyControl.manageEndOfBeats();
        gridManager.checkDamaged();
        animator.SetBool("AnimationEnded", false);
        Debug.Log("animator start");

        if (!thisBeatHit){
            beatHitCount = 0;
            updateChromatic();
        }
        thisBeatHit = false;
        //chromaticAberration.intensity.value = 0.2f;
        //update grid GridManager
        //animator.Play("Base Layer.PostFxAnimationTest", 1);

        //check player stuff

        //reset gridManager


        //musicPlayer.playSound("closedHatTest");
        
        //Debug.Log("EXITED");
        //square.transform.position = new Vector3(-5, 0, 0);
        /*if (!enemyControl.currAction.useHalfBeat && activeAction){
            //beatsSinceAction = 0;
            //halfBeatSinceAction = 0;
            enemyControl.actionPassed();
            if (!enemyControl.currAction.actionHit){
                enemyControl.missedAction();
            }
            //Debug.Log("after action");
            actionSquare.color = Color.red;
            enemyControl.currAction.actionHit = false;
            activeAction = false;
        }*/
    }

    /*
    public bool onHalfBeatTime(){
        if (timeSinceHalfBeat > (secPerHalfBeat - errorWindowHalfE) || timeSinceHalfBeat < errorWindowHalfL){
            if (!thisHalfBeatEntered) enterHalfBeat();
            return true;
        }
        else{
            if (!thisHalfBeatExited) exitHalfBeat();
        }
        return false;
    }
    public bool onQuarterBeatTime(){
        if (timeSinceQuarterBeat > (secPerQuarterBeat - errorWindowQuarterE) || timeSinceQuarterBeat < errorWindowQuarterL){
            if (!thisQuarterBeatEntered) enterQuarterBeat();
            return true;
        }
        else{
            if (!thisQuarterBeatExited) exitQuarterBeat();
            return false;
        }
        //return false;
    }
    public void enterQuarterBeat(){
        thisQuarterBeatEntered = true;
        thisQuarterBeatExited = false;
        quarterBeatCount++;
        quarterBeatsSinceAction++;
        if (enemyControl.currAction.useQuarterBeat && enemyControl.currAction.quarterBeatsLater == quarterBeatsSinceAction){
            //Debug.Log("active action");
            activeAction = true;
        }
        currQuarterBeatText.text = quarterBeatsSinceAction.ToString();
        
    }
    public void exitQuarterBeat(){
        thisQuarterBeatExited = true;
        thisQuarterBeatEntered = false;
        if (enemyControl.currAction.useQuarterBeat && activeAction){
            //beatsSinceAction = 0;
            //halfBeatSinceAction = 0;
            enemyControl.actionPassed();
            if (!enemyControl.currAction.actionHit){
                enemyControl.missedAction();
            }
            //Debug.Log("after action");
            enemyControl.currAction.actionHit = false;
            activeAction = false;
        }
    }
    
    public void enterHalfBeat(){
        thisHalfBeatEntered = true;
        thisHalfBeatExited = false;
        halfBeatSquare.color = onHalfBeatColor;
        halfBeatCount++;
        halfBeatSinceAction++;

        if (enemyControl.currAction.useHalfBeat && enemyControl.currAction.halfBeatsLater == halfBeatSinceAction){
            activeAction = true;
        }

    }
    public void exitHalfBeat(){
        thisHalfBeatExited = true;
        thisHalfBeatEntered = false;
        halfBeatSquare.color = offTimeColor;

        //if curr action, remove action
        if (activeAction && enemyControl.currAction.useHalfBeat){
            enemyControl.actionPassed();
            if (!enemyControl.currAction.actionHit){
                //Debug.Log("MISSED ACTION");
                enemyControl.missedAction();
            }
            enemyControl.currAction.actionHit = false;
            activeAction = false;
        }

    }
    */
}
