using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLand : MonoBehaviour
{
    public Transform origianl;
    public Transform breakMesh;

    bool startBreak = true;
    public bool start = false;

    float destroyTime = 0.0f;
    private void Update()
    {
        if (start)
        {
            destroyTime += Time.deltaTime;
            if (startBreak)
            {
                GameManager.instance.SetUserStateToFloating();
                GameManager.instance.SetGameStateToStory();
                Physics.gravity = new Vector3(0, -5.5f, 0);

                origianl.gameObject.SetActive(false);
                for (int i = 0; i < breakMesh.childCount; i++)
                {
                    breakMesh.GetChild(i).gameObject.SetActive(true);
                }
                startBreak = false;
            }

        }

        if (destroyTime > 6.5f)
        {
            for (int i = 0; i < breakMesh.childCount; i++)
            {
                Destroy(breakMesh.GetChild(i).gameObject);

            }
        }
    }
}
