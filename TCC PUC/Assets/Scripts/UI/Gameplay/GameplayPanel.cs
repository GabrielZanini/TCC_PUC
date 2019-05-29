using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour
{
    public GameObject sliderRight;
    public GameObject sliderLeft;
    public GameObject sliderBottom;

    [SerializeField] Direction position = Direction.Right;
    public Direction Position {
        get { return position; }
        private set { position = value; Save(); }
    }



    private void Start()
    {
        Load();
        ChangeSliderPosition();
    }



    private void DisableAllSliders()
    {
        sliderRight.SetActive(false);
        sliderLeft.SetActive(false);
        sliderBottom.SetActive(false);
    }

    public void SetSlideRight()
    {
        Position = Direction.Right;
        DisableAllSliders();
        sliderRight.SetActive(true);
    }

    public void SetSlideLeft()
    {
        Position = Direction.Left;
        DisableAllSliders();
        sliderLeft.SetActive(true);
    }

    public void SetSlideBottom()
    {
        Position = Direction.Down;
        DisableAllSliders();
        sliderBottom.SetActive(true);
    }

    public void ChangeSliderPosition()
    {
        if (Position == Direction.Right)
        {
            SetSlideRight();
        }
        else if (Position == Direction.Left)
        {
            SetSlideLeft();
        }
        else if (Position == Direction.Down)
        {
            SetSlideBottom();
        }
    }



    void Save()
    {
        PlayerPrefs.SetInt("TimeBarPosition", (int)position);
    }

    void Load()
    {
        if (PlayerPrefs.HasKey("TimeBarPosition"))
        {
            position = (Direction)PlayerPrefs.GetInt("TimeBarPosition");
        }        
    }
}
