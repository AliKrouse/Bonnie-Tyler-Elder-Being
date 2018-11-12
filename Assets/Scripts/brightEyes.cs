using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brightEyes : MonoBehaviour
{
    private GameObject[] eyeRotator = new GameObject[100];
    public Material eyes;

    private bool fadeIn, fadeOut;
    public float fadeInSpeed, fadeOutSpeed;
    private float a;

    private void Awake()
    {
        for (int i = 0; i < 100; i++)
            eyeRotator[i] = transform.GetChild(i).gameObject;
    }

    void OnEnable ()
    {
        foreach (GameObject g in eyeRotator)
        {
            float newY = Random.Range(0, 360);
            float newX = Random.Range(-45, 45);
            g.transform.eulerAngles = new Vector3(newX, newY, 0.0f);
        }

        fadeIn = true;
    }
	
	void Update ()
    {
        if (fadeIn)
        {
            a = eyes.color.a;
            a += Time.deltaTime * fadeInSpeed;
            eyes.color = new Color(1, 1, 1, a);

            if (a >= 1)
            {
                fadeOut = true;
                fadeIn = false;
            }
        }
        if (fadeOut)
        {
            a = eyes.color.a;
            a -= Time.deltaTime * fadeOutSpeed;
            eyes.color = new Color(1, 1, 1, a);

            if (a <= 0)
                this.gameObject.SetActive(false);
        }
	}
}
