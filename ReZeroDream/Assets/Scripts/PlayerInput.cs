using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //property

    //키보드 입력 & 물체 입력
    public float move { get; private set; }
    public float rotate { get; private set; }
    public bool run { get; private set; }
    public bool jump { get; private set; }    
    public bool lift { get; private set; }
    public bool Lclick { get; private set; }
    public bool enter { get; private set; }
    public GameObject scanObject { get; private set; } 

    void Update()
    {
        if (GameManager.instance.isGameover && GameManager.instance != null)
        {
            move = 0;
            rotate = 0;
            
            return;
        }

        
        move = Input.GetAxis("Vertical");
        rotate = Input.GetAxis("Horizontal");
        run = Input.GetButton("Run");
        jump = Input.GetButtonDown("Jump");
        lift = Input.GetButtonDown("Lift");
        Lclick = Input.GetMouseButtonDown(0);
        enter =  Input.GetButtonDown("Enter");
        scanObject = Ray();

    }

    GameObject Ray()
    {
        float maxDistance = 1.5f;
        RaycastHit Hit;
        if(Physics.SphereCast(transform.position, transform.localScale.x/1.5f,
            transform.forward, out Hit, maxDistance))
        {
            if(Hit.transform.tag == "RayObject")
            {
                return Hit.collider.gameObject;
            }   
        }
        return null;
    }

}
