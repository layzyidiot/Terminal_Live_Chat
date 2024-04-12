    using BepInEx;
using GameConsole;
using HarmonyLib;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace TerminalLivestreamChat
{
    [BepInPlugin("lazy.terminal.livestream", "Terminal Live Chat", "1.0.0.0")]
    public class PluginTerminal : BaseUnityPlugin
    {
        private static AssetBundle assetBundle;
        private static GameObject canvastc;
        private static List<TerminalWatcher> Terminals = new List<TerminalWatcher>();
        private static List<GameObject> instantiatedMessages = new List<GameObject>();
        private static GameObject terminalmessagePrefab;
        private static Transform contentTransform;

        void Awake()
        {
            
            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "terminal_livestream"));

            canvastc = assetBundle.LoadAsset<GameObject>("CanvasTERMINALCHAT");
            canvastc = Instantiate(canvastc);
            DontDestroyOnLoad(canvastc);

            contentTransform = canvastc.transform.Find("Image/Scroll View/Viewport/Content/Section Reference/Grid");

            terminalmessagePrefab = assetBundle.LoadAsset<GameObject>("TerminalMessage");

            StartCoroutine(createTerminalClass());

        }

        private int GetAmountOfSubscribers()
        {
            int subbedterminals = 0;
            for (int i = 0; i < Terminals.Count; i++)
            {
                TerminalWatcher curTerminal = Terminals[i];
                bool isSubscribed = curTerminal.GetIfSubbed();
                if (isSubscribed)
                {
                    subbedterminals++;
                }
            }
            return subbedterminals;
        }


        private void Update()
        {
            /*int hasTerminal447 = 0;
            if (contentTransform != null)
            {
                hasTerminal447 = contentTransform.GetComponent<Event_Terminal447>() ? 1 : 0;
            }
            else
            {
                contentTransform = canvastc.transform.Find("Image/Scroll View/Viewport/Content/Section Reference/Grid");
            }
            */
            
            
            GameObject viewCount = canvastc.transform.Find("ViewCount").gameObject;
            TMPro.TextMeshProUGUI viewCounttmpro = viewCount.GetComponent<TextMeshProUGUI>();
            viewCounttmpro.text = $"VIEW COUNT:{Terminals.Count}\n<color=green> SUB COUNT:{GetAmountOfSubscribers()}";
            
            if (contentTransform.childCount > 9)
            {
                // Remove the oldest message
                GameObject oldestMessage = contentTransform.GetChild(0).gameObject;
                Destroy(oldestMessage);
            }
        }

        IEnumerator createTerminalClass()
        {
            
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (SceneHelper.CurrentScene != "Main Menu" && SceneHelper.CurrentScene != "Intro" && SceneHelper.CurrentScene != "Bootstrap")
                {
                    StyleHUD styleHUD = MonoSingleton<StyleHUD>.Instance;

                    if (styleHUD.rankIndex == 0)
                    {
                        yield return new WaitForSeconds(8f);
                    }
                    else if (styleHUD.rankIndex == 1)
                    {
                        yield return new WaitForSeconds(7f);
                    }
                    else if (styleHUD.rankIndex == 2)
                    {
                        yield return new WaitForSeconds(6f);
                    }
                    else if (styleHUD.rankIndex == 3)
                    {
                        yield return new WaitForSeconds(5f);
                    }
                    else if (styleHUD.rankIndex == 4)
                    {
                        yield return new WaitForSeconds(4f);
                    }
                    else if (styleHUD.rankIndex == 5)
                    {
                        yield return new WaitForSeconds(3f);
                    }
                    else if (styleHUD.rankIndex == 6)
                    {
                        yield return new WaitForSeconds(2f);
                    }
                    else if (styleHUD.rankIndex == 7)
                    {
                        yield return new WaitForSeconds(0.6f);
                    }

                    int terminalid;

                    do
                        terminalid = Random.Range(1, 9999);
                    while (terminalid == 447);

                    TerminalWatcher terminalWatcher = contentTransform.gameObject.AddComponent<TerminalWatcher>();
                    terminalWatcher.TerminalID = terminalid;
                    terminalWatcher.HowMuchPeeDoDeyHav = Random.Range(400, 5000000);
                    terminalWatcher.contentTransform = contentTransform;
                    terminalWatcher.canvastc = canvastc.transform;
                    terminalWatcher.terminalmessagePrefab = terminalmessagePrefab;
                    terminalWatcher.minimum = Random.Range(2, 6);
                    terminalWatcher.Subscribed = Random.Range(0, 10) == 1 ? true : false;

                    Terminals.Add(terminalWatcher);


                }
                
                
            }

            
            
        }

        IEnumerator specialEvents()
        {
            int specialEventsAmount = 1;
            Transform contentTransform = canvastc.transform.Find("Image/Scroll View/Viewport/Content/Section Reference/Grid");
            
            while (true) {
                yield return new WaitForSeconds(UnityEngine.Random.Range(100,300));

                if (specialEventsAmount == 1)
                {
                    Debug.Log("terminal 447 spawned");
                    Event_Terminal447 term447 = contentTransform.gameObject.AddComponent<Event_Terminal447>();
                    term447.contentTransform = contentTransform;
                    term447.canvastc = canvastc.transform;
                    term447.terminalmessagePrefab = terminalmessagePrefab;
                    
                    yield return new WaitForSeconds(UnityEngine.Random.Range(10, 50));
                    term447.LeaveStream();
                }
            }


            
        }
    }
}
