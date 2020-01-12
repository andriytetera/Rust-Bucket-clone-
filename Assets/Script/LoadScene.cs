using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private int indexNextScene;
    [SerializeField] private Animator anim;
    [SerializeField] public static LoadScene instance;
    [SerializeField] public Canvas canvas;
    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }
    public void LoadScene_btn(int indexScene)
    {
        LoadSaveSystem.instance.SaveDamage(1);
        LoadSaveSystem.instance.SaveGold(0);
        LoadSaveSystem.instance.SaveHealth(1);
        for (int i = 0; i < 5; i++)
        { 
            PlayerPrefs.SetString("slot" + i, "");
        }
        //LoadSaveSystem.instance.SaveInventory(null);
        LoadSaveSystem.instance.SaveMaxHealth(2);
        LoadSaveSystem.instance.SaveMaxSlotCount(1);
        if (anim != null)
        {
            this.indexNextScene = indexScene;
            //canvas.gameObject.SetActive(true);
            anim.SetTrigger("fabe_out");
        }else
        {
            SceneManager.LoadScene(indexNextScene);
        }
        


    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    anim.SetTrigger("fabe_out");
    //}
    //private IEnumerator NextLevelScene()
    //{
    //    yield return new WaitForSeconds(1f);
    //    anim.SetTrigger("fabe_out");
    //    LoadScene_btn(SceneManager.GetActiveScene().buildIndex+1);
    //}
    public void OnFabeComplete()
    {
        SceneManager.LoadScene(indexNextScene);
    }
}
