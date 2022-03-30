using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;
    public List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();   
    }

    public static TextWriterSingle AddWriter_Static(Action onBegin, TextMeshProUGUI uiText, string dialog, float timePerChar, bool invisibleCharacters, bool removeWriterBeforeAdd, Action onComplete)
    {
        if (removeWriterBeforeAdd)
            instance.RemoveWriter(uiText);
        return instance.AddWriter(onBegin, uiText, dialog, timePerChar, invisibleCharacters, onComplete);
    }

    private TextWriterSingle AddWriter(Action onBegin, TextMeshProUGUI uiText, string dialog, float timePerChar, bool invisibleCharacters, Action onComplete)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(onBegin, uiText, dialog, timePerChar, invisibleCharacters, onComplete);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(TextMeshProUGUI uiText)
    {
        instance.RemoveWriter(uiText);
    }

    private void RemoveWriter(TextMeshProUGUI uiText)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if(textWriterSingleList[i].GetUIText() == uiText)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    /*
    * Single TextWriter instance
    */
    public class TextWriterSingle
    {
        private Action onBegin;
        private TextMeshProUGUI uiText;
        private string dialog;
        private int charIndex;
        private float timePerChar;
        private float timer;
        private bool invisibleCharacters;
        private Action onComplete;
        public TextWriterSingle(Action onBegin, TextMeshProUGUI uiText, string dialog, float timePerChar, bool invisibleCharacters, Action onComplete)
        {
            this.onBegin = onBegin;
            this.uiText = uiText;
            this.dialog = dialog;
            this.timePerChar = timePerChar;
            this.invisibleCharacters = invisibleCharacters;
            this.onComplete = onComplete;
            charIndex = 0;

            //Run onBegin Action at the start
            if (this.onBegin != null) onBegin();

        }//end of Constructor

        //Returns true on completion
        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                //Display next character
                timer += timePerChar;
                charIndex++;
                string text = dialog.Substring(0, charIndex);

                if (invisibleCharacters)
                    text += "<color=#00000000>" + dialog.Substring(charIndex) + "</color>";

                uiText.text = text;

                if (charIndex >= dialog.Length)
                {
                    //Entire string displayed
                    if (onComplete != null) onComplete();
                    return true;
                }
            }

            return false;
        }

        public TextMeshProUGUI GetUIText() { return uiText; }
        public bool IsActive () { return charIndex < dialog.Length; }

        public void WriteAllAndDestroy()
        {
            uiText.text = dialog;
            charIndex = dialog.Length;
            if (onComplete != null) onComplete();
            TextWriter.RemoveWriter_Static(uiText);
        }
    }
}
