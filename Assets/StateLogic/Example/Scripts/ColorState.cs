using StateLogic;
using UnityEngine;
using UnityEngine.UI;

public class ColorState : State {
    [SerializeField]
    private Image _image;

    public override void OnEnter() {
        Debug.Log("ColorState: Enter");
        _image.color = Color.red;
    }
}
