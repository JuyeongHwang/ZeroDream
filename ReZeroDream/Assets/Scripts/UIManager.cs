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
    [SerializeField] public UIView dialogueObjectNameText;
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
    [SerializeField] private UIView EnjoyCarText;
    [SerializeField] private UIView EnjoyBurgerText;
    [SerializeField] private UIView WantNote;
    [Header("Caution")]
    [SerializeField] private UIView CautionWindow;
    [SerializeField] private UIView CautionText;
    [SerializeField] private UIView CautionCloseBtn;
    [HideInInspector] public bool activateCaution = false;
    private QuestManager questManager;

    public void HideAllCanvasNoFade()
    {
        Canvas.Hide();
        SettingButtonsCavas.Hide();
        CautionWindow.Hide();
        CautionCloseBtn.Hide();
    }

    public void ShowAndHideCautionWindow(bool active)
    {
        if (active)
        {
            GameManager.instance.SetGameStateToSetting();
            activateCaution = true;
            CautionWindow.Show();
            CautionCloseBtn.Show();
        }
        else
        {
            Time.timeScale = 1f;
            activateCaution = false;
            GameManager.instance.SetGameStateToPlay();
            CautionWindow.Hide();
            CautionCloseBtn.Hide();
        }
    }
    public void UpdateCautionText(string txt)
    {
        CautionText.UpdateTextMeshProUGUI(txt);
    }

    public void HideAllCanvas(float speed)
    {
        Canvas.FadeOut(speed);
        SettingButtonsCavas.FadeOut(speed);
        //SettingButtonsCavas.Hide();
        //
    }

    public void ShowAllCanvas()
    {
        Canvas.Show();
        Canvas.FadeIn(1);
        SettingButtonsCavas.Show();
        SettingButtonsCavas.FadeIn(1);
    }
    private void Start()
    {
        //initialize
        catNameWindow.Hide();
        HuiNote.Hide();

        questManager = FindObjectOfType<QuestManager>();

        questMinWindow.Hide();
        questMaxWindow.Hide();
    }

    public void HideQuestWindow()
    {
        questMaxWindow.Hide();
        questMinWindow.Hide();
    }
    public void ShowQuestWindow()
    {
        questMinWindow.Show();
        questMaxButton.Show();
        questMinWindow.FadeIn(1.0f);
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
    public void OnOffEnzoNote(bool isActive)
    {
        EnjoyNote.ShowAndHide(isActive);
    }
    public void OnOffWantNote(bool isActive)
    {
        WantNote.ShowAndHide(isActive);
    }
    public void UpdateHuiCatText(string catName)
    {
        HuiCatText.UpdateTextMeshProUGUI(catName);
    }

    public void UpdateHuiFlowerText()
    {
        HuiFlowerText.UpdateTextMeshProUGUI("민들레");
    }

    public void UpdateEnjoyCarText()
    {
        EnjoyCarText.UpdateTextMeshProUGUI("가족과의\n놀이공원");
    }
    public void UpdateEnjoyBurgerText()
    {
        EnjoyBurgerText.UpdateTextMeshProUGUI("방과후\n햄버거집");
    }

    //public void OnOffEnjoyNote(bool isActive)
    //{
    //    EnjoyNote.ShowAndHide(isActive);
    //}


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
