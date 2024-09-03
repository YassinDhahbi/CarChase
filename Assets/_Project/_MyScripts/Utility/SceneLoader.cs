using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AudioClip _carEngineStartSfx;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAnimation(sceneName));
    }

    IEnumerator LoadSceneAnimation(string sceneName)
    {
        _animator.Play("SceneLoader_Anim");
        AudioManager.Instance.PlaySound(_carEngineStartSfx);
        float skillComboCounting = 2f;
        yield return new WaitForSeconds(skillComboCounting);
        Debug.Log("Load");
        SceneManager.LoadScene(sceneName);
    }
}
