// NULLcode Studio © 2015
// null-code.ru

using Tag;
using UnityEngine;

namespace NULLcode_Studio._15Puzzle.Scripts
{
	public class Puzzle : MonoBehaviour {

		public int ID; // номер должен соответствовать данной "костяшки"
		public ElementWithGear element;

		public bool isCanMove;

		private void OnEnable()
		{
			DialogueTag.OnTagGame += SetIsCanMove;
		}
	
		private void OnDisable()
		{
			DialogueTag.OnTagGame -= SetIsCanMove;
		}

		private void SetIsCanMove(bool status)
		{
			isCanMove = status;
		}

		private void Awake()
		{
			element = GetComponent<ElementWithGear>();
		}

		// текущая и пустая клетка, меняются местами
		void ReplaceBlocks(int x, int y, int XX, int YY)
		{
			if (element.isCanMove)
			{
				if(element.isWithGear) element.OnReady();
			
				GameControl.grid[x,y].transform.position = GameControl.position[XX,YY];
				GameControl.grid[XX,YY] = GameControl.grid[x,y];
				GameControl.grid[x,y] = null;
				GameControl.click++;
				GameControl.GameFinish();
			}
			else
			{
				element.MouseDown();
			}
		}

		// определение пустой клетки, рядом с текущей, по горизонтали или вертикали
		void OnMouseDown()
		{
			if(isCanMove)
			{
				for (int y = 0; y < 3; y++)
				{
					for (int x = 0; x < 3; x++)
					{
						if (GameControl.grid[x, y])
						{
							if (GameControl.grid[x, y].GetComponent<Puzzle>().ID == ID)
							{
								if (x > 0 && GameControl.grid[x - 1, y] == null)
								{
									ReplaceBlocks(x, y, x - 1, y);
									return;
								}
								else if (x < 2 && GameControl.grid[x + 1, y] == null)
								{
									ReplaceBlocks(x, y, x + 1, y);
									return;
								}
							}
						}

						if (GameControl.grid[x, y])
						{
							if (GameControl.grid[x, y].GetComponent<Puzzle>().ID == ID)
							{
								if (y > 0 && GameControl.grid[x, y - 1] == null)
								{
									ReplaceBlocks(x, y, x, y - 1);
									return;
								}
								else if (y < 2 && GameControl.grid[x, y + 1] == null)
								{
									ReplaceBlocks(x, y, x, y + 1);
									return;
								}
							}
						}
					}
				}

			}
		}
	}
}
