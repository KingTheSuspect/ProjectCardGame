using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanelObject;
    [SerializeField] private GameObject _roleSelectionPanelObject;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void Play()
    {
        _mainPanelObject.SetActive(false);
        _roleSelectionPanelObject.SetActive(true);
    }
}
