using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusTutorialManager : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("change", 0);
    }

    void ChangeAnimation() {
        animator.SetInteger("change", animator.GetInteger("change") + 1);
        if (animator.GetInteger("change") >= 7) {
            goToVersus();
        }
    }

    public void goToVersus() {
        SceneManager.LoadScene("VersusScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            goToVersus();
        } else if (Input.anyKeyDown) {
            ChangeAnimation();
        }
    }
}
