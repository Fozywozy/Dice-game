using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextboxManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI TextObject => transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
    private Image ImageObject => transform.GetChild(1).GetComponent<Image>();

    [SerializeField]
    private Sprite Empty;

    [SerializeField]
    private Sprite Dice;
    [SerializeField]
    private Sprite DiceHappy;
    [SerializeField]
    private Sprite DiceSad;
    [SerializeField]
    private Sprite DiceAnnoyed;
    [SerializeField]
    private Sprite DiceFlatFace;
    [SerializeField]
    private Sprite DiceSurprised;

    [SerializeField]
    private Sprite Wind;
    [SerializeField]
    private Sprite WindHappy;
    [SerializeField]
    private Sprite WindSad;
    [SerializeField]
    private Sprite WindAnnoyed;
    [SerializeField]
    private Sprite WindFlatFace;
    [SerializeField]
    private Sprite WindSurprised;

    private string Text = "";

    private float TextStarted = 0;
    private float LastText = 0;
    private int TextIndex = 0;

    private bool TextUnloading = false;
    private float TextUnloadTime = 0;
    

    private Sprite ImageEnumToSprite(TextImage C_TextImage)
    {
        return C_TextImage switch
        {
            TextImage.Dice => Dice,
            TextImage.DiceHappy => DiceHappy,
            TextImage.DiceSad => DiceSad,
            TextImage.DiceAnnoyed => DiceAnnoyed,
            TextImage.DiceFlatFace => DiceFlatFace,
            TextImage.DiceSurprised => DiceSurprised,

            TextImage.Wind => Wind,
            TextImage.WindHappy => WindHappy,
            TextImage.WindSad => WindSad,
            TextImage.WindAnnoyed => WindAnnoyed,
            TextImage.WindFlatFace => WindFlatFace,
            TextImage.WindSurprised => WindSurprised,
            _ => Empty,
        };
    }

    public void UpdateVisuals(string C_Text, TextImage C_TextImage)
    {
        ImageObject.sprite = ImageEnumToSprite(C_TextImage);
        TextStarted = Time.fixedUnscaledTime;
        Text = C_Text;
        TextIndex = -3;
        TextUnloading = false;
        LastText = 0;
    }

    public void Update()
    {
        LastText += Time.fixedUnscaledDeltaTime;
        if (!TextUnloading)
        {
            while (LastText > 0.05f)
            {
                LastText -= 0.05f;
                TextIndex++;
                if (TextIndex >= Text.Length)
                {
                    TextIndex = Text.Length - 1;
                    TextUnloadTime = Time.fixedUnscaledTime;
                    TextUnloading = true;
                }
            }

            if (TextIndex > 0)
            {
                TextObject.text = Text.Substring(0, TextIndex);
            }

            if (Time.fixedUnscaledTime - TextStarted < 0.25f)
            {
                transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1, 4 * (Time.fixedUnscaledTime - TextStarted), 1);
            }
            else
            {
                transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
        else if (Time.fixedUnscaledTime - TextUnloadTime > 1)
        {
            if (Time.fixedUnscaledTime - TextUnloadTime > 1.25f)
            {
                TextObject.text = "";
                transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1, 0, 1);
                return;
            }

            transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1, 1 - (4 * (Time.fixedUnscaledTime - TextUnloadTime - 1)), 1);
        }
    }
}
