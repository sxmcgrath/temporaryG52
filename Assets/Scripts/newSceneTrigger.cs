using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class newSceneTrigger : MonoBehaviour
{
    public int nextSceneNum;
    void onTriggerEnter(Collider other){
        SceneManager.LoadScene(nextSceneNum);
    }
}
