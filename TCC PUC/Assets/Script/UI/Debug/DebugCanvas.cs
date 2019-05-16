using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    public Text fps;
    public Text speed;
    public Text rate;
    public Text distance;
    public Text angle;
    public Text bullets;

    public StatusShip statusPlayer;
    public ShootShip shootPlayer;

    string text = "";

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
        text += "FPS: " + (1f / Time.deltaTime).ToString("0.0") + "\n";
        text += "Camera Resolution: " + Camera.main.scaledPixelWidth + "x" + Camera.main.scaledPixelHeight + "\n";
        text += "Camera Aspect: " + Camera.main.aspect + "\n";
        text += "Screen Orientation: " + Screen.orientation.ToString() + "\n";
        text += "Bullets: " + BulletPool.Instance.ActiveCount + "\n";
        text += "Asteroid: " + AsteroidPool.Instance.ActiveCount + "\n";

        fps.text = text;
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
}
