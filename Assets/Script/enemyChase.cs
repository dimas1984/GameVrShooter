using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChase : MonoBehaviour
{

    [SerializeField] Transform theTarget;
    [SerializeField] float forwardSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChaseTarget();
        FaceTarget();       
    }

    void ChaseTarget() {
        this.transform.Translate(0, 0, 0.01f * forwardSpeed);
        //Debug.Log("Hello: " + gameObject.name);
    }

    void FaceTarget() {
        Vector3 targetDirection = theTarget.position - this.transform.position;
        targetDirection.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(targetDirection), 0.1f);
    }
}
