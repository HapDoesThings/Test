using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transiciones : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator TransicionCorrutina(int escena)
    {
        anim.SetTrigger("Transicion");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(escena);
    }
}
