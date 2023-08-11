using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hitcode_RoomEscape.UI;
namespace Hitcode_RoomEscape {
    public class PanelJournal : MonoBehaviour, IListScrollerDelegate
    {
        /// <summary>
        /// The image that shows which item is selected
        /// </summary>
        public Image selectedImage;

        public Text selectedName;
        public Text selectedDesc;

        /// <summary>
        /// The list of inventory data
        /// </summary>
        private SimpleList<JournalData> _data;

        /// <summary>
        /// The vertical inventory scroller
        /// </summary>
        public ListScroller vScroller;


        /// <summary>
        /// The cell view prefab for the vertical scroller
        /// </summary>
        public ListScrollerCellView vCellViewPrefab;


        /// <summary>
        /// This function handles the cell view's button click event
        /// </summary>
        /// <param name="cellView">The cell view that had the button clicked</param>
        private void CellViewSelected(ListScrollerCellView cellView)
        {
            if (cellView == null)
            {
                // nothing was selected
                selectedImage.gameObject.SetActive(false);
                selectedName.text = "No Journals";
                selectedDesc.text = "";
            }
            else
            {
                // get the selected data index of the cell view
                var selectedDataIndex = (cellView as JouralCellView).DataIndex;

                // loop through each item in the data list and turn
                // on or off the selection state. This is done so that
                // any previous selection states are removed and new
                // ones are added.
                for (var i = 0; i < _data.Count; i++)
                {
                    _data[i].Selected = (selectedDataIndex == i);
                }

                selectedImage.gameObject.SetActive(true);
                selectedImage.sprite = _data[selectedDataIndex].illustration;

                if (_data[selectedDataIndex].nameLocalId.Trim() == "")
                {
                    selectedName.text = _data[selectedDataIndex].journalName;
                    selectedDesc.text = _data[selectedDataIndex].journalDesc;
                }
                else
                {
                    selectedName.text = Localization.Instance.GetString(_data[selectedDataIndex].nameLocalId);
                    selectedDesc.text = Localization.Instance.GetString(_data[selectedDataIndex].localId);
                }

                if(_data[selectedDataIndex].illustration != null)
                {
                    selectedImage.enabled = true;
                    selectedImage.sprite = _data[selectedDataIndex].illustration;
        
                }
                else
                {
                    selectedImage.enabled = false;
                }
            }
        }

        #region Controller UI Handlers

        /// <summary>
        /// This handles the toggle for the masks
        /// </summary>
        /// <param name="val">Is the mask on?</param>
        public void MaskToggle_OnValueChanged(bool val)
        {
            // set the mask component of each scroller
            vScroller.GetComponent<Mask>().enabled = val;
            //hScroller.GetComponent<Mask>().enabled = val;
        }

        /// <summary>
        /// This handles the toggle fof the looping
        /// </summary>
        /// <param name="val">Is the looping on?</param>
        public void LoopToggle_OnValueChanged(bool val)
        {
            // set the loop property of each scroller
            vScroller.Loop = val;
            //hScroller.Loop = val;
        }

        #endregion

        #region ListScroller Callbacks

        /// <summary>
        /// This callback tells the scroller how many inventory items to expect
        /// </summary>
        /// <param name="scroller">The scroller requesting the number of cells</param>
        /// <returns>The number of cells</returns>
        public int GetNumberOfCells(ListScroller scroller)
        {
            return _data.Count;
        }

        /// <summary>
        /// This callback tells the scroller what size each cell is.
        /// </summary>
        /// <param name="scroller">The scroller requesting the cell size</param>
        /// <param name="dataIndex">The index of the data list</param>
        /// <returns>The size of the cell (Height for vertical scrollers, Width for Horizontal scrollers)</returns>
        public float GetCellViewSize(ListScroller scroller, int dataIndex)
        {
            if (scroller == vScroller)
            {
                // return a static height for all vertical scroller cells
                return 50f;
            }
            else
            {
                // return a static width for all horizontal scroller cells
                return 150f;
            }
        }

        /// <summary>
        /// This callback gets the cell to be displayed by the scroller
        /// </summary>
        /// <param name="scroller">The scroller requesting the cell</param>
        /// <param name="dataIndex">The index of the data list</param>
        /// <param name="cellIndex">The cell index (This will be different from dataindex if looping is involved)</param>
        /// <returns>The cell to display</returns>
        public ListScrollerCellView GetCellView(ListScroller scroller, int dataIndex, int cellIndex)
        {

            JouralCellView cellView = scroller.GetCellView(vCellViewPrefab) as JouralCellView;



            cellView.name = _data[dataIndex].journalDesc;
            // set the selected callback to the CellViewSelected function of this controller. 
            // this will be fired when the cell's button is clicked
            cellView.selected = CellViewSelected;

            // set the data for the cell
            cellView.SetData(dataIndex, _data[dataIndex], (scroller == vScroller));

            // return the cell view to the scroller
            return cellView;
        }

        // Use this for initialization
        bool inited = false;
        void Start() {
            // set up the delegates for each scroller

            vScroller.Delegate = this;
            //hScroller.Delegate = this;

            // reload the data
            Reload();
            inited = true;
        }

        void OnEnable()
        {
            if (!inited) return;
            vScroller.Delegate = this;
            //hScroller.Delegate = this;

            // reload the data
            Reload();
        }

        /// <summary>
        /// This function sets up our inventory data and tells the scrollers to reload
        /// </summary>
        private void Reload()
        {
            // if the data existed previously, loop through
            // and remove the selection change handlers before
            // clearing out the data.
            if (_data != null)
            {
                for (var i = 0; i < _data.Count; i++)
                {
                    _data[i].selectedChanged = null;
                }
            }

            // set up a new inventory list
            _data = new SimpleList<JournalData>();


            for (int i = 0; i < GameData.Instance.journalDatas.Count; i++)
            {
                string tname = GameData.Instance.journalDatas[i].journalName;
                string tjournalDesc = GameData.Instance.journalDatas[i].journalDesc;
                Sprite tIcon = GameData.Instance.journalDatas[i].icon;
                Sprite tIllustration = GameData.Instance.journalDatas[i].illustration;
                string tnameLocalId = GameData.Instance.journalDatas[i].nameLocalId;
                string tDescId = GameData.Instance.journalDatas[i].localId;

         
                _data.Add(new JournalData() { journalName = tname, journalDesc = tjournalDesc, nameLocalId = tnameLocalId,localId = tDescId,icon = tIcon,illustration = tIllustration });
                
                
            }
                // tell the scrollers to reload
                vScroller.ReloadData();
            //hScroller.ReloadData();
        }


        // Update is called once per frame
        void Update() {

        }
        #endregion

        public void OnClick(GameObject g)
        {
            switch (g.name) {
                case "btnClose":
                    gameObject.SetActive(false);
                    break;
            }

        }
    }
}