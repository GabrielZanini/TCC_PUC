using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    public bool hideAtStart = true;

    [Header("Data")]
    public Text data;

    [Header("Buttons Labels")]
    public GameObject buttons;
    public Text speed;
    public Text rate;
    public Text distance;
    public Text angle;
    public Text bullets;

    [Header("Player")]
    public StatusShip statusPlayer;
    public ShootShip shootPlayer;

    string text = "";
    bool hide = false;



    private void Start()
    {
        if (hideAtStart)
        {
            Hide();
        }
    }

    private void Update()
    {
        PrintFPS();

        PrintSpeed();
        PrintRate();
        PrintDistance();
        PrintAngle();
        PrintBullets();
    }




    private void PrintFPS()
    {
        text = "";
        text += "FPS: " + ((1f / Time.deltaTime) * Time.timeScale) .ToString("0.0") + "\n";

        text += "\n";
        text += "Camera Resolution: " + Camera.main.scaledPixelWidth + "x" + Camera.main.scaledPixelHeight + "\n";
        text += "Camera Aspect: " + Camera.main.aspect + "\n";
        text += "Screen Orientation: " + Screen.orientation.ToString() + "\n";

        text += "\n";
        text += "Ative Bullets: " + BulletPool.Instance.ActiveCount + "\n";
        text += "Inactive Bullets: " + BulletPool.Instance.InactiveCount + "\n";
        text += "Total Bullets: " + BulletPool.Instance.TotalCount + "\n";
        text += "Max Bullets: " + BulletPool.Instance.MaxCount + "\n";

        text += "\n";
        text += "Ative Asteroid: " + AsteroidPool.Instance.ActiveCount + "\n";
        text += "Inactive Asteroid: " + AsteroidPool.Instance.InactiveCount + "\n";
        text += "Total Asteroid: " + AsteroidPool.Instance.TotalCount + "\n";
        text += "Max Asteroid: " + AsteroidPool.Instance.MaxCount + "\n";

        text += "\n";
        text += "TimeBodys: " + TimeController.Instance.TimebodysCount + "\n";
        text += "Max PITs: " + TimeController.Instance.MaxPointsInTime + "\n";
        text += "Points in time: " + TimeController.Instance.PointsInTimeCount + "\n";
        text += "Current PIT: " + TimeController.Instance.CurrentPointInTime + "\n";

        text += "\n";
        text += "Delta Time: " + Time.deltaTime + "\n";
        text += "Fixed Delta Time: " + Time.fixedDeltaTime + "\n";
        text += "Time Scale: " + Time.timeScale + "\n";
        text += "UnscaledDeltaTime: " + Time.unscaledDeltaTime + "\n";

        text += "\n";
        text += "Application.Platform: " + Application.platform.ToString() + "\n";
        text += "GameManager.Platform: " + GameManager.Instance.Platform.ToString() + "\n";

        data.text = text;
    }

    private void PrintSpeed()
    {
        speed.text = statusPlayer.shootingSpeed.ToString("0");
    }

    private void PrintRate()
    {
        rate.text = statusPlayer.shootingRate.ToString("0.00");
    }

    private void PrintDistance()
    {
        distance.text = shootPlayer.bulletDistance.ToString("0");
    }

    private void PrintAngle()
    {
        angle.text = shootPlayer.bulletAngle.ToString("0");
    }

    private void PrintBullets()
    {
        bullets.text = shootPlayer.bulletsPerShoot.ToString("0");
    }

    public void Hide()
    {
        data.gameObject.SetActive(hide);
        buttons.SetActive(hide);

        hide = !hide;
    }



    public void NBullett(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.shoot.MoreBullets();
        }
        else
        {
            GameManager.Instance.Player.shoot.LessBullets();
        }
    }

    public void NAngle(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.shoot.MoreAngle();
        }
        else
        {
            GameManager.Instance.Player.shoot.LessAngle();
        }
    }
    
    public void NDistance(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.shoot.MoreDistance();
        }
        else
        {
            GameManager.Instance.Player.shoot.LessDistance();
        }
    }

    public void NRate(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.status.MoreShootingRate();
        }
        else
        {
            GameManager.Instance.Player.status.LessShootingRate();
        }
    }
    
    public void NSpeed(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.status.MoreShootingSpeed();
        }
        else
        {
            GameManager.Instance.Player.status.LessShootingSpeed();
        }
    }

}
