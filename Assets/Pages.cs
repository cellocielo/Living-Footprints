using UnityEngine;
using UnityEngine.UI;

public class Pages : MonoBehaviour
{
   public GameObject[] pages;
   public Button nextButton, prevButton;
   private int currentPage = 0;
   
   void Start()
   {
       ShowPage(0);
       
       if (nextButton != null)
           nextButton.onClick.AddListener(NextPage);
       if (prevButton != null)
           prevButton.onClick.AddListener(PrevPage);
   }
   
   void OnEnable()
   {
       ShowPage(0);
   }
   
   void Update()
   {
       if (Input.GetKeyDown(KeyCode.RightArrow))
       {
           NextPage();
       }
       if (Input.GetKeyDown(KeyCode.LeftArrow))
       {
           PrevPage();
       }
   }
   
   void NextPage()
   {
       if (currentPage < pages.Length - 1)
       {
           ShowPage(currentPage + 1);
       }
   }
   
   void PrevPage()
   {
       if (currentPage > 0)
       {
           ShowPage(currentPage - 1);
       }
   }
   
   void ShowPage(int pageIndex)
   {
       foreach (GameObject page in pages)
       {
           page.SetActive(false);
       }
       
       currentPage = pageIndex;
       if (pages[currentPage] != null)
       {
           pages[currentPage].SetActive(true);
       }
       
       if (prevButton != null)
           prevButton.interactable = currentPage > 0;
       if (nextButton != null)
           nextButton.interactable = currentPage < pages.Length - 1;
   }
}