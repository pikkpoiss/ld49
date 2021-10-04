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

  private List<string> messages = new List<string> {
    "If you don't get this supply of bibles to Mr. Thorston quick then he'll bring the wrath of God down on you!",
    "Hurry up will ya! Those packages won't deliver themselves!",
    "If you don't break everything you may actually get paid!",
    "You got the easy job. Dispatchings where the real work gets done!",
    "Get that cat ear medicine delivered - stat!",
    "One o' those boxes has a bunch of angry bees in it. I'd deliver it quick if I were you...",
    "Every single dent on one of those packages is coming out of your paycheck!",
    "At least you have a truck! I used to have to carry all my packages on my back!",
    "Press 'h' to use the truck's horn. Try not to let the power go to your head...",
    "Deliver that rabies vaccine to Dr. Herzog before it's too late!",
    "Who ships blood sausage through the mail?",
    "If you're slow you're not just wasting your time. You're also wasting the package's time!",
    "If you do this well you may just end up dispatcher one day. Capeesh?",
    "We're really the modern day cowboys when you stop and think about it."
  };

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
      var index = Random.Range(0, messages.Count);
      dispatchText.text = $"DISPATCH\n" + messages[index];
    }
  }
}
