using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{

    float tiempo;

    float minutos;
    float segundos;

    TextMeshProUGUI textTiempo;

    // Start is called before the first frame update
    void Start()
    {
        textTiempo = GameObject.Find("TextoTemporizador").GetComponent<TextMeshProUGUI>();
        tiempo = 180;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;

        minutos = (int)tiempo / 60;

        segundos = (int)tiempo - minutos * 60;


        textTiempo.text = "Tiempo: " + minutos + ":" + segundos;

        if (tiempo <= 0)
        {
            SceneManager.LoadScene(2);
        }

    }
}
