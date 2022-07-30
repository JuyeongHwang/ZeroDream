using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{

    PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        UIManager.instance.SetPlayerClickImage( (playerInput.scanObject ? true : false)  );
    }
}
