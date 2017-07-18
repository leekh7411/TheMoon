using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System;
[Serializable]
public class ChatDataJson {
    
    public ChatDataJson()
    {
        dialogAction = new DialogAction();
    }
    public DialogAction dialogAction { get; set; }
    public class DialogAction
    {
        public DialogAction()
        {
            message = new Message();
        }
        public string type { get; set; }
        public string fulfillmentState { get; set; }
        public Message message { get; set; }
    }
    public class Message
    {
        public Message()
        {
            data = new Data();
        }
        public string contentType { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public Data()
        {
            slots = new Slots();
        }
        public string intentName { get; set; }
        public Slots slots { get; set; }
        public string message { get; set; }
        public string dialogState { get; set; }
        public string slotToElicit { get; set; }
    }
    public class Slots
    {
        public object Name { get; set; }
    }
}
