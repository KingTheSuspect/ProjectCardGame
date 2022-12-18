using Ink.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using TMPro;
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

    public float writingDelay;
    public float rotateDegreeLeft;
    public float rotateDegreeRight;
    public float rotateSpeed;
    //public float randomValue;
    public float index;

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

        StartCoroutine(TypeMainStory(eventList[((int)(index * jObj.Count)).ToString()][0].ToString()));
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (DisableTouch)
            return;

        //pointerEventData.delta = new Vector2(pointerEventData.delta.x, 0);
        float currentPosition = pointerEventData.delta.x / Screen.width;
        move_aim_x += touch_movement_sensivity * currentPosition;
        move_aim_x = Mathf.Clamp(move_aim_x, -range / 2f, range / 2f);

        transform.rotation = Quaternion.Euler(0f, 0f, move_aim_x * rotateSpeed);

        // Inform the Player to the choose
        card.anchoredPosition += pointerEventData.delta / canvas.scaleFactor;

        if (isInLeft)
            choices.text = eventList[((int)(index * jObj.Count)).ToString()][1].ToString();
        else if (isInRight)
            choices.text = eventList[((int)(index * jObj.Count)).ToString()][2].ToString();
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

        Debug.Log("choose left:" + chooseLeft);
        Debug.Log("choose right:" + chooseRight);
        Debug.Log("story can countinue:" + story.canContinue);

            if (chooseLeft && isInLeft)
            {
                circleScript.HealtAdd((int)(eventList[((int)(index * jObj.Count)).ToString()][3][0]));
                circleScript.HappyAdd((int)(eventList[((int)(index * jObj.Count)).ToString()][3][1]));

                index = Random.value;

                StartCoroutine(TypeMainStory(eventList[((int)(index * jObj.Count)).ToString()][0].ToString()));
            }
            else if (chooseRight && isInRight)
            {
                circleScript.HealtAdd((int)(eventList[((int)(index * jObj.Count)).ToString()][4][0]));
                circleScript.HappyAdd((int)(eventList[((int)(index * jObj.Count)).ToString()][4][1]));
                index = Random.value;
                StartCoroutine(TypeMainStory(eventList[((int)(index * jObj.Count)).ToString()][0].ToString()));
            }

        Debug.Log("Health: " + CircleScript.healtcount);
        Debug.Log("Happiness: " + CircleScript.happycount);
        ResetCardCanvas();
    }

    private void ResetCardCanvas()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
    }

    private void HandleStory()
    {
        /*GameObject card = Instantiate(cardPrefab, cardPrefab.transform.position, cardPrefab.transform.rotation);
        GameObject card2 = Instantiate(cardPrefab, cardPrefab.transform.position, cardPrefab.transform.rotation);

        card.name = "Kart";
        card2.name = "Kart 2";

        card.transform.SetParent(container.transform, false);
        card2.transform.SetParent(container.transform, false);*/
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