using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager_Cut1 : MonoBehaviour
{
    [SerializeField]
    private GameObject DialougeBox;
    [SerializeField]
    private float TimelineTime1;

    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private GameObject controlPanel;

    private IEnumerator startDialogue(){
        Debug.Log("time start");
        PlayerPrefs.SetInt("canRunMenu", 0);
        yield return new WaitForSeconds(TimelineTime1);
        Debug.Log("time over");
        PlayerPrefs.SetInt("canRunMenu", 1);
        DialougeBox.SetActive(true);
    }
    void Start()
    {
       // director.played += Director_Played;
       // director.stopped += Director_Stop;
       // StartCoroutine(startDialogue());
    }

    /*private void Director_Played(PlayableDirector obj){
        controlPanel.SetActive(false);
    }
    private void Director_Stopped(PlayableDirector obj){
        controlPanel.SetActive(true);
    }
    public void startTimeline(){
        director.Play();
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
