using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public GameObject gameOverWindow;
    private Transform canvas;
    public int row;
    public int column;

    private Board board;

    public Sprite xSprite;
    public Sprite oSprite;

    private Image image;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        board = FindAnyObjectByType<Board>();
        canvas = FindAnyObjectByType<Canvas>().transform;
    }

    public void ChangeImage(string s)
    {
        if (s == "x")
        {
            image.sprite = xSprite;
        }
        else
        {
            image.sprite = oSprite;
        }
    }

    public void OnClick()
    {
        if (!board.CanPlay(this.row, this.column)) return;

        board.ExpandBoardIfNecessary(this.row, this.column);

        ChangeImage(board.currentTurn);

        board.matrix[this.row, this.column] = board.currentTurn;
  
        //Ktra ket thuc tran dau
        if (board.Check(this.row, this.column))
        {
            GameObject window = Instantiate(gameOverWindow, canvas);
            window.GetComponent<GameOverWindow>().SetName(board.currentTurn);
        }

        //Chuyen luot
        //if (board.currentTurn == "x")
        //{
        //    board.currentTurn = "o";
        //}
        //else
        //{
        //    board.currentTurn = "x";
        //}
        board.currentTurn = board.currentTurn == "x" ? "o" : "x";
    }
}
