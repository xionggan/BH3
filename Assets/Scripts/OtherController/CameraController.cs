using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraController : MonoBehaviour {

    public Transform player;

    public float offset;

    public Vector3 shakeDir = Vector3.one * 0.02f;

    public float shakeTime = 0.02f;

    public float rotateSpeed = 2f;

    private float currentTime = 0.0f;
    private float totalTime = 0.0f;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

        bool mouseLeftBtn = Input.GetMouseButton(0);

        if (mouseLeftBtn)
        {
            float xDelta = Input.GetAxis("Mouse X");

            float yDelta = -Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, xDelta * rotateSpeed, Space.World);

            transform.Rotate(transform.right, yDelta * rotateSpeed, Space.World);
        }
        else
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            Transform target = playerController.LockTarget;

            if (target)
            {
                Vector3 targetDir = target.position - transform.position;

                targetDir.y = 0;

                transform.forward = Vector3.MoveTowards(transform.forward, targetDir, Time.unscaledDeltaTime * 2);
            }
        }

        transform.position = (player.position + Vector3.up * 1.5f) - transform.forward * offset;

        UpdateShake();
	}


    public void StartShake()
    {
        totalTime = shakeTime;
        currentTime = shakeTime;
    }

    public void StopShake()
    {
        currentTime = 0.0f;
        totalTime = 0.0f;
    }

    public void UpdateShake()
    {
        if (currentTime > 0.0f && totalTime > 0.0f)
        {
            float percent = currentTime / totalTime;

            Vector3 shakePos = Vector3.zero;
            shakePos.x = UnityEngine.Random.Range(-Mathf.Abs(shakeDir.x) * percent, Mathf.Abs(shakeDir.x) * percent);
            shakePos.y = UnityEngine.Random.Range(-Mathf.Abs(shakeDir.y) * percent, Mathf.Abs(shakeDir.y) * percent);
            shakePos.z = UnityEngine.Random.Range(-Mathf.Abs(shakeDir.z) * percent, Mathf.Abs(shakeDir.z) * percent);

            Camera.main.transform.position += shakePos;

            currentTime -= Time.unscaledDeltaTime;
        }
        else
        {
            currentTime = 0.0f;
            totalTime = 0.0f;
        }
    }
 
}
