using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScripts : MonoBehaviour
{
    // Start is called before the first frame update
    private float fallDealy = 0.3f;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit(Collider other){
        if(other.tag== "Player"){
            TileManager.Instance.SpawnTile();
            StartCoroutine(FallDown());
        }
    }
    void OnTriggerEnter(Collider other){
       if(other.tag== "Player"){
           // PlayerScript.LogC("dasihd ais dia");
        }  
    }
    IEnumerator FallDown(){
        yield return new WaitForSeconds(fallDealy);
        GetComponent<Rigidbody>().isKinematic = false;

        yield return new WaitForSeconds(2);
        switch(gameObject.name){
            case "LeftTile":
                TileManager.Instance.LeftTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
            case "TopTile":
                TileManager.Instance.TopTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
        }
    }
}
