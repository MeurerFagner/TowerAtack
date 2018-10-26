using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredArea {
	

	public Vector3 InitialPosition {get; set;}
	public int Range {get; set;}
	public Color ColorArea {get; set;}
	public TypeShape Shape { get; set; }

	public ColoredArea(Vector3 initialPos, int range, Color colorArea, TypeShape shape)
	{
		InitialPosition = initialPos;
		Range  = range;
		ColorArea = colorArea;
		Shape = shape;		
	}
	
}
