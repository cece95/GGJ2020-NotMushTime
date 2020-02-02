using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueStuff : MonoBehaviour
{
    public Text txt;
    public Image cannie;
    float timecounter = 0.0f;
    float timestore = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = "Mush: Oh no! everything is broken";
    }

    // Update is called once per frame
    void Update()
    {
        timecounter += Time.deltaTime;
        if(timecounter - timestore > 2.0f)
        {
            txt.text = "Shroom: we better fix it!";
        }
        if (timecounter - timestore > 4.0f)
        {
            txt.text = "Mush: Yeah, or we'll die!";
        }
        if (timecounter - timestore > 6.0f)
        {
            txt.gameObject.SetActive(false);
            cannie.gameObject.SetActive(false);
        }
    }
}
