using System.Collections.Generic;

namespace MiniprojectSoapService
{
    public class TeacherServiceImpl : ITeacherService
    {
        private readonly IRelationDb ctx;
        public TeacherServiceImpl(IRelationDb ctx)
        {
            this.ctx = ctx;
        }
        public void AddNewTeacher(Teacher teacherData)
        {
            ctx.CreateTeacher(teacherData);
        }

        public void RemoveTeacher(int id)
        {
            ctx.DeleteTeacher(id);
        }

        public List<Teacher> GetAllTeachers()
        {
            return ctx.ReadAll();
        }

        public Teacher GetTeacher(int id)
        {
            return ctx.Read(id);
        }

        public void UpdateTeacherInfo(int id, Teacher newTeacherData)
        {
            ctx.UpdateTeacher(id, newTeacherData);
        }
    }
}