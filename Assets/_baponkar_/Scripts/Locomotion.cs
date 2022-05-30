using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    Animator animator;
    public float speed = 0.5f;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>(); 
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", speed);
    }
}
