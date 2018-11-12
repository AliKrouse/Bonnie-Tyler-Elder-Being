using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camScript : MonoBehaviour
{
    public float speed;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private AudioSource source;
    public AudioClip preGame;
    public AudioClip[] clips;
    public int i = 0;

    private bool hasMouse = true;
    private bool gameBegun;

    public GameObject rotationClone;
    private GameObject frontFloater, backFloater;
    private Vector3 target, lookPoint;
    public float distance;

    public GameObject brightEyes;

    public Text t;
    public GameObject forever;
    
    public Material moonMaterial;
    private float time = 664;

    public GameObject squid;
    private Vector3 squidStart, squidEnd;

    void Start ()
    {
        source = GetComponent<AudioSource>();
        source.clip = preGame;
        source.Play();

        frontFloater = rotationClone.transform.GetChild(0).gameObject;
        backFloater = rotationClone.transform.GetChild(1).gameObject;
        target = backFloater.transform.position;

        moonMaterial.color = new Color(1, 1, 1, 1);

        squidStart = squid.transform.position;
        squidEnd = new Vector3(0, 10000, 0);
    }
	
	void Update ()
    {
        if (hasMouse)
        {
            yaw += speed * Input.GetAxis("Mouse X");
            pitch -= speed * Input.GetAxis("Mouse Y");
            if (pitch > 75)
                pitch = 75;
            if (pitch < -75)
                pitch = -75;

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //    hasMouse = !hasMouse;

        rotationClone.transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
        lookPoint = frontFloater.transform.position;
        
        distance = Vector3.Distance(lookPoint, target);
        Debug.DrawLine(lookPoint, target, Color.red);
        
        if (!gameBegun)
        {
            if (distance < 0.25)
            {
                source.Stop();
                gameBegun = true;
                source.clip = clips[i];
                source.Play();
                t.gameObject.SetActive(false);
            }
        }
        if (gameBegun)
        {
            if (source.isPlaying)
            {
                target = backFloater.transform.position;
                time -= Time.deltaTime;
            }
            if (!source.isPlaying)
            {
                if (distance < 0.25 && i < 17)
                {
                    i++;
                    source.clip = clips[i];
                    source.Play();

                    if (i == 5 || i == 6 || i == 11 || i == 12 || i == 13 || i == 14 || i == 15 || i == 16)
                        brightEyes.SetActive(true);
                }
            }

            moonMaterial.color = new Color(1, 1, 1, time / 664);

            squid.transform.position = Vector3.Lerp(squidEnd, squidStart, time / 664);
            
            if (time <= 145)
            {
                for (int i = 0; i < 4; i++)
                {
                    Text text = forever.transform.GetChild(i).GetComponent<Text>();
                    float a = text.color.a;
                    a += Time.deltaTime;
                    text.color = new Color(0.5f, 0, 0, a);
                }
            }
            //if (time < 134)
            //{
            //    float a = t.color.a;
            //    a -= Time.deltaTime;
            //    t.color = new Color(0.5f, 0, 0, a);
            //}
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Time.timeScale = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Time.timeScale = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Time.timeScale = 4;

        source.pitch = Time.timeScale;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
