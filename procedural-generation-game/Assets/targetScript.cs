using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    public Vector2 screenPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Vector3 pos3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenPosition[0] = pos3.x;
            screenPosition[1] = pos3.y;
            Debug.Log(screenPosition);
            transform.position = screenPosition;
        }
    }


        
}
