using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : UIMenu
{
    [SerializeField] private Text _messageText;

    [SerializeField] private float _hideDelay = 3f;

    public void ShowMessage(string message)
    {
        _messageText.text = message;

        base.Display(true);

        StopAllCoroutines();
        StartCoroutine(WaitHide());
    }

    private IEnumerator WaitHide()
    {
        yield return new WaitForSecondsRealtime(_hideDelay);

        _animator.SetTrigger("Hide");
    }
}
