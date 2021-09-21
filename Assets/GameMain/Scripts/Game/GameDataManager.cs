using GameFramework.DataTable;
using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Voyage
{
    public class GameDataManager : GameComponent
    {


        private IDataTable<DRDialogue> dtDialogue;
        private IDataTable<DRSelection> dtSelection;
        private IDataTable<DRLevel> dtLevel;
        private IDataTable<DRTask> dtTask;

        public override void Init()
        {
            base.Init();

            dtDialogue = Game.DataTable.GetDataTable<DRDialogue>();
            dtSelection = Game.DataTable.GetDataTable<DRSelection>();
            dtLevel = Game.DataTable.GetDataTable<DRLevel>();
            dtTask = Game.DataTable.GetDataTable<DRTask>();
            
        }



        public DRDialogue GetDialogueTable(int id)
        {
            return dtDialogue.GetDataRow(id);
        }


        public DRSelection GetSelectTable(int id)
        {
            return dtSelection.GetDataRow(id);
        }

        public DRLevel GetLevelTable(int id)
        {
            return dtLevel.GetDataRow(id);
        }

        public List<int> GetLevelData(int id)
        {
            List<int> DataList = new List<int>();
            string str = dtLevel.GetDataRow(id).DateShow;
            string[] ss = str.Split('-');
            for (int i = 0; i < ss.Length; i++)
            {
                DataList.Add(int.Parse(ss[i]));
            }
            return DataList;
        }

        public DRTask GetTaskTable(int id)
        {
            return dtTask.GetDataRow(id);
        }


        public List<int> GetSelectionData(int id)
        {
            List<int> DataList = new List<int>();
            string str = dtDialogue.GetDataRow(id).SpecialNext;
            string[] ss = str.Split(',');
            for (int i = 0; i < ss.Length; i++)
            {
                DataList.Add(int.Parse(ss[i]));
            }
            return DataList;
        }

        public List<string> GetSpecialData(int id)
        {
            List<string> DataList = new List<string>();
            string str = dtDialogue.GetDataRow(id).SpecialNext;
            string[] ss = str.Split(',');
            for (int i = 0; i < ss.Length; i++)
            {
                DataList.Add(ss[i]);
            }
            return DataList;
        }





    }
}
