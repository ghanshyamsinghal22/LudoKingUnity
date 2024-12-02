using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Gotimovement : MonoBehaviour
{
    public Vector2 InitialPosition;
    public int state =0;
    // List<GameObject> path;
    
    [SerializeField] Diceplace diceplace;
    int currentposition =-1;
    [SerializeField] State data;
    [SerializeField] public bool ismovable=false;
    [SerializeField] public int colour =0;
    [SerializeField]HandlePositions handlePositions;
    void Start(){
        // path=diceplace.path;
        InitialPosition=transform.position;
        state=0;
        ismovable=false;
    }

    private void Update(){
        if(ismovable){
            transform.localScale=new Vector2(1,1);
        }else{
            transform.localScale=new Vector2(0.5f,0.5f);
        }
        if (Input.GetMouseButtonDown(0)&&data.currentturn==diceplace.turn){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction); 

            if (hit.collider != null && hit.transform == transform&&ismovable){
                if(CheckMoveable(diceplace.num)){
                    Movegoti(diceplace.num);
                    if(diceplace.num==6){
                        // Debug.Log("Hum first");
                        data.currentturn--;
                        diceplace.num=0;
                    }else{
                        diceplace.num=0;
                        // Debug.Log("Hum Last");
                    }
                }
                        data.currentturn=(data.currentturn+1)%4;

            }
        }
    }
    public bool CheckMoveable(int val){
        // Debug.Log(state+ "->>" +val);
        // Debug.Log("Movable function "+diceplace.turn +"->" +diceplace.count);
        if(state==0&&val==6){
            ismovable=true;
            return true;
        }else if(state==1&&
        currentposition+val<diceplace.path.Count&&
        handlePositions.CountOfPawnsAtDestination(diceplace.path[currentposition+val].transform.position,colour)<2
        ){
            //also check if the destination have less than 2 pawns of same colour 
            //if there is one then that pawn will die that is it will go back to initial state 0 and initial position 
            ismovable=true;
            return true;
        }else{
            return false;
        }
    }
    void Movegoti(int val){
        if(ismovable){
        if(state==0){
            state=1;
            currentposition=0;
        float c=0;
        while(c<1){
            c+=0.001f;
        transform.position=Vector2.Lerp(InitialPosition,diceplace.path[currentposition].transform.position,c);
        }
        }else if(state==1){
        float c=0;
        while(c<1){
            c+=0.001f;
        transform.position=Vector2.Lerp(diceplace.path[currentposition].transform.position,diceplace.path[currentposition+val].transform.position,c);
        }
        currentposition+=val;
        }
        for(int i=0;i<diceplace.goti.Count;i++){
            diceplace.goti[i].GetComponent<Gotimovement>().ismovable=false;
        }
        handlePositions.KillPawn(transform.position,colour);
        //kill
            // data.currentturn=(data.currentturn+1)%4;
            // Debug.Log(data.currentturn+"I am next");
        }
    }
}
