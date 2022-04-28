using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Object : MonoBehaviour
{
    public int XSize, YSize;
    public GameObject tile_prefab;
    public Transform level_object;
    public Material[] Wall_Mat, Floor_Mat;
    public GameObject[] sprite_prefab;
    public TextAsset CSV_Ground, CSV_Zone, CSV_Threat;
    public TextAsset[] CSV_Wall;
    public bool pass_thru_index0, pass_thru_index1, pass_thru_index2, pass_thru_index3;
}
