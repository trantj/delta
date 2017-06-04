using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlaneController : MonoBehaviour {
    //private GameObject plane;
    private Rigidbody plane;
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private Vector3 currentSwipe;
    private float dragDistance;
    private bool intialTouch = false;

    public enum Swipe { None, Up, Down, Left, Right };

	// Use this for initialization
	void Start () {
        //plane = GameObject.FindGameObjectWithTag("Player");
        plane = GetComponent<Rigidbody>();
        plane.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update () {
        print("");
	}
    
    void LateUpdate()
    {
        checkForSwipe();
    }

    void checkForSwipe()
    {
        if(Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Began)
            {
                // Save beginning of touch
                firstPoint = new Vector3(t.position.x, t.position.y);
            }

            if(t.phase == TouchPhase.Stationary)
            {
                //move(Swipe.Up);
                plane.AddForce(new Vector3(0.0f, 10.0f, 0.0f), ForceMode.VelocityChange);
                Debug.Log("move up");
            }

            if(t.phase == TouchPhase.Ended)
            {
                //plane.AddForce(new Vector3(0.0f, 0.0f, 0.0f), ForceMode.VelocityChange);
                // Save ending of touch 
                secondPoint = new Vector3(t.position.x, t.position.y);

                // Create vector from the two points
                currentSwipe = new Vector3(secondPoint.x - firstPoint.x, secondPoint.y - firstPoint.y);

                currentSwipe.Normalize();
                
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    sideMove(Swipe.Left);
                    Debug.Log("left swipe");
                }
                //swipe right
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    sideMove(Swipe.Right);
                    Debug.Log("right swipe");
                }
            }
        }
    }

    void sideMove(Swipe direction)
    {
        bool leftAble = false;
        bool rightAble = false;
        float zPos = plane.transform.position.z;
        float xPos = plane.transform.position.x;
        float yPos = plane.transform.position.y;
        if (zPos == 0.0f)
        {
            leftAble = true;
            rightAble = true;
        }
        else if (zPos == -10.0f)
        {
            leftAble = false;
            rightAble = true;
        }
        else if(zPos == 10.0f)
        {
            leftAble = true;
            rightAble = false;
        }


        if (direction == Swipe.Left && leftAble == true)
        {
            //plane.transform.position = new Vector3(xPos, yPos, zPos -10.0f);
            Vector3 newPos = new Vector3(xPos, yPos, zPos - 10.0f);
            plane.MovePosition(newPos);
        }
        else if(direction == Swipe.Right && rightAble == true)
        {
            //plane.transform.position = new Vector3(xPos, yPos, zPos + 10.0f);
            Vector3 newPos = new Vector3(xPos, yPos, zPos + 10.0f);
            plane.MovePosition(newPos);
        }
    }
}
