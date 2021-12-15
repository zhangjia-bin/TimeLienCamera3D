using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InputManager : MonoBehaviour
{
    private bool _isDragging;
    private Vector3 _startpos;
    private Vector3 _endPos;
    private Rect _selectionRect;
    public float offsetRad=5;
    Camera mainCamera;

    //选中角色集合
    List<Solider> Solider=new List<Solider>();
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessDragRect();

        ProcessMovement();
       
    }

    private void ProcessMovement()
    {
        if (Input.GetMouseButtonDown(1))
        { 
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit,1000,1<<8))
            {
                if (hit.transform.CompareTag("Foreigners"))
                {
                    for (int i = 0; i < Solider.Count; i++)
                    {
                        var solider = Solider[i];
                        solider.MoveAndAttack(hit.transform);
                    }

                }
            }
            else if(Physics.Raycast(ray, out hit, 1000, 1 << 9))
            {
                foreach (var item in Solider)
                {

                    var segRad = 2 * Mathf.PI / Solider.Count;
                    if (Physics.Raycast(ray, out hit, 1000, 1 << 9))
                    {
                        var dest = hit.point;
                        for (int i = 0; i < Solider.Count; i++)
                        {
                            var theta = segRad * i;
                            var offset = new Vector3(offsetRad * Mathf.Sin(theta), 0, (float)(offsetRad * Math.Cos(theta)));
                            Solider[i].SetPos(dest + offset);
                        }

                    }

                }
            }
            
        }
    }

    private void ProcessDragRect()
    {
        bool IsSelect = false;
        
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _startpos = Input.mousePosition;
        }
       
        //松开的时候判断所有的物体
        if (Input.GetMouseButtonUp(0))
        {
            //判断选中之前清空掉！
            ClearSelectionList();
            

            UIManager.Instance.HideRectangle();

            var allUnits = GameMgr.Instacne.GetAllSelectableUnits();

            
            foreach (var item in allUnits)
            {
                var screenPos = mainCamera.WorldToScreenPoint(item.position);
                if (_selectionRect.Contains(screenPos))
                {
                    Debug.Log("士兵在框选范围内！！");
                    //处理士兵的可视化处理
                  var s=  item.GetComponent<Solider>();

                    AddSelectionList(s);

                    IsSelect = true;

                }
            
            }
            _isDragging = false;

            if (IsSelect)
            {
                UIManager.Instance.HideRectangle();
            }
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,1000))
            {
               
                if (hit.transform.CompareTag("Locals"))
                {
                    var s = hit.transform.GetComponent<Solider>();
                    AddSelectionList(s);
                }
            }
        }
        if (_isDragging)
        {
            _endPos = Input.mousePosition;

            Vector2 center = (_startpos + _endPos) / 2;
            var size = new Vector2(Mathf.Abs(_endPos.x - _startpos.x), Mathf.Abs(_endPos.y - _startpos.y));

            _selectionRect = new Rect(center - size / 2, size);


            UIManager.Instance.SetRectangle(center, size);
        }
      
       
        
    }

    private void ClearSelectionList()
    {
        foreach (var item in Solider)
        {
            item.SetSelected(false);
        }
        Solider.Clear();
    }

    private void AddSelectionList(Solider item)
    {
        item.SetSelected(true);
        //放入集合当中
        Solider.Add(item);
    }
}

