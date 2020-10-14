using UnityEngine;
using System.Collections;

public class Particles : MonoBehaviour
{
    public ParticleSystem ps;
    public float hSliderValue = 1.0F;

    void Start()
    {
        

        // var psr = GetComponent<ParticleSystemRenderer>();
        // psr.material = new Material(Shader.Find("Sprites/Default"));    // this material renders a square billboard, so we can see the rotation
    }

    void Update()
    {
        var main = ps.main;
        main.startRotation = hSliderValue;
    }

    void OnGUI()
    {
        hSliderValue = GUI.HorizontalSlider(new Rect(25, 45, 100, 30), hSliderValue, 0.0F, 360.0F * Mathf.Deg2Rad);
    }
}