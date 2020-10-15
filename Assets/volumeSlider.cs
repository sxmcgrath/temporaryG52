using UnityEngine;


// This function is learned and adpated from here: https://www.youtube.com/watch?v=QZDw8ycoLRw
public class volumeSlider : MonoBehaviour
{


    private AudioSource audioBG;


    private float volumeBG = 1f;

    void Start()
    {
        audioBG = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioBG.volume = volumeBG;
    }


    public void SetVolume(float volIn)
    {
        volumeBG = volIn;
    }
}