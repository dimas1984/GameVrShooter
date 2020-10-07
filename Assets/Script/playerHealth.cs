using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class playerHealth : MonoBehaviour
{
    [SerializeField] string levelName = "GameVr";
    [SerializeField] int hitsToTake = 3;
    [SerializeField] float respwanTime = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            print("Enemy touched the player");
            hitsToTake--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hitsToTake <= 0) {
            print("player has died");
            SceneManager.LoadScene(levelName);
        }
        
    }
}
