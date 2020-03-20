using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Transform targetTran;
    Transform unitTran;
    Vector3 targetVec;
    int force;
    bool bMoveFinished;
    void Start()
    {
        unitTran = GetComponent<Transform>();
        force = 5;
        float distance = 3;
        bMoveFinished = false;
        targetVec = new Vector3(targetTran.position.x, transform.position.y, transform.position.z);



    }

    // Update is called once per frame
    void Update()
    {

        if (!bMoveFinished)
        {
            float step = force * Time.deltaTime;
            unitTran.position = Vector3.MoveTowards(transform.position, targetVec, step);
        }
       

        if (!bMoveFinished)
        {
            if (Vector3.Distance(transform.position, targetTran.position) < 3f)
            {
                bMoveFinished = true;
            }
        }

       
        




       
    }
}
