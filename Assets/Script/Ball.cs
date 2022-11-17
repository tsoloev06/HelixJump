using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce = 400f;
    AudioManager audioManager;
    //public ParticleSystem PaintSlash;
    public ParticleSystem PlatformDestruction;
    public ParticleSystem Confetti;
    public GameObject splitPrefab;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
        audioManager.Play("Land");

        GameObject newsplit = Instantiate(splitPrefab, new Vector3(transform.position.x, other.transform.position.y + 0.19f, transform.position.z), transform.rotation);

        newsplit.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
        newsplit.transform.parent = other.transform;

            string materialName = other.transform.GetComponent<MeshRenderer>().material.name;
            if (materialName == "Safe (Instance)") {
                //Debug.Log("you are safe");
            }
            if (materialName == "UnSafe (Instance)") {
                //Material float DissolveAmount = 0;
                //dissolveAmount++;
                //DissolveAmount = 1;
                PlatformDestruction.Play();
                GameManager.gameOver = true;
                audioManager.Play("GameOver");
            }
            if (materialName == "LastRing (Instance)" && !GameManager.levelWin) {
                Confetti.Play();
                GameManager.levelWin = true;
                audioManager.Play("LevelWin");
            }
        }

 } 

