using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    private Text stats;
    private int texts;
    public SaveData _saveData;
    private GameManager gameManager;

    void Start ()
    {
        gameManager = GameManager.instance;
        stats = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        texts = gameManager.rage;
        stats.text = texts.ToString();    
    }

}
