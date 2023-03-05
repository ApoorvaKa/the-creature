using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject target;
    public GameObject body;
    public List<GameObject> bodyAnchors = new List<GameObject>();
    List<Vector3> anchorOffset = new List<Vector3>();

    public GameObject tentPiece;

    public int tentSize = 5;
    public float tentDist = 5.0f;
    public float tentWidth = 1.0f;
    public float followSpeed = 1.0f;
    List<List<GameObject>> tentacles = new List<List<GameObject>>();
    List<Vector3> tentTargets = new List<Vector3>();
   
    public float rotationSpeed;
    private Vector2 direction;
    private Quaternion lastRot;
    private Quaternion rotationDiff;


    // Start is called before the first frame update
    void Start()
    {
        int numAnchor = 0;

        foreach (var anchor in bodyAnchors){ //create tentacle at bodyAnchor
            tentacles.Add(new List<GameObject>());
            anchorOffset.Add(body.transform.position - anchor.transform.position);
            tentTargets.Add(new Vector3(0.0f, 0.0f, 0.0f));
            for(int i = 0; i < tentSize; i++){
                tentacles[numAnchor].Insert(0, Instantiate(tentPiece, transform.position, transform.rotation)); //insert to add to array in backwards for inverse kinematics
                tentacles[numAnchor][0].transform.parent = transform;
                tentacles[numAnchor][0].transform.localScale = new Vector3(transform.localScale.x - tentWidth*((float)tentSize  + 1.0f - (float)i)/(float)tentSize - 1.0f,transform.localScale.y - tentWidth*((float)tentSize + 1.0f - (float)i)/(float)tentSize - 1.0f, 1.0f);
            };
            // tentacles[numAnchor][0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            // Debug.Log(tentacles);
            numAnchor += 1;
        };

    }

    // Update is called once per frame
    void Update()
    {
        // rotate towards mouse
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - body.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        body.transform.rotation = Quaternion.Slerp(body.transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        rotationDiff = rotation * Quaternion.Inverse(lastRot); // difference between quaternions
        lastRot = rotation;
        // Debug.Log( rotationDiff.eulerAngles);

        float step = speed * Time.deltaTime;

        //move towards target
        body.transform.position = Vector2.MoveTowards(body.transform.position, target.transform.position, step);
        // body.transform.position = new Vector3(body.transform.position.x, body.transform.position.y, 5);
        
        int offsetInd = 0;
        foreach (var anchor in bodyAnchors){
            float distanceToBody = Vector3.Distance(anchor.transform.position, body.transform.position);

            if(distanceToBody >= Vector3.Distance(body.transform.position + anchorOffset[offsetInd], body.transform.position)){
                anchor.transform.position = Vector2.MoveTowards(anchor.transform.position, body.transform.position + anchorOffset[offsetInd], step*0.95f);
            }
            else{
                anchor.transform.position = Vector2.MoveTowards(anchor.transform.position, RotatePointAroundPivot(anchor.transform.position, body.transform.position, rotationDiff.eulerAngles), step*0.95f);
            }
            //anchor.transform.position = new Vector3(0,0,0);
            offsetInd += 1;
        };

        for(int i = 0; i < tentacles.Count; i++){
            float distanceToAnchor = Vector3.Distance(bodyAnchors[i].transform.position, tentacles[i][0].transform.position);
            // float distanceToTarget = Vector3.Distance(tentTargets[i], tentacles[i][0].transform.position);
            // Debug.Log(distanceToAnchor);
            for( int j = tentSize-1; j >= 0; j--){

                // if(j==tentSize-1){ // forward kinematics
                //     tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, bodyAnchors[i].transform.position, step * followSpeed);
                //     tentacles[i][j].transform.up = Vector2.MoveTowards(tentacles[i][j].transform.up, bodyAnchors[i].transform.position - tentacles[i][j].transform.position, step * followSpeed* 10.0f);
                // }   
                // else{
                //     tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, (tentacles[i][j].transform.position - tentacles[i][j+1].transform.position).normalized * tentDist + tentacles[i][j+1].transform.position, step * followSpeed);
                //     tentacles[i][j].transform.up = Vector2.MoveTowards(tentacles[i][j].transform.up, tentacles[i][j].transform.position - tentacles[i][j+1].transform.position, step * followSpeed);
                // }

                if(j==0 && distanceToAnchor > tentDist * tentSize * 1.4f){
                    tentTargets[i] = (body.transform.position - bodyAnchors[i].transform.position).normalized * tentDist *tentSize* 1.25f   + body.transform.position ;
                }
                else if(j != 0){ //inverse kinematics
                    tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, (tentacles[i][j].transform.position - tentacles[i][j-1].transform.position).normalized * tentDist + tentacles[i][j-1].transform.position, step * followSpeed);
                    // tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, (tentacles[i][0].transform.position - bodyAnchors[i].transform.position).normalized * tentDist+ tentacles[i][j-1].transform.position, step * followSpeed);
                    tentacles[i][j].transform.up = Vector2.MoveTowards(tentacles[i][j].transform.up, tentacles[i][j].transform.position - tentacles[i][j-1].transform.position, step * followSpeed * 5.0f);
                }
                if(j==0 ){
                    tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, tentTargets[i], step * followSpeed);
                    tentacles[i][j].transform.up = Vector2.MoveTowards(tentacles[i][j].transform.up, tentacles[i][j].transform.position - body.transform.position, step * followSpeed * 5.0f);
                }
            };
        };

        for(int i = 0; i < tentacles.Count; i++){
            for( int j = 0; j < tentSize; j++){
                if(j == tentSize-1){
                    tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, bodyAnchors[i].transform.position, step * followSpeed * 1.5f);
                }
                else{
                    tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, (tentacles[i][j+1].transform.position - tentacles[i][j].transform.position).normalized * tentDist + tentacles[i][j+1].transform.position, step);
                    // tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, tentacles[i][j+1].transform.position, step * followSpeed);
                }
                
            }
        }
        Debug.Log(tentTargets[0]);
        // for(int i = 0; i < tentacles.Count; i++){
        //     for( int j = tentSize-1; j >= 0; j--){
        //         float distanceToAnchor = Vector3.Distance(bodyAnchors[i].transform.position, tentacles[i][j].transform.position);
        //         if(distanceToAnchor >= (float)tentSize * tentDist){
        //             if(j==tentSize-1){
        //                 tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, Quaternion.Euler(60, 0, 0) * (tentacles[i][j].transform.position - bodyAnchors[i].transform.position) + bodyAnchors[i].transform.position, step);
        //             }   
        //             else{
        //                 tentacles[i][j].transform.position = Vector2.MoveTowards(tentacles[i][j].transform.position, (tentacles[i][j].transform.position - tentacles[i][j+1].transform.position).normalized * tentDist + tentacles[i][j+1].transform.position, step);
        //             }
        //         }
        //         Debug.Log(j);
        //     }
        // };


        
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

}
