using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public List<ItemInventory> Items;
    public GameObject DataBase;
    public GameObject cellContainer;

    [SerializeField]
    private KeyCode showInventory;
    [SerializeField]
    private KeyCode takeButton;

    [SerializeField]
    private FirstPersonController player;
    [SerializeField]
    private GameObject point;

    public GameObject DragPrefab;
    public GameObject PopUpMenu;

    [Header("Message")]
    [SerializeField]
    private GameObject messageManager;
    [SerializeField]
    private GameObject message;


    void Start()
    {
        Items = new List<ItemInventory>();

        cellContainer.SetActive(false);

        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
            Items.Add(new ItemInventory());
        }
    }

    void Update()
    {
        ToggleInventory();
        if (Input.GetKeyDown(takeButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.GetComponent<ItemInventory>())
                {
                    ItemInventory currentItem = hit.collider.GetComponent<ItemInventory>();
                    Message(currentItem);
                    AddItem(currentItem);
                }
            }
        }
    }

    void Message(ItemInventory currentItem)
    {
        GameObject msgObj = Instantiate(message);
        msgObj.transform.SetParent(messageManager.transform);
        Image msgImg = msgObj.transform.GetChild(0).GetComponent<Image>();
        msgImg.sprite = currentItem.Icon;
        Text msgText = msgObj.transform.GetChild(1).GetComponent<Text>();
        msgText.text = currentItem.ItemName;
    }

    void AddUnstackableItem(ItemInventory currentItem)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].ItemId == 0)
            {
                Items[i] = currentItem;
                Items[i].ItemCount = 1;
                DisplayItems();
                Destroy(currentItem.gameObject);
                break;
            }
        }
    }

    void AddStackableItem(ItemInventory currentItem)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].ItemId == currentItem.ItemId)
            {
                Items[i].ItemCount++;
                DisplayItems();
                Destroy(currentItem.gameObject);
                return;
            }
        }
        AddUnstackableItem(currentItem);
    }

    void ToggleInventory()
    {
        if (Input.GetKeyDown(showInventory))
        {
            if (cellContainer.activeSelf)
            {
                cellContainer.SetActive(false);
                player.enabled = true;
                point.SetActive(true);
                //Time.timeScale = 1;
            }
            else
            {
                cellContainer.SetActive(true);
                player.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                point.SetActive(false);
                //Time.timeScale = 0;
            }
        }
    }

    void AddItem(ItemInventory currentItem)
    {
        if (currentItem.IsStackable == true)
        {
            AddStackableItem(currentItem);
        }
        else
        {
            AddUnstackableItem(currentItem);
        }
    }

    public void DisplayItems()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform count = icon.GetChild(0);
            Text txt = count.GetComponent<Text>();

            Image img = icon.GetComponent<Image>();
            if (Items[i].ItemId != 0)
            {               
                img.enabled = true;
                img.sprite = Items[i].Icon;
                if(Items[i].ItemCount > 1)
                {
                    txt.text = Items[i].ItemCount.ToString();
                }
                else
                {
                    txt.text = null;
                }
            }
            else
            {
                img.enabled = false;
                img.sprite = null;
                txt.text = null;
            }
        }
    }
}
