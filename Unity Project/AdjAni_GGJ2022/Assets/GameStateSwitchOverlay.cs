using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateSwitchOverlay : MonoBehaviour
{
    public Image BlackScreen, GameStateSwitchImage, GameStateSwitchBG;
    public float FadeInTime, FadeOutTime, StayTime;
    // Start is called before the first frame update
    void Start()
    {
        BlackScreen.canvasRenderer.SetAlpha(0);
        GameStateSwitchImage.canvasRenderer.SetAlpha(0);
        GameStateSwitchBG.canvasRenderer.SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFade()
    {
        BlackScreen.gameObject.SetActive(true);
        GameStateSwitchImage.gameObject.SetActive(true);
        GameStateSwitchBG.gameObject.SetActive(true);
        Debug.Log("aaa");
        GameController.Instance.PlayerHasControl = false;
        BlackScreen.CrossFadeAlpha(1, FadeInTime / 2, false);
        StartCoroutine(CoroutineHelper.DelaySeconds(() =>
        {
            GameStateSwitchImage.CrossFadeAlpha(1, FadeInTime / 2, false);
            GameStateSwitchBG.CrossFadeAlpha(1, FadeInTime / 2, false);
        }, FadeInTime / 2));
        StartCoroutine(CoroutineHelper.DelaySeconds(() =>
        {
            GameController.Instance.SwitchToOwnerState();
        }, FadeInTime));
        
        StartCoroutine(CoroutineHelper.DelaySeconds(() =>
        {
            GameStateSwitchImage.CrossFadeAlpha(0, FadeOutTime / 2, false);
            GameStateSwitchBG.CrossFadeAlpha(0, FadeOutTime / 2, false);
        }, FadeInTime + StayTime));
        StartCoroutine(CoroutineHelper.DelaySeconds(() =>
        {
            BlackScreen.CrossFadeAlpha(0, FadeOutTime / 2, false);
        }, FadeInTime + StayTime + FadeOutTime / 2));
        StartCoroutine(CoroutineHelper.DelaySeconds(() =>
        {
            GameController.Instance.PlayerHasControl = true;
            GameController.Instance.NoBrokenBreakablesCheck();
        }, FadeInTime + StayTime + FadeOutTime));
    }
}
