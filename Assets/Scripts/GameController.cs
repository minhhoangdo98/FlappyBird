using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //public
    [HideInInspector]
    public GameObject player, leftOffScreen, rightOffScreen;
    public bool gameplay = false, enableSpawn = true;
    public float spawnTime = 3.5f, groundSpeed = 1f;
    public AudioClip pointSound;

    //private
    GameObject obstacleObj, ObstaclesParent, gameoverPanel;
    int score = 0, highScore = 0;
    Text scoreText;

    void Start()
    {
        //khoi tao gia tri
        player = GameObject.FindGameObjectWithTag("Player");
        leftOffScreen = Camera.main.transform.Find("Left").gameObject;
        rightOffScreen = Camera.main.transform.Find("Right").gameObject;
        obstacleObj = Resources.Load<GameObject>("Prefabs/Obstacles");
        ObstaclesParent = GameObject.FindGameObjectWithTag("ObstaclesParent");
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        gameoverPanel = GameObject.FindGameObjectWithTag("Gameover");
        gameoverPanel.SetActive(false);

        //load diem cao
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    private void Update()
    {
        if (gameplay)
            ObstaclesParent.transform.Translate(Vector2.left * groundSpeed * Time.deltaTime);

        if (enableSpawn && gameplay)
        {
            StartCoroutine(SpawnObj());
        }

        scoreText.text = score.ToString();
    }

    /// <summary>
    /// kiem tra va cham giua 2 doi tuong
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    public bool CheckBoundWithPlayer(GameObject obj, GameObject obj2)
    {
        if (obj.GetComponent<SpriteRenderer>().bounds.Intersects(obj2.GetComponent<SpriteRenderer>().bounds))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Ket thuc game
    /// </summary>
    /// <returns></returns>
    public IEnumerator Gameover()
    {
        player.GetComponent<PlayerController>().canMove = false;
        player.GetComponent<AudioSource>().clip = player.GetComponent<PlayerController>().hurtSound;
        player.GetComponent<AudioSource>().Play();
        player.GetComponent<Animator>().SetInteger("State", 1);
        player.transform.Translate(Vector2.up * 0.2f);
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<Animator>().SetInteger("State", 2);
        player.transform.Translate(Vector2.down * 0.3f);
        yield return new WaitForSeconds(0.2f);
        gameplay = false;
        ShowGameoverPanel();
    }

    /// <summary>
    /// Tao chuong ngai vat
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnObj()
    {
        enableSpawn = false;
        Vector2 position = new Vector2(rightOffScreen.transform.position.x, Random.Range(0, 5));
        GameObject obj = Instantiate(obstacleObj, position, Quaternion.identity, ObstaclesParent.transform);
        yield return new WaitForSeconds(spawnTime);
        enableSpawn = true;
    }

    /// <summary>
    /// Nut Play
    /// </summary>
    public void ButtonPlay()
    {
        gameplay = true;
    }

    /// <summary>
    /// Tang diem
    /// </summary>
    public void GainPoint()
    {
        score++;
        gameObject.GetComponent<AudioSource>().clip = pointSound;
        gameObject.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// hien gameover Panel khi ket thuc game
    /// </summary>
    void ShowGameoverPanel()
    {
        gameoverPanel.SetActive(true);
        Image medalImg = gameoverPanel.transform.Find("MedalPanel").transform.Find("Medal").GetComponent<Image>();
        Text gScoreText = gameoverPanel.transform.Find("ScorePanel").transform.Find("Score").GetComponent<Text>();
        Text bestScoreText = gameoverPanel.transform.Find("ScorePanel").transform.Find("BestScore").GetComponent<Text>();
        gScoreText.text = score.ToString();

        //xac dinh diem cao va luu
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
        bestScoreText.text = highScore.ToString();

        //hien medal tuong ung voi so diem
        if(score >= 10 && score < 20)
        {
            medalImg.sprite = Resources.Load<Sprite>("Sprites/GUI/medal_02");
        }
        if (score >= 20)
        {
            medalImg.sprite = Resources.Load<Sprite>("Sprites/GUI/medal_03");
        }
    }

    /// <summary>
    /// Nut choi lai
    /// </summary>
    public void ButtonReplay()
    {
        SceneManager.LoadScene(0);
    }
}
