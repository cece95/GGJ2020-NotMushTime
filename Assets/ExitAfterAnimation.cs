using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAfterAnimation : MonoBehaviour




{

    public GameObject myObject;
    bool animationIsComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void AnimationComplete()
    {
        SceneManager.LoadScene("main");
    }
}
