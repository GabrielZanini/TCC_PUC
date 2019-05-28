using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    [Header("Time Controller")]
    public TimeController timeController;
    
    [Header("Hide")]
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
        text += "TimeBodys: " + timeController.TimebodysCount + "\n";
        text += "Max PITs: " + timeController.MaxPointsInTime + "\n";
        text += "Points in time: " + timeController.PointsInTimeCount + "\n";
        text += "Current PIT: " + timeController.CurrentPointInTime + "\n";

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
        speed.text = shootPlayer.guns[0].bulletSpeed.ToString("0");
    }

    private void PrintRate()
    {
        rate.text = shootPlayer.guns[0].bulletRate.ToString("0.00");
    }

    private void PrintDistance()
    {
        distance.text = shootPlayer.guns[0].BarrelAngle.ToString("0");
    }

    private void PrintAngle()
    {
        angle.text = shootPlayer.guns[0].MuzzleAngle.ToString("0");
    }

    private void PrintBullets()
    {
        bullets.text = shootPlayer.guns[0].MaxBarrels.ToString("0");
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
            GameManager.Instance.Player.shoot.AddBullet();
        }
        else
        {
            GameManager.Instance.Player.shoot.RemoveBullet();
        }
    }

    public void NAngle(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.shoot.AddMuzzleAngle();
        }
        else
        {
            GameManager.Instance.Player.shoot.RemoveMuzzleAngle();
        }
    }
    
    public void NDistance(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.shoot.AddBarrelAngle();
        }
        else
        {
            GameManager.Instance.Player.shoot.RemoveBarrelAngle();
        }
    }

    public void NRate(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.shoot.AddBulletRate();
        }
        else
        {
            GameManager.Instance.Player.shoot.RemoveBulletRate();
        }
    }
    
    public void NSpeed(bool add)
    {
        if (add)
        {
            GameManager.Instance.Player.shoot.AddBulletSpeed();
        }
        else
        {
            GameManager.Instance.Player.shoot.RemoveBulletSpeed();
        }
    }

}
