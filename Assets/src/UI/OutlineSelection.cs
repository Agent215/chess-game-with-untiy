using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    public Transform highlight;
    public Transform selection;

    public Transform previousSelection;
    public RaycastHit raycastHit;

    public GameBoard _gameBoard;

        void Start()
    {
        _gameBoard = GameObject.Find("pivot").GetComponent<GameBoard>();

    }
    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            Transform hitTransform = raycastHit.transform;
            highlight = FindSelectableChild(hitTransform);
            
            if (highlight != null && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    outline.OutlineColor = Color.magenta;
                    outline.OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

    

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                previousSelection = selection;
                selection = highlight;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;
                //TODO handle piece movement
                bool isSquare = selection.CompareTag("SelectableSqaure");
                if (isSquare && previousSelection != null)
                {
                    previousSelection.parent.GetComponent<GamePiece>().MovePiece(selection.GetComponent<GameSquare>().getsquareName(), _gameBoard);
                }
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
        }
    }

    private Transform FindSelectableChild(Transform parent)
    {

        // Recursively check child objects
        foreach (Transform child in parent)
        {
            Transform selectableChild = FindSelectableChild(child);
            if (selectableChild != null)
            {
                return selectableChild;
            }
        }

            if (parent.CompareTag("Selectable"))
        {
            return parent;
        }
        if(parent.CompareTag("SelectableSqaure"))
        {
            return parent;
        }

        return null;
    }


    private Transform FindSelectableSquare(Transform parent)
    {
        // Check if the parent itself is selectable
        if (parent.CompareTag("SelectableSqaure"))
        {
            return parent;
        }

        // Recursively check child objects
        foreach (Transform child in parent)
        {
            Transform selectableChild = FindSelectableSquare(child);
            if (selectableChild != null)
            {
                return selectableChild;
            }
        }

        return null;
    }
    
}