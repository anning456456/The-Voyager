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

        private IDataTable<DRSelectControl> dtSelectControl;

        public override void Init()
        {
            base.Init();
          
            dtDialogue = Game.DataTable.GetDataTable<DRDialogue>();
            dtSelectControl = Game.DataTable.GetDataTable<DRSelectControl>();
        }



        public DRDialogue GetDialogueTable(int id)
        {
            return dtDialogue.GetDataRow(id);
        }


        public DRSelectControl GetSelectTable(int id)
        {
            return dtSelectControl.GetDataRow(id);
        }



    }
}
