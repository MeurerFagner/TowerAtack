using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TypeShape {
    Point,
	Line,
	Column,
	Square,
	Diamond
}
public class GameManagmentScript : MonoBehaviour {

	// Use this for initialization
	public Tilemap tileMap;
	public Player player;

    List<ColoredArea> listColoredAreas;
	void Start () {
        listColoredAreas = new List<ColoredArea>();
    }

	// Update is called once per frame
	void Update () {
		StartCoroutine ("PaintCells");
	}

	IEnumerator PaintCells () {

		for (int x = 0; x < tileMap.size.x; x++) {
			for (int y = 0; y < tileMap.size.y; y++) {
				var cell = new Vector3Int (x + tileMap.origin.x, y + tileMap.origin.y, 0);
				tileMap.SetTileFlags (cell, TileFlags.None);
				tileMap.SetColor (cell, Color.white);
                for (int i = 0; i < listColoredAreas.Count; i++)
                {
                    ColorCellArea(listColoredAreas[i], cell, tileMap);
                }
				
			}

		}
		var tileMouse = tileMap.WorldToCell (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		
		tileMap.SetColor (tileMouse, (Color.red + tileMap.GetColor (tileMouse)) / 2);
		yield return null;
	}

	public void SetColoredArea (ColoredArea coloredArea) {
        if(!listColoredAreas.Exists(p=>p == coloredArea))
            listColoredAreas.Add(coloredArea);
	}
	public void RemoveColoredArea (ColoredArea coloredArea) {
        if (listColoredAreas.Exists(p => p == coloredArea))
            listColoredAreas.Remove(coloredArea);
    }
	void ColorCellArea (ColoredArea coloredArea, Vector3Int cell, Tilemap tileMap) {
		if (coloredArea != null) {
			var initCell = tileMap.WorldToCell (coloredArea.InitialPosition);
			
			if (IsInRange (coloredArea.Range, initCell, cell, coloredArea.Shape))
				tileMap.SetColor (cell, coloredArea.ColorArea);
		}
	}

	/// <summary>
	/// Retorna se uma determinada celula está na distancia definida de outra
	/// </summary>
	/// <param name="range">distancia entre as celulas</param>
	/// <param name="initCell">celula de ponto de origem</param>
	/// <param name="cell">celula a ser testada</param>
	/// <param name="shape">foma da area em torno da celula de origem</param>
	/// <returns>retorna true se a Célula está dentro da distanciapermitida da Célula e origem</returns>
	public static bool IsInRange (int range, Vector3Int initCell, Vector3Int cell, TypeShape shape) {
		switch (shape) {
			case TypeShape.Column:
				if ((cell.x == initCell.x) &&
					(cell.y >= initCell.y - range) &&
					(cell.y <= initCell.y + range))
					return true;
				break;
			case TypeShape.Line:
				if ((cell.x >= initCell.x - range) &&
					(cell.x <= initCell.x + range) &&
					(cell.y == initCell.y))
					return true;
				break;
			case TypeShape.Square:
				if ((cell.x >= initCell.x - range) &&
					(cell.x <= initCell.x + range) &&
					(cell.y >= initCell.y - range) &&
					(cell.y <= initCell.y + range))
					return true;					
				break;
			case TypeShape.Diamond:
				if ((cell.x >= initCell.x - range + Math.Abs (initCell.y - cell.y)) &&
					(cell.x <= initCell.x + range - Math.Abs (initCell.y - cell.y)))
					return true;
				break;
		}
		return false;
	}

    public List<Inimigo> FindinimigosInArea(Vector3Int initCell,TypeShape shape, int range)
    {
        var inimgos = new List<Inimigo>();
        foreach (var item in GameObject.FindGameObjectsWithTag("Inimigo"))
        {
            var cell = tileMap.WorldToCell(item.transform.position);
            if (IsInRange(range, initCell, cell, shape))
                inimgos.Add(item.GetComponent<Inimigo>());                
        }
        return inimgos;
    }
}