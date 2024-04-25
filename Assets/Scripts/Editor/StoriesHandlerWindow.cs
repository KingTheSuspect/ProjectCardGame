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


    private Vector2 scrollPosition; // Kayd�rma i�lemi i�in
    private string searchQuery = ""; // Arama sorgusu
    private int selectedStoryIndex = -1; // Se�ilen hikaye indeksi

    // Se�ili hikayenin yaz� stilleri
    private GUIStyle selectedStoryStyle = new GUIStyle();

    [MenuItem("Window/Stories Handler")]
    public static void ShowWindow()
    {
        GetWindow<StoriesHandlerWindow>("Stories Handler");
    }

    private void OnEnable()
    {
        _storiesHandler = FindObjectOfType<StoriesHandler>();
//SetTerm();
        // Se�ilen hikaye stilini ayarla (ye�il renk)
        selectedStoryStyle.normal.textColor = Color.green;
    }

    private void OnGUI()
    {

      // SetTerm();

        //Enum de�erlerine g�re indexler atan�r
        switch (_termType)
        {
            case TermManager.TermType.KurulusAra:
                _termIndex = 0;
                _storiesHandler.CurrentSelectedStoriesListPath = StoriesHandler.KurulusAraStoriesPath;
                break;
            case TermManager.TermType.Kurulus:
                _termIndex = 1;
                _storiesHandler.CurrentSelectedStoriesListPath = StoriesHandler.KurulusListPath;
                break;
            case TermManager.TermType.FetretBas:
                _termIndex = 2;
                _storiesHandler.CurrentSelectedStoriesListPath = StoriesHandler.FetretBasStoriesPath;
                break;
            case TermManager.TermType.Fetret:
                _termIndex = 3;
                _storiesHandler.CurrentSelectedStoriesListPath = StoriesHandler.FetretStoriesPath;
                break;
            case TermManager.TermType.LaleBas:
                _termIndex = 4;
                _storiesHandler.CurrentSelectedStoriesListPath = StoriesHandler.LaleBasStoriesPath;
                break;
            case TermManager.TermType.Lale:
                _termIndex = 5;
                _storiesHandler.CurrentSelectedStoriesListPath = StoriesHandler.LaleStoriesPath;
                break;
            case TermManager.TermType.CokusBas:
                _termIndex = 6;
                _storiesHandler.CurrentSelectedStoriesListPath = StoriesHandler.CokusBasStoriesPath;
                break;
            case TermManager.TermType.Cokus:
                _termIndex = 7;
                _storiesHandler.CurrentSelectedStoriesListPath = StoriesHandler.CokusStoriesPath;
                break;

        }

        TermVariablesContainer currentTermContainer = null;

        if (TermManager.Instance != null) 
        {
            currentTermContainer = TermManager.Instance.GetTermVairablesContainerWithIndex(_termIndex);

            if (currentTermContainer == null)
            {
                Debug.LogError("�ndexe ait d�nem kontainer� bulunamad�, eklenmemi� olabilir.");
            }
        }

        GUILayout.Label("Story Card Editor", EditorStyles.boldLabel);

        storyTellerNameInput = EditorGUILayout.TextField("Story Teller Name", storyTellerNameInput);

        EditorGUILayout.LabelField("Story Content");
        storyContentInput = EditorGUILayout.TextArea(storyContentInput, GUILayout.Height(100));

        _termType = (TermManager.TermType)EditorGUILayout.EnumPopup("D�nem", _termType);

        // Option A ve Option B alanlar�n� ekleyin
        storyOptionA = EditorGUILayout.TextField("Sol Se�enek", storyOptionA);
        privacyInputA = EditorGUILayout.IntField(currentTermContainer.Variable1Name, privacyInputA);
        aggressivenessInputA = EditorGUILayout.IntField(currentTermContainer.Variable2Name, aggressivenessInputA);
        lawInputA = EditorGUILayout.IntField(currentTermContainer.Variable3Name, lawInputA);
        royaltyInputA = EditorGUILayout.IntField(currentTermContainer.Variable4Name, royaltyInputA);
        //rebelMainQuestModifierTypeOptionA = (RebelMainQuestModifierType)EditorGUILayout.EnumPopup("Main Quest",rebelMainQuestModifierTypeOptionA);
        optionAEvent = EditorGUILayout.ObjectField("Sol Se�enek Event", optionAEvent, typeof(StoryEventContainer), true) as StoryEventContainer;
        EditorGUILayout.Space();
        storyOptionB = EditorGUILayout.TextField("Sa� Se�enek", storyOptionB);
        privacyInputB = EditorGUILayout.IntField(currentTermContainer.Variable1Name, privacyInputB);
        aggressivenessInputB = EditorGUILayout.IntField(currentTermContainer.Variable2Name, aggressivenessInputB);
        lawInputB = EditorGUILayout.IntField(currentTermContainer.Variable3Name, lawInputB);
        royaltyInputB = EditorGUILayout.IntField(currentTermContainer.Variable4Name, royaltyInputB);
        //rebelMainQuestModifierTypeOptionB = (RebelMainQuestModifierType)EditorGUILayout.EnumPopup("Main Quest", rebelMainQuestModifierTypeOptionB);
        optionBEvent = EditorGUILayout.ObjectField("Sa� Se�enek Event", optionBEvent, typeof(StoryEventContainer), true) as StoryEventContainer;

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
                Debug.LogWarning("Yeni bir kart eklemeye �al��t�n ama StoriesHandler sahnede yok knk");
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
                Debug.LogWarning("Kart� g�ncelleyemezsin ��nk� sahnede storieshandler yok dostum.");
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

                // G�ncellenmi� hikayeleri dosyaya kaydet
                _storiesHandler.SaveStoriesListToFile(stories);
            }
        }

        // Arama kutusu
        searchQuery = EditorGUILayout.TextField("Search", searchQuery);

        // Scrollview'� ba�lat
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Tablo ba�l�klar�
        GUILayout.BeginHorizontal();
        GUILayout.Label("Story Index", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("Story Teller Name", EditorStyles.boldLabel, GUILayout.Width(200));
        GUILayout.Space(50); // Di�er s�tunlar�n geni�li�i
        EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel, GUILayout.Width(100));
        GUILayout.EndHorizontal();

        // Hikaye kartlar�n� listele
        if (_storiesHandler != null)
        {
            List<StoryCard> stories = _storiesHandler.LoadStoriesList();
            for (int i = 0; i < stories.Count; i++)
            {
                // Arama sorgusu ile kar��la�t�rma yap
                if (string.IsNullOrEmpty(searchQuery) || stories[i].StoryContent.ToLower().Contains(searchQuery.ToLower()))
                {
                    GUILayout.BeginHorizontal();

                    // Se�ilen hikayenin yaz� rengini ayarla
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
                        // Se�ilen hikayenin indeksini g�ncelle
                        selectedStoryIndex = i;

                        // Se�ilen hikaye verilerini giri� alanlar�na aktar
                        storyTellerNameInput = stories[i].StoryTellerName;
                        storyContentInput = stories[i].StoryContent;
                        storyOptionA = stories[i].OptionA.OptionName;
                        storyOptionB = stories[i].OptionB.OptionName;
                        privacyInputA = stories[i].OptionA.Privacy;
                        aggressivenessInputA = stories[i].OptionA.Aggressiveness;
                        lawInputA = stories[i].OptionA.Law;
                        royaltyInputA = stories[i].OptionA.Royalty;
                        privacyInputB = stories[i].OptionB.Privacy;
                        aggressivenessInputB = stories[i].OptionB.Aggressiveness;
                        lawInputB = stories[i].OptionB.Law;
                        royaltyInputB = stories[i].OptionB.Royalty;
                        //rebelMainQuestModifierTypeOptionA = stories[i].OptionA.MainQuestModifierType;
                        //rebelMainQuestModifierTypeOptionB = stories[i].OptionB.MainQuestModifierType;
                        optionAEvent = stories[i].OptionA.StoryEventContainer;
                        optionBEvent = stories[i].OptionB.StoryEventContainer;
                        ignoreRandomization = stories[i].IgnoreRandomization;
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }

        // Scrollview'� kapat
        EditorGUILayout.EndScrollView();
    }
}
