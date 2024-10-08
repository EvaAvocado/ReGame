// NULLcode Studio © 2015
// null-code.ru

using System;
using UnityEngine;
using UnityEngine.UI;

namespace NULLcode_Studio._15Puzzle.Scripts
{
    public class GameControl : MonoBehaviour
    {
        public GameObject[] _puzzle; // оригинальный массив

        // стартовая позиция для первого элемента
        public float startPosX = -6f;
        public float startPosY = 6f;

        // отступ по Х и Y, рассчитывается в зависимости от размера объекта
        public float outX = 1.1f;
        public float outY = 1.1f;

        public Text _text; // вывод текстовой информации

        public static int click;
        public static GameObject[,] grid;
        public static Vector3[,] position;
        private GameObject[] puzzleRandom;
        public static bool win;
        
        private bool _isFirstStart = true;
        public bool isFindGear;

        public static event Action OnReady;

        void Start()
        {
            puzzleRandom = new GameObject[_puzzle.Length];

            // заполнение массива позиций клеток
            float posXreset = startPosX;
            position = new Vector3[3, 3];
            for (int y = 0; y < 3; y++)
            {
                startPosY -= outY;
                for (int x = 0; x < 3; x++)
                {
                    startPosX += outX;
                    position[x, y] = new Vector3(startPosX, startPosY, 0);
                }

                startPosX = posXreset;
            }

            StartNewGame();
        }

        public void StartNewGame()
        {
            win = false;
            click = 0;
            RandomPuzzle();
            Debug.Log("New Game");
        }

        /*public void ExitGame()
        {
            Save();
            Application.Quit();
        }*/

        /*void Save()
        {
            string content = string.Empty;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (content.Length > 0) content += "|";
                    if (grid[x, y]) content += grid[x, y].GetComponent<Puzzle>().ID.ToString();
                    else content += "null";
                }
            }

            PlayerPrefs.SetString("Puzzle", content);
            PlayerPrefs.SetString("PuzzleInfo", click.ToString());
            //PlayerPrefs.Save(); // записать на диск сейчас, если в приложении не используется функция выхода
            Debug.Log(this + " сохранение 15 Puzzle.");
        }*/

        void Load()
        {
            string[] content = PlayerPrefs.GetString("Puzzle").Split(new char[] { '|' });

            if (content.Length == 0 || content.Length != 16) return;

            //if (PlayerPrefs.HasKey("PuzzleInfo")) click = Parse(PlayerPrefs.GetString("PuzzleInfo"));

            grid = new GameObject[3, 3];
            int i = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int j = FindPuzzle(Parse(content[i]));

                    if (j >= 0)
                    {
                        grid[x, y] = Instantiate(_puzzle[j], position[x, y], Quaternion.identity) as GameObject;
                        grid[x, y].name = "ID-" + i;
                        grid[x, y].transform.parent = transform;
                    }

                    i++;
                }
            }
        }

        public void ReloadPuzzle()
        {
            RandomPuzzle();
        }

        int FindPuzzle(int index)
        {
            int j = 0;
            foreach (GameObject e in _puzzle)
            {
                if (e.GetComponent<Puzzle>().ID == index) return j;
                j++;
            }

            return -1;
        }

        int Parse(string text)
        {
            int value;
            if (int.TryParse(text, out value)) return value;
            return -1;
        }

        void CreatePuzzle()
        {
            if (transform.childCount > 0)
            {
                // удаление старых объектов, если они есть
                for (int j = 0; j < transform.childCount; j++)
                {
                    Destroy(transform.GetChild(j).gameObject);
                }
            }

            int i = 0;
            grid = new GameObject[3, 3];
            //int h = Random.Range(0, 3);
            //int v = Random.Range(0, 3);
            GameObject clone = new GameObject();
            //grid[h, v] = clone; // размещаем пустой объект в случайную клетку
            grid[1, 0] = clone;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    // создание дубликатов на основе временного массива
                    if (grid[x, y] == null)
                    {
                        grid[x, y] = Instantiate(puzzleRandom[i], position[x, y], Quaternion.identity) as GameObject;
                        grid[x, y].name = "ID-" + i;
                        grid[x, y].transform.parent = transform;
                        if (!_isFirstStart)
                        {
                            grid[x, y].GetComponent<Puzzle>().isCanMove = true;
                        }

                        if (isFindGear)
                        {
                            grid[x, y].GetComponent<Puzzle>().element.isCanMove = true;
                            grid[x, y].GetComponent<Puzzle>().element.isSabotage = true;
                        }
                        
                        _isFirstStart = false;
                        i++;
                    }
                }
            }

            Destroy(clone); // очистка случайной клетки
            for (int q = 0; q < _puzzle.Length; q++)
            {
                Destroy(puzzleRandom[q]); // очистка временного массива
            }
        }
    
        /*int i = 1;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (grid[x, y])
                {
                    if (grid[x, y].GetComponent<Puzzle>().ID == i) i++;
                }
                else i = 1;
            }
        }
        if (i==9) isWin = true;
        */

        // проверка на выигрыш
        static public void GameFinish()
        {
            var isWin = false;
            var counter = 0;
        
            if (grid[0, 0]) if (grid[0, 0].GetComponent<Puzzle>().ID == 7) counter++;
            if (grid[1, 0]) if (grid[1, 0].GetComponent<Puzzle>().ID == 6) counter++;
            if (grid[2, 0]) if (grid[2, 0].GetComponent<Puzzle>().ID == 2) counter++;
            if (grid[0, 1]) if (grid[0, 1].GetComponent<Puzzle>().ID == 5) counter++;
            if (grid[1, 1]) if (grid[1, 1].GetComponent<Puzzle>().ID == 8) counter++;
            if (grid[0, 2]) if (grid[0, 2].GetComponent<Puzzle>().ID == 4) counter++;
            if (grid[1, 2]) if (grid[1, 2].GetComponent<Puzzle>().ID == 1) counter++;
            if (grid[2, 2]) if (grid[2, 2].GetComponent<Puzzle>().ID == 3) counter++;

            /*print("_________________________" + counter);
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (grid[x, y])
                {
                    print(grid[x, y].GetComponent<Puzzle>().ID + "-" + x + "-" + y);
                }
            }
        }
        print("_________________________" + counter);*/

            if (counter == 8) isWin = true;
        

            if (isWin)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (grid[x, y]) Destroy(grid[x, y].GetComponent<Puzzle>());
                    }
                }

                win = true;
                OnReady?.Invoke();
                Debug.Log("Finish!");
            }
        }

        // создание временного массива, с случайно перемешанными элементами
        void RandomPuzzle()
        {
            for (int i = 0; i < _puzzle.Length; i++)
            {
                puzzleRandom[i] = Instantiate(_puzzle[i], new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
            }

            /*int[] tmp = new int[_puzzle.Length];
        for (int i = 0; i < _puzzle.Length; i++)
        {
            tmp[i] = 1;
        }

        int c = 0;
        while (c < _puzzle.Length)
        {
            int r = Random.Range(0, _puzzle.Length);
            if (tmp[r] == 1)
            {
                puzzleRandom[c] = Instantiate(_puzzle[r], new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
                tmp[r] = 0;
                c++;
            }
        }*/

            CreatePuzzle();
        }

        /*void LateUpdate()
        {
            if (!win)
            {
                _text.text = "Ходов:\n" + click;
            }
            else
            {
                click = 0;
                _text.text = "Игра\nЗавершена!";
            }
        }*/
    }
}