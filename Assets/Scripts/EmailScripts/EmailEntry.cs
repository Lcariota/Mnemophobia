using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailEntry : MonoBehaviour
{
    public TMP_Text subjectText;
    public TMP_Text senderText;
    private EmailData emailData;
    private EmailViewPanelController emailViewPanel;

    private bool isRead = false;

    public void Setup(EmailData data, EmailViewPanelController panel)
    {
        emailData = data;
        emailViewPanel = panel;

        subjectText.text = data.subject;
        senderText.text = data.sender;

        GetComponent<Button>().onClick.AddListener(OpenEmail);
    }

    void OpenEmail()
    {
        emailViewPanel.gameObject.SetActive(true);
        emailViewPanel.PopulateEmail(emailData, this);
    }

    public void MarkAsRead()
    {
        if (!isRead)
        {
            isRead = true;


            Color grayColor = new Color(0.5f, 0.5f, 0.5f); 
            subjectText.color = grayColor;
            senderText.color = grayColor;
        }
    }
}