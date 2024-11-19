using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TankGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject introScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject player_1;
    [SerializeField]
    private GameObject enemy_1;

    [SerializeField]
    private TMP_Text countText;

    private bool countStart = false;


    private IEnumerator CountDown(){
        countStart = true;
        introScreen.SetActive(false);
        countText.text = "3";
        yield return new WaitForSeconds(1f);
        countText.text = "2";
        yield return new WaitForSeconds(1f);
        countText.text = "1";
        yield return new WaitForSeconds(1f);
        player_1.SetActive(false);
        enemy_1.SetActive(false);
        player.SetActive(true);
        enemy.SetActive(true);
        countText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countText.text = "";
    }

    private void Start(){
        introScreen.SetActive(true);
    }

    private void Update(){
        if(!countStart && Input.GetKeyDown(KeyCode.Return)){
            countStart = true;
            StartCoroutine(CountDown());
        }
    }

}
