using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Amazon.Lambda;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using System.Text;
using Amazon.Lambda.Model;
public class ChannelGBot : MonoBehaviour {
    public GameObject player;
    public GameObject channelTextController;
    public InputField ifield;
    public GameObject choiceText;
    private int CurrentChannel = 0;
    private int NONE = 0;
    private string gameOverTag = "Choice is yours";
    // Sara
    private int SARA = 1;
    private string chatSara = "ChatWithGBotSara";
    public string IdentityPoolId_Sara = "us-east-1:f8b8fa49-3da8-4a96-94b4-4a52f191d80f";
    
    // Alex
    private int ALEX = 2;
    private string chatAlex = "ChatWithGBotAlex";
    public string IdentityPoolId_Alex = "us-east-1:f8b8fa49-3da8-4a96-94b4-4a52f191d80f";

    
    //------------------------------------------------- mono behavior ---------------------------------------------//
    #region Awake
    private void Awake()
    {
        reply = new DAJson();
        UnityInitializer.AttachToGameObject(this.gameObject);
    }
    #endregion

    #region Update
    private void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            InputFieldControll();
        }
    }
    #endregion
    //--------------------------------------------------- Input Field Controll -------------------------------------//
    void InputFieldControll()
    {
        if(ifield.text.Length != 0)
        {
            sendDialogData(CurrentChannel, ifield.text);
            ifield.text = "";
        }
        else
        {
            ifield.ActivateInputField();
        }
    }
    //--------------------------------------------------- Dialog Controll -------------------------------------------//
    // Dialog
    public Text dialogText;
    public DAJson reply;
    public string sendDialogData(int who, string dialog)
    {
        dialogText.text = dialog; // show the dialog text on a Screen
        DAJson dataJson = new DAJson();
        dataJson.data = dialog;
        dialog = JsonUtility.ToJson(dataJson);
        switch (who)
        {
            case 1://SARA
                {
                    IdentityPoolId = IdentityPoolId_Sara;
                    Invoke(chatSara, dialog);
                    break;
                }
            case 2://Alex
                {
                    IdentityPoolId = IdentityPoolId_Alex;
                    Invoke(chatAlex, dialog);
                    break;
                }
            default:
                {
                    IdentityPoolId = "";
                    break;
                }

        }

        return dialog;
    }
    //--------------------------------------------------- channel controll -----------------------------------------//
    public void setConnectedStatus(int stat)
    {
        CurrentChannel = stat;
        //Debug.Log("GBotChannel status : " + stat);
    }
    //--------------------------------------------------- AWS SDK --------------------------------------------------//
    public string IdentityPoolId = "";
    public string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;
    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }
    public string LambdaRegion = RegionEndpoint.USEast1.SystemName;
    private RegionEndpoint _LambdaRegion
    {
        get { return RegionEndpoint.GetBySystemName(LambdaRegion); }
    }
    
    #region private members

    private IAmazonLambda _lambdaClient;
    private AWSCredentials _credentials;

    private AWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
                _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
            return _credentials;
        }
    }

    private IAmazonLambda Client
    {
        get
        {
            if (_lambdaClient == null)
            {
                _lambdaClient = new AmazonLambdaClient(Credentials, _LambdaRegion);
            }
            return _lambdaClient;
        }
    }
    #endregion

    #region Invoke
    /// <summary>
    /// Example method to demostrate Invoke. Invokes the Lambda function with the specified
    /// function name (e.g. helloWorld) with the parameters specified in the Event JSON.
    /// Because no InvokationType is specified, the default 'RequestResponse' is used, meaning
    /// that we expect the AWS Lambda function to return a value.
    /// </summary>
    public void Invoke(string function_name,string event_text)
    {
        string result_text = "";
        Client.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
        {
            FunctionName = function_name,
            Payload = event_text
        },
        (responseObject) =>
        {
            if (responseObject.Exception == null)
            {
                result_text += Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()) + "\n";

                dialogText.text = reply.GetDialogString(result_text);
                if (reply.GetDialogStringOrigin(result_text).StartsWith(gameOverTag))
                {
                    choiceText.SetActive(true);
                    if (CurrentChannel == ALEX) channelTextController.SendMessage("setPossibleSelectAlex");
                    if (CurrentChannel == SARA) channelTextController.SendMessage("setPossibleSelectSara");
                }
            }
            else
            {
                result_text += responseObject.Exception + "\n";
                dialogText.text = "...(Poor communication status)";
            }

            //Debug.Log(result_text);

        }
        );
    }

    #endregion

}
