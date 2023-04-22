using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneChange : MonoBehaviour
{
    [SerializeField] StartSceneDirector director;
    [SerializeField] string text;
    [SerializeField] string toScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            director.toScene = toScene;
            director.ShowUI(text);
        }
    }
}
