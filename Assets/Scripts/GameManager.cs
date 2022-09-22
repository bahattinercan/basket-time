using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform cameraTransform;
    public bool canThrow = true;
    private bool canSpawn = true;
    [SerializeField] private GameObject basketPrefab,starPrefab;
    public Transform currentBasketTransform;
    private Transform ballTransform;
    [SerializeField] private float maxX, minX;
    [SerializeField] private float maxY, minY;
    private int scoreNumber,starNumber;
    private TextMeshProUGUI scoreText,starText;
    Animator scoreTextAnimator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip collectClip;
    [SerializeField] private GameObject collectPE;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        scoreNumber = 0;
        canThrow = true;
        canSpawn = true;
        ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        scoreText = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();
        scoreTextAnimator = scoreText.GetComponent<Animator>();
        starText = GameObject.Find("starText").GetComponent<TextMeshProUGUI>();
        currentBasketTransform = GameObject.Find("ilkPota").transform;
        audioSource = GetComponent<AudioSource>();
        AddScore(-1);
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentBasketTransform!=null &&ballTransform.position.y + 15 < currentBasketTransform.position.y)
        {
            RestartGame();
        }
    }

    private void SpawnRandomBasket()
    {
        if (canSpawn)
        {
            Vector3 randomBasketPos = Vector3.zero;
            do
            {
                randomBasketPos = new Vector3(
                           Random.Range(minX, maxX),
                           Random.Range(ballTransform.position.y + minY, ballTransform.position.y + maxY),
                           0);
            } while (randomBasketPos.x > -.8f + ballTransform.position.x && randomBasketPos.x < .8f + ballTransform.position.x);

            SpawnBasket(randomBasketPos);
        }
    }

    private void SpawnBasket(Vector3 spawnPos)
    {
        Transform basket = Instantiate(basketPrefab, spawnPos, Quaternion.identity).transform;
       
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(starPrefab, basket.Find("starSpawnPos").position, Quaternion.identity).transform.SetParent(basket);
        }
    }

    public void Score(Vector3 particlePos)
    {
        SpawnRandomBasket();
        AddScore(1);
        audioSource.PlayOneShot(collectClip);
        Instantiate(collectPE, particlePos, Quaternion.identity);
    }

    private void AddScore(int value)
    {
        scoreNumber += value;
        scoreText.text = scoreNumber.ToString();
        scoreTextAnimator.Play("scoreText_collect");
    }

    public void AddStar()
    {
        starNumber += 1;
        starText.text = starNumber.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}