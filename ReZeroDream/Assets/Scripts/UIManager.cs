using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    private static UIManager m_instance;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueImage;
    [SerializeField] private TextMeshProUGUI dialogueObjectNameText;
    [SerializeField] private TextMeshProUGUI dialogueTalkText;
    [SerializeField] private Image dialoguePortraitImg;

    public void SetActiveDialogueImage(bool active) { dialogueImage.SetActive(active); }

    public void UpdateDialogeText(string name, string talk)
    {
        dialogueObjectNameText.text = name;
        dialogueTalkText.text = talk;
    }

    public void UpdateDialoguePortraitImg(Sprite portrait)
    {
        dialoguePortraitImg.sprite = portrait;
        dialoguePortraitImg.color = new Color(1, 1, 1, 1);
    }








}
