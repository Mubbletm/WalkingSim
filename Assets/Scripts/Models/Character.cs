using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    public new string name;
    public Color color;
    public AudioClip[] voice;
    public Sprite sprite;
}
