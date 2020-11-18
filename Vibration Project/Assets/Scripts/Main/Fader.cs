using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    /*
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InstantateFader()
    {
        fader = Instantiate(Resources.Load<GameObject>("UI/FadeCanvas"));
        faderBlack = Instantiate(Resources.Load<GameObject>("UI/FadeCanvasBlack"));

        fader.GetComponent<FadeCanvas>().faderImg.raycastTarget = false;
        faderBlack.GetComponent<FadeCanvas>().faderImg.raycastTarget = false;

        DontDestroyOnLoad(fader);
        DontDestroyOnLoad(faderBlack);
    }

    private static GameObject fader;
    private static GameObject faderBlack;
    */
    static public void FadeOut(float time)
    {
        GameObject fader = Instantiate(Resources.Load<GameObject>("FadeCanvas"));
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        fader.GetComponent<Canvas>().worldCamera = cam;
        Canvas faderCanvas = fader.GetComponent<Canvas>();
        faderCanvas.sortingOrder = 100;
        faderCanvas.planeDistance = 1;
        fader.GetComponent<FadeCanvas>().FadeOut(time);
    }

    static public void FadeOutBlack(float time)
    {
        GameObject faderBlack = Instantiate(Resources.Load<GameObject>("FadeCanvas"));
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        faderBlack.GetComponent<Canvas>().worldCamera = cam;
        Canvas faderBlackCanvas = faderBlack.GetComponent<Canvas>();
        faderBlackCanvas.sortingOrder = 100;
        faderBlackCanvas.planeDistance = 1;
        faderBlack.GetComponent<FadeCanvas>().FadeOut(time);
    }

    static public void FadeIn(float time,string sceneName,bool destory)
    {
        GameObject fader = Instantiate(Resources.Load<GameObject>("FadeCanvas"));
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        fader.GetComponent<Canvas>().worldCamera = cam;
        Canvas faderCanvas = fader.GetComponent<Canvas>();
        faderCanvas.sortingOrder = 100;
        faderCanvas.planeDistance = 1;
        fader.GetComponent<FadeCanvas>().FadeIn(time,sceneName);
    }

    static public void FadeIn(float time)
    {
        GameObject fader = Instantiate(Resources.Load<GameObject>("FadeCanvas"));
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        fader.GetComponent<Canvas>().worldCamera = cam;
        Canvas faderCanvas = fader.GetComponent<Canvas>();
        faderCanvas.sortingOrder = 100;
        faderCanvas.planeDistance = 1;
        fader.GetComponent<FadeCanvas>().FadeIn(time);
    }

    static public void FadeInBlack(float time, string sceneName)
    {
        GameObject faderBlack = Instantiate(Resources.Load<GameObject>("FadeCanvasBlack"));
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        faderBlack.GetComponent<Canvas>().worldCamera = cam;
        Canvas faderBlackCanvas = faderBlack.GetComponent<Canvas>();
        faderBlackCanvas.sortingOrder = 100;
        faderBlackCanvas.planeDistance = 1;
        faderBlack.GetComponent<FadeCanvas>().FadeIn(time, sceneName);
    }

    static public void FadeInBlack(float time)
    {
        GameObject faderBlack = Instantiate(Resources.Load<GameObject>("FadeCanvasBlack"));
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        faderBlack.GetComponent<Canvas>().worldCamera = cam;
        Canvas faderBlackCanvas = faderBlack.GetComponent<Canvas>();
        faderBlackCanvas.sortingOrder = 100;
        faderBlackCanvas.planeDistance = 1;
        faderBlack.GetComponent<FadeCanvas>().FadeIn(time);
    }

    static public void FadeInAndOut(float inTime,float waitTime,float outTime)
    {
        GameObject fader = Instantiate(Resources.Load<GameObject>("FadeCanvas"));
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        fader.GetComponent<Canvas>().worldCamera = cam;
        Canvas faderCanvas = fader.GetComponent<Canvas>();
        faderCanvas.sortingOrder = 100;
        faderCanvas.planeDistance = 1;
        fader.GetComponent<FadeCanvas>().FadeInAndOut(inTime, waitTime, outTime, faderCanvas);
    }

    static public void FadeInAndOutBlack(float inTime, float waitTime, float outTime)
    {
        GameObject faderBlack = Instantiate(Resources.Load<GameObject>("FadeCanvasBlack"));
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        faderBlack.GetComponent<Canvas>().worldCamera = cam;
        Canvas faderBlackCanvas = faderBlack.GetComponent<Canvas>();
        faderBlackCanvas.sortingOrder = 100;
        faderBlackCanvas.planeDistance = 1;
        faderBlack.GetComponent<FadeCanvas>().FadeInAndOut(inTime, waitTime, outTime, faderBlackCanvas);
    }
}
