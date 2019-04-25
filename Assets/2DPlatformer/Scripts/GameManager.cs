using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

    private const string SAVE_DATA_FILE = "/save.dat";

	private const string SERVER_URL = "http://localhost/ohko/ohkoserver.php";
	private const int SERVER_ACTION_REGISTER = 1;
	private const int SERVER_ACTION_ADD_MATCH = 2;

    private Scene currentScene;
    private static GameManager _instance;
    private string _saveDataPath;
    private SaveData _saveData;

    public GameObject[] size;
    public List<GameObject> characters;
    public bool player1CharacterSpawned;
    public bool player2CharacterSpawned;
    public bool player3CharacterSpawned;
    public bool player4CharacterSpawned;
    public int rage;
	public int matches;

    public static GameManager instance
    {
        get
        {
            if (!_instance)
                _instance = Instantiate<GameManager>(Resources.Load<GameManager>("GameManager"));

            return _instance;
        }
    }


    void Start()
    {
        _saveDataPath = Application.persistentDataPath + SAVE_DATA_FILE;

		if (File.Exists (_saveDataPath)) {
			LoadSaveData ();
		} else 
		{
			CreateSaveData ();
			CallRegisterPlayer();
		}

        characters = new List<GameObject>();
        DontDestroyOnLoad(gameObject);
        rage = _saveData.rageQuitTimes;
    }

    void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadSaveData()
    {

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(_saveDataPath, FileMode.Open);

        _saveData = (SaveData)binaryFormatter.Deserialize(fileStream);

        fileStream.Close();

    }

    private void CreateSaveData()
    {
        _saveData = new SaveData();

        _saveData.rageQuitTimes = 0;

        StoreSavedData();
    }

    private void StoreSavedData()
    {  

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(_saveDataPath);

        binaryFormatter.Serialize(fileStream, _saveData);

        fileStream.Close();
    }

    public void RageQuit()
    {
        ++_saveData.rageQuitTimes;
        StoreSavedData();
    }

    public void ResetRagequit()
    {
        _saveData.rageQuitTimes = 0;
        StoreSavedData();
        LoadSaveData();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            player1CharacterSpawned = false;
            player2CharacterSpawned = false;
            player3CharacterSpawned = false;
            player4CharacterSpawned = false;

            characters.Clear();
        }

        if (characters.Count == 2)
        {
            SpawnPlayer1Character();
            SpawnPlayer2Character();
        }
        if (characters.Count == 3)
        {
            SpawnPlayer1Character();
            SpawnPlayer2Character();
            SpawnPlayer3Character();
        }
        if (characters.Count == 4)
        {
            SpawnPlayer1Character();
            SpawnPlayer2Character();
            SpawnPlayer3Character();
            SpawnPlayer4Character();
        }
    }



    public void SpawnPlayer1Character()
    {
        if (!player1CharacterSpawned)
        {
            player1CharacterSpawned = true;
            GameObject PlayerOneCharacter = Instantiate(characters[0], new Vector2(-5, 3), transform.rotation);
            PlayerOneCharacter.transform.SetParent(GameObject.Find("Player1").GetComponent<Transform>());
        }
    }

    public void SpawnPlayer2Character()
    {
        if (!player2CharacterSpawned)
        {
            player2CharacterSpawned = true;
            GameObject PlayerOneCharacter = Instantiate(characters[1], new Vector2(5, 3), transform.rotation);
            PlayerOneCharacter.transform.SetParent(GameObject.Find("Player2").GetComponent<Transform>());
        }
    }

    public void SpawnPlayer3Character()
    {
        if (!player3CharacterSpawned)
        {
            player3CharacterSpawned = true;
            GameObject PlayerOneCharacter = Instantiate(characters[2], new Vector2(-3, 3), transform.rotation);
            PlayerOneCharacter.transform.SetParent(GameObject.Find("Player3").GetComponent<Transform>());
        }
    }

    public void SpawnPlayer4Character()
    {
        if (!player4CharacterSpawned)
        {
            player4CharacterSpawned = true;
            GameObject PlayerOneCharacter = Instantiate(characters[3], new Vector2(3, 3), transform.rotation);
            PlayerOneCharacter.transform.SetParent(GameObject.Find("Player4").GetComponent<Transform>());
        }
    }

	private IEnumerator CallRegisterPlayer()
	{
		WWWForm form = new WWWForm ();
		form.AddField ("action", SERVER_ACTION_REGISTER);

		using (UnityWebRequest www = UnityWebRequest.Post (SERVER_URL, form)) 
		{
			yield return www.SendWebRequest();	

			string[] reply = www.downloadHandler.text.Split(' ');

			_saveData.id = int.Parse (reply [0]);
			_saveData.passcode = int.Parse (reply [1]);

			StoreSavedData ();

			// APAGAR A DB E O FICHEIRO DE SAVE DATA
		}
	}
		
	public IEnumerator CallAddMatches()
	{
		WWWForm form = new WWWForm ();
		form.AddField ("action", SERVER_ACTION_ADD_MATCH);
		form.AddField ("id", _saveData.id);
		form.AddField ("passcode", _saveData.passcode);
		form.AddField ("matches", matches);

		using (UnityWebRequest www = UnityWebRequest.Post (SERVER_URL, form)) 
		{
			yield return www.SendWebRequest();
		}
	}

}
