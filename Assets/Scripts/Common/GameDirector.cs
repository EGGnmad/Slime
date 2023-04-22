using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject Text;
    [SerializeField] GameObject Board;


    public void PlayerDied()
    {
        print("Player Died");
        StartCoroutine(ChangeScene("StartScene", 2f));

        Text.GetComponent<Text>().color = Color.red;
        Text.GetComponent<Text>().text = "You Died!";
    }

    public void BossDied(string bossName)
    {
        print("Boss Died");

        if (bossName == "King")
        {
            PlayerPrefs.SetInt("KingClear", 1);

            Board.active = true;
            Text.GetComponent<Text>().color = Color.cyan;
            Text.GetComponent<Text>().text = "+ Ego Sword";
        }
        else if (bossName == "Troll King")
        {

            PlayerPrefs.SetInt("TrollkingClear", 1);

            Board.active = true;
            Text.GetComponent<Text>().color = Color.red;
            Text.GetComponent<Text>().text = "hp: 10(+5)";
        }


        StartCoroutine(ChangeScene("StartScene", 2f));
    }

    IEnumerator ChangeScene(string name, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(name);
    }
}