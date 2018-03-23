using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
	public string interactButton;

	public float interactDistance = 2f;
	public LayerMask interactLayer;

	public Image interactIcon;
    public Image moveSceneIcon;
	public Text interactText;
	public bool isInteracting;

    private int nBattery, nBottle, nKey, nLighter, nOil, nSalt;
    [SerializeField]
    Image m_Item0;
    [SerializeField]
    Image m_Item1;
    [SerializeField]
    Image m_Item2;
    [SerializeField]
    Image m_Item3;

    // Use this for initialization
    void Start () {
        m_Item0.enabled = false;
		if (interactIcon != null)
        {
			interactIcon.enabled = false;
		}
        if (moveSceneIcon != null)
        {
            moveSceneIcon.enabled = false;
        }
        if (interactText != null)
        {
            interactText.enabled = false;
        }
        nBattery = nBottle = nKey = nLighter = nOil = nSalt = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.L)) {
			GameObject.Find ("GameManager").SendMessage ("unchasing");
		}
		//key [L] to stop kitten chasing

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

		if (Physics.Raycast (ray, out hit, interactDistance, interactLayer))
        {
			if (isInteracting == false)
            {
                if (interactIcon != null && !(hit.collider.CompareTag("Stairs")))
                {
                    interactIcon.enabled = true;
                }

                if (moveSceneIcon != null && hit.collider.CompareTag("Stairs"))
                {
                    moveSceneIcon.enabled = true;
                }

                if (interactText != null)
                {
                    interactText.enabled = true;
                }

				if (Input.GetButtonDown (interactButton)) {
					
					//test
					GameObject.Find ("GameManager").SendMessage ("getgetget");
					//test

					if (hit.collider.CompareTag ("Salt")) {
						Destroy (hit.collider.gameObject);
						nSalt++;
						Debug.Log ("[소금] : " + nSalt);
					}
					if (hit.collider.CompareTag ("Key")) {
						Destroy (hit.collider.gameObject);
						nKey++;
						Debug.Log ("[키] : " + nKey);
					}
					if (hit.collider.CompareTag ("Oil")) {
						Destroy (hit.collider.gameObject);
						nOil++;
						Debug.Log ("[기름] : " + nOil);
					}
					if (hit.collider.CompareTag ("Battery")) {
						Destroy (hit.collider.gameObject);
						nBattery++;
						Debug.Log ("[건전지] : " + nBattery);
					}
					if (hit.collider.CompareTag ("Lighter")) {
						Destroy (hit.collider.gameObject);
						nLighter++;
						m_Item0.enabled = true;
						Transform copyform = m_Item0.transform;     // 여기 전부 수정...
						copyform.position = new Vector3 (30, 0, 0);
						if (nLighter >= 2)
							Instantiate (m_Item0, copyform);
						Debug.Log ("[라이터] : " + nLighter);
					}
					if (hit.collider.CompareTag ("Bottle")) {
						Destroy (hit.collider.gameObject);
						nBottle++;
						Debug.Log ("[물] : " + nBottle);
					}
					if (hit.collider.CompareTag ("Door")) {
						Debug.Log ("[문]");
						hit.collider.GetComponent<Door> ().ChangeDoorState ();
					}
					if (hit.collider.CompareTag ("Computer")) {
						Debug.Log ("[computer]");
					}
					if (hit.collider.CompareTag ("Stairs")) {
						Debug.Log ("[씬 이동]");
						SceneManager.LoadScene ("2nd");
					}
				}  
				
			}
		}
        else
        {
            interactIcon.enabled = false;
            moveSceneIcon.enabled = false;
            interactText.enabled = false;
        }

	

	}
}
