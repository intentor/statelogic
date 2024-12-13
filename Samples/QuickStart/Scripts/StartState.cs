using System.Collections;
using StateLogic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : State {
    private readonly Color ColorOriginal = Color.white;
    private readonly Color ColorTransparent = new Color(255, 255, 255, 0.5f);

    [SerializeField]
    private Image _image;

    public override void OnEnter() {
        Debug.Log("StartState: Enter");
        StartCoroutine(BlinkAndChangeColor());
    }

    public override void OnExit() {
        Debug.Log("StartState: Exit");
    }

    private IEnumerator BlinkAndChangeColor() {
        for (int i = 0; i < 10; i++) {
            _image.color = i % 2 == 0 ? ColorOriginal : ColorTransparent;
            yield return new WaitForSeconds(0.25f);
        }

        SendEvent("ChangeColor");
    }
}
