using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuUIManager : MonoBehaviour
{
    public Image mainBtnPanel;

    public Image loginPanel;

    public Image registerPanel;

    public Image mainPanel;

    public Image levelSelectPanel;

    public Text userNameText;

    //mainBtn
    public Button mainRegisterBtn;

    public Button mainLoginBtn;

    public Button mainGuestBtn;

    //login and register
    public Button gotoRegisterBtn;

    public Button loginBtn;

    public InputField loginUsernameText;

    public InputField loginPasswordText;

    public Text loginTipText;

    public Button backLoginBtn;

    public Button registerBtn;

    public InputField registerUsernameText;

    public InputField registerPasswordText;

    public InputField registerConfirmpasswordText;

    public Text registerTipText;

    //selectLevelPanle
    public Button backBtn;

    public Button BtnLevel1_1;

    public Button BtnLevel1_10;

    public ElevatorController elevatorController;

    public GameObject spaceShip;

    public Animator playerAnimator;

    public AudioManager startAudio;

    public AudioManager loginAudio;

    public Image imageMask;

    private Client client;

	void Start ()
    {
        if (ApplicationConstant.hasLogin)
        {
            elevatorController.gameObject.SetActive(false);

            mainBtnPanel.gameObject.SetActive(false);

            spaceShip.SetActive(true);

            mainPanel.gameObject.SetActive(true);

            spaceShip.transform.position += Vector3.up * -4;

            Camera.main.transform.position += Vector3.forward * -2.3f;

            playerAnimator.Play(AnimaConstant.StandByNoWeapon);

            playerAnimator.transform.position += Vector3.forward * 0.6f;

            imageMask.color = Color.clear;

            loginAudio.Play(3,true);
        }
        else
        {
            StartCoroutine(FadeToUI());

            startAudio.PlayAllAudiosByOrder();
        }

        AddEventTrigger(loginPanel.transform, EventTriggerType.PointerClick, OnLoginCanvasClick);

        AddEventTrigger(registerPanel.transform, EventTriggerType.PointerClick, OnRegisterCanvasClick);

        client = GameObject.Find("Client").GetComponent<Client>();
	}

    private IEnumerator FadeToUI()
    {
        yield return new WaitForSeconds(3f);
        imageMask.DOFade(0, 3);
    }

    //main btn
    public void OnMainGuestButtonClick()
    {
        elevatorController.animator.SetTrigger(AnimaConstant.LoginSuccess);
        mainBtnPanel.gameObject.SetActive(false);
        ApplicationConstant.hasLogin = true;
        userNameText.text = "游客";
        StartCoroutine(PlayLoginAudio());
    }

    private IEnumerator PlayLoginAudio()
    {
        yield return new WaitForSeconds(0.2f);
        startAudio.Stop();
        loginAudio.PlayAllAudiosByOrder();
    }

    public void OnMainLoginButtonClick()
    {
        loginPanel.gameObject.SetActive(true);
    }

    public void OnMainRegisterButtonClick()
    {
        registerPanel.gameObject.SetActive(true);
    }

    //login and register
    public void OnGotoRegisterButtonClick()
    {
        loginPanel.gameObject.SetActive(false);
        registerPanel.gameObject.SetActive(true);
        ClearTextValue();
    }

    public void OnLoginButtonClick()
    {
        //检查输入的值是否正确
        if (string.IsNullOrEmpty(loginUsernameText.text))
        {
            loginTipText.text = "账号不能为空";
            StartCoroutine(ClearText());
            return;
        }
        else if (string.IsNullOrEmpty(loginPasswordText.text))
        {
            loginTipText.text = "密码不能为空";
            StartCoroutine(ClearText());
            return;
        }

        string message = ((int)NetMessageType.Login).ToString() + "_"
            + loginUsernameText.text + "_" + loginPasswordText.text;

        client.Send(message); 
    }

    public void OnLoginResult(string mess)
    {
        string[] tmpArray = mess.Split('_');
        int res = int.Parse(tmpArray[1]);
        if (res == 0)
        {
            loginTipText.text = "账号不存在或密码有误,请重试";
        }
        else if (res == 1)
        {
            loginTipText.text = "登陆成功";
            elevatorController.animator.SetTrigger(AnimaConstant.LoginSuccess);
            mainBtnPanel.gameObject.SetActive(false);
            loginPanel.gameObject.SetActive(false);
            ApplicationConstant.hasLogin = true;
            userNameText.text = loginUsernameText.text;
            StartCoroutine(PlayLoginAudio());
        }
    }

    public void OnBackLoginButtonClick()
    {
        registerPanel.gameObject.SetActive(false);
        loginPanel.gameObject.SetActive(true);
        ClearTextValue();
    }

    public void OnRegisterButtonClick()
    {
        //检查输入的值是否正确
        if (string.IsNullOrEmpty(registerUsernameText.text))
        {
            registerTipText.text = "账号不能为空";
            StartCoroutine(ClearText());
            return;
        }
        else if (string.IsNullOrEmpty(registerPasswordText.text))
        {
            registerTipText.text = "密码不能为空";
            StartCoroutine(ClearText());
            return;
        }
        else if (string.IsNullOrEmpty(registerConfirmpasswordText.text))
        {
            registerTipText.text = "确认密码不能为空";
            StartCoroutine(ClearText());
            return;
        }
        else if (registerPasswordText.text != registerConfirmpasswordText.text)
        {
            registerTipText.text = "两次输入密码不一致";
            StartCoroutine(ClearText());
            return;
        }

        string message = ((int)NetMessageType.Register).ToString() + "_"
            + registerUsernameText.text + "_" + registerPasswordText.text;

        client.Send(message);  
    }

    public void OnRegisterResult(string mess)
    {
        string[] tmpArray = mess.Split('_');
        int res = int.Parse(tmpArray[1]);
        if (res == 0)
        {
            registerTipText.text = "账号已存在，注册失败";
        }
        else if (res == 1)
        {
            registerTipText.text = "注册成功";
            StartCoroutine(BackToLogin());
        }
        StartCoroutine(ClearText());
    }

    private void ClearTextValue()
    {
        registerUsernameText.text = string.Empty;
        registerPasswordText.text = string.Empty;
        registerConfirmpasswordText.text = string.Empty;
        registerTipText.text = string.Empty;

        loginUsernameText.text = string.Empty;
        loginPasswordText.text = string.Empty;
        loginTipText.text = string.Empty;
    }

    private IEnumerator ClearText()
    {
        yield return new WaitForSeconds(1.5f);
        ClearTextValue();
    }
    private IEnumerator BackToLogin()
    {
        yield return new WaitForSeconds(1.5f);
        OnBackLoginButtonClick();
    }

    public void OnLoginCanvasClick(BaseEventData data)
    {
        loginPanel.gameObject.SetActive(false);
    }

    public void OnRegisterCanvasClick(BaseEventData data)
    {
        registerPanel.gameObject.SetActive(false);
    }

    //main menu
    public void OnAttackButtonClick()
    {
        mainPanel.gameObject.SetActive(false);

        levelSelectPanel.gameObject.SetActive(true);
    }

    //select level btn
    public void OnBackBtnClick()
    {
        mainPanel.gameObject.SetActive(true);

        levelSelectPanel.gameObject.SetActive(false);
    }

    public void OnBtn1_1Click()
    {
        SceneManager.LoadScene("1-1");
    }

    public void OnBtn1_10Click()
    {
        SceneManager.LoadScene("1-10");
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
