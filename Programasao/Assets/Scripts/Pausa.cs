using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour
{

    Canvas canvasPausa;
    bool boolPausa;
    Frog scriptFrog;

    // Start is called before the first frame update
    void Start()
    {
        canvasPausa = GetComponent<Canvas>();
        boolPausa = false;
        canvasPausa.enabled = false;

        scriptFrog = GameObject.FindGameObjectWithTag("Frog").GetComponent<Frog>();
        scriptFrog.enabled = true;

    }

    // Update is called once per frame 
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            boolPausa = !boolPausa;
            scriptFrog.enabled = !scriptFrog.enabled;
        }

        canvasPausa.enabled = boolPausa;

        Time.timeScale = boolPausa ? 0 : 1;


    }
}
