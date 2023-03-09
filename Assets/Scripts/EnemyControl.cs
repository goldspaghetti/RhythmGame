using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    // Start is called before the first frame update
    //public Action[] actions = new Action[3];
    public int currActionIndex = 0;
    public int actionCount;
    public List<EnemyAction> enemyActions = new List<EnemyAction>();

    public SpikeTrapBuilder[] spikeTrapBuilders;
    public List<SpikeTrap> spikeTraps = new List<SpikeTrap>();
    public GameObject spikeTrapPrefab;

    public LaserBuilder[] laserBuilders;
    public List<Laser> lasers = new List<Laser>();
    public GameObject laserPrefab;
    public GridManager gridManager;
    public MusicPlayer musicPlayer;
    
    //public Action currAction;
    public TimeManager timeManager;
    public double[] intensities;
    //public int beatHitCount;
    //public int beatMissedCount;

    void Awake(){
        musicPlayer.initSound();
        //add spikeTraps and 
        /*foreach(SpikeTrapBuilder currSpikeTrapBuilder in spikeTrapBuilders){
            SpikeTrap currSpikeTrap = Instantiate(spikeTrapPrefab).GetComponent<SpikeTrap>();
            currSpikeTrap.init(currSpikeTrapBuilder.xPos, currSpikeTrapBuilder.yPos);
            spikeTraps.Add(currSpikeTrap);
            enemyActions.Add(currSpikeTrap);
        }
        foreach(LaserBuilder currLaserBuilder in laserBuilders){
            Laser currLaser = Instantiate(laserPrefab).GetComponent<Laser>();
            currLaser.init(currLaserBuilder.orientation, currLaserBuilder.position, currLaserBuilder.beatsLater);
            lasers.Add(currLaser);
            enemyActions.Add(currLaser);
        }*/
        generateSpikeTraps();
        generateLasers();

        int currTotalBeats = 0;
        foreach(EnemyAction currEnemyAction in enemyActions){
            currTotalBeats += currEnemyAction.beatsLater;
            currEnemyAction.beatsSinceStart = currTotalBeats;
        }
        //init all enemyActions
        /*foreach(EnemyAction currEnemyAction in enemyActions){
            
        }*/
        foreach(SpikeTrap spiketrap in spikeTraps){
            //spiketrap.activate(spikeTrapPrefab);
        }
    }
    public void manageEndOfBeats(){
        //reset gridManager
        gridManager.resetGrid();
        foreach(EnemyAction currEnemyAction in enemyActions){
            currEnemyAction.endOfBeat();
            gridManager.updateGrid(currEnemyAction.updateGridValue);
        }
        //update grid gridManager

    }
    public void manageEndOfAnimation(){
        foreach(EnemyAction currEnemyAction in enemyActions){
            currEnemyAction.endOfAnimation();
            //gridManager.updateGrid(currEnemyAction.updateGridValue);
        }
    }
    public void generateSpikeTraps(){
        for (int i = 0; i < gridManager.gridWidth; i++){
            for (int j = 0; j < gridManager.gridHeight; j++){
                bool hasSpike = Random.Range(0, 5) == 0;
                if (i == 0 && j == 0){
                    continue;
                }
                if (hasSpike){

                    SpikeTrap currSpikeTrap = Instantiate(spikeTrapPrefab).GetComponent<SpikeTrap>();
                    currSpikeTrap.init(i, j);
                    spikeTraps.Add(currSpikeTrap);
                    enemyActions.Add(currSpikeTrap);

                }
            }
        }
    }
    public void generateLasers(){
        double currTime = 0;
        int nextIntenseIndex = 0;
        bool intenseFinished = false;
        bool currIntense = false;
        /*while(!musicPlayer.soundLoaded){

        }*/
        while(currTime < musicPlayer.currMusicSound.audioClip.length){
            int beatIncrease;
            if (currIntense){
                beatIncrease = Random.Range(0, 3);
            }
            else{
                beatIncrease = Random.Range(4, 6);
            }
            bool orientation = (Random.Range(0, 2)) == 1;
            int position;
            if (orientation){
                position = Random.Range(0, gridManager.gridWidth);
            }
            else{
                position = Random.Range(0, gridManager.gridHeight);
            }

            //generate it!
            Laser currLaser = Instantiate(laserPrefab).GetComponent<Laser>();
            currLaser.init(orientation, position, beatIncrease);
            lasers.Add(currLaser);
            enemyActions.Add(currLaser);


            //intensities stuff
            currTime += beatIncrease * timeManager.secPerBeat;
            //Debug.Log(nextIntenseIndex);
            if (currTime > intensities[nextIntenseIndex] && !intenseFinished){
                currIntense = !currIntense;
                if (nextIntenseIndex >= intensities.Length - 1){
                    intenseFinished = true;
                    continue;
                }
                else{
                    nextIntenseIndex++;
                }
            }
        }
    }



    //DEPRECIATED STUFF

    /*public void actionPassed(){
        timeManager.quarterBeatsSinceAction = 0;
        timeManager.halfBeatSinceAction = 0;
        //timeManager.beatsSinceAction = 0;
        currActionIndex++;
        //REMOVE LATER, having this for now
        if (currActionIndex >= actions.Length){
            currActionIndex = 0;
            //timeManager.beatCount = 0;
        }
        currAction = actions[currActionIndex];
    }
    public void beatHit(){
        beatHitCount++;
        beatHitText.text = beatHitCount.ToString();
    }
    public void beatMissed(){
        beatMissedCount++;
        beatMissedText.text = beatMissedCount.ToString();
    }
    public void missedAction(){
        beatMissed();
        //Debug.Log("missed action");
    }*/
}
