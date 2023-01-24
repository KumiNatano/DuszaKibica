using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using TMPro;


public class ShowStatUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public TMP_Text StatUpgradeText;
    
    // Start is called before the first frame update
    void Start()
    {
        StatUpgradeText.text = "";
        player = GameObject.FindGameObjectsWithTag("player")[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void UpdateTextBox(string upgrade, string colour) {
        StatUpgradeText.text = upgrade;

        if (colour == "red") {
            StatUpgradeText.color = Color.red;
        }
        else if (colour == "green") {
            StatUpgradeText.color = Color.green;
        }
        else {
            StatUpgradeText.color = Color.white;
        }

        StatUpgradeText.transform.position = player.transform.position;

        EmptyTextBox();
    }

    private async Task EmptyTextBox() {
        await Task.Delay(700);
        StatUpgradeText.text = "";
    }
}
