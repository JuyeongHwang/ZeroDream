using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLand : MonoBehaviour
{
    public Transform origianl;
    public Transform breakMesh;

    public bool start = true;

    float destroyTime = 0.0f;
    private void Update()
    {
        if (GameManager.instance.IsStoryStateEnjoy())
        {
            destroyTime += Time.deltaTime;
            if (start)
            {
                GameManager.instance.SetUserStateToFloating();
                GameManager.instance.SetGameStateToStory();
                Physics.gravity = new Vector3(0, -0.5f, 0);

                origianl.gameObject.SetActive(false);
                for (int i = 0; i < breakMesh.childCount; i++)
                {
                    breakMesh.GetChild(i).gameObject.SetActive(true);
                }
                start = false;
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
