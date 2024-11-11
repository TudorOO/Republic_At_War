using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeUp : MonoBehaviour
{
    private IEnumerator Suicide(){
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Suicide());
    }

}
