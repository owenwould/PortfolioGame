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
    int force = 20;




   

    void Start()
    {
        cameraTran = GetComponent<Transform>();
        fLeftBoundary = leftCameraBoundaryTran.position.x;
        fRightBoundary = rightCameraBoundaryTran.position.x;
        cameraVec = new Vector3(cameraTran.position.x, 0, 0);
        resetVec = new Vector3(0, cameraTran.position.y, cameraTran.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.mainMenuMode || GameManager.gameover)
            return;
        

        if (cameraTran.position.x > fRightBoundary || cameraTran.position.x < fLeftBoundary)
        {
            resetVec.x = Mathf.Clamp(cameraTran.position.x, fLeftBoundary, fRightBoundary);
            cameraTran.position = resetVec;
        }


        if (Input.GetAxis("Mouse X") > 0)
        {
            movement();

        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            movement();

        }
    }

    void movement()
    {
        float xVal = Input.GetAxis("Mouse X");
        cameraVec.x = xVal;
        cameraTran.position += cameraVec * Time.deltaTime * force;
    }
}
