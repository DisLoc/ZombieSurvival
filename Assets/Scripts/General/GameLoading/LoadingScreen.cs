using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Text _applicationVersionText;
    [SerializeField] private Text _loadingDescription;

    [SerializeField] private LoadingBar _loadingBar;

    [Header("Test")]
    public int loadingTime;
    private float _timer;

    private void OnEnable()
    {
        _applicationVersionText.text = Application.version;
        _loadingBar.Initialize();
        _timer = 0;
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        
        if (_timer <= loadingTime)
        {
            _loadingBar.SetValue((int)(_timer / loadingTime * 100));
        }
        if (_timer >= loadingTime)
        {
            _loadingBar.SetValue(100);
        }
        
        float updater = _timer % 1.5f;
        if (updater <= 0.5f)
        {
            _loadingDescription.text = "Loading.";
        }
        else if (updater <= 1f)
        {
            _loadingDescription.text = "Loading..";
        }
        else if (updater <= 1.5f)
        {
            _loadingDescription.text = "Loading...";
        }

        if (_timer >= loadingTime + 1f)
        {
            EventBus.Publish<IGameStartHandler>(handler => handler.OnGameStart());

            gameObject.SetActive(false);
        }
    }
}
