using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarMovement[] carMove;
    public bool isActive = false;
    bool change = false;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (carMove[0].isActive) return;
            for(int i = 0; i<carMove.Length; i++)
            {
                carMove[i].isActive = true;
            }

        }
        else if(!isActive)
        {
            if (!carMove[0].isActive) return;
            for (int i = 0; i < carMove.Length; i++)
            {
                carMove[i].isActive = false;
            }
        }


    }
}
