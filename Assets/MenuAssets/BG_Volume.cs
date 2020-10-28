using UnityEngine;
using UnityEngine.UI;


// This function is learned and adpated from here: https://www.youtube.com/watch?v=QZDw8ycoLRw
public class BG_Volume : MonoBehaviour
{
    private AudioSource audioBG;

    private float volumeBG = 0.7f;

    void Start()
    {
        audioBG = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioBG.volume = volumeBG;
    }


    public void SetVolume(Slider volIn)
    {
        volumeBG = volIn.value;
    }
}