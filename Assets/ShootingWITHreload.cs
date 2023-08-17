using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWITHreload : MonoBehaviour
{
    private GameObject k;
    public GameObject a;
    public GameObject a2;
    public GameObject a3;
    private GameObject bullets;
    private GameObject y22;

    public float f_shoot, f_up;
    public float time_shooting, spread, t_shots;

    //for gun
    public float timeBwShooting, range, reloadingTime, timeBwShots;
    public int magazineSize, bulletperclick;
    public bool allowButtonHold;
    private int bulletleft, bulletShot;

    //common bools
    private bool shooting, readyToShoot, reloading;

    //Camera, particleSystem etc.
    public Camera fps;
    public Transform point;
    public ParticleSystem flash;

    private void Awake()
    {
        bulletleft = magazineSize;
        readyToShoot = true;
        k = a;
    }

    private void Update()
    {
        k = a2;
        MouseInput();
    }

    private void MouseInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(bulletleft==0 && !reloading)
        {
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletleft > 0)
        {
            bulletShot = bulletperclick;
            flash.Play();
            Shoot();
        }
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinish", reloadingTime);
    }

    private void ReloadFinish()
    {
        bulletleft = magazineSize;
        reloading = false;
    }

    private void Shoot()
    {
        GameObject y22 = k;
        readyToShoot = false;

        //RayCast
        Ray middle = fps.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit shot;
        Vector3 target;

        if (Physics.Raycast(middle, out shot))
        {
            target = shot.point;
        }
        else
        {
            target = middle.GetPoint(75);
        }
        Vector3 dir = target - point.position;
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 dir2 = dir + new Vector3(x, y, 0);

        bullets = Instantiate(y22, point.position, Quaternion.identity);

        bullets.transform.forward = dir2.normalized;

        bullets.GetComponent<Rigidbody>().AddForce(dir2.normalized * f_shoot, ForceMode.Impulse);
        bullets.GetComponent<Rigidbody>().AddForce(fps.transform.up * f_up, ForceMode.Impulse);


        bulletleft--;
        bulletShot--;
        Invoke("ResetShot", timeBwShooting);

        if (bulletShot > 0 && bulletleft > 0)
        Invoke("Shoot", timeBwShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
}
