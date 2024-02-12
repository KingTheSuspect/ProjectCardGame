using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class StoriesHandlerWindow : EditorWindow
{
    private TermManager.TermType _termType;
    private int _termIndex = 0;

    private StoriesHandler _storiesHandler;
    private string storyTellerNameInput;
    private string storyContentInput;
    private string storyOptionA;
    private string storyOptionB;
    private int privacyInputA;
    private int aggressivenessInputA;
    private int lawInputA;
    private int royaltyInputA;
    private int privacyInputB;
    private int aggressivenessInputB;
    private int lawInputB;
    private int royaltyInputB;
    private bool ignoreRandomization;

    private StoryEventContainer optionAEvent;
    private StoryEventContainer optionBEvent;

    private RebelMainQuestModifierType rebelMainQuestModifierTypeOptionA;
    private RebelMainQuestModifierType rebelMainQuestModifierTypeOptionB;


    private Vector2 scrollPosition; // Kaydýrma iþlemi için
    private string searchQuery = ""; // Arama sorgusu
    private int selectedStoryIndex = -1; // Seçilen hikaye indeksi

    // Seçili hikayenin yazý stilleri
    private GUIStyle selectedStoryStyle = new GUIStyle();

    [MenuItem("Window/Stories Handler")]
    public static void ShowWindow()
    {
        GetWindow<StoriesHandlerWindow>("Stories Handler");
    }

    private void OnEnable()
    {
        _storiesHandler = FindObjectOfType<StoriesHandler>();

        // Seçilen hikaye stilini ayarla (yeþil renk)
        selectedStoryStyle.normal.textColor = Color.green;

    }

    private void OnGUI()
    {
        //Enum deðerlerine göre indexler atanýr
        switch (_termType)
        {
            case TermManager.TermType.Kurulus:
                _termIndex = 0;
                break;
            case TermManager.TermType.Fetret:
                _termIndex = 1;
                break;
            case TermManager.TermType.Lale:
                _termIndex = 2;
                break;
            case TermManager.TermType.Cokus:
                _termIndex = 3;
                break;
        }

        TermVariablesContainer currentTermContainer = null;

        if (TermManager.Instance != null) 
        {
            currentTermContainer = TermManager.Instance.GetTermVairablesContainerWithIndex(_termIndex);

            if (currentTermContainer == null)
            {
                Debug.LogError("Ýndexe ait dönem kontainerý bulunamadý, eklenmemiþ olabilir.");
            }
        }

        GUILayout.Label("Story Card Editor", EditorStyles.boldLabel);

        storyTellerNameInput = EditorGUILayout.TextField("Story Teller Name", storyTellerNameInput);

        EditorGUILayout.LabelField("Story Content");
        storyContentInput = EditorGUILayout.TextArea(storyContentInput, GUILayout.Height(100));

        _termType = (TermManager.TermType)EditorGUILayout.EnumPopup("Dönem", _termType);

        // Option A ve Option B alanlarýný ekleyin
        storyOptionA = EditorGUILayout.TextField("Sol Seçenek", storyOptionA);
        privacyInputA = EditorGUILayout.IntField(currentTermContainer.Variable1Name, privacyInputA);
        aggressivenessInputA = EditorGUILayout.IntField(currentTermContainer.Variable2Name, aggressivenessInputA);
        lawInputA = EditorGUILayout.IntField(currentTermContainer.Variable3Name, lawInputA);
        royaltyInputA = EditorGUILayout.IntField(currentTermContainer.Variable4Name, royaltyInputA);
        rebelMainQuestModifierTypeOptionA = (RebelMainQuestModifierType)EditorGUILayout.EnumPopup("Main Quest",rebelMainQuestModifierTypeOptionA);
        optionAEvent = EditorGUILayout.ObjectField("Sol Seçenek Event", optionAEvent, typeof(StoryEventContainer), true) as StoryEventContainer;

        storyOptionB = EditorGUILayout.TextField("Sað Seçenek", storyOptionB);
        privacyInputB = EditorGUILayout.IntField(currentTermContainer.Variable1Name, privacyInputB);
        aggressivenessInputB = EditorGUILayout.IntField(currentTermContainer.Variable2Name, aggressivenessInputB);
        lawInputB = EditorGUILayout.IntField(currentTermContainer.Variable3Name, lawInputB);
        royaltyInputB = EditorGUILayout.IntField(currentTermContainer.Variable4Name, royaltyInputB);
        rebelMainQuestModifierTypeOptionB = (RebelMainQuestModifierType)EditorGUILayout.EnumPopup("Main Quest", rebelMainQuestModifierTypeOptionB);
        optionBEvent = EditorGUILayout.ObjectField("Sað Seçenek Event", optionBEvent, typeof(StoryEventContainer), true) as StoryEventContainer;

        ignoreRandomization = EditorGUILayout.Toggle("Dont Get With Random : ", ignoreRandomization);


        if (GUILayout.Button("Add Story Card"))
        {
            StoryCard card = new StoryCard();
            card.StoryTellerName = storyTellerNameInput;
            card.StoryContent = storyContentInput;
            card.IgnoreRandomization = ignoreRandomization;
            card.OptionA = new RebelOption
            {
                OptionName = storyOptionA,
                Privacy = privacyInputA,
                Aggressiveness = aggressivenessInputA,
                Law = lawInputA,
                Royalty = royaltyInputA,
                MainQuestModifierType = rebelMainQuestModifierTypeOptionA,
                StoryEventContainer = optionAEvent
            };
            card.OptionB = new RebelOption
            {
                OptionName = storyOptionB,
                Privacy = privacyInputB,
                Aggressiveness = aggressivenessInputB,
                Law = lawInputB,
                Royalty = royaltyInputB,
                MainQuestModifierType = rebelMainQuestModifierTypeOptionB,
                StoryEventContainer = optionBEvent
            };

            if (_storiesHandler != null)
            {
                int storyCount = _storiesHandler.LoadStoriesList().Count;

                if (card.OptionA.StoryEventContainer != null)
                    card.OptionA.StoryEventContainer.StoryID = storyCount + "A";
                if (card.OptionB.StoryEventContainer != null)
                    card.OptionB.StoryEventContainer.StoryID = storyCount + "B";

                _storiesHandler.AddNewStoryCard(card);
            }
            else
            {
                Debug.LogWarning("Yeni bir kart eklemeye çalýþtýn ama StoriesHandler sahnede yok knk");
            }
        }
        if (GUILayout.Button("Update Selected Story"))
        {
            List<StoryCard> stories = _storiesHandler.LoadStoriesList();
            StoryCard card = new StoryCard();
            card.StoryTellerName = storyTellerNameInput;
            card.StoryContent = storyContentInput;
            card.IgnoreRandomization = ignoreRandomization;

            card.OptionA = new RebelOption
            {
                OptionName = storyOptionA,
                Privacy = privacyInputA,
                Aggressiveness = aggressivenessInputA,
                Law = lawInputA,
                Royalty = royaltyInputA,
                MainQuestModifierType = rebelMainQuestModifierTypeOptionA,
                StoryEventContainer = optionAEvent
            };
            card.OptionB = new RebelOption
            {
                OptionName = storyOptionB,
                Privacy = privacyInputB,
                Aggressiveness = aggressivenessInputB,
                Law = lawInputB,
                Royalty = royaltyInputB,
                MainQuestModifierType = rebelMainQuestModifierTypeOptionB,
                StoryEventContainer = optionBEvent
            };

            if (_storiesHandler != null)
            {

                if (card.OptionA.StoryEventContainer != null)
                    card.OptionA.StoryEventContainer.StoryID = _storiesHandler.GetIndexWithContent(card) + "A";
                if (card.OptionB.StoryEventContainer != null)
                    card.OptionB.StoryEventContainer.StoryID = _storiesHandler.GetIndexWithContent(card) + "B";
            }
            else
            {
                Debug.LogWarning("Kartý güncelleyemezsin çünkü sahnede storieshandler yok dostum.");
            }

            stories[selectedStoryIndex] = card;
            _storiesHandler?.SaveStoriesListToFile(stories);
        }
        if (GUILayout.Button("Update All ID"))
        {
            if (_storiesHandler != null)
            {
                List<StoryCard> stories = _storiesHandler.LoadStoriesList();

                for (int i = 0; i < stories.Count; i++)
                {
                    if (stories[i].OptionA.StoryEventContainer != null)
                        stories[i].OptionA.StoryEventContainer.StoryID = i + "A";
                    if (stories[i].OptionB.StoryEventContainer != null)
                        stories[i].OptionB.StoryEventContainer.StoryID = i + "B";
                }

                // Güncellenmiþ hikayeleri dosyaya kaydet
                _storiesHandler.SaveStoriesListToFile(stories);
            }
        }

        // Arama kutusu
        searchQuery = EditorGUILayout.TextField("Search", searchQuery);

        // Scrollview'ý baþlat
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Tablo baþlýklarý
        GUILayout.BeginHorizontal();
        GUILayout.Label("Story Index", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("Story Teller Name", EditorStyles.boldLabel, GUILayout.Width(200));
        GUILayout.Space(50); // Diðer sütunlarýn geniþliði
        EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel, GUILayout.Width(100));
        GUILayout.EndHorizontal();

        // Hikaye kartlarýný listele
        if (_storiesHandler != null)
        {
            List<StoryCard> stories = _storiesHandler.LoadStoriesList();
            for (int i = 0; i < stories.Count; i++)
            {
                // Arama sorgusu ile karþýlaþtýrma yap
                if (string.IsNullOrEmpty(searchQuery) || stories[i].StoryContent.ToLower().Contains(searchQuery.ToLower()))
                {
                    GUILayout.BeginHorizontal();

                    // Seçilen hikayenin yazý rengini ayarla
                    if (i == selectedStoryIndex)
                    {
                        GUILayout.Label(i.ToString(), selectedStoryStyle, GUILayout.Width(100));
                        EditorGUILayout.LabelField(stories[i].StoryTellerName, selectedStoryStyle, GUILayout.Width(200));
                    }
                    else
                    {
                        GUILayout.Label(i.ToString(), GUILayout.Width(100));
                        EditorGUILayout.LabelField(stories[i].StoryTellerName, GUILayout.Width(200));
                    }

                    if (GUILayout.Button("Delete", GUILayout.Width(100)))
                    {
                        _storiesHandler.DeleteStoryWithIndex(i);
                    }
                    if (GUILayout.Button("Select", GUILayout.Width(100)))
                    {
                        // Seçilen hikayenin indeksini güncelle
                        selectedStoryIndex = i;

                        // Seçilen hikaye verilerini giriþ alanlarýna aktar
                        storyTellerNameInput = stories[i].StoryTellerName;
                        storyContentInput = stories[i].StoryContent;
                        storyOptionA = stories[i].OptionA.OptionName;
                        storyOptionB = stories[i].OptionB.OptionName;
                        privacyInputA = stories[i].OptionA.Privacy;
                        aggressivenessInputA = stories[i].OptionA.Aggressiveness;
                        lawInputA = stories[i].OptionA.Law;
                        royaltyInputA = stories[i].OptionA.Royalty;
                        rebelMainQuestModifierTypeOptionA = stories[i].OptionA.MainQuestModifierType;
                        rebelMainQuestModifierTypeOptionB = stories[i].OptionB.MainQuestModifierType;
                        optionAEvent = stories[i].OptionA.StoryEventContainer;
                        optionBEvent = stories[i].OptionB.StoryEventContainer;
                        ignoreRandomization = stories[i].IgnoreRandomization;
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }

        // Scrollview'ý kapat
        EditorGUILayout.EndScrollView();
    }
}
