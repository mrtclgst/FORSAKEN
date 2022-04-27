using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerRoot, lookRoot;
    [SerializeField] bool invert, canUnlock = true; // alt ile mouse i ekrana getirmek icin tanimladik
    [SerializeField] float sensivity = 5f, smoothWeight = 0.4f, rollAngle = 10f, rollSpeed = 3f;
    [SerializeField] int smootSteps = 10;
    [SerializeField] Vector2 defaultLookLimit = new Vector2(-70f, 80f);
    Vector2 lookAngles, currentMouseLook, smoothMove;
    float currentRollAngle;
    int lastLookFrame;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //cursoru merkeze kitliyor ve gorunmez yapiyor.
    }
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }
    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    void LookAround()
    {
        //Y �LE X � ters yazmamizin nedeni rotasyonel olarak dondurgumuz icin ters isliyor.
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MOUSEY), Input.GetAxis(MouseAxis.MOUSEX));
        lookAngles.x += currentMouseLook.x * sensivity * (invert ? 1f : -1f);
        //yukari cektigimizde yukari gelmesi icin inverti -1 yaptik.
        lookAngles.y += currentMouseLook.y * sensivity;
        //x bakisimizi limitlendiriyoruz.
        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimit.x, defaultLookLimit.y);
        //x bakisimizi gidecegimiz yere goturuyoruz.
        //currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.MOUSEX) * rollAngle
        //    , Time.deltaTime * rollSpeed);
        //geciste bize bir bas donmesi efekti yaratiyor 0 da tutup o etkiyi kaldirabiliyoruz.
        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f /*currentRollAngle*/);
        playerRoot.localRotation = Quaternion.Euler(0, lookAngles.y, 0);
        //playeri rotate etmemizin sebebi lookRootu rotateledigimiz zaman movementimiz farkli calismaya basliyor.


    }
}
