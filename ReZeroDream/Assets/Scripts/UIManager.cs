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

<<<<<<< HEAD

=======
    [HideInInspector]
    public TypeEffect textEffect;
    public bool isQuest;
>>>>>>> parent of cf825a5 (í€˜ìŠ¤íŠ¸ ë²„íŠ¼)

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

<<<<<<< HEAD

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
    //[SerializeField] private GameObject qwOnButton;
    //[SerializeField] private GameObject qwOffButton;
=======
    [Header("Quest")]
    [SerializeField] private Image[] questStateArr;
    [SerializeField] private GameObject questImage;
    [SerializeField] private TextMeshProUGUI questNameText;
    [SerializeField] private GameObject questWindowImage;
    [SerializeField] private TextMeshProUGUI questWindowNameText;
    [SerializeField] private TextMeshProUGUI questWindowDescriptText;
    [SerializeField] private TextMeshProUGUI questWindowContentText;
    [SerializeField] private TextMeshProUGUI questWindowRewardText;
>>>>>>> parent of cf825a5 (í€˜ìŠ¤íŠ¸ ë²„íŠ¼)

    private QuestManager questManager;


    private void Start()
    {
        catNameWindow.Hide();


        questManager = FindObjectOfType<QuestManager>();
        SetFirstQuestUI(); //Äù½ºÆ®UIÃÊ±âÈ­ (¿ø·¡´Â ´ëÈ­ ³¡³¯ ¶§¸¸ ºÒ¸®´Ï±î)
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


    /// ============== ¼öÁ¤ Áß =====================
    /// 
    public void OnQuestMaxOffQuestMinWindow()
    {
        questMinWindow.Hide();
        questMaxWindow.Show();
        //qwOffButton.SetActive(true);
        //qwOnButton.SetActive(false);
    }

    public void OffQuestMaxOnQuestMinWindow()
    {
        questMaxWindow.Hide();
        questMinWindow.Show();
        //qwOnButton.SetActive(true);
        //qwOffButton.SetActive(false);
    }

    private void SetFirstQuestUI()
    {
<<<<<<< HEAD
        //isQuest = false;
        questMinWindow.Show();
        questMaxWindow.Hide();

        //qwOnButton.SetActive(true);
        //qwOffButton.SetActive(false);
        questMinText.UpdateTextMeshProUGUI("???¿Í ´ëÈ­ÇÏ±â");
        questMaxText.UpdateTextMeshProUGUI("???¿Í ´ëÈ­ÇÏ±â");
        questMaxDescriptText.UpdateTextMeshProUGUI("???¿¡°Ô ´ëÈ­¸¦ °É¾î ÀÌ°÷¿¡ ´ëÇÑ Á¤º¸¸¦ ¾ò¾îº¸ÀÚ.");
        questMaxContentText.UpdateTextMeshProUGUI("???¿Í ´ëÈ­ÇÏ±â");
        questWindowRewardText.UpdateTextMeshProUGUI(" ");
=======
        isQuest = false;
        questImage.SetActive(true);
        questWindowImage.SetActive(false);
        questNameText.text = "???¿Í ´ëÈ­ÇÏ±â";
        questWindowNameText.text = "???¿Í ´ëÈ­ÇÏ±â";
        questWindowDescriptText.text = "???¿¡°Ô ´ëÈ­¸¦ °É¾î ÀÌ°÷¿¡ ´ëÇÑ Á¤º¸¸¦ ¾ò¾îº¸ÀÚ.";
        questWindowContentText.text = "???¿Í ´ëÈ­ÇÏ±â";
        questWindowRewardText.text = " ";
>>>>>>> parent of cf825a5 (í€˜ìŠ¤íŠ¸ ë²„íŠ¼)
    }

    //¿©±â¼­ ¸¸µé°í DialogueManager¿¡¼­ È£Ãâ (CheckQuest¿¡¼­ npcId > questId)
    public void UpdateQuestUI(int npcId)
    {
        string npc = questManager.CheckQuest(npcId);

<<<<<<< HEAD
        questMinText.UpdateTextMeshProUGUI(npc);
        questMaxText.UpdateTextMeshProUGUI(npc);
        questMaxDescriptText.UpdateTextMeshProUGUI(questManager.qwDescript);
        questMaxContentText.UpdateTextMeshProUGUI(questManager.qwContent);
        questWindowRewardText.UpdateTextMeshProUGUI(questManager.qwReward);
=======
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
>>>>>>> parent of cf825a5 (í€˜ìŠ¤íŠ¸ ë²„íŠ¼)
    }



    //¾Ö¸Å..
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
