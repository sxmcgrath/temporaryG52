using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level_text : MonoBehaviour
{


    public static int currentLevel = 1;
    // Start is called before the first frame update
    Text str_currentLevel;
    void Start()
    {
        str_currentLevel = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        str_currentLevel.text = "" + currentLevel;
    }

    public void levelUp()
    {
        currentLevel = currentLevel + 1;
    }


    public void levelDown()
    {
        currentLevel = currentLevel - 1;
    }
}
