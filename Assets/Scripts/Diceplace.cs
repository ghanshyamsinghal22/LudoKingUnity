using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Diceplace : MonoBehaviour{
    [SerializeField] public int turn =0;
    [SerializeField] public List<GameObject> goti;
    [SerializeField] public List<GameObject> path;
    [SerializeField] State data;
    [SerializeField] List<Sprite> s;
    [SerializeField] SpriteRenderer spriteRenderer;
    // int[] nur={6,6,6,2,6,1};
    public int num=0;
    public int count =0;
    int available=0;

    void Start(){
        data.moves=0;
        data.currentturn=0;
        Debug.Log(data.currentturn);
       
        // if(gameObject.tag=="Player"){
        //     int i;
        //     for(i=0;i<path.Count;i++){
        //         if(path[i]!=null){
        //     spriteRenderer=path[i].GetComponent<SpriteRenderer>();
        //     spriteRenderer.color=Color.gray;
        //     new WaitForSeconds(1f);
        //         }
        //     }
        // }
    }
    void Update(){
        if(data.currentturn!=turn){
            gameObject.GetComponent<SpriteRenderer>().sprite=spriteRenderer.sprite;
        }
        // if(data.currentturn==turn &&Input.GetMouseButtonDown(0)==false){
        //     gameObject.GetComponent<SpriteRenderer>().sprite=s[0];
        // }
        if (Input.GetMouseButtonDown(0)&&data.currentturn==turn){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            // Debug.Log("i was clicked ");

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.transform == transform){
                GenerateNumber();
                available=0;
                for(int i =0;i<goti.Count;i++){
                    if(goti[i].GetComponent<Gotimovement>().CheckMoveable(num)){
                        available++;
                    }
                }
                gameObject.GetComponent<SpriteRenderer>().sprite=s[num-1];
                Debug.Log("sprite was set");
                StartCoroutine(WaitForSecondsDelay());
            }
        }
    }
    void GenerateNumber(){
        num =Random.Range(1,7);
        data.moves++;
    }
    private IEnumerator WaitForSecondsDelay(){
        Debug.Log("Delay started");
        yield return new WaitForSeconds(2);
        Debug.Log("3 seconds have passed");
        if(available==0){
                    data.currentturn=(data.currentturn+1)%4;
        }
    }
}