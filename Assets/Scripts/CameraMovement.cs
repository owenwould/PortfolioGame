using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform leftCameraBoundaryTran, rightCameraBoundaryTran;
    Transform cameraTran;
    float fLeftBoundary, fRightBoundary;
    Vector3 cameraVec;
    Vector3 resetVec;
    float force = 1f;
    float defaultZ, defaultY;
    Vector3 destination;
    bool camMove;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTran = GetComponent<Transform>();
        fLeftBoundary = leftCameraBoundaryTran.position.x;
        fRightBoundary = rightCameraBoundaryTran.position.x;
        cameraVec = new Vector3(cameraTran.position.x, 0, 0);
        resetVec = new Vector3(0, cameraTran.position.y, cameraTran.position.z);
        destination = new Vector3(0, 0, 0);
        camMove = false;

        defaultY = cameraTran.position.y;
        defaultZ = cameraTran.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        if (camMove)
        {
            float step = 12f * Time.deltaTime;
            cameraTran.position = Vector3.MoveTowards(cameraTran.position, destination, step);
           
            if (Vector3.Distance(cameraTran.position, destination) < 0.1f)
                camMove = false;
           
        }


        if (GameManager.mainMenuMode || GameManager.gameover)
            return;
        //Either Arrow keys or AD
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movement(false);
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movement(true);
       
    }

    void movement(bool forwards)
    {
        if (forwards)
            cameraTran.position -= cameraVec * Time.deltaTime * force;
        else
            cameraTran.position += cameraVec * Time.deltaTime * force;


        if (cameraTran.position.x > fRightBoundary || cameraTran.position.x < fLeftBoundary)
        {
            resetVec.x = Mathf.Clamp(cameraTran.position.x, fLeftBoundary, fRightBoundary);
            cameraTran.position = resetVec;
        }

       

    }

   public void setCameraPos(Vector3 position)
   {
        camMove = true;
        destination = position;
        destination.y = defaultY;
        destination.z = defaultZ;
   }
}
