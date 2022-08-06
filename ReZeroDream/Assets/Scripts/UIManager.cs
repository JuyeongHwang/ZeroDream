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



    [Header("Player")]
    [SerializeField] private UIView playerClickImage;
    [SerializeField] private UIView playerLiftImage;


    [Header("Dialogue")]
    [SerializeField] private UIView dialogueWindow;
    [SerializeField] private UIView dialoguePortraitImg;
    [SerializeField] private UIView dialogueObjectNameText;
    [SerializeField] private UIView dialogueTalkText;
    [SerializeField] private Animator animator;


    [Header("Story - Cat")]
    [SerializeField] public UIView catNameWindow;
    [SerializeField] private UIView InputCatName;
    [SerializeField] private CanvasGroup canvasGroup;


    [Header("Quest Min")]
    //[SerializeField] private Image[] questStateArr;
    [SerializeField] private UIView questMinWindow;
    [SerializeField] private UIView questMinText;
    [Header("Quest Max")]
    [SerializeField] private UIView questMaxWindow;
    [SerializeField] private UIView questMaxText;
    [SerializeField] private UIView questMaxDescriptText;
    [SerializeField] private UIView questMaxContentText;
    [SerializeField] private UIView questWindowRewardText;
    [SerializeField] private UIView questMinButton;
    [SerializeField] private UIView questMaxButton;

    private QuestManager questManager;


    private void Start()
    {
        catNameWindow.Hide();


        questManager = FindObjectOfType<QuestManager>();
        SetFirstQuestUI(); //퀘스트UI초기화 (원래는 대화 끝날 때만 불리니까)
    }

    // *****     [Header("Player")]     *******
    public void OnOffPlayerClickImage(bool isActive)
    {
        playerClickImage.ShowAndHide(isActive);
    }

    public void OnOffPlayerLiftImage(bool isActive)
    {
        playerLiftImage.ShowAndHide(isActive);
    }

    // *****     [Header("Dialogue")]     *******
    private bool isFirstDialogue = true;
    public void OnOffDialogueWindow(bool active) { 
        
        if(active)
        {
            dialogueWindow.Show();
            if(isFirstDialogue)
            {
                animator.SetTrigger("show");
                isFirstDialogue = false;
            }

        }
        else
        {
            //dialogueWindow.Hide();
            animator.SetTrigger("hide");
            Debug.Log("off");
            isFirstDialogue = true;
        }

        //dialogueWindow.ShowAndHide(active);

    }

    public void UpdateDialogeText(string name, string talk)
    {
        dialogueTalkText.UpdateTextEffect(talk);
        dialogueObjectNameText.UpdateTextMeshProUGUI(name);
    }

    public void UpdateDialoguePortraitImage(Sprite portrait)
    {
        dialoguePortraitImg.UpdateImage(portrait);
        dialoguePortraitImg.UpdateColor(new Color(1, 1, 1, 1));
    }


    // *****     [Header("CatName")]     *******
    public void OnOffCatNameWindow(bool active) 
    {
        if (active)
        {
            catNameWindow.Show();
            StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 1));
        }
        else
        {
            StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 0));
        }
        //catNameWindow.ShowAndHide(active); 
    
    }

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;
        while(counter < 0.4f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / 0.4f);
            yield return null;
        }
    }


    /// ============== 수정 중 =====================
    /// 
    public void OnQuestMaxOffQuestMinWindow()
    {
        questMinWindow.Hide();
        questMaxWindow.Show();
        questMinButton.Show();
        questMaxButton.Hide();
    }

    public void OffQuestMaxOnQuestMinWindow()
    {
        questMaxWindow.Hide();
        questMinWindow.Show();
        questMaxButton.Show();
        questMinButton.Hide();
    }

    private void SetFirstQuestUI()
    {
        //isQuest = false;
        questMinWindow.Show();
        questMaxWindow.Hide();

        questMaxButton.Show();
        questMinButton.Hide();
        questMinText.UpdateTextMeshProUGUI("???와 대화하기");
        questMaxText.UpdateTextMeshProUGUI("???와 대화하기");
        questMaxDescriptText.UpdateTextMeshProUGUI("???에게 대화를 걸어 이곳에 대한 정보를 얻어보자.");
        questMaxContentText.UpdateTextMeshProUGUI("???와 대화하기");
        questWindowRewardText.UpdateTextMeshProUGUI(" ");
    }

    //여기서 만들고 DialogueManager에서 호출 (CheckQuest에서 npcId > questId)
    public void UpdateQuestUI(int npcId)
    {
        string npc = questManager.CheckQuest(npcId);

        questMinText.UpdateTextMeshProUGUI(npc);
        questMaxText.UpdateTextMeshProUGUI(npc);
        questMaxDescriptText.UpdateTextMeshProUGUI(questManager.qwDescript);
        questMaxContentText.UpdateTextMeshProUGUI(questManager.qwContent);
        questWindowRewardText.UpdateTextMeshProUGUI(questManager.qwReward);
    }



    //애매..
    public void UpdateCatName() {
        string catName = catNameWindow.getTextMeshProInputField();
        GameObject cat = GameObject.Find("NPC_Cat");
        cat.GetComponent<ObjData>()._name = catName;
        FindObjectOfType<DialogueManager>().Action(cat);
    }

    public void OnEndEdit(string str)
    {
        print("on End edit");
        string catName = catNameWindow.getTextMeshProInputField();
        catName = InputCatName.changeKoreanText(catName);

        GameObject cat = GameObject.Find("NPC_Cat");
        cat.GetComponent<ObjData>()._name = catName;
        FindObjectOfType<DialogueManager>().Action(cat);

        OnOffCatNameWindow(false);
        GameManager.instance.SetGameStateToDialogue();
    }

}
