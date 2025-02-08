using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerHealth;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void takeDamage(float damage)
    {

        playerHealth -= damage;



        if (playerHealth <= 0)
        {

            Debug.Log("Öldü");
            SceneManager.LoadScene(0);

        }


    }
}
