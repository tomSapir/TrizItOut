using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hitcode_RoomEscape;
public class PanelReadJournal : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Find("bg").Find("btnCancel").GetComponent<Button>().onClick.AddListener(Cancel);
        transform.Find("bg").Find("btnTake").GetComponent<Button>().onClick.AddListener(Take);

    }

    private void OnEnable()
    {
        transform.Find("bg").Find("btnTake").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnTake");
        transform.Find("bg").Find("btnCancel").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnCancel");

    }

    void Take()
    {
        JournalData tjournal = GameData.Instance.getJournalByName(cName);
        GameData.Instance.AddJournalByName(cName);
        Time.timeScale = 1;

        PlayerPrefs.SetInt(takedParam+0, 1);
        myJournal.SetActive(false);
        GameData.Instance.SaveGame();
        StartCoroutine("waitUnlock");
    }
    void Cancel()
    {
       
        Time.timeScale = 1;
        StartCoroutine("waitUnlock");

    }

    IEnumerator waitUnlock()
    {
        yield return new WaitForEndOfFrame();
        GameData.Instance.locked = false;
        panelReadJournal.SetActive(false);
    }



    // Update is called once per frame
    void Update () {
		
	}

    string cName, cDesc;
  
    GameObject panelReadJournal;
    string takedParam = "";
    GameObject myJournal;
    public void showPanel(GameObject journal , string take_param = "")
    {
        panelReadJournal = GameObject.Find("UICam").transform.Find("Canvas").GetComponent<GameUI>().panelReadJournal;
        myJournal = journal;
        takedParam = take_param;
        panelReadJournal.SetActive(true);
        string journalName = journal.name;
        JournalData tjournal = GameData.Instance.getJournalByName(journalName);
        cName = tjournal.journalName;
        cDesc = tjournal.journalDesc;

        //interactive = g.transform.parent.parent.parent.gameObject;
        Text previewTitle = transform.Find("bg").Find("previewTitle").GetComponent<Text>();
        Text previewDesc = transform.Find("bg").Find("previewDesc").GetComponent<Text>();

        if (tjournal.nameLocalId != null && tjournal.nameLocalId.Trim() != "")
        {
            previewTitle.text = Localization.Instance.GetString(tjournal.nameLocalId);
            previewDesc.text = Localization.Instance.GetString(tjournal.localId);
        }
        else
        {
            previewTitle.text = cName;
            previewDesc.text = cDesc;
        }
        Image tIcon = previewTitle.transform.Find("icon").GetComponent<Image>();
        if (tjournal.icon != null)
        {

            tIcon.enabled = true;
            tIcon.sprite = tjournal.icon;

        }
        else
        {
            tIcon.enabled = false;
        }

        Image tIllustration = previewDesc.transform.Find("illustration").GetComponent<Image>();
        if (tjournal.illustration != null)
        {
           
            tIllustration.enabled = true;
            tIllustration.sprite = tjournal.illustration;
            tIllustration.SetNativeSize();
        }
        else
        {
            tIllustration.enabled = false;
        }

        transform.Find("bg").Find("btnTake").GetComponent<Button>().interactable = take_param.Trim() != "";
        if (take_param.Trim() == "")
        {
            transform.Find("bg").Find("btnTake").GetComponentInChildren<Text>().color = new Color(1, 1, 1, .3f);
        }
        else
        {
            transform.Find("bg").Find("btnTake").GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1);
        }
       Time.timeScale = 0;
        GameData.Instance.locked = true;

    }
}
