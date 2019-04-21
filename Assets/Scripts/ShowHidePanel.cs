using UnityEngine;

/**
* \brief Used by buttons to show and hide the options panel
*/
public class ShowHidePanel : MonoBehaviour
{
    //panel to hide and show
    public GameObject Panel;

    /**
     * \brief on button click, hide if panel is shown and show
     *        if it is hidden
     * 
     * \return null
     */
    public void PanelView()
    {
        if (Panel.gameObject.activeSelf == true)
        {
            Panel.gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
        }
    }
}
