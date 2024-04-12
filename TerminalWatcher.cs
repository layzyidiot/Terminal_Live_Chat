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
    internal class TerminalWatcher : MonoSingleton
    {
        public int TerminalID;
        public GameObject terminalmessagePrefab;
        public Transform contentTransform;
        public Transform canvastc;

        public TextMeshProUGUI curMessageText;

        public bool Subscribed;
        public bool Lurker;


        public bool thinkPlayerIsPlayingBad;
        public bool thinkPlayerIsPlayingBetter;
        public int minimum;




        public int HowMuchPeeDoDeyHav;

        public string[] joinMessages =
        {
            "hello v1! just joined, what are we doing today?",
            "whats UP gamers",
            "did i miss anything important yet i just joined",
            "oh holy peak v1 is streaming"
        };

        public string[] randomMessages =
        {
            "how is your day going V1",
            "ULTRAKILL",
            "V1 have you killed V2 yet",
            "Grahhhhhhhhhh",
            "Have Fun V1",
            "V1 did you know that you can punch your own bullets?",
            "V1 did you see what i said",
            "V1 did you know that you can shoot your coins at the right time to do a splitshot?",
            "Hello fellow chatters!",
            "bro",
            "Hakita",
            "V1 im such a big fan please",
            "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
            "im bored",
            "Skibidi dop dop dop yes yes", // kill me
            "V1 you are SO cool",
            "V1 can you please do a chargeback i think thatll be cool",
            "hello v1 my grandma terminal is terminally ill and is about to die soon can you give me a shout out to make my day better",
            "when r u fighitnvg v3",
            "mmmm"

        };


        IEnumerator Start()
        {

            //var harmony = new Harmony("com.lazy.idiot");
            //harmony.PatchAll();
            if (UnityEngine.Random.Range(0, 5) != 0)
            {
                AddMessageToChat(joinMessages[UnityEngine.Random.Range(0, joinMessages.Length)]);
            }

            while (true) {
                yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 30f));

                
                if (UnityEngine.Random.Range(0, 50) == 1)
                {
                    StartCoroutine(Subscribe());

                }
                else if (UnityEngine.Random.Range(0, 5) >= 1)
                {
                    AddMessageToChat(randomMessages[UnityEngine.Random.Range(0, randomMessages.Length)]);
                }
                else
                {
                    ReactToCurrentStyle();
                }


                if (curMessageText.text == $"<color=yellow>Terminal {TerminalID}</color> - AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" || curMessageText.text == $"<color=green>Terminal {TerminalID} (SUBSCRIBER)</color> - AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
                {
                    Debug.Log("aaaaaa");
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));
                    curMessageText.text = "<color=grey>Message removed by moderator for : Spam</color>";
                }
                if (curMessageText.text == $"<color=yellow>Terminal {TerminalID}</color> - Skibidi dop dop dop yes yes" || curMessageText.text == $"<color=green>Terminal {TerminalID} (SUBSCRIBER)</color> - Skibidi dop dop dop yes yes")
                {
                    Debug.Log("skivbi");
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));
                    curMessageText.text = $"<color=grey>Terminal {TerminalID} was timed out for 10 minutes by moderator for : stop</color>";
                    yield return new WaitForSeconds(600);
                    AddMessageToChat("WOOOHOO im UNMUTED");
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

            
            newMessage = Subscribed ? $"<color=green>Terminal {TerminalID} (SUBSCRIBER) </color> - {msg}" : $"<color=yellow>Terminal {TerminalID}</color> - {msg}";
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

        IEnumerator Subscribe()
        {
            if (HowMuchPeeDoDeyHav >= 300 && Subscribed != true)
            {
                HowMuchPeeDoDeyHav -= 100;
                Transform t = canvastc.transform.Find("Image/Scroll View/Viewport/Content/Section Reference/Grid/StreamBegunText");
                if (t != null)
                {
                    Destroy(t.gameObject);
                }


                string newMessage = "";


                newMessage = $"<color=green>TERMINAL {TerminalID} JUST SUBSCRIBED TO THE CHANNEL! THANK YOU!";
                Subscribed = true;
                GameObject terminalMessageInstance = Instantiate(terminalmessagePrefab, contentTransform);


                TMPro.TextMeshProUGUI textMeshPro = terminalMessageInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                curMessageText = textMeshPro;
                textMeshPro.text = newMessage;

                yield return new WaitForSeconds(2f);
                AddMessageToChat("Hey V1 I just bought a subscription!");
            }
            else
            {
                yield break;
            }


        }
        public bool GetIfSubbed() { return Subscribed; }

        public string[] playingBadQuotes =
        {
            "ur bad",
            "BORING",
            "be stylish IDIOT",
            "V1 you gotta be more stylish man",
            "Get! That! Style!",
            "be more stylish i dont care how you do it but BE MORE STYLISH",
            "please be stylish",
            "this is boring be more stylish"
        };
        public string[] playingGoodQuotes =
        {
            "I rank you an ULTRAKILL outta 10!",
            "holy shit you are amazing at this",
            "v1 really be poppin off"
        };

        private void ReactToCurrentStyle()
        {
            if (SceneHelper.CurrentScene != "Main Menu" && SceneHelper.CurrentScene != "Intro") 
            {
                AddMessageToChat(joinMessages[UnityEngine.Random.Range(0, joinMessages.Length)]);
                return;
            }
            StyleHUD styleHUD = MonoSingleton<StyleHUD>.Instance;

            thinkPlayerIsPlayingBad = styleHUD.rankIndex <= minimum;

            if (styleHUD.rankIndex <= 1)
            {
                AddMessageToChat(playingBadQuotes[UnityEngine.Random.Range(0, playingBadQuotes.Length)]);
            }
            else if (thinkPlayerIsPlayingBad)
            {
                AddMessageToChat(playingBadQuotes[UnityEngine.Random.Range(0, playingBadQuotes.Length)]);
            }
            else if (styleHUD.rankIndex == 7)
            {
                AddMessageToChat(playingGoodQuotes[UnityEngine.Random.Range(0, playingGoodQuotes.Length)]);
            }
            
        }

    }


}
