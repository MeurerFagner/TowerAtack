using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

	public int AcoesDispoiveis = 3;
	public int MovmentRange = 3;
	public Color MovmentColor = Color.blue;
	public float MovmentSpeed = 20f;
	public Tilemap TileBoard;
	public GameManagmentScript gameManagment;

	bool atackIsActive;
	bool movmentIsActive;
	Vector3 newPosition;
	ColoredArea colored;

	// Use this for initialization
	void Start () {
		transform.position = GetPositionInBoard();
		newPosition = transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
		if (movmentIsActive)
		{
			if(Input.GetMouseButtonDown(0))
			{
				var tileMouse = TileBoard.WorldToCell (Camera.main.ScreenToWorldPoint (Input.mousePosition));
				if (TileBoard.GetTile(tileMouse) != null &&
				    GameManagmentScript.IsInRange(MovmentRange, 
					TileBoard.WorldToCell(transform.position),
					tileMouse, TypeShape.Diamond))
					
					{
						var pos = TileBoard.GetCellCenterWorld(tileMouse);
						newPosition.Set(pos.x,pos.y,transform.position.z);
						movmentIsActive = false;
						gameManagment.RemoveColoredArea(colored);
					}
			}
		}
		if (newPosition != transform.position)
		{
			var step = MovmentSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, newPosition,step);
		}
        
		else
			transform.position = GetPositionInBoard();

			if (atackIsActive)
        {

        }
	}

	public void ShowMovment()
	{
        if (movmentIsActive)
            return;
		colored = new ColoredArea(transform.position,MovmentRange,MovmentColor,TypeShape.Diamond);
		gameManagment.SetColoredArea(colored);
		movmentIsActive = true;
	}
	

	Vector3 GetPositionInBoard()
	{
		var cellPos = TileBoard.WorldToCell(transform.position);
		var pos = TileBoard.GetCellCenterLocal(cellPos);
		return new Vector3(pos.x,pos.y,transform.position.z);	
	}
}
