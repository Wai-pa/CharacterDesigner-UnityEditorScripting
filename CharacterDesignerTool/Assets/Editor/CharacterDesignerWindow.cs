using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class CharacterDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D warlockSectionTexture;
    Texture2D titanSectionTexture;
    Texture2D hunterSectionTexture;
    Texture2D warlockTexture;
    Texture2D titanTexture;
    Texture2D hunterTexture;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    Rect headerSection;
    Rect warlockSection;
    Rect titanSection;
    Rect hunterSection;
    Rect warlockIconSection;
    Rect titanIconSection;
    Rect hunterIconSection;

    GUISkin skin;

    static WarlockData warlockData;
    static TitanData titanData;
    static HunterData hunterData;

    public static WarlockData WarlockInfo { get { return warlockData; } }
    public static TitanData TitanInfo { get { return titanData; } }
    public static HunterData HunterInfo { get { return hunterData; } }

    float iconSize = 80;

    [MenuItem ("Window/Character Designer")]
    static void OpenWindow()
    {
        CharacterDesignerWindow window = (CharacterDesignerWindow)GetWindow(typeof(CharacterDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    // Similar to Start() or Awake()
    void OnEnable()
    {
        InitTextures();
        InitData();
        skin = Resources.Load<GUISkin>("GuiStyles/CharacterDesignerSkin");
    }

    public static void InitData()
    {
        warlockData = (WarlockData)ScriptableObject.CreateInstance(typeof(WarlockData));
        titanData = (TitanData)ScriptableObject.CreateInstance(typeof(TitanData));
        hunterData = (HunterData)ScriptableObject.CreateInstance(typeof(HunterData));
    }

    // Initialize Texture2D values
    void InitTextures()
    {
        // First way to define textures for editor windows
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        // Second way to define textures for editor windows
        warlockSectionTexture = Resources.Load<Texture2D>("Icons/warlockGradient");

        titanSectionTexture = Resources.Load<Texture2D>("Icons/titanGradient");

        hunterSectionTexture = Resources.Load<Texture2D>("Icons/hunterGradient");

        warlockTexture = Resources.Load<Texture2D>("Icons/warlockIcon");

        titanTexture = Resources.Load<Texture2D>("Icons/titanIcon");

        hunterTexture = Resources.Load<Texture2D>("Icons/hunterIcon");
    }

    // Similar to any Update(), but it is not called once per frame
    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawWarlockSettings();
        DrawTitanSettings();
        DrawHunterSettings();
    }

    // Defines Rect values and points textures based on Rects
    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        warlockSection.x = 0;
        warlockSection.y = 50;
        warlockSection.width = Screen.width / 3f;
        warlockSection.height = Screen.height - 50;

        warlockIconSection.x = (warlockSection.x + warlockSection.width / 2f) - iconSize / 2f;
        warlockIconSection.y = warlockSection.y + 8;
        warlockIconSection.width = iconSize;
        warlockIconSection.height = iconSize;

        titanSection.x = Screen.width / 3f;
        titanSection.y = 50;
        titanSection.width = Screen.width / 3f;
        titanSection.height = Screen.height - 50;

        titanIconSection.x = (titanSection.x + titanSection.width / 2f) - iconSize / 2f;
        titanIconSection.y = titanSection.y + 8;
        titanIconSection.width = iconSize;
        titanIconSection.height = iconSize;

        hunterSection.x = Screen.width / 3f * 2;
        hunterSection.y = 50;
        hunterSection.width = Screen.width / 3f;
        hunterSection.height = Screen.height - 50;

        hunterIconSection.x = (hunterSection.x + hunterSection.width / 2f) - iconSize / 2f;
        hunterIconSection.y = hunterSection.y + 8;
        hunterIconSection.width = iconSize;
        hunterIconSection.height = iconSize;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(warlockSection, warlockSectionTexture);
        GUI.DrawTexture(titanSection, titanSectionTexture);
        GUI.DrawTexture(hunterSection, hunterSectionTexture);
        GUI.DrawTexture(warlockIconSection, warlockTexture);
        GUI.DrawTexture(titanIconSection, titanTexture);
        GUI.DrawTexture(hunterIconSection, hunterTexture);
    }

    // Draw contents of header
    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Space(3);
        GUILayout.Label("Character Designer", skin.GetStyle("Header1"));

        GUILayout.EndArea();
    }

    // Draw contents of warlock region
    void DrawWarlockSettings()
    {
        GUILayout.BeginArea(warlockSection);

        GUILayout.Space(iconSize + 8);

        GUILayout.Label("Warlock", skin.GetStyle("Header2"));
        GUILayout.Space(1);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(6);
        GUILayout.Label("Subclass", skin.GetStyle("Field"));
        warlockData.warlockSubclass = (WarlockSubclass)EditorGUILayout.EnumPopup(warlockData.warlockSubclass);
        GUILayout.Space(3);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(3);

        switch (warlockData.warlockSubclass)
        {
            case WarlockSubclass.Dawnblade:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                warlockData.warlockDawnblade = (WarlockDawnblade)EditorGUILayout.EnumPopup(warlockData.warlockDawnblade);
                warlockData.warlockStormcaller = WarlockStormcaller.NONE;
                warlockData.warlockVoidwalker = WarlockVoidwalker.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
            case WarlockSubclass.Stormcaller:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                warlockData.warlockStormcaller = (WarlockStormcaller)EditorGUILayout.EnumPopup(warlockData.warlockStormcaller);
                warlockData.warlockDawnblade = WarlockDawnblade.NONE;
                warlockData.warlockVoidwalker = WarlockVoidwalker.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
            case WarlockSubclass.Voidwalker:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                warlockData.warlockVoidwalker = (WarlockVoidwalker)EditorGUILayout.EnumPopup(warlockData.warlockVoidwalker);
                warlockData.warlockDawnblade = WarlockDawnblade.NONE;
                warlockData.warlockStormcaller = WarlockStormcaller.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
        }

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(6);
        if(warlockData.warlockDawnblade == WarlockDawnblade.NONE && warlockData.warlockStormcaller == WarlockStormcaller.NONE && warlockData.warlockVoidwalker == WarlockVoidwalker.NONE)
        {
            
        }
        else if (GUILayout.Button("Create!", GUILayout.Height(30)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARLOCK);
        }
        GUILayout.Space(3);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    // Draw contents of titan region
    void DrawTitanSettings()
    {
        GUILayout.BeginArea(titanSection);

        GUILayout.Space(iconSize + 8);

        GUILayout.Label("Titan", skin.GetStyle("Header2"));
        GUILayout.Space(1);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(6);
        GUILayout.Label("Subclass", skin.GetStyle("Field"));
        titanData.titanSubclass = (TitanSubclass)EditorGUILayout.EnumPopup(titanData.titanSubclass);
        GUILayout.Space(3);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(3);

        switch (titanData.titanSubclass)
        {
            case TitanSubclass.Sunbreaker:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                titanData.titanSunbreaker = (TitanSunbreaker)EditorGUILayout.EnumPopup(titanData.titanSunbreaker);
                titanData.titanStriker = TitanStriker.NONE;
                titanData.titanSentinel = TitanSentinel.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
            case TitanSubclass.Striker:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                titanData.titanStriker = (TitanStriker)EditorGUILayout.EnumPopup(titanData.titanStriker);
                titanData.titanSunbreaker = TitanSunbreaker.NONE;
                titanData.titanSentinel = TitanSentinel.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
            case TitanSubclass.Sentinel:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                titanData.titanSentinel = (TitanSentinel)EditorGUILayout.EnumPopup(titanData.titanSentinel);
                titanData.titanSunbreaker = TitanSunbreaker.NONE;
                titanData.titanStriker = TitanStriker.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
        }

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(6);
        if(titanData.titanSunbreaker == TitanSunbreaker.NONE && titanData.titanStriker == TitanStriker.NONE && titanData.titanSentinel == TitanSentinel.NONE)
        {
            
        }
        else if (GUILayout.Button("Create!", GUILayout.Height(30)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.TITAN);
        }
        GUILayout.Space(3);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    // Draw contents of hunter region
    void DrawHunterSettings()
    {
        GUILayout.BeginArea(hunterSection);

        GUILayout.Space(iconSize + 8);

        GUILayout.Label("Hunter", skin.GetStyle("Header2"));
        GUILayout.Space(1);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(6);
        GUILayout.Label("Subclass", skin.GetStyle("Field"));
        hunterData.hunterSubclass = (HunterSubclass)EditorGUILayout.EnumPopup(hunterData.hunterSubclass);
        GUILayout.Space(3);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(3);

        switch (hunterData.hunterSubclass)
        {
            case HunterSubclass.Gunslinger:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                hunterData.hunterGunslinger = (HunterGunslinger)EditorGUILayout.EnumPopup(hunterData.hunterGunslinger);
                hunterData.hunterArcstrider = HunterArcstrider.NONE;
                hunterData.hunterNightstalker = HunterNightstalker.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
            case HunterSubclass.Arcstrider:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                hunterData.hunterArcstrider = (HunterArcstrider)EditorGUILayout.EnumPopup(hunterData.hunterArcstrider);
                hunterData.hunterGunslinger = HunterGunslinger.NONE;
                hunterData.hunterNightstalker = HunterNightstalker.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
            case HunterSubclass.NightStalker:
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(6);
                GUILayout.Label("Tree", skin.GetStyle("Field"));
                hunterData.hunterNightstalker = (HunterNightstalker)EditorGUILayout.EnumPopup(hunterData.hunterNightstalker);
                hunterData.hunterGunslinger = HunterGunslinger.NONE;
                hunterData.hunterArcstrider = HunterArcstrider.NONE;
                GUILayout.Space(3);
                EditorGUILayout.EndHorizontal();
                break;
        }

        GUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(6);
        if(hunterData.hunterGunslinger == HunterGunslinger.NONE && hunterData.hunterArcstrider == HunterArcstrider.NONE && hunterData.hunterNightstalker == HunterNightstalker.NONE)
        {
            
        }
        else if (GUILayout.Button("Create!", GUILayout.Height(30)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.HUNTER);
        }
        GUILayout.Space(3);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}

public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        WARLOCK,
        TITAN,
        HUNTER
    }

    static SettingsType dataSetting;
    static GeneralSettings window;
    GUISkin skin;

    public static void OpenWindow(SettingsType setting)
    {
        dataSetting = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(300, 250);
        window.Show();
    }

    private void OnEnable()
    {
        skin = Resources.Load<GUISkin>("GuiStyles/CharacterDesignerSkin");
    }

    void OnGUI()
    {
        switch (dataSetting)
        {
            case SettingsType.WARLOCK:
                DrawSettings(CharacterDesignerWindow.WarlockInfo);
                break;
            case SettingsType.TITAN:
                DrawSettings(CharacterDesignerWindow.TitanInfo);
                break;
            case SettingsType.HUNTER:
                DrawSettings(CharacterDesignerWindow.HunterInfo);
                break;
        }
    }

    void DrawSettings(CharacterData charData)
    {
        GUILayout.Space(3);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(6);
        GUILayout.Label(dataSetting.ToString(), skin.GetStyle("Header3"));
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(1);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        charData.name = EditorGUILayout.TextField(charData.name);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power");
        charData.power = EditorGUILayout.IntField(charData.power);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mobility");
        charData.mobility = (int)EditorGUILayout.Slider(charData.mobility, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Resistance");
        charData.resistance = (int)EditorGUILayout.Slider(charData.resistance, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Recovery");
        charData.recovery = (int)EditorGUILayout.Slider(charData.recovery, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Discipline");
        charData.discipline = (int)EditorGUILayout.Slider(charData.discipline, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Intellect");
        charData.intellect = (int)EditorGUILayout.Slider(charData.intellect, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strength");
        charData.strength = (int)EditorGUILayout.Slider(charData.strength, 0, 100);
        EditorGUILayout.EndHorizontal();

        if (charData.prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Warning);
        }

        else if (charData.name == null || charData.name.Length < 1)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created.", MessageType.Warning);
        }

        else if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
        {
            SaveCharacterData();
            window.Close();
        }
    }

    void SaveCharacterData()
    {
        string prefabPath; // Path to the prefab
        string newPrefabPath = "Assets/prefabs/characters/";
        string dataPath = "Assets/resources/characterData/data/";

        switch (dataSetting)
        {
            case SettingsType.WARLOCK:
                // create the .asset file
                dataPath += "Warlock/" + CharacterDesignerWindow.WarlockInfo.name + ".asset";
                AssetDatabase.CreateAsset(CharacterDesignerWindow.WarlockInfo, dataPath);

                newPrefabPath += "Warlock/" + CharacterDesignerWindow.WarlockInfo.name + ".prefab";
                // Get prefab path
                prefabPath = AssetDatabase.GetAssetPath(CharacterDesignerWindow.WarlockInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject warlockPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!warlockPrefab.GetComponent<Warlock>())
                {
                    warlockPrefab.AddComponent(typeof(Warlock));
                }
                warlockPrefab.GetComponent<Warlock>().warlockData = CharacterDesignerWindow.WarlockInfo;

                break;

            case SettingsType.TITAN:
                // create the .asset file
                dataPath += "Titan/" + CharacterDesignerWindow.TitanInfo.name + ".asset";
                AssetDatabase.CreateAsset(CharacterDesignerWindow.TitanInfo, dataPath);

                newPrefabPath += "Titan/" + CharacterDesignerWindow.TitanInfo.name + ".prefab";
                // Get prefab path
                prefabPath = AssetDatabase.GetAssetPath(CharacterDesignerWindow.TitanInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject titanPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!titanPrefab.GetComponent<Titan>())
                {
                    titanPrefab.AddComponent(typeof(Titan));
                }
                titanPrefab.GetComponent<Titan>().titanData = CharacterDesignerWindow.TitanInfo;
                break;

            case SettingsType.HUNTER:
                // create the .asset file
                dataPath += "Hunter/" + CharacterDesignerWindow.HunterInfo.name + ".asset";
                AssetDatabase.CreateAsset(CharacterDesignerWindow.HunterInfo, dataPath);

                newPrefabPath += "Hunter/" + CharacterDesignerWindow.HunterInfo.name + ".prefab";
                // Get prefab path
                prefabPath = AssetDatabase.GetAssetPath(CharacterDesignerWindow.HunterInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject hunterPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!hunterPrefab.GetComponent<Hunter>())
                {
                    hunterPrefab.AddComponent(typeof(Hunter));
                }
                hunterPrefab.GetComponent<Hunter>().hunterData = CharacterDesignerWindow.HunterInfo;
                break;
        }
    }
}
