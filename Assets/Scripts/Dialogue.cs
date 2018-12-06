using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Dialogue
{
    public string SpeakerName;

    public Sprite SpeakerImage;

    [TextArea(3,10)]
    public string[] Sentences;

    public bool Active;
}
