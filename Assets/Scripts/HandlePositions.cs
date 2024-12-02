using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePositions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera camera;
    [SerializeField] List<GameObject> pawns;
    GameObject pawntobeKilled;

    public int CountOfPawnsAtDestination(Vector3 position,int colour){
        int i;
        Vector2 pawnposition=camera.WorldToScreenPoint(position);
        Debug.Log(pawnposition);
        int countofpawns=0;
        for(i=0;i<pawns.Count;i++){
            if(pawns[i].GetComponent<Gotimovement>().state==1&&pawns[i].GetComponent<Gotimovement>().colour!=colour){
                Vector2 enemypawnposition=camera.WorldToScreenPoint(pawns[i].transform.position);
                if(enemypawnposition==pawnposition){
                countofpawns++;
                pawntobeKilled=pawns[i];
                }
            }
        }
        Debug.Log("i am count "+ countofpawns);
        return countofpawns;
    }
    public void KillPawn(Vector3 position,int colour){
        int i;
        Vector3 pawnposition=camera.WorldToScreenPoint(position);
        Debug.Log(pawnposition);
        for(i=0;i<pawns.Count;i++){
            if(pawns[i].GetComponent<Gotimovement>().state==1&&pawns[i].GetComponent<Gotimovement>().colour!=colour){
                Vector3 enemypawnposition=camera.WorldToScreenPoint(pawns[i].transform.position);
                if(enemypawnposition==pawnposition){
                pawntobeKilled=pawns[i];
                pawns[i].transform.position=pawns[i].GetComponent<Gotimovement>().InitialPosition;
                pawns[i].GetComponent<Gotimovement>().state=0;
                }
            }
        }
    }
}
                // (colour!=pawns[i].GetComponent<Gotimovement>().colour)&&