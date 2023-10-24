using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SleekRender;
public class GameManager : MonoBehaviour
{
    public GameObject block1;
    public GameObject block2;
    public Sprite[] blocks;
    int colorChoice1;
    int colorChoice2;
    int lastChoice;
    public bool gameOver = false;
    public bool gameStart = false;
    public GameObject Ball;
    public Transform[] pos;
    GameObject instance1, instance2, firstBall;
    public static bool secondBallCreated = false;
    public Text Score;
    public static int ballsCount;
    public bool setSpeedOnce1;
    public bool setSpeedOnce2;
    public bool setSpeedOnce3;
    string ballname;
    float defaultSpeed = 3.5f;
    public static bool canspawnFirst = false;
    public static bool canSpawnSecond = false;
    public static bool canSpawnThird = false;
    public static int scoreamount = 1;
    public static bool rageMode = false;
    public Text RageText;
    public int RageTimer = 5;
    public Text RageMode;
    public bool canEnterRage = true;
    private float rageModeSpeed = 10f;
    public bool hault = true;
    public GameObject Tips;
    public GameObject RG1;
    public GameObject ShowGuide;
    public static bool canCreateNewBall;
    // Use this for initialization
    void Start()
    {
        blocks[0].name = "brown";
        blocks[1].name = "green";
        blocks[2].name = "orange";
        blocks[3].name = "red";
        blocks[4].name = "yellow";
        if (PlayerPrefs.GetInt("Guide1") == 0)
        {
            ShowGuide.SetActive(true);
            PlayerPrefs.SetInt("Guide1", 1);
        }
        else
        {
            CreateFirstBall();
        }
    }

    public void CreateFirstBall()
    {
        firstBall = Instantiate(Ball, pos[4].position, Quaternion.identity);
        firstBall.GetComponent<BallControl>().direction = Vector2.down;
        firstBall.name = "FirstBall";
        ballsCount += 1;
        StartCoroutine(ReadyToPlay());
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Score.text = "" + BallControl.score;
        }

        if (GameObject.Find("FirstBall") && !GameObject.Find("SecondBall") && !GameObject.Find("ThirdBall") && setSpeedOnce3)
        {
            if (firstBall.GetComponent<BallControl>().movingUp)
            {

                if (Mathf.Abs(firstBall.GetComponent<BallControl>().speed) < defaultSpeed)
                {
                    firstBall.GetComponent<BallControl>().speed = 3.5f;
                }
                else
                {
                    firstBall.GetComponent<BallControl>().speed *= -1;
                }
            }
            else
            {
                if (Mathf.Abs(firstBall.GetComponent<BallControl>().speed) < defaultSpeed)
                {
                    firstBall.GetComponent<BallControl>().speed = 3.5f;
                }
                else
                {
                    firstBall.GetComponent<BallControl>().speed *= -1;
                }
            }
            setSpeedOnce3 = false;
        }

        if (GameObject.Find("SecondBall") && !GameObject.Find("FirstBall") && !GameObject.Find("ThirdBall") && setSpeedOnce1)
        {
            if (instance1.GetComponent<BallControl>().movingUp)
            {
                if (Mathf.Abs(instance1.GetComponent<BallControl>().speed) < defaultSpeed)
                {
                    instance1.GetComponent<BallControl>().speed = 3.5f;
                }
                else
                {
                    instance1.GetComponent<BallControl>().speed *= -1;
                }
            }
            else
            {
                if (Mathf.Abs(instance1.GetComponent<BallControl>().speed) < defaultSpeed)
                {
                    instance1.GetComponent<BallControl>().speed = 3.5f;
                }
                else
                {
                    instance1.GetComponent<BallControl>().speed *= -1;
                }
            }
            setSpeedOnce1 = false;
        }

        if (GameObject.Find("ThirdBall") && !GameObject.Find("FirstBall") && !GameObject.Find("SecondBall") && setSpeedOnce2)
        {
            if (instance2.GetComponent<BallControl>().movingUp)
            {

                if (Mathf.Abs(instance2.GetComponent<BallControl>().speed) < defaultSpeed)
                {
                    instance2.GetComponent<BallControl>().speed = 3.5f;
                }
                else
                {
                    instance2.GetComponent<BallControl>().speed *= -1;
                }
            }
            else
            {
                if (Mathf.Abs(instance2.GetComponent<BallControl>().speed) < defaultSpeed)
                {
                    instance2.GetComponent<BallControl>().speed = 3.5f;
                }
                else
                {
                    instance2.GetComponent<BallControl>().speed *= -1;
                }
            }
            setSpeedOnce2 = false;
        }

