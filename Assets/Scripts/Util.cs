using UnityEngine;
using System.Collections;

public class IntVector2
{
    public int x { get; set; }
    public int y { get; set; }
    public IntVector2(int newX, int newY) { x = newX; y = newY; }
}

public class MinMax
{
    public int min { get; set; }
    public int max { get; set; }
    public MinMax(int newMin, int newMax) { min = newMin; max = newMax; }
}

public class Util : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
