using System;
using System.Collections.Generic;
using System.Linq;
using RapidInterface;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using System.Drawing;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using DevExpress.Xpo;
using DevExpress.XtraSplashScreen;

namespace TagGenerator
{
    [DBAttribute(Caption = "Объект", IconFile = "Object.png")]
    public partial class ObjectView : DBViewInterface
    {
        //private object[] strArray = null;
        public ObjectView()
        {
            InitializeComponent();
        }

        private void tableGridView1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column)
            {
                GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                //Erasing the default menu items 
                //menu.Items.Clear();
                if (menu.Column != null)
                {
                    //Adding new items 
                    menu.Items.Add(CreateCheckItem("Вставить из буфера", menu.Column, TagGenerator.Properties.Resources.Paste, new EventHandler(PasteFromClipboar)));
                    if (menu.Column.Caption == "ArchestrA импорт")
                    {
                        menu.Items.Add(CreateCheckItem("Автивировать все", menu.Column, null, new EventHandler(ActivateAll)));
                        menu.Items.Add(CreateCheckItem("Деактивировать все", menu.Column, null, new EventHandler(DectivateAll)));
                    }
                }
                else
                {
                    menu.Items.Add(CreateCheckItem("Вставить из Simatic PCS", menu.Column, TagGenerator.Properties.Resources.Paste, new EventHandler(PasteFromSimaticPCS)));
                }
            }
        }

        //Create a menu item 
        DXMenuCheckItem CreateCheckItem(string caption, GridColumn column, Image image, EventHandler eventHandler)
        {
            DXMenuCheckItem item = new DXMenuCheckItem(caption, false, image, eventHandler);
            item.Tag = column;
            return item;
        }

        //Menu item click handler 
        void PasteFromClipboar(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            GridColumn column = item.Tag as GridColumn;
            if (column == null) return;

            string[] strArray = Clipboard.GetText().Split(new string[] { "\t", "\r\n" }, StringSplitOptions.None);

            int countMin = 0;
            if (tableGridView1.RowCount < strArray.Length)
                countMin = tableGridView1.RowCount;
            else
                countMin = strArray.Length;

            for (int i = 0; i < countMin; i++)
            {
                if (!string.IsNullOrEmpty(strArray[i]))
                    tableGridView1.SetRowCellValue(i, column, strArray[i]);
            }
        }

        void ActivateAll(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            GridColumn column = item.Tag as GridColumn;
            if (column == null) return;

            for (int i = 0; i < tableGridView1.RowCount; i++)
                tableGridView1.SetRowCellValue(i, column, true);
        }

        void DectivateAll(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            GridColumn column = item.Tag as GridColumn;
            if (column == null) return;

            for (int i = 0; i < tableGridView1.RowCount; i++)
                tableGridView1.SetRowCellValue(i, column, false);
        }

        /// <summary>
        /// Класс данных с 
        /// </summary> Simatic PCS с Procces Object View вкладка Blocks.
        class SimaticPCSBlock
        {
            public string Block { get; set; }
            public string BlockComment { get; set; }
            public string BlockIcon { get; set; }
            public string InstanceDB { get; set; }
            public string InternalID { get; set; }
        }

        //Menu item click handler 
        void PasteFromSimaticPCS(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitFormEx));
            SplashScreenManager.Default.SetWaitFormDescription("Происходит анализ данных...");

            // Не особо работает.
            RemoveAll(tableGridView1);            
            tableNavigatorControl1.Focus();

            // Заполнение словаря по типам объектов.
            XPCollection<ObjectType> objectTypes = new XPCollection<ObjectType>(baseXPCollecton1.Session);
            Dictionary<string, ObjectType> dictObjectTypes = new Dictionary<string, ObjectType>();
            foreach (ObjectType type in objectTypes)
                dictObjectTypes.Add(type.SimaticPCSBlock, type);            

            string[] strRows = Clipboard.GetText().Split(new string[] { "\r\n" }, StringSplitOptions.None); 
            Collection<SimaticPCSBlock> blocks = new Collection<SimaticPCSBlock>();
            for (int i = 0; i < strRows.Length; i++)
                if (!string.IsNullOrEmpty(strRows[i]))
                {
                    string[] strColumns = strRows[i].Split(new string[] { "\t" }, StringSplitOptions.None);

                    if (strColumns.Length == 18)
                    {
                        SimaticPCSBlock block = new SimaticPCSBlock();
                        block.Block = strColumns[3];
                        block.BlockComment = strColumns[4];
                        block.BlockIcon = strColumns[6];
                        block.InstanceDB = strColumns[12];
                        block.InternalID = strColumns[16];

                        if (!string.IsNullOrEmpty(block.InternalID) && dictObjectTypes.ContainsKey(block.InternalID))
                            blocks.Add(block);
                    }
                }


            for (int i = 0; i < blocks.Count + 1; i++)
                tableGridView1.AddNewRow();

            for (int i = 0; i < blocks.Count; i++)
            {
                tableGridView1.SetRowCellValue(i, ObjectTypeIDGridColumn1, dictObjectTypes[blocks[i].InternalID]);
                tableGridView1.SetRowCellValue(i, ChartGridColumn1,  blocks[i].Block);
                tableGridView1.SetRowCellValue(i, AttributeGridColumn1, blocks[i].Block.Replace('-', '_'));
                tableGridView1.SetRowCellValue(i, ItemReferenceGridColumn1, blocks[i].InstanceDB);
                tableGridView1.SetRowCellValue(i, CommentGridColumn1, blocks[i].BlockComment);
                tableGridView1.SetRowCellValue(i, UnitGridColumn1, blocks[i].BlockIcon);
            }
            tableGridView1.DeleteRow(blocks.Count);
            tableGridView1.ClearSelection();

            SplashScreenManager.CloseForm();
        }

        private static void RemoveAll(GridView gvView)
        {
            gvView.SelectAll();
            gvView.DeleteSelectedRows();
            gvView.ClearSelection();
        }
    }
}
