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
    public bool AnimationControl;
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
    private Canvas canvas;
    private TextMeshProUGUI mainStory;
    private TextMeshProUGUI choices;

    private StoriesHandler _storiesHandler;
    private StoryCard _currentHandlingStoryCard;
    private RebelOption _currentSelectedOption;

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

        mainStory = TopStory.GetComponent<TextMeshProUGUI>();
        choices = GameObject.Find("Choices").GetComponent<TextMeshProUGUI>();

        _storiesHandler = FindObjectOfType<StoriesHandler>();

        List<StoryCard> stories = _storiesHandler.LoadStoriesList();
        HandleStory(stories[8]);

    }

    private void Update()
    {
        CardMovementUpdate();
        CardVisualUpdate();
        HandleArrivalAndAnimationControl();
        CardPuttingBackAnimation();
    }

    //Kart hareketleri güncellernir.
    private void CardMovementUpdate()
    {
        animationLeftTime -= Time.deltaTime;
        resetLeftTime -= Time.deltaTime;

        if (AnimationControl == true)
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

    //Kardýn seçimlerde alpha deðerinin deðiþmesini saðlar.
    private void CardVisualUpdate()
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alpha = math.abs(gameObject.transform.position.x - 297) * 1 / 100f;
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alpha = math.abs(gameObject.transform.position.x - 297) * 1 / 100f;
    }

    //Kardýn hedefe varýp varmadýðý kontrol edilir, seçim yapýlýp kart býrakýldýðýnda
    //oynatýlan animasyonun bitmesini bekler ve bitince kardý diðer event için sýfýrlar.
    private void HandleArrivalAndAnimationControl()
    {
        if (math.abs(gameObject.transform.position.x - targetPosition.x) <= 100 && math.abs(gameObject.transform.position.y - targetPosition.y) <= 100 && animationLeftTime <= 0 && AnimationControl == true)
        {  
            if (_currentSelectedOption.StoryEventContainer != null)
            {
                StoryEventContainer currentEventContainer = StoryEventHandler.Instance.GetEventWithIndex(_currentSelectedOption.StoryEventContainer.StoryID);

                StoryEventHandler.Instance.ExecuteEvent(currentEventContainer.StoryID);

                if (currentEventContainer.ContinueRandomStoryHandlingAfterEvent)
                {
                    HandleStory();
                }
            }
            else
            {
                HandleStory();
            }
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
        //Bu sola ilk kaydýrdýðýmda gelen þey lütfen yazýn
        if (isInLeft)
        {
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _currentHandlingStoryCard.OptionA.OptionName;
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

            RebelOption viewingOption = _currentHandlingStoryCard.OptionA;

            SetGonnaChanges(viewingOption);

        }
        else if (isInRight)
        {
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _currentHandlingStoryCard.OptionB.OptionName;
            gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

            RebelOption viewingOption = _currentHandlingStoryCard.OptionB;

            SetGonnaChanges(viewingOption);

        }
    }
    private void SetGonnaChanges(RebelOption viewingOption)
    {
        GonnaChange[0].SetActive(Mathf.Abs(viewingOption.Privacy) > 0);
        GonnaChange[1].SetActive(Mathf.Abs(viewingOption.Aggressiveness) > 0);
        GonnaChange[2].SetActive(Mathf.Abs(viewingOption.Law) > 0);
        GonnaChange[3].SetActive(Mathf.Abs(viewingOption.Royalty) > 0);
    }
    private void ResetGonnaChanges()
    {
        for (int i = 0; i < 4; i++)
        {
            GonnaChange[i].gameObject.SetActive(false);
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
            _currentSelectedOption = selectedOption;
            
            ChangeStatsAfterSelection(selectedOption);
            RefreshStatsUi();
            CardAnimation();

        }
        else if (chooseRight && isInRight)
        {
            selectedOption = _currentHandlingStoryCard.OptionB;
            _currentSelectedOption = selectedOption;
            
            ChangeStatsAfterSelection(selectedOption);
            RefreshStatsUi();
            CardAnimation();

        }
        else
        {
            Debug.Log("PutBack");
            PutBackCard();
        }


        ResetGonnaChanges();
    }
    private void ChangeStatsAfterSelection(RebelOption selectedOption)
    {
        RebelStatsManager.Instance.AddPrivacy(selectedOption.Privacy);
        RebelStatsManager.Instance.AddAggressiveness(selectedOption.Aggressiveness);
        RebelStatsManager.Instance.AddLawCount(selectedOption.Law);
        RebelStatsManager.Instance.AddRoyaltyCount(selectedOption.Royalty);
    }
    public void ResetCardCanvas()
    {
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
    //Kart býrakýldýðýnda yerine yumaþak þekilde geçiþ yapar.
    private void CardPuttingBackAnimation()
    {
        Vector2 targetPosition = Vector2.zero / canvas.scaleFactor;
        Quaternion targetQuaternion = Quaternion.Euler(0, 0, 0);

        if (Vector2.Distance(card.anchoredPosition, targetPosition) < 4f)
        {
            DisableTouch = false;
            isCardPuttingBack = false;
        }

        if (!isCardPuttingBack)
            return;

        //10 deðeri keyfidir, bir deðiþkene atanabilir.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * 10);
        card.anchoredPosition = Vector2.Lerp(card.anchoredPosition, targetPosition, Time.deltaTime * 10);

        //Geri dönüþ animasyonu bitmeden kart tekrardan hareket ettirilemez.
        DisableTouch = true;
    }
    public void HandleStory()
    {
        ResetCardCanvas();
        StoryCard randomStoryCard = GetRandomStoryFromJsonFile();
        CardUiUpdateAfterStoryHandling(randomStoryCard);
        _currentHandlingStoryCard = randomStoryCard;
        AnimationControl = false;

    }
    public void HandleStory(StoryCard card)
    {
        ResetCardCanvas();
        CardUiUpdateAfterStoryHandling(card);
        _currentHandlingStoryCard = card;
        AnimationControl = false;
    }
    private void CardUiUpdateAfterStoryHandling(StoryCard card)
    {
        StartCoroutine(TypeMainStory(card.StoryContent));
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = card.StoryTellerName;
        gameObject.GetComponent<Image>().sprite = RebelCharactersImagesDatabase.Instance.GetCharacterSpriteWithName(card.StoryTellerName);
        gameObject.GetComponent<Image>().color = Color.white;
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
        AnimationControl = true;
    }

    private void RandomIndex()
    {
        index = Random.value;
    }
    public void RefreshStatsUi()
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
