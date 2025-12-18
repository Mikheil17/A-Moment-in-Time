using UnityEngine;
using System.Collections;

public class RecordPlayer : MonoBehaviour
{
    public bool recordPlayerActive = false;

    GameObject disc;
    GameObject arm;

    int mode;
    float armAngle;
    float discAngle;
    float discSpeed;

    // 🎵 NEW: assign your audio clip in Inspector
    public AudioSource musicSource;

    // For player interaction
    private bool playerInRange = false;

    void Awake()
    {
        disc = transform.Find("teller").gameObject;
        arm = transform.Find("arm").gameObject;
    }

    void Start()
    {
        mode = 0;
        armAngle = 0.0f;
        discAngle = 0.0f;
        discSpeed = 0.0f;
    }

    void Update()
    {
        // 🎹 Input: toggle record player if player is close
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            recordPlayerActive = !recordPlayerActive;

            if (recordPlayerActive)
            {
                if (!musicSource.isPlaying)
                    musicSource.UnPause(); // resume where it left off
            }
            else
            {
                musicSource.Pause(); // keep position, don't reset
            }
        }


        //-- Mode 0: player off
        if (mode == 0)
        {
            if (recordPlayerActive == true)
                mode = 1;
        }
        //-- Mode 1: activation
        else if (mode == 1)
        {
            if (recordPlayerActive == true)
            {
                armAngle += Time.deltaTime * 30.0f;
                if (armAngle >= 30.0f)
                {
                    armAngle = 30.0f;
                    mode = 2;
                }
                discAngle += Time.deltaTime * discSpeed;
                discSpeed += Time.deltaTime * 80.0f;
            }
            else
                mode = 3;
        }
        //-- Mode 2: running
        else if (mode == 2)
        {
            if (recordPlayerActive == true)
                discAngle += Time.deltaTime * discSpeed;
            else
                mode = 3;
        }
        //-- Mode 3: stopping
        else
        {
            if (recordPlayerActive == false)
            {
                armAngle -= Time.deltaTime * 30.0f;
                if (armAngle <= 0.0f)
                    armAngle = 0.0f;

                discAngle += Time.deltaTime * discSpeed;
                discSpeed -= Time.deltaTime * 80.0f;
                if (discSpeed <= 0.0f)
                    discSpeed = 0.0f;

                if ((discSpeed == 0.0f) && (armAngle == 0.0f))
                    mode = 0;
            }
            else
                mode = 1;
        }

        //-- update objects
        arm.transform.localEulerAngles = new Vector3(0.0f, armAngle, 0.0f);
        disc.transform.localEulerAngles = new Vector3(0.0f, discAngle, 0.0f);
    }

    // Trigger detection (make sure the record player has a collider with "Is Trigger" checked!)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}

