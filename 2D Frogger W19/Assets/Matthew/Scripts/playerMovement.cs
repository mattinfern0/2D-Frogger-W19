using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveScale = 1f;
    [SerializeField] float moveSpeed = 1f;

    bool doCorrectPos; // Set this to false when player is on moving platform and vice verca
    Rigidbody2D m_rigidbody2D;
    bool moving;
    float halfway;

    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        moving = false;
        halfway = moveScale * 0.5f;
        doCorrectPos = true;

        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
            moving = false;
        }*/

        handleMoveInput();
        //correctPlayerPosition();
    }

    void FixedUpdate()
    {
        movePlayer();
        correctPlayerPosition();
    }

    void handleMoveInput()
    {
        if (transform.position == targetPos){
            moving = false;
            if (Input.GetAxis("Horizontal") > 0 && !moving){
                //moving = true;
                Debug.Log("Moving Right");
                targetPos = new Vector3(transform.position.x + (1 * moveScale), transform.position.y, transform.position.z);
                //m_rigidbody2D.MovePosition(new Vector2(position.x + (1 * moveScale), position.y));

            } else if (Input.GetAxis("Horizontal") < 0 && !moving){
                //moving = true;
                Debug.Log("Moving Left");
                targetPos = new Vector3(transform.position.x - (1 * moveScale), transform.position.y, transform.position.z);
                //Vector3 position = transform.position;
                //m_rigidbody2D.MovePosition(new Vector2(position.x + (-1 * moveScale), position.y));
            } else if (Input.GetAxis("Vertical") > 0 && !moving){
                //moving = true;
                Debug.Log("Moving Up");
                targetPos = new Vector3(transform.position.x, transform.position.y + (1 * moveScale), transform.position.z);
                //Vector3 position = transform.position;
                //m_rigidbody2D.MovePosition(new Vector2(position.x, position.y + (1 * moveScale)));

            } else if (Input.GetAxis("Vertical") < 0 && !moving){
                // moving = true;
                Debug.Log("Moving Down");
                targetPos = new Vector3(transform.position.x, transform.position.y - (1 * moveScale), transform.position.z);
                //Vector3 position = transform.position;
                // m_rigidbody2D.MovePosition(new Vector2(position.x, position.y + (-1 * moveScale)));
            }
        } else {
            moving = true;
        }
    }

    void movePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    // Centers player onto grid square; important when player gets off moving object
    void correctPlayerPosition(){
        if (!moving && doCorrectPos){
            if (transform.position.x % halfway != 0){
                Debug.Log("Not centered! (x)");
                float correctPosX = Mathf.Floor(transform.position.x) + halfway;
                m_rigidbody2D.MovePosition(new Vector2(correctPosX, transform.position.y));
            }

            if (transform.position.y % halfway != 0){
                float correctPosY = Mathf.Floor(transform.position.y) + halfway;
                m_rigidbody2D.MovePosition(new Vector2(transform.position.x, correctPosY));
            }
        }
    }


}
