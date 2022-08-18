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

    [Header("Canvas")]
    [SerializeField] private UIView SettingButtonsCavas;
    [SerializeField] private UIView Canvas;

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
    [SerializeField] private Animator questAnimator;

    [Header("Memory")]
    [SerializeField] private UIView NotePadWindow;
    [SerializeField] private UIView HuiNote;
    [SerializeField] private UIView HuiCatText;
    [SerializeField] private UIView HuiFlowerText;
    [SerializeField] private UIView EnjoyNote;


    private QuestManager questManager;

    public void HideAllCanvas(float speed)
    {
        Canvas.FadeOut(speed);

        //SettingButtonsCavas.Hide();
        //SettingButtonsCavas.FadeOut(1);
    }

    public void ShowAllCanvas()
    {
        Canvas.FadeIn(1);
        //SettingButtonsCavas.Show();
        //SettingButtonsCavas.FadeIn(1);
    }
    private void Start()
    {
        //initialize
        catNameWindow.Hide();
        HuiNote.Hide();

        questManager = FindObjectOfType<QuestManager>();
        SetFirstQuestUI(); //퀘스트UI초기화 (원래는 대화 끝날 때만 불리니까)
    }

    private void Update()
    {

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

        if (active)
        {
            dialogueWindow.Show();
            if (isFirstDialogue)
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
            catNameWindow.FadeIn(1);
        }
        else
        {
            catNameWindow.Hide();
        }

    }

    // *****     [Header("Memory")]     *******

    public void OnOffHuiNote(bool isActive)
    {
        HuiNote.ShowAndHide(isActive);
    }

    public void UpdateHuiCatText(string catName)
    {
        HuiCatText.UpdateTextMeshProUGUI(catName);
    }

    public void UpdateHuiFlowerText()
    {
        HuiFlowerText.UpdateTextMeshProUGUI("민들레");
    }

    public void OnOffEnjoyNote(bool isActive)
    {
        EnjoyNote.ShowAndHide(isActive);
    }


    // *****     [Header("")]     *******
    public void HideStoryMode()
    {
        Canvas.FadeOut(1);
        SettingButtonsCavas.FadeOut(1);
    }
    public void ShowStoryMode()
    {
        Canvas.FadeIn(1);
        SettingButtonsCavas.FadeIn(1);
    }


    public void OnQuestMaxOffQuestMinWindow()
    {
        questMinWindow.Hide();
        questMaxWindow.Show();
        questMinButton.Show();
        questMaxButton.Hide();
        questAnimator.SetTrigger("Max");
    }

    public void OffQuestMaxOnQuestMinWindow()
    {
        questAnimator.SetTrigger("Min");
        Invoke("InvokeOffQuestMaxOnQuestMinWindow", 0.3f);
    }

    public void InvokeOffQuestMaxOnQuestMinWindow()
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




    public void OnEndEdit(string str)
    {
        string catName = catNameWindow.getTextMeshProInputField();
        catName = InputCatName.changeKoreanText(catName);

        UpdateHuiCatText(catName);

        GameObject cat = GameObject.Find("NPC_Cat");
        cat.GetComponent<ObjData>()._name = catName;

        OnOffCatNameWindow(false);
        GameManager.instance.SetGameStateToDialogue();
    }

}
