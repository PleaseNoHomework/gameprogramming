using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    public ParticleSystem particlesystem;
    public int flag;
    // Start is called before the first frame update
    void Start()
    {
        var main = particlesystem.main;

        switch (flag)
        {
            case 1: //blue
                main.startColor = Color.blue;
                break;
            case 2:
                main.startColor = Color.red;
                break;
            case 3:
                main.startColor = Color.green;
                break;
        }
    }

    // Update is called once per frame

}
