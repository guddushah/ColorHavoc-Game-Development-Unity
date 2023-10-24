using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BallControl : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public bool movingUp = false;
    public Sprite[] ball;
    int colorChoice;
    int lastChoice;
    public Vector2 direction;
    public static int score;
    public static bool initBall;
    public GameObject particleEffectsUp;
    AudioSource audioSource;
    public AudioClip clipBounce;
    // Use this for initialization
    void Start()
    {
        ball[0].name = "brown";
        ball[1].name = "green";
        ball[2].name = "orange";
        ball[3].name = "red";
        ball[4].name = "yellow";
        colorChoice = Random.Range(0, 5);
        lastChoice = colorChoice;
        GetComponent<SpriteRenderer>().sprite = ball[colorChoice];
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Block1")
        {
            ContactPoint2D contacts = collision.contacts[0];
            Instantiate(particleEffectsUp, contacts.point, Quaternion.Euler(0, 0, -180));
            if (collision.collider.GetComponent<SpriteRenderer>().sprite.name.Equals(GetComponent<SpriteRenderer>().sprite.name))
            {
                audioSource.PlayOneShot(clipBounce);
                //speed *= -1;
                direction *= -1;
                //speed = rate;
                GameObject.FindObjectOfType<GameManager>().IncreaseSpeed();
                colorChoice = Random.Range(0, 5);
                while (lastChoice == colorChoice)
                {
                    colorChoice = Random.Range(0, 5);
                }
                lastChoice = colorChoice;
                movingUp = !movingUp;
                ChangeColorBall();
                SetScore();
                
            }
            else
            {
                
                if (!GameManager.rageMode)
                {
                    GameManager.ballsCount--;
                    if (GameManager.ballsCount == 0)
                    {
                        GameObject.FindObjectOfType<GameManager>().gameOver = true;
                        SceneManager.LoadScene("GameOver");
                    }
                    Destroy(gameObject);
                }
                else
                {
                    direction *= -1;
                }
            }
        }

        if (collision.collider.gameObject.tag == "Block2")
        {
            ContactPoint2D contacts = collision.contacts[0];
            Instantiate(particleEffectsUp, contacts.point, Quaternion.identity);
            if (collision.collider.GetComponent<SpriteRenderer>().sprite.name.Equals(GetComponent<SpriteRenderer>().sprite.name))
            {
                audioSource.PlayOneShot(clipBounce);
                //speed *= -1;
                direction *= -1;
                //speed = -rate;
                GameObject.FindObjectOfType<GameManager>().IncreaseSpeed();
                colorChoice = Random.Range(0, 5);
                while (lastChoice == colorChoice)
                {
                    colorChoice = Random.Range(0, 5);
                }

                movingUp = !movingUp;
                ChangeColorBall();
                SetScore();

            }
            else
            {
               
                if (!GameManager.rageMode)
                {
                    GameManager.ballsCount--;
                    if (GameManager.ballsCount == 0)
                    {
                        GameObject.FindObjectOfType<GameManager>().gameOver = true;
                        SceneManager.LoadScene("GameOver");
                    }
                    Destroy(gameObject);   
                }
                else
                {
                    direction *= -1;
                }
            }
        }
        PlayerPrefs.SetInt("score", BallControl.score);
        if(PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore",BallControl.score);
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.2f);       
        SceneManager.LoadScene("GameOver");
    }

    public void ChangeColorBall()
    {
        GetComponent<SpriteRenderer>().sprite = ball[colorChoice];
    }

    public void SetScore()
    {
        score = score + GameManager.scoreamount;
        if (GameManager.canCreateNewBall)
        {
            if (GameManager.canSpawnSecond)
            {
                if (score % 5 == 0)
                {
                    GameObject.FindObjectOfType<GameManager>().CreateNewBall("SecondBall");
                    GameManager.canSpawnSecond = false;
                }

            }

            if (GameManager.canSpawnThird)
            {

                if (score % 7 == 0)
                {
                    GameObject.FindObjectOfType<GameManager>().CreateNewBall("ThirdBall");
                    GameManager.canSpawnThird = false;
                }
            }

            if (GameManager.canspawnFirst)
            {

                if (score % 13 == 0)
                {
                    GameObject.FindObjectOfType<GameManager>().CreateNewBall("FirstBall");
                    GameManager.canspawnFirst = false;
                }
            }
        }
     
    }
}
