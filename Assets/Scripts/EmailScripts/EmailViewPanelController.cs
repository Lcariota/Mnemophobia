using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailViewPanelController : MonoBehaviour
{
    [Header("Text Fields")]
    public TMP_Text senderText;
    public TMP_Text subjectText;
    public TMP_Text bodyText;

    [Header("Close Button")]
    public Button closeButton;

    private EmailData currentEmail;
    private EmailEntry currentEmailEntry;
    private InboxManager inboxManager;

    public void PopulateEmail(EmailData email, EmailEntry emailEntry)
    {
        currentEmail = email;
        currentEmailEntry = emailEntry;

        senderText.text = $"{email.sender}";
        subjectText.text = $"{email.subject}";
        bodyText.text = email.body;

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(CloseEmail);
    }

    void CloseEmail()
    {
        currentEmailEntry.MarkAsRead();
        inboxManager.UpdateInboxLabel();
        gameObject.SetActive(false);
    }
    
    public void Initialize(InboxManager manager)
    {
        inboxManager = manager;
    }
}