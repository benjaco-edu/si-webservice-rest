using System.Collections.Generic;
using System.ServiceModel;

namespace MiniprojectSoapService
{
    [ServiceContract]
    public interface ITeacherService
    {
        [OperationContract]
        Teacher GetTeacher(int id);
        [OperationContract]
        List<Teacher> GetAllTeachers(); 
        [OperationContract]
        void UpdateTeacherInfo(int id, Teacher newTeacherData);
        [OperationContract]
        void RemoveTeacher(int id);
        [OperationContract]
        void AddNewTeacher(Teacher teacherData);
    }
}