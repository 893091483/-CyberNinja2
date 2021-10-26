using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    
    public GameObject fKey;
    public bool playerIsAttack;
    public float delayTime = 1;
    public bool isHit;
    public GameObject player;
   // GameObject player = GameObject.FindGameObjectWithTag("Player");

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fKey.SetActive(false);
        
    }
    private void OnTriggerEnter2D(Collider2D  collision)
    {

        
        fKey.SetActive(true);
        player.GetComponent<PlayerMovement>().canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        fKey.SetActive(false);
        player.GetComponent<PlayerMovement>().canAttack = false;
    }
    // Update is called once per frame
    void Update()
    {
       // playerIsAttack= player.GetComponent<PlayerMovement>().IsAttack;
        if (isHit == true)
        {
            
            player.GetComponent<PlayerMovement>().canAttack = false;
            GameObject.Destroy(fKey);

           GameObject.Destroy(gameObject,.8f);
        }

      //  if (playerIsAttack == true)
       // {
      //      GameObject.Destroy(fKey);
       // }
    }

    
}
