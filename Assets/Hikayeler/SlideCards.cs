using Ink.Runtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public TextAsset text;

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

    private void Start()
    {
        canvas = transform.root.GetComponent<Canvas>();
        card = GetComponent<RectTransform>();
        rb = GetComponent<Rigidbody2D>();
        cardPrefab = Resources.Load("Kart") as GameObject;

        mainStory = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        choices = GameObject.Find("Choices").GetComponent<TextMeshProUGUI>();

        story = new Story(text.text);

        StartCoroutine(TypeMainStory(story.Continue()));
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (DisableTouch)
            return;

        pointerEventData.delta = new Vector2(pointerEventData.delta.x, 0);
        float currentPosition = pointerEventData.delta.x / Screen.width;
        move_aim_x += touch_movement_sensivity * currentPosition;
        move_aim_x = Mathf.Clamp(move_aim_x, -range / 2f, range / 2f);

        transform.rotation = Quaternion.Euler(0f, 0f, move_aim_x * rotateSpeed);

        // Inform the Player to the choose
        card.anchoredPosition += pointerEventData.delta / canvas.scaleFactor;

        if (story.currentChoices.Count > 0)
        {
            if (isInLeft)
                choices.text = story.currentChoices[0].text;
            else if (isInRight)
                choices.text = story.currentChoices[1].text;
        }
        else if (story.canContinue && story.currentChoices.Count == 0)
        {
            if (isInLeft)
                choices.text = "Hayýr";
            else if (isInRight)
                choices.text = "Evet";
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (DisableTouch)
            return;

        ResetCardCanvas();
        choices.text = "";
        move_aim_x = 0;

        Debug.Log("choose left:" + chooseLeft);
        Debug.Log("choose right:" + chooseRight);
        Debug.Log("story can countinue:" + story.canContinue);

        if (story.canContinue)
        {
            if (chooseLeft && isInLeft && story.currentChoices.Count == 0)
            {
                StartCoroutine(TypeMainStory(story.Continue()));
            }
            else if (chooseRight && isInRight && story.currentChoices.Count == 0)
            {
                StartCoroutine(TypeMainStory(story.Continue()));
            }
        }
        else
        {
            if (chooseLeft)
            {
                story.ChooseChoiceIndex(0);
                StartCoroutine(TypeMainStory(story.Continue()));
            }
            else if (chooseRight)
            {
                story.ChooseChoiceIndex(1);
                StartCoroutine(TypeMainStory(story.Continue()));
            }
        }
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