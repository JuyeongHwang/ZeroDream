using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoveController : MonoBehaviour
{
    public BuildingMovement buildingMove;
    public bool isMove = false;
    AudioSource audio;
    public AudioClip clip;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (isMove)
        {
            if (buildingMove.isMove) return;
            {
                audio.Play();
                buildingMove.isMove = true;
            }
        }
        else if (!isMove)
        {
            if (!buildingMove.isMove) return;
            {
                audio.Stop();
                buildingMove.isMove = false;
            }
        }
    }
}
