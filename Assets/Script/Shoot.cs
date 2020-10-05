using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    // variables for the gun
    // serializeField digunakan untuk menampilkan variabel pada jendela inspektor baik private maupun public
    [SerializeField] new Camera camera; // reference the camera, needed for raycasting
    [SerializeField] int damage = 1; // set default damage to enemy
    [SerializeField] int range = 100; // range of raycast


    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // check input
        if (Input.GetButtonDown("Fire1")) 
        {
            FireWeapon();
        }
    }

    void FireWeapon() 
    {
        RaycastHit hit;

        // this will check if the raycast hit anything
        if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hit, range)) 
        {
            print("You hit" + hit.transform.name);
         }
    }
}
