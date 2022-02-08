using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Preview", menuName = "Building Preview")]
public class BuildingPreview : ScriptableObject
{
    public GameObject buildObject;
    public Sprite previewSprite;
    public LayerMask buildingLayer;
}
