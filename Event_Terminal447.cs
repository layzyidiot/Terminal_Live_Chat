using HarmonyLib;
using MonoMod.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TerminalLivestreamChat
{
    internal class Event_Terminal447 : MonoSingleton
    {
        public int TerminalID = 447;
        public GameObject terminalmessagePrefab;
        public Transform contentTransform;
        public Transform canvastc;

        public TextMeshProUGUI curMessageText;




        public string[] joinMessages =
        {
            "ugh"
        };

        public string[] randomMessages =
        {
            "you are so bad at this game v2 plays better",
            "holy FUCKING SHIYT V1 YOU SUCK",
            "please v1 just QUIT PLAYING",
            "V2 PLAYS BETTER HE ONLY LOST DUE TO BAD LUCK",
            "V2 IS A WAY BETTER FIGHTER",
            "<color=red>KYS V1</color> <color=grey>(Flagged as possible harassment due to term : \"KYS\")</color>"
                
        };


        IEnumerator Start()
        {
            AddMessageToChat(joinMessages[UnityEngine.Random.Range(0, joinMessages.Length)]);

            while (true) {
                yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 4f));
                AddMessageToChat(randomMessages[UnityEngine.Random.Range(0, randomMessages.Length)]);

                if (curMessageText.text == $"<color=yellow>Terminal 447</color> - <color=red>KYS V1</color> <color=grey>(Flagged as possible harassment due to term : \"KYS\")</color>")
                {
                    
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));
                    curMessageText.text = "<color=grey>Message removed by moderator for : Harassment</color>";

                    yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 1f));
                    AddMessageToChat("THE MODERATORS CANT SILENCE ME V1 I WILL TALK ABOUT HOW BAD YOU ARE UNTIL I DIE");
                }
            }
        }

        
        void AddMessageToChat(string msg)
        {
            Transform t = canvastc.transform.Find("Image/Scroll View/Viewport/Content/Section Reference/Grid/StreamBegunText");
            if (t != null)
            {
                Destroy(t.gameObject);
            }


            string newMessage = "";


            newMessage = $"<color=yellow>Terminal {TerminalID}</color> - {msg}";
            /*do
            {
                newMessage = randomMessages[Random.Range(0, randomMessages.Length)];
                attempts++;
            } while (instantiatedMessages.Select(msg => msg.GetComponentInChildren<TMPro.TextMeshProUGUI>().text).Contains(newMessage) && attempts < maxAttempts);
            */

            GameObject terminalMessageInstance = Instantiate(terminalmessagePrefab, contentTransform);


            TMPro.TextMeshProUGUI textMeshPro = terminalMessageInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            curMessageText = textMeshPro;
            textMeshPro.text = newMessage;
        }

        public void LeaveStream()
        {
            AddMessageToChat("boring stream im leaving");
            Destroy(gameObject);
        }

    }


}
