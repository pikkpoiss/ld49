using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GamePlayHUD : MonoBehaviour {
  public TextMeshProUGUI timeText;
  public TextMeshProUGUI moneyText;
  public TextMeshProUGUI deliveriesText;
  public TextMeshProUGUI dispatchText;

  private List<string> messages = new List<string>();

  private const string messagesFilename = "messages.txt";

  void Start() {
    PopulateMessagesList();
  }

  public void SetTimeText(string text) {
    if (timeText) {
      timeText.text = text;
    }
  }

  public void SetMoneyText(string text) {
    if (moneyText) {
      moneyText.text = text;
    }
  }

  public void SetDeliveriesText(string text) {
    if (deliveriesText) {
      deliveriesText.text = text;
    }
  }

  public void SetDispatchText() {
    if (dispatchText) {
      if (messages.Count == 0) {
        PopulateMessagesList();
      }
      var index = Random.Range(0, messages.Count);
      dispatchText.text = $"DISPATCH\n" + messages[index];
    }
  }

  private void PopulateMessagesList() {
    var messagesString = TextFileReader.ReadFileAsString(messagesFilename);
    var messagesArray = messagesString.Split('\n');
    messages = new List<string>(messagesArray);
  }
}
