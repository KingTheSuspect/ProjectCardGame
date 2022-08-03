using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SlideCards : MonoBehaviour
{
    public float writingSpeed;
    public float rotateDegreeLeft;
    public float rotateDegreeRight;
    public float rotateSpeed;

    public GameObject container;
    

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
        card = container.transform.GetChild(0).GetComponent<RectTransform>();
        rb = GetComponent<Rigidbody2D>();
        cardPrefab = Resources.Load("Kart") as GameObject;

        mainStory = container.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        choices = container.transform.GetChild(1).GetComponent<TextMeshProUGUI>();


        storydata = FindObjectOfType<StoryData>().story;
        story = new Story(storydata.text);


        StartCoroutine(TypeMainStory(story.Continue()));
    }


    void Update()
    {
        HandleDrag();
    }


    void HandleDrag()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Touch began
            if (touch.phase == TouchPhase.Began)
            {
                
            }

            // Touch Moved
            else if (touch.phase == TouchPhase.Moved)
            {
                // Rotate card according to center and update card position

                float clamp = Mathf.Clamp(0f, rotateDegreeRight, rotateDegreeLeft);
                if (card.anchoredPosition.x >= 0 && card.anchoredPosition.x <= 0)
                {
                    card.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), (rotateSpeed * 2) * Time.deltaTime);
                }
                

                else if (card.anchoredPosition.x < -10)
                {
                    card.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rotateDegreeLeft), rotateSpeed * Time.deltaTime);
                }
                else if (card.anchoredPosition.x > 10)
                {
                    card.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rotateDegreeRight), rotateSpeed * Time.deltaTime);
                }

                // Inform the Player to the choose
                card.anchoredPosition += new Vector2(touch.deltaPosition.x, 0f) / canvas.scaleFactor;






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

            // Touch Ended
            else if (touch.phase == TouchPhase.Ended)
            {
                if (story.canContinue)
                {
                    if (DisableTouch == false)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                        card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                        if (isInLeft && story.currentChoices.Count == 0)
                        {
                            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                            card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                            choices.text = "";
                            StartCoroutine(TypeMainStory(story.Continue()));
                        }
                        else if (isInRight && story.currentChoices.Count == 0)
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
                    else if (chooseLeft)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                        card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                        choices.text = "";
                        story.ChooseChoiceIndex(0);
                        StartCoroutine(TypeMainStory(story.Continue()));
                    }
                    else if (chooseRight)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                        card.anchoredPosition = new Vector2(0f, 0f) / canvas.scaleFactor;
                        choices.text = "";
                        story.ChooseChoiceIndex(1);
                        StartCoroutine(TypeMainStory(story.Continue()));
                    }
                }
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
        else if (collision.CompareTag("ChooseRight")) chooseRight = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInLeft = false;
        isInRight = false;

        chooseLeft = false;
        chooseRight = false;
    }


}
