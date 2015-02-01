using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;
using IES.JW.Model;
using IES.Resource.Model;

namespace IES.G2S.OC.IBLL.FC
{
    public interface IFCBLL
    {

        List<OCFC> OCFC_List(int OCID, int UserID);

        OCFCInfo OCFCInfo_Get(int FCID);

        int OCFC_ADDorEdit(OCFC fc);

        OCFC OCFC_Get(OCFC fc);

        List<OCFCFile> OCFCFile_List(OCFC fc);

        List<OCFCLive> OCFCLive_List(OCFC fc);

        bool OCFCFile_Del(OCFCFile file);

        bool OCFCLive_Del(OCFCLive live);

        bool OCFCFile_Must(OCFCFile file);
        //List<Exercise> OCFCExercise_List(OCFC ocfc);
        
    }
}
