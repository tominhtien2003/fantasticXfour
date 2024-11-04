using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private List<GameObject> listCamera = new List<GameObject>();
    private int currentIndex = 0;

    private void Start()
    {
        CinemachineVirtualCamera[] childCameras = GetComponentsInChildren<CinemachineVirtualCamera>(true);
        foreach (var cam in childCameras)
        {
            listCamera.Add(cam.gameObject);
        }

        for (int i = 0; i < listCamera.Count; i++)
        {
            listCamera[i].SetActive(i == currentIndex);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            listCamera[currentIndex].SetActive(false);

            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = listCamera.Count - 1;
            }

            listCamera[currentIndex].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            listCamera[currentIndex].SetActive(false);

            currentIndex++;
            if (currentIndex >= listCamera.Count)
            {
                currentIndex = 0;
            }

            listCamera[currentIndex].SetActive(true);
        }
    }
}
