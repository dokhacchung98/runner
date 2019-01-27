using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    private Vector3 dir;
    public static bool enableClick = true;
    public GameObject ps;
    public GameObject floatingText;
    private bool isDead;
    public GameObject resetBtn;
    public GameObject uiGame;
    public Text scoreOver;
    public Text scoreUiOver;
    public Text bestScoreUiOver;
    public GameObject canvasStart;
    private int score = 0;
    private int tempScore=0;
    public Text scoreTxt;
    private AudioSource audioSource;
    private AudioSource audioClick;
    void Start()
    {
        isDead = false;
        enableClick = true;
        dir=Vector3.zero;
        audioClick = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isDead){
            if(enableClick){
                if(audioClick.isPlaying){
                    audioClick.Stop();
                }
                audioClick.Play();
                canvasStart.SetActive(false);
                score++;
                scoreTxt.text = score.ToString();
                if(dir==Vector3.forward){
                    dir=Vector3.left;
                }else{
                    dir=Vector3.forward;
                }
            }
        }
        float amoutToSpeed = speed * Time.deltaTime;

        transform.Translate(dir * amoutToSpeed);
        //enableClick = false;
    }
    void FixedUpdate()
    {
        if(score >= (tempScore + 190)){
            tempScore = score;
            speed += 2;
        } 
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Coin"){
            audioSource = gameObject.GetComponent<AudioSource>();
            if(audioSource.isPlaying){
                audioSource.Stop();
            }
            audioSource.Play();

            other.gameObject.SetActive(false);
            score += 20;
            scoreTxt.text = score.ToString();
            Instantiate(ps, transform.position, Quaternion.identity);
            var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = "+20";
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag == "Tile"){
            RaycastHit hit;
            Ray downRay = new Ray(transform.position, - Vector3.up);
            if(!Physics.Raycast(downRay, out hit)){
                //Kill phayer
                isDead = true;

                int bestScore = PlayerPrefs.GetInt("BestScore",0);
                if( score > bestScore ){
                    scoreOver.text = "New High Score";
                    bestScoreUiOver.text = score.ToString();
                    PlayerPrefs.SetInt("BestScore", score);
                }else{
                    scoreOver.text = "Score";
                    bestScoreUiOver.text = bestScore.ToString();
                }
                scoreUiOver.text = score.ToString();

                resetBtn.SetActive(true);
                uiGame.SetActive(false);
                if(transform.childCount > 0){
                    transform.GetChild(0).transform.parent = null;
                }
            }
        }
    }
}
