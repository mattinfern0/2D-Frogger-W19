using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveScale = 1f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] bool snapToGridOn = true;

    bool doCorrectPos; // Set this to false when player is on moving platform and vice verca
    Rigidbody2D m_rigidbody2D;
    float halfway;
    bool reachedTargetPos;

    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        halfway = moveScale * 0.5f;
        doCorrectPos = true;
 

        targetPos = transform.position;
        reachedTargetPos = true;
    }

    // Update is called once per frame
    void Update()
    {
        handleMoveInput();
    }

    void FixedUpdate()
    {
        correctPlayerPosition();
        movePlayer();
    }

    // Use this instead of "targetPos = newPosition"
    void moveTargetPos(Vector3 newPosition)
    {
        targetPos = newPosition;
        reachedTargetPos = false;
    }

    void handleMoveInput()
    {
        if (reachedTargetPos){
            if (Input.GetAxis("Horizontal") > 0){
                Debug.Log("Moving Right");
                moveTargetPos(new Vector3(transform.position.x + (1 * moveScale), transform.position.y, transform.position.z));
            } else if (Input.GetAxis("Horizontal") < 0){
                Debug.Log("Moving Left");
                moveTargetPos(new Vector3(transform.position.x - (1 * moveScale), transform.position.y, transform.position.z));
            } else if (Input.GetAxis("Vertical") > 0){
                Debug.Log("Moving Up");
                moveTargetPos(new Vector3(transform.position.x, transform.position.y + (1 * moveScale), transform.position.z));
            } else if (Input.GetAxis("Vertical") < 0){
                Debug.Log("Moving Down");
                moveTargetPos(new Vector3(transform.position.x, transform.position.y - (1 * moveScale), transform.position.z));
            }
        }
    }

    void movePlayer()
    {
        // Makes sure player only moves to targetPos once.
        if (!reachedTargetPos){
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
            if (transform.position == targetPos){
                reachedTargetPos = true;
            }
        }
    }

    // Centers player onto grid square; important when player gets off moving object
    // CURRENTLY ONLY WORKS WITH SCALE 1
    void correctPlayerPosition(){
        if (reachedTargetPos && doCorrectPos && snapToGridOn){
            if (transform.position.x % halfway != 0 || transform.position.y % halfway != 0){
                Debug.Log("Centering player");
                float correctPosX = Mathf.Floor(transform.position.x) + halfway;
                float correctPosY = Mathf.Floor(transform.position.y) + halfway;
                moveTargetPos(new Vector3(correctPosX, correctPosY, transform.position.z));
            }
        }
    }


}
