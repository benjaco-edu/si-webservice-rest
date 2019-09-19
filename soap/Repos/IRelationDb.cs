using System.Collections.Generic;

namespace MiniprojectSoapService
{
    public interface IRelationDb
    {
        Teacher Read(int id);
        List<Teacher> ReadAll();
        void CreateTeacher(Teacher teacherData);
        void UpdateTeacher(int id, Teacher teacherData);
        void DeleteTeacher(int id);
    }
}