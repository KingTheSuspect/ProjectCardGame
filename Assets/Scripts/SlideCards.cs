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
    private Vector3 beganmove;
    private float animationDuration = 0.3f;
    private Vector3 targetPosition;
    private bool animationControl;

    public float writingDelay;
    public float rotateDegreeLeft;
    public float rotateDegreeRight;
    public float rotateSpeed;
    public int eventLength;
    public int choice1;
    public int choice2;
    public float index;
    public int controlLength;

    public TextAsset text;
    public TextAsset events;
    public GameObject gameManagerObject;

    private Story story;
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

        mainStory = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        choices = GameObject.Find("Choices").GetComponent<TextMeshProUGUI>();

        story = new Story(text.text);

        index = Random.value;
        eventLength = eventList[((int)(index * jObj.Count)).ToString()].Count();

        HandleStory();
    }

    private void Update()
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alpha = math.abs(gameObject.transform.position.x-297) * 1 / 563f;
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alpha = math.abs(gameObject.transform.position.x-297) * 1 / 563f;

        if (gameObject.transform.position == targetPosition && animationControl == true)
        {
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
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = eventList[((int)(index * jObj.Count)).ToString()][choice1].ToString();
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        }
        else if (isInRight)
        {
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = eventList[((int)(index * jObj.Count)).ToString()][choice2].ToString();
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
            if (controlLength + 3 == eventLength)
            {
                circleScript.HealtAdd((int)(eventList[((int)(index * jObj.Count)).ToString()][eventLength - 3][0]));
                circleScript.HappyAdd((int)(eventList[((int)(index * jObj.Count)).ToString()][eventLength - 3][1]));
            }

            CardAnimation();

        }
        else if (chooseRight && isInRight)
        {
            if (controlLength + 3 == eventLength)
            {
                circleScript.HealtAdd((int)(eventList[((int)(index * jObj.Count)).ToString()][eventLength - 2][0]));
                circleScript.HappyAdd((int)(eventList[((int)(index * jObj.Count)).ToString()][eventLength - 2][1]));
            }

            CardAnimation();
        }
        else
        {
            PutBackCard();
        }

        Debug.Log("Health: " + CircleScript.healtcount);
        Debug.Log("Happiness: " + CircleScript.happycount);
    }

    private void ResetCardCanvas()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
        gameObject.transform.position = new Vector3(canvas.transform.localPosition.x, canvas.transform.localPosition.y+1000, 0f);
        targetPosition = new Vector3(canvas.transform.localPosition.x, canvas.transform.localPosition.y, 0f);
        LeanTween.move(gameObject, targetPosition, animationDuration).setEaseInOutCubic().setDelay(0.2f);
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
        if (controlLength < eventList[((int)(index * jObj.Count)).ToString()].Count() - 3)
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
        if (isInRight && chooseRight)
        {
            targetPosition = new Vector3(1202, -500, 0);
            LeanTween.move(gameObject, targetPosition, animationDuration).setEaseInOutCubic().setDelay(0.1f);
        }
        else if (isInLeft && chooseLeft)
        {
            targetPosition = new Vector3(-642, -500, 0);
            LeanTween.move(gameObject, targetPosition, animationDuration).setEaseInOutCubic().setDelay(0.1f);
        }
        animationControl = true;
    }

    private void RandomIndex()
    {
        index = Random.value;
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