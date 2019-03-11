using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSide : MonoBehaviour
{
    public float pos_x_start;
    public float pos_x_end;
    public float speed = 1f;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float width = GetComponent<BoxCollider2D>().bounds.size.x;
        if (pos_x_start>pos_x_end){
            transform.position += transform.right*-speed*Time.deltaTime;
            if(transform.position.x+width<pos_x_end){
                Vector3 temp = new Vector3(pos_x_start + width,transform.position.y,transform.position.z);
                transform.position = temp;
            }
        }
        else{
            transform.position += transform.right*speed*Time.deltaTime;
            if(transform.position.x+width>pos_x_end){
                Vector3 temp = new Vector3(pos_x_start-width,transform.position.y,transform.position.z);
                transform.position=temp;
            }
        }
    }
}
