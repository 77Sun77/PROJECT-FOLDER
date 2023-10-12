using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableData : MonoBehaviour
{
    public OpenFile file;
    
    public void ChangePos(OpenFile newFile)
    {
        if (file)
        {
            //file.transform.position = 
            if (file.isFolder && !newFile.isRecycleBin)
            {
                file.AddFile(newFile.gameObject);
            }
            else
            {
                newFile.transform.position = newFile.thisTable.transform.position;
                
            }
        }
        else
        {
            newFile.transform.position = transform.position;
            ResetFile(newFile);
            file = newFile;
            newFile.thisTable = this;

        }
    }

    public void ResetFile(OpenFile file)
    {
        if(file.thisTable) file.thisTable.file = null;

        file.thisTable = null;

    }
}
