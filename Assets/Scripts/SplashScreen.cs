using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    private GameObject Splashscreen => GameObject.FindGameObjectWithTag("Splash");

    void Start()
    {
        TimerManager.NewTimer("SplashFadeIn", 0.3f, 7);
        TimerManager.NewTimer("TextFadeIn", 0.3f, 7.2f);
        TimerManager.NewTimer("SplashFadeOut", 0.3f, 8.7f);
        TimerManager.NewTimer("BlackroundFadeOut", 0.3f, 9.7f);
    }

    void Update()
    {
        Splashscreen.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1, TimerManager.GetTimer("TextFadeIn"));
        Splashscreen.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, TimerManager.GetTimer("SplashFadeIn"));

        if (TimerManager.GetTimer("SplashFadeOut").Active)
        {
            Splashscreen.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1, 1 - TimerManager.GetTimer("TextFadeIn"));
            Splashscreen.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1 - TimerManager.GetTimer("SplashFadeOut"));
        }

        Splashscreen.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1 - TimerManager.GetTimer("BlackroundFadeOut"));

        if (TimerManager.GetTimer("BlackroundFadeOut"))
        {
            //Splash Completed
            Splashscreen.SetActive(false);
            TimerManager.RemoveTimer("SplashFadeIn");
            TimerManager.RemoveTimer("TextFadeIn");
            TimerManager.RemoveTimer("SplashFadeOut");
            TimerManager.RemoveTimer("BlackroundFadeOut");
        }
    }
}
