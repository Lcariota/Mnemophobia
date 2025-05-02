using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class InboxManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject emailEntryPrefab;
    [SerializeField] private Transform contentParent; 
    [SerializeField] private EmailViewPanelController emailViewPanel;
    [SerializeField] private TMP_Text inboxLabel; 

    private List<EmailData> emailList = new List<EmailData>();

    void Start()
    {
        emailViewPanel.Initialize(this);
        LoadTestEmails();
        PopulateInbox();
    }

    void LoadTestEmails()
    {
        EmailData testEmail1 = new EmailData
        {
            sender = "KellyMoore@EmailCompany.com",
            subject = "Family Trip",
            body = "Flight Number: JDU 263 \n Destination: Flordia Keys \n Departure date: October 12, 1997 \n Departure time: 2:00 am \nPlease arrive at your gate at least 2 hours ahead of the printed time. ",
            choice1 = "Reply with Option A",
            choice2 = "Reply with Option B",
            isRead = false
        };

        EmailData testEmail2 = new EmailData
        {
            sender = "FastTax@EmailCompany.com",
            subject = "URGENT Action Required",
            body = "Dear Mr. Moore \nThe team at FastTax is reaching out today to remind you of your payment due on (October 15, 1997) for a total of $132.00 \nPlease use the online portal to set up a payment plan to avoid any fees. \n\n Your FastTax Team",
            choice1 = "Looks good!",
            choice2 = "Needs some changes.",
            isRead = false
        };

        emailList.Add(testEmail1);
        emailList.Add(testEmail2);
    }

    void PopulateInbox()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var email in emailList)
        {
            var entryGO = Instantiate(emailEntryPrefab, contentParent);
            var entry = entryGO.GetComponent<EmailEntry>();
            entry.Setup(email, emailViewPanel);
        }

        UpdateInboxLabel(); 
    }

    public void UpdateInboxLabel()
    {
        int unreadCount = 0;
        foreach (var email in emailList)
        {
            if (!email.isRead)
                unreadCount++;
        }

        inboxLabel.text = $"Inbox ({unreadCount})";
    }
}