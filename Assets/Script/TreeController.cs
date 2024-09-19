using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TreeController : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject rottenApplePrefab;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI timeText;
    public float speed = 1f;
    public float leftAndRightEdge = 9f;
    public float changeDirChance = 0.1f;
    public float appleDropDelay = 1f;
    private float timeElapsed = 0f;
    private float timeToIncrease = 40f;
    private float speedIncreaseRate = 1.35f;
    public float rottenAppleChance = 0.33f;
    private int currentRound = 1;
    private float countdownTime;
    // Start is called before the first frame update
    void Start()
    {
        countdownTime = timeToIncrease; // Initialize the countdown timer with 2 minutes
        roundText.text = "Round: " + currentRound;
        timeText.text = "Time: " + countdownTime.ToString("F0") + "s";
        StartAppleDrop();
    }
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed);
        } else if (pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed);
        }

        timeElapsed += Time.deltaTime;
        countdownTime -= Time.deltaTime;
        timeText.text = "Time: " + Mathf.Max(0, countdownTime).ToString("F0") + "s"; // Display countdown

        if(timeElapsed >= timeToIncrease) {
            currentRound++;
            roundText.text = "Round: " + currentRound;
            speed *= speedIncreaseRate;
            rottenAppleChance += 0.1f;
            appleDropDelay = Mathf.Max(0.1f, appleDropDelay * 0.7f);
            StartAppleDrop();
            timeElapsed = 0f;
            countdownTime = timeToIncrease;
        }

        if(currentRound > 4) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    void FixedUpdate() {
        if (Random.value < changeDirChance) {
            speed *= -1;
        }
    }

    void StartAppleDrop()
    {
        CancelInvoke(nameof(AppleIns)); // Cancel the previous apple drop invoke
        InvokeRepeating(nameof(AppleIns), 1, appleDropDelay); // Start with new rate
    }

    // Update is called once per frame
    void AppleIns()
    {
        Instantiate(ReturnCollectables(), transform.position, ReturnCollectables().transform.rotation);
    }

    GameObject ReturnCollectables(){
        if(Random.value < rottenAppleChance) {
            return rottenApplePrefab;
        }
        return applePrefab;
    }
}
