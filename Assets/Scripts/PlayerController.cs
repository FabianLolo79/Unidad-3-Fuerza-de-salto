using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip JumpSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;

    public float jumpForce = 10f; 
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // salta el player alpresionar SpaceBar si está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(JumpSound, 1.0f);
        }
    }

    // detecta colisión con el suelo y colisión con obstáculo condición de Game Over
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();

        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            GameObject.Find("FX_DirtSplatter").gameObject.SetActive(false); // corrige el bug de morir y volver a tocar el piso que activaba las partículas 
            playerAudio.PlayOneShot(crashSound, 1.0f);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        }
    }
}
