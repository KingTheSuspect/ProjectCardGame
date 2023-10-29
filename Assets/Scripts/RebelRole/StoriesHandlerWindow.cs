using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class StoriesHandlerWindow : EditorWindow
{
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
        GUILayout.Label("Story Card Editor", EditorStyles.boldLabel);

        storyTellerNameInput = EditorGUILayout.TextField("Story Teller Name", storyTellerNameInput);

        EditorGUILayout.LabelField("Story Content");
        storyContentInput = EditorGUILayout.TextArea(storyContentInput, GUILayout.Height(100));

        // Option A ve Option B alanlarýný ekleyin
        storyOptionA = EditorGUILayout.TextField("Option A Name", storyOptionA);
        privacyInputA = EditorGUILayout.IntField("Privacy", privacyInputA);
        aggressivenessInputA = EditorGUILayout.IntField("Aggressiveness", aggressivenessInputA);
        lawInputA = EditorGUILayout.IntField("Law", lawInputA);
        royaltyInputA = EditorGUILayout.IntField("Royalty", royaltyInputA);
        rebelMainQuestModifierTypeOptionA = (RebelMainQuestModifierType)EditorGUILayout.EnumPopup("Main Quest",rebelMainQuestModifierTypeOptionA);
        optionAEvent = EditorGUILayout.ObjectField("Option A Event", optionAEvent, typeof(StoryEventContainer), true) as StoryEventContainer;

        storyOptionB = EditorGUILayout.TextField("Option B Name", storyOptionB);
        privacyInputB = EditorGUILayout.IntField("Privacy", privacyInputB);
        aggressivenessInputB = EditorGUILayout.IntField("Aggressiveness", aggressivenessInputB);
        lawInputB = EditorGUILayout.IntField("Law", lawInputB);
        royaltyInputB = EditorGUILayout.IntField("Royalty", royaltyInputB);
        rebelMainQuestModifierTypeOptionB = (RebelMainQuestModifierType)EditorGUILayout.EnumPopup("Main Quest", rebelMainQuestModifierTypeOptionB);
        optionBEvent = EditorGUILayout.ObjectField("Option B Event", optionBEvent, typeof(StoryEventContainer), true) as StoryEventContainer;



        if (GUILayout.Button("Add Story Card"))
        {
            StoryCard card = new StoryCard();
            card.StoryTellerName = storyTellerNameInput;
            card.StoryContent = storyContentInput;
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
                card.OptionA.StoryEventContainer.StoryID = storyCount;
                card.OptionB.StoryEventContainer.StoryID = storyCount;

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
            stories[selectedStoryIndex] = card;
            _storiesHandler?.SaveStoriesListToFile(stories);
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
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }

        // Scrollview'ý kapat
        EditorGUILayout.EndScrollView();
    }
}
