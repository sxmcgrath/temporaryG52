using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class newSceneTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
       if(other.gameObject.tag=="Player")
 {
  int y = SceneManager.GetActiveScene().buildIndex;
  SceneManager.LoadScene(y+1);
 }
     }
}
