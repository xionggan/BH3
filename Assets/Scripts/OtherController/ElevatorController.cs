using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorController : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;

    public GameObject spaceShip;

    public GameObject bg1;

    public GameObject bg2;

    public GameObject bg3;

    public GameObject pillar;

    public Material leftShadow;

    public Material rightShadow;

    public MainMenuUIManager UIManager;

	void Start ()
    {
        animator = GetComponent<Animator>();
        Color shadowColor = new Color(1,1,1,0.15f);
        leftShadow.color = shadowColor;
        rightShadow.color = shadowColor;
	}

    public void OnDoorOpenFinish()
    {
        Camera.main.transform.DOMove(new Vector3(0, 0, -2.3f), 2).SetRelative().OnComplete(DistoryElevator);
    }

    public void LoadSpaceShip()
    {
        spaceShip.SetActive(true);
        bg1.SetActive(false);
        bg2.SetActive(false);
        bg3.SetActive(false);
        pillar.SetActive(false);
        leftShadow.DOColor(new Color(1,1,1,0),0.5f);
        rightShadow.DOColor(new Color(1, 1, 1, 0), 0.5f);
        spaceShip.transform.DOMove(new Vector3(0, -4f, 0), 0.75f).SetRelative();
    }

    public void OpenDoor()
    {
        animator.SetTrigger(AnimaConstant.OpenDoor);
    }

    public void DistoryElevator()
    {
        UIManager.mainPanel.gameObject.SetActive(true);
        GameObject.Destroy(this.gameObject);
    }
}
