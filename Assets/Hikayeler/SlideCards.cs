using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEditor.UIElements;

public class SlideCards : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{


    private float move_aim_x = 0;
    [SerializeField] float touch_movement_sensivity;
    float range = 1;
    Vector3 beganmove;



    public float writingSpeed;
    public float rotateDegreeLeft;
    public float rotateDegreeRight;
    public float rotateSpeed;

    public TextAsset text;

    private Story story;
    bool isInLeft;
    bool isInRight;
    bool chooseLeft;
    bool chooseRight;
    bool DisableTouch;

    RectTransform card;
    GameObject cardPrefab;
    Rigidbody2D rb;
    Canvas canvas;
    TextMeshProUGUI mainStory;
    TextMeshProUGUI choices;
    TextAsset storydata;
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


        float currentPosition = pointerEventData.delta.x / Screen.width;

        move_aim_x += touch_movement_sensivity * currentPosition;
        move_aim_x = Mathf.Clamp(move_aim_x, -range / 2f, range / 2f);

        transform.rotation = Quaternion.Euler(0f, 0f, move_aim_x * rotateSpeed);


        // Inform the Player to the choose
        card.anchoredPosition += pointerEventData.delta / canvas.scaleFactor;



        // Show choice text 0 when holding card in the left
        if (isInLeft && story.currentChoices.Count > 0)
        {
            choices.text = story.currentChoices[0].text;
        }

        // Show choice text 1 when holding card in the right
        else if (isInRight && story.currentChoices.Count > 0)
        {
            choices.text = story.currentChoices[1].text;
        }
        else
        {
            choices.text = "";
        }



        if (story.canContinue)
        {
            if (DisableTouch == false)
            {
                if (isInLeft && story.currentChoices.Count == 0)
                {
                    choices.text = "Continue";
                }
                else if (isInRight && story.currentChoices.Count == 0)
                {
                    choices.text = "Continue";
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {

    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (story.canContinue)
        {
            if (DisableTouch == false)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                if (chooseLeft && isInLeft && story.currentChoices.Count == 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                    choices.text = "";
                    StartCoroutine(TypeMainStory(story.Continue()));
                }
                else if (chooseRight && isInRight && story.currentChoices.Count == 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                    choices.text = "";
                    StartCoroutine(TypeMainStory(story.Continue()));
                }
            }
        }

        else
        {
            if (!chooseLeft && !chooseRight)
            {

                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
            }
            if (chooseLeft)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                choices.text = "";
                story.ChooseChoiceIndex(0);
                StartCoroutine(TypeMainStory(story.Continue()));
            }
            if (chooseRight)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                choices.text = "";
                story.ChooseChoiceIndex(1);
                StartCoroutine(TypeMainStory(story.Continue()));
            }
        }
    }







    void HandleStory()
    {
        /*GameObject card = Instantiate(cardPrefab, cardPrefab.transform.position, cardPrefab.transform.rotation);
        GameObject card2 = Instantiate(cardPrefab, cardPrefab.transform.position, cardPrefab.transform.rotation);

        card.name = "Kart";
        card2.name = "Kart 2";

        card.transform.SetParent(container.transform, false);
        card2.transform.SetParent(container.transform, false);*/

    }


    IEnumerator TypeMainStory(string sentence)
    {
        DisableTouch = true;
        if (DisableTouch)
        {
            mainStory.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                mainStory.text += letter;
                yield return new WaitForSeconds(writingSpeed);
            }
            mainStory.text += "...";

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
        isInLeft = false;
        isInRight = false;

        chooseLeft = false;
        chooseRight = false;
    }


}
