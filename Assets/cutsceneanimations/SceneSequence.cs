using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject cutsceneCam;
    public GameObject playerCam;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TheSequence());   
    }

    IEnumerator TheSequence(){
        yield return new WaitForSeconds(6);
        playerCam.SetActive(true);
        cutsceneCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
