
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Vector3 rotation;
    public GameObject saliva;
    // Start is called before the first frame update
    void Start()
    {
        rotation = saliva.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            var ps = saliva.GetComponent<ParticleSystem>();
            ps.Play();

            saliva.transform.eulerAngles = new Vector3(90,90,90);


        }
    }
}
