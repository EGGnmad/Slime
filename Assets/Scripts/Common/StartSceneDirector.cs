using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneDirector : MonoBehaviour
{
    [SerializeField] GameObject Text;
    [SerializeField] GameObject Ask;
    public string toScene;


    public void ChangeScene()
    {
        SceneManager.LoadScene(toScene);
    }

    public void ShowUI(string text)
    {
        Text.GetComponent<Text>().text = text;
        Ask.active = true;
    }

    public void DisableUI()
    {
        Text.GetComponent<Text>().text = "";
        Ask.active = false;
    }
}
