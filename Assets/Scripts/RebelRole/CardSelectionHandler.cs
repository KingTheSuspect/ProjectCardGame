using Ink.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardSelectionHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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
    [HideInInspector] public JObject eventList;
    [HideInInspector] public JObject jObj;
    public GameObject[] GonnaChange;


    private bool isInLeft;
    private bool isInRight;
    private bool chooseLeft;
    private bool chooseRight;
    private bool DisableTouch;
    private bool isCardPuttingBack;

    private RectTransform card;
    private GameObject cardPrefab;
    private Rigidbody2D rb;
    private Canvas canvas;
    private TextMeshProUGUI mainStory;
    private TextMeshProUGUI choices;
    private TextAsset storydata;

    private StoriesHandler _storiesHandler;
    private StoryCard _currentHandlingStoryCard;

    CircleScript circleScript;

    private void Start()
    {
        Application.targetFrameRate = 60;
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

        _storiesHandler = FindObjectOfType<StoriesHandler>();

        HandleStory();
    }

    private void Update()
    {
        CardMovementUpdate();
        CardVisualUpdate();
        HandleArrivalAndAnimationControl();
        CardPuttingBackAnimation();
    }

    //Kart hareketleri g�ncellernir.
    private void CardMovementUpdate()
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

        if (math.abs(targetPosition.x - 297) < 5 && math.abs(targetPosition.y - 282) < 5 && math.abs(gameObject.transform.position.x - targetPosition.x) <= 10 && math.abs(gameObject.transform.position.y - targetPosition.y) <= 10)
        {
            resetLeftTime = 0f;
        }
    }

    //Kard�n se�imlerde alpha de�erinin de�i�mesini sa�lar.
    private void CardVisualUpdate()
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alpha = math.abs(gameObject.transform.position.x - 297) * 1 / 100f;
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alpha = math.abs(gameObject.transform.position.x - 297) * 1 / 100f;
    }

    //Kard�n hedefe var�p varmad��� kontrol edilir, se�im yap�l�p kart b�rak�ld���nda
    //oynat�lan animasyonun bitmesini bekler ve bitince kard� di�er event i�in s�f�rlar.
    private void HandleArrivalAndAnimationControl()
    {
        if (math.abs(gameObject.transform.position.x - targetPosition.x) <= 100 && math.abs(gameObject.transform.position.y - targetPosition.y) <= 100 && animationLeftTime <= 0 && animationControl == true)
        {
            Debug.Log("Same Position");
            ResetCardCanvas();
            HandleStory();
            animationControl = false;
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        Debug.Log(DisableTouch);
        if (DisableTouch)
            return;

        float currentPosition = pointerEventData.delta.x / Screen.width;
        move_aim_x += touch_movement_sensivity * currentPosition;
        move_aim_x = Mathf.Clamp(move_aim_x, -range / 2f, range / 2f);

        transform.rotation = Quaternion.Euler(0f, 0f, move_aim_x * rotateSpeed);

        card.anchoredPosition += pointerEventData.delta / canvas.scaleFactor;
        //Bu sola ilk kayd�rd���mda gelen �ey l�tfen yaz�n
        if (isInLeft)
        {
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _currentHandlingStoryCard.OptionA.OptionName;
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

            //Buras� update edilecek!
            for (int i = 0; i < 4; i++)
            {
                //if (eventList[((int)(index * jObj.Count)).ToString()][choice1][2][i].ToObject<int>() > 0 || eventList[((int)(index * jObj.Count)).ToString()][choice1][2][i].ToObject<int>() < 0)
                //{
                //    GonnaChange[i].gameObject.SetActive(true);
                //}
                //else
                //{
                //    GonnaChange[i].gameObject.SetActive(false);
                //}
            }
        }
        else if (isInRight)
        {
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _currentHandlingStoryCard.OptionB.OptionName;
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

            for (int i = 0; i < 4; i++)
            {
                //if (eventList[((int)(index * jObj.Count)).ToString()][choice2][2][i].ToObject<int>() > 0 || eventList[((int)(index * jObj.Count)).ToString()][choice2][2][i].ToObject<int>() < 0)
                //{
                //    GonnaChange[i].gameObject.SetActive(true);
                //}
                //else
                //{
                //    GonnaChange[i].gameObject.SetActive(false);
                //}
            }
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

        RebelOption selectedOption;

        if (chooseLeft && isInLeft)
        {
            selectedOption = _currentHandlingStoryCard.OptionA;
            ChangeStatsAfterSelection(selectedOption);
            RefreshStatsUi();
            CardAnimation();

        }
        else if (chooseRight && isInRight)
        {
            selectedOption = _currentHandlingStoryCard.OptionB;
            ChangeStatsAfterSelection(selectedOption);
            RefreshStatsUi();
            CardAnimation();
        }
        else
        {
            Debug.Log("PutBack");
            PutBackCard();
        }


        for (int i = 0; i < 4; i++)
        {
            GonnaChange[i].gameObject.SetActive(false);
        }
    }
    private void ChangeStatsAfterSelection(RebelOption selectedOption)
    {
        RebelStatsManager.Instance.AddPrivacy(selectedOption.Privacy);
        RebelStatsManager.Instance.AddAggressiveness(selectedOption.Aggressiveness);
        RebelStatsManager.Instance.AddLawCount(selectedOption.Law);
        RebelStatsManager.Instance.AddRoyaltyCount(selectedOption.Royalty);
    }
    private void ResetCardCanvas()
    {
        Debug.Log("ResetCardCanvas");
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
        transform.position = new Vector3(297f, 1000f, 0f);
        targetPosition = new Vector3(canvas.transform.localPosition.x, canvas.transform.localPosition.y, 0f);
        resetLeftTime = 0.245f;
    }

    private void PutBackCard()
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

        isCardPuttingBack = true;

        //Old Code Blocks
        //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        //card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
    }
    //Kart b�rak�ld���nda yerine yuma�ak �ekilde ge�i� yapar.
    private void CardPuttingBackAnimation()
    {
        Vector2 targetPosition = Vector2.zero / canvas.scaleFactor;
        Quaternion targetQuaternion = Quaternion.Euler(0, 0, 0);

        Debug.Log(card.anchoredPosition == targetPosition);

        if (Vector2.Distance(card.anchoredPosition, targetPosition) < 4f)
        {
            DisableTouch = false;
            isCardPuttingBack = false;
        }

        if (!isCardPuttingBack)
            return;

        //10 de�eri keyfidir, bir de�i�kene atanabilir.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * 10);
        card.anchoredPosition = Vector2.Lerp(card.anchoredPosition, targetPosition, Time.deltaTime * 10);

        //Geri d�n�� animasyonu bitmeden kart tekrardan hareket ettirilemez.
        DisableTouch = true;
    }
    private void HandleStory()
    {
        Debug.Log("Story Handle");
        StoryCard randomStoryCard = GetRandomStoryFromJsonFile();
        StartCoroutine(TypeMainStory(randomStoryCard.StoryContent));
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = randomStoryCard.StoryTellerName;
        gameObject.GetComponent<Image>().sprite = RebelCharactersImagesDatabase.Instance.GetCharacterSpriteWithName(randomStoryCard.StoryTellerName);
        gameObject.GetComponent<Image>().color = Color.white;
        _currentHandlingStoryCard = randomStoryCard;
    }
    private StoryCard GetRandomStoryFromJsonFile()
    {
        List<StoryCard> stories = _storiesHandler.LoadStoriesList();
        Debug.Log(stories.Count);
        StoryCard randomCard = stories[Random.Range(0, stories.Count)];
        return randomCard;
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
    private void RefreshStatsUi()
    {
        Stats.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = RebelStatsManager.Instance.PrivacyCount + "";
        Stats.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = RebelStatsManager.Instance.AggressivenessCount + "";
        Stats.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = RebelStatsManager.Instance.LawCount + "";
        Stats.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = RebelStatsManager.Instance.RoyaltyCount + "";
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
