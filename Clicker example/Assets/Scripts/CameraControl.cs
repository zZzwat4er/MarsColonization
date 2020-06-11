using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   this script that control camera
*   it can be droped to any object including camera itself
*   you need to provide main camera object to it script
*   problem: this sctipt will work all the time 
*   problem: the is no bounderes so far
*/

public class CameraControl : MonoBehaviour
{
    private Touch initTouch;        //initial touch
    private float posX = 0f;        // camera current x coordinate
    private float posY = 0f;        // camera current Y coordinate
    private Vector3 originalPos;    // current camera coordinates
    private float dir = 1;          // dir should be -1 or 1 (put '-' here to invert camera muvement)
    
    public Camera cam;              // camera
    public float camSpeed = 0.5f;   //camera speed (0.5 is the best speed but it can be reduset a little bit)
    
    void Start()
    {
        initTouch = new Touch();    // make Touch varible clean
        originalPos = cam.transform.position;   //find ititial cameras's coordinates
        posX = originalPos.x;       // get x camera coordinate
        posY = originalPos.y;       // get y camera coordinate
    }

    void FixedUpdate()
    {
        //searching for touches (can be reolased with first touch)
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                //if we just touch the screen make this touch initial
                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //swiping action
                float deltaX = initTouch.position.x - touch.position.x;
                float deltaY = initTouch.position.y - touch.position.y;
                posX += deltaX * Time.deltaTime * camSpeed * dir;
                posY += deltaY * Time.deltaTime * camSpeed * dir;
                cam.transform.position = new Vector3(posX, posY, -10);
                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // clear touch after it's diapeare
                initTouch = new Touch();
            }
        }
    }
}
