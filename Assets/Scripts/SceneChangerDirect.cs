using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerDirect : MonoBehaviour
{
    [SerializeField] private string _levelName;

    public void GoLevel()
    {
        SceneManager.LoadScene(_levelName);
    }
}
