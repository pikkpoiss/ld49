using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayHUD : MonoBehaviour {
  public TextMeshProUGUI timeText;
  public TextMeshProUGUI moneyText;
  public TextMeshProUGUI deliveriesText;

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

}
