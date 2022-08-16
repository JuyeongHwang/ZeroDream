using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIView : MonoBehaviour
{

    bool active = false;
    [HideInInspector] public Animator Dialogue;

    // ****** view hide show **********
    public void Show()
    {
        gameObject.SetActive(true);
        active = true;

    }

    public void Hide()
    {
        gameObject.SetActive(false);
        active = false;
    }

    public void ShowAndHide(bool isActive)
    {
        if (isActive)
        {
            Show();
        }
        else
        {
            Hide();
        }

    }

    public float GetAlphaValue()
    {
        return GetComponent<CanvasGroup>().alpha;
    }
    public void FadeOut(float speed)
    {
        StartCoroutine(DoFade(1,0, speed));
    }
    public void FadeIn(float speed)
    {
        StartCoroutine(DoFade(0, 1, speed));
    }
    IEnumerator DoFade(float start, float end, float speed)
    {
        float counter = 0f;
        while (counter < 0.4f)
        {
            counter += Time.deltaTime * speed;
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(start, end, counter / 0.4f);
            yield return null;
        }
    }

    // ****** update value **********
    public void UpdateImage(Sprite update)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.sprite = update;
        }
    }

    public void UpdateColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }

    public void UpdateTextMeshProUGUI(string update)
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = update;
        }
    }

    public void UpdateTextEffect(string talk)
    {
        TypeEffect textEffect = GetComponent<TypeEffect>();
        textEffect.SetMsg(talk);
    }

    // ****** get value **********
    public bool isActive()
    {
        return active;
    }

    public string getTextMeshProInputField()
    {
        return GetComponent<TMP_InputField>().text;
    }
    public string getTextMeshPro()
    {
        return GetComponent<TextMeshProUGUI>().text;
    }
    public int getTextMeshProLength()
    {
        return GetComponent<TextMeshProUGUI>().text.Length;
    }


    // ****** get value **********
    public string changeKoreanText(string text)
    {
        string inputText = getTextMeshPro();

        int leng = inputText.Length;

        if(leng < 3) return inputText;

        if (inputText[leng - 2] == '>' && inputText[leng - 3] == 'u')
        {
            string temp = inputText.Substring(0, leng - 5);
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == '<')
                {
                    if (temp[i + 1] == 'u')
                    {
                        text += temp.Substring(0, temp.Length - 4);
                        text += temp.Substring(temp.Length - 1);
                    }
                    break;
                }
            }
        }
        else
        {
            text = inputText;
        }


        return text;
    }

}
