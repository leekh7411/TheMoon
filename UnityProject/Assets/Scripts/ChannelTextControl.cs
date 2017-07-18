using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChannelTextControl : MonoBehaviour {
    public GameObject player;
    public GameObject gbotChannel;
    public GameObject screenEnd;
    public Text channelText;
    public Text choiceText;
    public InputField ifield;
    string defaultText = " channel-disconnected";
    string connect01Text = " 01 channel-connected\n    press 'x' to Disconnect";
    string connect02Text = " 02 channel-connected\n    press 'x' to Disconnect";
    string withGBot = " Press 'X' to connect";
    int blinkCount = 0;
    int Blink = 30;
    bool IsBlink = false;
    bool IsShowChannelText = true;
    private int NONE = 0;
    private int SARA = 1;
    private int ALEX = 2;

    bool IsSelectPossible_Sara = false;
    bool IsSelectPossible_Alex = false;
    int playerSelected = 0;
    public void setPossibleSelectAlex() { IsSelectPossible_Alex = true; }
    public void setPossibleSelectSara() { IsSelectPossible_Sara = true; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("s"))
        {
            if(IsSelectPossible_Alex && IsPlayerCloseWithAlex())
            {
                // Player Select Alex !
                playerSelected = ALEX;
            }
            else
            {
                // Can't selected yet
            }

            if (IsSelectPossible_Sara && IsPlayerCloseWithSara())
            {
                // Player Select Sara !
                playerSelected = SARA;
            }
            else
            {
                // Can't selected yet
            }
        }
        if (Input.GetKeyDown("x") && !ifield.isFocused)
        {
            //ifield.ActivateInputField();
            IsShowChannelText = !IsShowChannelText;
            if (IsPlayerCloseWithSara())
            {

                channelText.text = connect01Text;
                gbotChannel.SendMessage("setConnectedStatus", SARA);
               
            }
            else if (IsPlayerCloseWithAlex())
            {
                channelText.text = connect02Text;
                gbotChannel.SendMessage("setConnectedStatus", ALEX);
            }
            else
            {
                //channelText.text = "";
                gbotChannel.SendMessage("setConnectedStatus", NONE);
            }
        }

        if (IsShowChannelText)
        {
            gbotChannel.SendMessage("setConnectedStatus", NONE);
            if (IsPlayerCloseWithAlex() || IsPlayerCloseWithSara())
            {
                // closed with Sara
                BlinkChannelText();
            }
            else
            {
                channelText.text = defaultText;
            }
        }

        if (playerSelected != NONE)
        {
            //Game Over.
            screenEnd.SetActive(true);
        }
		
	}

    bool IsPlayerCloseWithSara()
    {
        if (player.gameObject.transform.position.x > 8 && player.gameObject.transform.position.x < 12)
            return true;
        else
            return false;
    }

    bool IsPlayerCloseWithAlex()
    {
        if (player.gameObject.transform.position.x < -8 && player.gameObject.transform.position.x > -12)
            return true;
        else
            return false;
    }
    void BlinkChannelText()
    {
        // closed with Alex
        blinkCount++;
        
        if (blinkCount > Blink)
        {
            IsBlink = !IsBlink;
            blinkCount = 0;
        }

        if (IsBlink)
        {
            channelText.text = withGBot;
        }
        else
        {
            channelText.text = "";
        }
        
    }

}
