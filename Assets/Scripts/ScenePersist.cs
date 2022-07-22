using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        //FindObjectOfType<PlayerMovementScript>().bgmMusic=FindObjectOfType<PlayerMovementScript>().GetComponent<AudioSource>();
        //FindObjectOfType<PlayerMovementScript>().bgmMusic.Play(0);
        int numScenePersists=FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists>1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
        
    }
    public void ResetScenePersist(){
        Destroy(gameObject);
    }
}