        if (BallControl.score % 2 == 0 && BallControl.score != 0 && !rageMode && canEnterRage)
        {
            rageMode = true;
            canEnterRage = false;
            canCreateNewBall = false;
            Camera.main.GetComponent<Animator>().SetTrigger("shake");
            Camera.main.GetComponent<SleekRenderPostProcess>().enabled = true;
            RageMode.gameObject.SetActive(true);
            RageText.gameObject.SetActive(true);
            RageMode.GetComponent<Animator>().SetBool("rage", true);
            RageText.GetComponent<Animator>().SetBool("rage", true);
            SetSpeedForRage();
            if (PlayerPrefs.GetInt("Guide2") == 0)
            {
                
                PlayerPrefs.SetInt("Guide2", 1);
                StartCoroutine(PauseGame());
            }
            StartCoroutine(RageModeEnbDisb());
        }
    
        if (GameObject.Find("FirstBall"))
        {
            canspawnFirst = false;
        }
        else
        {
            canspawnFirst = true;
        }
        if (GameObject.Find("SecondBall"))
        {
            canSpawnSecond = false;

        }
        else
        {
            canSpawnSecond = true;
        }
        if (GameObject.Find("ThirdBall"))
        {
            canSpawnThird = false;
        }
        else
        {
            canSpawnThird = true;
        }
    }

    IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(0.3f);
        RG1.SetActive(true);
        Time.timeScale = 0;
    }

    public void SetSpeedForRage()
    {
        if (GameObject.Find("FirstBall"))
        {
            firstBall.GetComponent<BallControl>().speed = rageModeSpeed;
        }

        if (GameObject.Find("SecondBall"))
        {
            instance1.GetComponent<BallControl>().speed = rageModeSpeed;
        }

        if (GameObject.Find("ThirdBall"))
        {
            instance2.GetComponent<BallControl>().speed = rageModeSpeed;
        }
    }

    public void SetDefaultSpeed()
    {
        if (GameObject.Find("FirstBall"))
        {
            firstBall.GetComponent<BallControl>().speed = defaultSpeed;
        }

        if (GameObject.Find("SecondBall"))
        {
            instance1.GetComponent<BallControl>().speed = defaultSpeed;
        }

        if (GameObject.Find("ThirdBall"))
        {
            instance2.GetComponent<BallControl>().speed = defaultSpeed;
        }
    }


    IEnumerator RageModeEnbDisb()
    {
        while (RageTimer > 0)
        {
            RageText.text = "" + RageTimer;
            yield return new WaitForSeconds(1f);
            RageTimer--;
        }
        RageText.text = "" + RageTimer;
        RageTimer = 5;
        RageMode.gameObject.SetActive(false);
        RageText.gameObject.SetActive(false);
        SetDefaultSpeed();
        RageMode.GetComponent<Animator>().SetBool("rage", false); ;
        RageText.GetComponent<Animator>().SetBool("rage", false); ;
        Camera.main.GetComponent<SleekRenderPostProcess>().enabled = false;
        StartCoroutine(Revival());
        canCreateNewBall = true;
        yield return new WaitForSeconds(10f);
        canEnterRage = true;
    }

    IEnumerator Revival()
    {
        yield return new WaitForSeconds(1f);
        rageMode = false;
    }

    public void IncreaseSpeed()
    {
        if (GameObject.Find("FirstBall"))
        {
            firstBall.GetComponent<BallControl>().speed += 0.05f;
        }
        else if (GameObject.Find("SecondBall"))
        {
            instance1.GetComponent<BallControl>().speed += 0.05f;
        }
        else if (GameObject.Find("ThirdBall"))
        {
            instance2.GetComponent<BallControl>().speed += 0.05f;
        }
    }

    public void ChangeColorBlock(GameObject block)
    {
        if (block.Equals(block1))
        {
            colorChoice1++;
            if (colorChoice1 == 5)
            {
                colorChoice1 = 0;
            }
            block.GetComponent<SpriteRenderer>().sprite = blocks[colorChoice1];
        }
        else
        {
            colorChoice2++;
            if (colorChoice2 == 5)
            {
                colorChoice2 = 0;
            }
            block.GetComponent<SpriteRenderer>().sprite = blocks[colorChoice2];
        }
    }

    public void TapTop()
    {
        if (!gameOver)
        {
            ChangeColorBlock(block2);
        }
        if (rageMode)
        {

            BallControl.score += scoreamount;
        }
    }

    public void TapBottom()
    {
        if (!gameOver)
        {
            ChangeColorBlock(block1);
        }
        if (rageMode)
        {
            BallControl.score += scoreamount;
        }
    }

    IEnumerator ReadyToPlay()
    {
        //gameStart = true;
        yield return new WaitForSeconds(1.5f);
        firstBall.GetComponent<BallControl>().speed = 3.5f;
        gameStart = true;
    }

    public void CreateNewBall(string ball)
    {
        ballname = ball;
        ballsCount = ballsCount + 1;
        StartCoroutine(WaitTimeNext());
    }

    IEnumerator WaitTimeNext()
    {
        yield return new WaitForSeconds(1f);
        if (ballname == "FirstBall")
        {
            setSpeedOnce3 = true;
            if (GameObject.Find("SecondBall"))
            {
                if (!instance1.GetComponent<BallControl>().movingUp)
                {
                    firstBall = Instantiate(Ball, pos[4].position, Quaternion.identity);
                    firstBall.GetComponent<BallControl>().direction = Vector2.down;
                    firstBall.GetComponent<BallControl>().speed = 1.5f;
                    firstBall.name = "" + ballname;
                }
                else
                {
                    firstBall = Instantiate(Ball, pos[5].position, Quaternion.identity);
                    firstBall.GetComponent<BallControl>().direction = Vector2.up;
                    firstBall.GetComponent<BallControl>().speed = 1.5f;
                    firstBall.name = "" + ballname;
                }
            }

            else if (GameObject.Find("ThirdBall"))
            {
                if (!instance2.GetComponent<BallControl>().movingUp)
                {
                    firstBall = Instantiate(Ball, pos[4].position, Quaternion.identity);
                    firstBall.GetComponent<BallControl>().direction = Vector2.down;
                    firstBall.GetComponent<BallControl>().speed = 1.5f;
                    firstBall.name = "" + ballname;
                }
                else
                {
                    firstBall = Instantiate(Ball, pos[5].position, Quaternion.identity);
                    firstBall.GetComponent<BallControl>().direction = Vector2.up;
                    firstBall.GetComponent<BallControl>().speed = 1.5f;
                    firstBall.name = "" + ballname;
                }
            }

        }
        else if (ballname == "SecondBall")
        {
            setSpeedOnce1 = true;
            if (GameObject.Find("FirstBall"))
            {
                if (!firstBall.GetComponent<BallControl>().movingUp)
                {
                    instance1 = Instantiate(Ball, pos[0].position, Quaternion.identity);
                    instance1.GetComponent<BallControl>().direction = Vector2.down;
                    instance1.GetComponent<BallControl>().speed = 1.5f;
                    instance1.name = "" + ballname;
                }
                else
                {
                    instance1 = Instantiate(Ball, pos[1].position, Quaternion.identity);
                    instance1.GetComponent<BallControl>().direction = Vector2.up;
                    instance1.GetComponent<BallControl>().speed = 1.5f;
                    instance1.name = "" + ballname;
                }
            }
            else if (GameObject.Find("ThirdBall"))
            {
                if (!instance2.GetComponent<BallControl>().movingUp)
                {
                    instance1 = Instantiate(Ball, pos[0].position, Quaternion.identity);
                    instance1.GetComponent<BallControl>().direction = Vector2.down;
                    instance1.GetComponent<BallControl>().speed = 1.5f;
                    instance1.name = "" + ballname;
                }
                else
                {
                    instance1 = Instantiate(Ball, pos[1].position, Quaternion.identity);
                    instance1.GetComponent<BallControl>().direction = Vector2.up;
                    instance1.GetComponent<BallControl>().speed = 1.5f;
                    instance1.name = "" + ballname;
                }
            }
        }
        else
        {
            setSpeedOnce2 = true;
            if (GameObject.Find("FirstBall"))
            {
                if (!firstBall.GetComponent<BallControl>().movingUp)
                {
                    instance2 = Instantiate(Ball, pos[2].position, Quaternion.identity);
                    instance2.GetComponent<BallControl>().direction = Vector2.down;
                    instance2.GetComponent<BallControl>().speed = 1.5f;
                    instance2.name = "" + ballname;
                }
                else
                {
                    instance2 = Instantiate(Ball, pos[3].position, Quaternion.identity);
                    instance2.GetComponent<BallControl>().direction = Vector2.up;
                    instance2.GetComponent<BallControl>().speed = 1.5f;
                    instance2.name = "" + ballname;
                }
            }
            else if (GameObject.Find("SecondBall"))
            {
                if (!instance1.GetComponent<BallControl>().movingUp)
                {
                    instance2 = Instantiate(Ball, pos[2].position, Quaternion.identity);
                    instance2.GetComponent<BallControl>().direction = Vector2.down;
                    instance2.GetComponent<BallControl>().speed = 1.5f;
                    instance2.name = "" + ballname;
                }
                else
                {
                    instance2 = Instantiate(Ball, pos[3].position, Quaternion.identity);
                    instance2.GetComponent<BallControl>().direction = Vector2.up;
                    instance2.GetComponent<BallControl>().speed = 1.5f;
                    instance2.name = "" + ballname;
                }
            }
        }
    }

    public void DisableTips()
    {
        ShowGuide.SetActive(false);
        CreateFirstBall();
    }

    public void DisableRage()
    {
        RG1.SetActive(false);
        Time.timeScale = 1;
    }
}
