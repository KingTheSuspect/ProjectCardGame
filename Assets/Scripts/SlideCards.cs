using Ink.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SlideCards : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private float move_aim_x = 0;
    [SerializeField] private float touch_movement_sensivity;
    private float range = 1;
    private Vector3 targetPosition;
    private bool animationControl;
    private float animationLeftTime;
    private float resetLeftTime;

    public float writingDelay;
    public float rotateDegreeLeft;
    public float rotateDegreeRight;
    public float rotateSpeed;
    public int eventLength;
    public int choice1;
    public int choice2;
    public float index;
    public int controlLength;

    //public TextAsset text;
    public TextAsset events;
    public GameObject gameManagerObject;
    public GameObject TopStory;
    public GameObject Stats;

    private bool isInLeft;
    private bool isInRight;
    private bool chooseLeft;
    private bool chooseRight;
    private bool DisableTouch;

    private RectTransform card;
    private GameObject cardPrefab;
    private Rigidbody2D rb;
    private Canvas canvas;
    private TextMeshProUGUI mainStory;
    private TextMeshProUGUI choices;
    private TextAsset storydata;
    private JObject eventList;
    private JObject jObj;

    CircleScript circleScript;

    private void Start()
    {
        controlLength = 0;
        eventList = JObject.Parse(events.text);
        jObj = (JObject)JsonConvert.DeserializeObject(events.text);
        circleScript = gameManagerObject.GetComponent<CircleScript>();
        canvas = transform.root.GetComponent<Canvas>();
        card = GetComponent<RectTransform>();
        rb = GetComponent<Rigidbody2D>();
        cardPrefab = Resources.Load("Kart") as GameObject;

        mainStory = TopStory.GetComponent<TextMeshProUGUI>();
        choices = GameObject.Find("Choices").GetComponent<TextMeshProUGUI>();

        index = Random.value;
        eventLength = eventList[((int)(index * jObj.Count)).ToString()].Count();

        HandleStory();
    }

    private void Update()
    {
        animationLeftTime -= Time.deltaTime;
        resetLeftTime -= Time.deltaTime;

        if (animationControl == true)
        {
            transform.position += (targetPosition - transform.position).normalized * 3000f * Time.deltaTime;
        }

        if (resetLeftTime >= 0f)
        {
            transform.position += (targetPosition - transform.position).normalized * 3000f * Time.deltaTime;
        }

        if (targetPosition.x == 297 && targetPosition.y == 282 && math.abs(gameObject.transform.position.x - targetPosition.x) <= 10 && math.abs(gameObject.transform.position.y - targetPosition.y) <= 10)
        {
            resetLeftTime = 0f;
        }
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alpha = math.abs(gameObject.transform.position.x-297) * 1 / 100f;
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alpha = math.abs(gameObject.transform.position.x-297) * 1 / 100f;

        if (math.abs(gameObject.transform.position.x - targetPosition.x) <= 100 && math.abs(gameObject.transform.position.y - targetPosition.y) <= 100 && animationLeftTime <=0 && animationControl == true)
        {
            Debug.Log("Same Position");
            ResetCardCanvas();
            HandleStory();
            animationControl = false;
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (DisableTouch)
            return;

        float currentPosition = pointerEventData.delta.x / Screen.width;
        move_aim_x += touch_movement_sensivity * currentPosition;
        move_aim_x = Mathf.Clamp(move_aim_x, -range / 2f, range / 2f);

        transform.rotation = Quaternion.Euler(0f, 0f, move_aim_x * rotateSpeed);

        card.anchoredPosition += pointerEventData.delta / canvas.scaleFactor;

        if (isInLeft)
        {
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = eventList[((int)(index * jObj.Count)).ToString()][choice1][0].ToString();
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (isInRight)
        {
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = eventList[((int)(index * jObj.Count)).ToString()][choice2][0].ToString();
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (DisableTouch)
            return;

        choices.text = "";
        move_aim_x = 0;

        if (chooseLeft && isInLeft)
        {
            SetStats((int)(eventList[((int)(index * jObj.Count)).ToString()][choice1][2][0]),
                (int)(eventList[((int)(index * jObj.Count)).ToString()][choice1][2][1]),
                (int)(eventList[((int)(index * jObj.Count)).ToString()][choice1][2][2]),
                (int)(eventList[((int)(index * jObj.Count)).ToString()][choice1][2][3]),
                (int)(eventList[((int)(index * jObj.Count)).ToString()][choice1][1]));

            if ((int)eventList[((int)(index * jObj.Count)).ToString()][choice1][1] == 0)
            {
                controlLength = (int)eventList[((int)(index * jObj.Count)).ToString()].Count();
            }
            CardAnimation();

        }
        else if (chooseRight && isInRight)
        {
            SetStats((int)(eventList[((int)(index * jObj.Count)).ToString()][choice2][2][0]),
                (int)(eventList[((int)(index * jObj.Count)).ToString()][choice2][2][1]),
                (int)(eventList[((int)(index * jObj.Count)).ToString()][choice2][2][2]),
                (int)(eventList[((int)(index * jObj.Count)).ToString()][choice2][2][3]),
                (int)(eventList[((int)(index * jObj.Count)).ToString()][choice2][1]));

            if ((int)eventList[((int)(index * jObj.Count)).ToString()][choice2][1] == 0)
            {
                controlLength = (int)eventList[((int)(index * jObj.Count)).ToString()].Count();
            }
            CardAnimation();
        }
        else
        {
            Debug.Log("PutBack");
            PutBackCard();
        }
    }

    private void ResetCardCanvas()
    {
        Debug.Log("ResetCardCanvas");
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
        transform.position = new Vector3(297f, 1000f, 0f);
        targetPosition = new Vector3(canvas.transform.localPosition.x, canvas.transform.localPosition.y, 0f);
        resetLeftTime = 5f;
    }

    private void PutBackCard()
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
    }

    private void HandleStory()
    {
        if (controlLength < eventList[((int)(index * jObj.Count)).ToString()].Count() - 1)
        {
            StartCoroutine(TypeMainStory(eventList[((int)(index * jObj.Count)).ToString()][controlLength].ToString()));
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (eventList[((int)(index * jObj.Count)).ToString()][eventLength - 1]).ToString();
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"PersonImages/{eventList[((int)(index * jObj.Count)).ToString()][eventLength - 1]}");
            gameObject.GetComponent<Image>().color = Color.white;
            choice1 = controlLength + 1;
            choice2 = controlLength + 2;
            controlLength += 3;
        }
        else
        {
            RandomIndex();
            eventLength = eventList[((int)(index * jObj.Count)).ToString()].Count();
            controlLength = 0;
            HandleStory();
        }
    }
    private void CardAnimation()
    {
        animationLeftTime = 0.6f;
        Debug.Log("CardAnimation");
        if (isInRight && chooseRight)
        {
            Debug.Log("RightAnimation");
            targetPosition = new Vector3(1200f, -500f, 0);
        }
        else if (isInLeft && chooseLeft)
        {
            Debug.Log("LeftAnimation");
            targetPosition = new Vector3(-640f, -500f, 0);
        }
        animationControl = true;
    }

    private void RandomIndex()
    {
        index = Random.value;
    }

    private void SetStats(int stat1, int stat2, int stat3, int stat4, int ageSituation)
    {
        if (ageSituation == 0)
        {
            circleScript.AgeAdd();
            Stats.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = $"Age\n{CircleScript.age}";
        }

        circleScript.HealthAdd(stat1);
        Stats.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"Health\n{CircleScript.healthcount}";

        circleScript.HappyAdd(stat2);
        Stats.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = $"Happiness\n{CircleScript.happycount}";

        circleScript.MoneyAdd(stat3);
        Stats.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = $"Money\n{CircleScript.money}";

        circleScript.SociabilityAdd(stat4);
        Stats.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = $"Sociability\n{CircleScript.sociability}";
        
    }

    private IEnumerator TypeMainStory(string sentence)
    {
        DisableTouch = true;
        mainStory.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            mainStory.text += letter;
            yield return new WaitForSeconds(writingDelay);
        }
        DisableTouch = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftChoice")) isInLeft = true;
        else if (collision.CompareTag("RightChoice")) isInRight = true;

        if (collision.CompareTag("ChooseLeft")) chooseLeft = true;
        else if (collision.CompareTag("ChooseRight")) chooseRight = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftChoice")) isInLeft = false;
        else if (collision.CompareTag("RightChoice")) isInRight = false;

        if (collision.CompareTag("ChooseLeft")) chooseLeft = false;
        else if (collision.CompareTag("ChooseRight")) chooseRight = false;
    }
}