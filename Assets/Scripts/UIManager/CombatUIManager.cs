using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CombatUIManager : MonoBehaviour
{
    public Image imagePause;

    public Image imageScreenShot;

    public Slider sliderHp;

    public Slider sliderEnemyHp;

    public Slider sliderEnemyShield;

    public Text textTime;

    public Button giveUpBtn;

    public Button resumeBtn;

    public Button exitBtn;

    public Button againBtn;

    public Image imageMask; 

    public Image combatPanel;

    public Image pausePanel;

    public Image victoryPanel;

    public AudioManager audioSourceBg;

    private int curSecond;

    private int curMinute;

    [HideInInspector]
    public PlayerController playerController;

    [HideInInspector]
    public EnemyController enemyController;

	// Use this for initialization
	void Start ()
    {
        AddEventTrigger(imagePause.transform, EventTriggerType.PointerClick, OnPauseImageClick);
        AddEventTrigger(imageScreenShot.transform, EventTriggerType.PointerClick, OnScreenShotImageClick);
        giveUpBtn.onClick.AddListener(OnGiveUpBtnClick);
        resumeBtn.onClick.AddListener(OnResumeBtnClick);
        exitBtn.onClick.AddListener(OnExitBtnClick);
        againBtn.onClick.AddListener(OnAgainBtnClick);
        StartCoroutine(TimeTick());
	}

    private void MaskFinish()
    {
        combatPanel.gameObject.SetActive(false);

        victoryPanel.gameObject.SetActive(true);

        imageMask.DOFade(0, 0.8f);

        playerController.animator.SetTrigger(AnimaConstant.Victory);

        Transform startPoint = playerController.transform.Find("CameraPoint1");

        Transform endPoint = playerController.transform.Find("CameraPoint2");

        DestroyImmediate(Camera.main.GetComponent<CameraController>());

        Camera.main.transform.parent = playerController.transform;

        Camera.main.transform.localPosition = startPoint.localPosition;

        Camera.main.transform.localRotation = startPoint.localRotation;

        float durTime = 3;

        if(playerController is KianaController)
        {
            durTime = 2f;
        }

        Camera.main.transform.DOLocalMove(endPoint.localPosition, durTime).SetEase(Ease.OutSine);

        Camera.main.transform.DOLocalRotate(endPoint.localRotation.eulerAngles, durTime).SetEase(Ease.OutSine);

        audioSourceBg.Play(1, false);

        playerController.audioManager.Play(playerController.audioManager.audios.Length - 1, false);
    }

    public void PlayVictoryAnima(float deltaTime)
    {
        imageMask.DOFade(1, 0.8f).SetDelay(deltaTime).OnComplete(MaskFinish);
    }


    private IEnumerator TimeTick()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            curSecond += 1;
            if (curSecond == 60)
            {
                curSecond = 0;
                curMinute += 1;
            }
            textTime.text = string.Format("{0:D2}:{1:D2}", curMinute, curSecond);
        }
    }

    public void UpdatePlayerInfo()
    {
        sliderHp.value = (float)playerController.Hp / playerController.maxHp;
    }

    public void UpdateEnemyInfo()
    {
        if (enemyController && enemyController.Hp > 0)
        {
            sliderEnemyHp.gameObject.SetActive(true);

            sliderEnemyHp.value = (float)enemyController.Hp / enemyController.maxHp;

            if (enemyController.maxShieldCount > 0)
            {
                sliderEnemyShield.gameObject.SetActive(true);

                sliderEnemyShield.value = (float)enemyController.ShieldCount / enemyController.maxShieldCount;
            }
            else
            {
                sliderEnemyShield.gameObject.SetActive(false);
            }
        }
        else
        {
            sliderEnemyHp.gameObject.SetActive(false);
            sliderEnemyShield.gameObject.SetActive(false);
        }
    }

    void OnPauseImageClick(BaseEventData data)
    {
        pausePanel.gameObject.SetActive(true);
    }

    void OnScreenShotImageClick(BaseEventData data)
    {
        Debug.Log("OnScreenShotImageClick");
    }

    void OnGiveUpBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnResumeBtnClick()
    {
        pausePanel.gameObject.SetActive(false);
    }

    void OnExitBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnAgainBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AddEventTrigger(Transform uiTransform, EventTriggerType eventType,
        UnityAction<BaseEventData> callback)
    {
        EventTrigger trigger = uiTransform.gameObject.GetComponent<EventTrigger>();

        if (trigger == null)
        {
            trigger = uiTransform.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerClick;

        entry.callback = new EventTrigger.TriggerEvent();

        entry.callback.AddListener(callback);

        trigger.triggers.Add(entry);
    }
}
