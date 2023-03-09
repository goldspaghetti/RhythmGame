using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : EnemyAction
{
    // Start is called before the first frame update
    private bool activated = false;
    private int[,] activeReturnValue = new int[1,2];
    //public override int beatsLater = 2;
    public int posX;
    public int posY;
    private int delay = 2;
    public Color onColor = Color.red;
    public Color offColor = Color.grey;
    public Color almostOn = Color.blue;
    private SpriteRenderer spriteRenderer;
    /*public SpikeTrap(int beatsLater, int beatsSinceStart, int actionType, int posX, int posY) 
    : base(beatsLater, beatsSinceStart, actionType){
        this.posX = posX;
        this.posY = posY;
        this.activeReturnValue[0,0] = posX;
        this.activeReturnValue[0,1] = posY;
    }*/
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void activate(){
        
    }
    public void init(int posX, int posY){
        beatsLater = 0;
        this.gameObject.SetActive(true);
        this.posX = posX;
        this.posY = posY;
        transform.position = new Vector3(posX * GridManager.tileSpace, posY * GridManager.tileSpace, 0);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = offColor;

        activeReturnValue[0, 0] = posX;
        activeReturnValue[0, 1] = posY;
    }
    public override void endOfBeat(){
        beatsSinceActivation++;
        if (activated){
            activated = false;
            updateGridValue = null;
            spriteRenderer.color = almostOn;
        }
        else{
            activated = true;
            updateGridValue = activeReturnValue;
            spriteRenderer.color = onColor;
            //ANIMATION FOR TURNING ON HERE!
        }
        
    }
    public override void endOfAnimation()
    {
        if (activated){
            spriteRenderer.color = offColor;
        }
    }
}
