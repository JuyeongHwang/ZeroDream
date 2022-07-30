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
    //public bool isQuest;

    [Header("Player")]
    [SerializeField] private Image playerClickImage;
    [SerializeField] private Image playerLiftImage;


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

    [Header("Quest")]
    [SerializeField] private Image[] questStateArr;
    [SerializeField] private Image questImage;
    [SerializeField] private TextMeshProUGUI questNameText;
    [SerializeField] private Image questWindowImage;
    [SerializeField] private TextMeshProUGUI questWindowNameText;
    [SerializeField] private TextMeshProUGUI questWindowDescriptText;
    [SerializeField] private TextMeshProUGUI questWindowContentText;
    [SerializeField] private TextMeshProUGUI questWindowRewardText;
    [SerializeField] private GameObject qwOnButton;
    [SerializeField] private GameObject qwOffButton;

    private QuestManager questManager;

    private void Start()
    {
        textEffect = dialogueTalkText.GetComponent<TypeEffect>();

        catNameImage.SetActive(false);
        catName = catNameInput.GetComponent<TMP_InputField>().text;

        questManager = FindObjectOfType<QuestManager>();
        SetFirstQuestUI(); //퀘스트UI초기화 (원래는 대화 끝날 때만 불리니까)
    }

    public void SetPlayerClickImage(bool isActive)
    {
        playerClickImage.gameObject.SetActive(isActive);
    }
    public void SetPlayerLiftImage(bool isActive)
    {
        playerLiftImage.gameObject.SetActive(isActive);
    }


    private void SetFirstQuestUI()
    {
        //isQuest = false;
        questImage.gameObject.SetActive(true);
        questWindowImage.gameObject.SetActive(false);

        qwOnButton.SetActive(true);
        qwOffButton.SetActive(false);

        questNameText.text = "???와 대화하기";
        questWindowNameText.text = "???와 대화하기";
        questWindowDescriptText.text = "???에게 대화를 걸어 이곳에 대한 정보를 얻어보자.";
        questWindowContentText.text = "???와 대화하기";
        questWindowRewardText.text = " ";
    }

    //여기서 만들고 DialogueManager에서 호출 (CheckQuest에서 npcId > questId)
    public void SetQuestUI(int npcId)
    {
        //questNameText.text = questManager.CheckQuest(npcId);
        
        questWindowNameText.text = questManager.CheckQuest(npcId);
        questWindowDescriptText.text = questManager.qwDescript;
        questWindowContentText.text = questManager.qwContent;
        questWindowRewardText.text = questManager.qwReward;
    }

    public void OnQuestWindow()
    {
        qwOnButton.SetActive(false);
        questImage.gameObject.SetActive(false);
        questWindowImage.gameObject.SetActive(true);
        qwOffButton.SetActive(true);
    }

    public void OffQuestWindow()
    {
        questWindowImage.gameObject.SetActive(false);
        questImage.gameObject.SetActive(true);
        qwOnButton.SetActive(true);
        qwOffButton.SetActive(false);
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
    public void UpdateCatName(ObjData cat) { cat._name = catName; }

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
