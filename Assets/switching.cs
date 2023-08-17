using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switching : MonoBehaviour
{

    public int activeWepon;
    private AudioSource audi;
    [SerializeField] List <GameObject> wepons;
    [SerializeField] Transform originalLoc;
    public GameObject activeWeponObject;


    public bool switchin = false;

    private void Start()
    {

        activeWepon = 0;
        activeWeponObject = wepons[activeWepon];
    }
    void Update()
    {
       // Debug.Log(activeWepon);
        activeWeponObject = wepons[activeWepon];
        if (Input.GetKeyDown(KeyCode.Z) && !Input.GetMouseButton(1))
        {
            switchin = true;
            audi.PlayOneShot(audi.clip);
        }
        if (switchin)
        {
            RotateWepon(wepons[activeWepon], 45, -0.48f);
        }
        if (wepons[activeWepon].transform.localPosition.z <=-0.2f && switchin)
        {
           
            wepons[activeWepon].SetActive(false);
            if (activeWepon + 1 == 3)
            {
                
                activeWepon=0;
                Debug.Log(activeWepon);
            }
            else
            {
                activeWepon++;
            }
            wepons[activeWepon].SetActive(true);
                //Debug.Log("hello");
                wepons[activeWepon].GetComponent<breathing>().enabled = false;
                wepons[activeWepon].GetComponent<breathing>().enabled = true;
            switchin = false;
        }
       /* if (!switchin)
        {
            wepons[activeWepon].transform.localRotation = Quaternion.Lerp(
                                                    wepons[activeWepon].transform.localRotation,
                                                    Quaternion.Euler(0, 0, 0),
                                                    Time.deltaTime * 4000
                                                   );
        }

        */
    }

    private void RotateWepon(GameObject we, int targetAngle, float targetPos)
    {
        
        //switchin = true;
        
       /* we.transform.localRotation = Quaternion.Lerp(
                                                        we.transform.localRotation,
                                                        Quaternion.Euler(targetAngle, we.transform.rotation.y, we.transform.rotation.z),
                                                        Time.deltaTime * 40
                                                       );*/
        we.transform.localPosition = Vector3.Lerp(we.transform.localPosition, new Vector3(we.transform.localPosition.x, we.transform.localPosition.y, -0.48f), Time.deltaTime * 20);
    }

    private void Awake()
    {
        // a1 = we1.GetComponent<Animator>();
        audi = this.GetComponent<AudioSource>();
        // a2 = we2.GetComponent<Animator>();
    }



   
}
