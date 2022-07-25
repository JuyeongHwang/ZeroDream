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

    [HideInInspector]
    public TypeEffect textEffect;


    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueImage;
    [SerializeField] private TextMeshProUGUI dialogueObjectNameText;
    [SerializeField] private TextMeshProUGUI dialogueTalkText;
    [SerializeField] private Image dialoguePortraitImg;

    [Header("Story - Cat")]
    [SerializeField] private TMP_InputField catNameInput;
    [SerializeField] public GameObject catNameImage;
    [SerializeField] public string catName = "";
    [SerializeField] private TextMeshProUGUI catText;

    private void Start()
    {
        textEffect = dialogueTalkText.GetComponent<TypeEffect>();

        catNameImage.SetActive(false);
        catName = catNameInput.GetComponent<TMP_InputField>().text;
    }


    public void SetActiveDialogueImage(bool active) { dialogueImage.SetActive(active); }

    public void UpdateDialogeText(string name, string talk)
    {
        textEffect.SetMsg(talk);
        dialogueObjectNameText.text = name;
    }

    public void UpdateDialoguePortraitImg(Sprite portrait)
    {
        dialoguePortraitImg.sprite = portrait;
        dialoguePortraitImg.color = new Color(1, 1, 1, 1);
    }



    public void SetActiveCatNameImage(bool active) { catNameImage.SetActive(active); }
    public void UpdateCatName(ObjData cat) { cat.name = catName; }

    public void OnValueChanedEvent(string str)
    {
        catText.text += " ";
    }
    public void OnEndEdit(string str)
    {
        int leng = catText.text.Length;

        if (catText.text[leng - 2] == '>' && catText.text[leng - 3] == 'u')
        {
            string temp = catText.text.Substring(0, leng - 5);
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == '<')
                {
                    if (temp[i + 1] == 'u')
                    {
                        catName += temp.Substring(0, temp.Length - 4);
                        catName += temp.Substring(temp.Length - 1);
                    }
                    break;
                }
            }
        }
        else
        {
            catName = catText.text;
        }
    }

}
