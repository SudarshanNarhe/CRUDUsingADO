using System.Data.SqlClient;

namespace CRUDUsingADO.Models
{
    public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;

        private readonly IConfiguration config;

        public StudentDAL(IConfiguration config)
        {
            this.config = config;
            string connstr = this.config.GetConnectionString("DefaultConnection");
            con = new SqlConnection(connstr);
        }

        public List<Student> GetStudents()
        {
            List<Student> studentlist = new List<Student>();
            string qry = "select * from student";
            cmd = new SqlCommand(qry,con);
            con.Open();
            reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(reader["id"]);
                    student.Name = reader["name"].ToString();
                    student.Java = Convert.ToDouble(reader["Java"]);
                    student.SQL = Convert.ToDouble(reader["SQL"]);
                    student.DotNet = Convert.ToDouble(reader["DotNet"]);
                    student.Angular = Convert.ToDouble(reader["Angular"]);
                    student.Percentage = Convert.ToDouble(reader["percentage"]);
                    studentlist.Add(student);
                }
            }
            con.Close();
            return studentlist;
        }

        private static double CalculatePercentage(double sub1, double sub2,double sub3,double sub4)
        {
            return (sub1 + sub2 + sub3 + sub4) / 4;
        }
        public int AddStudent(Student student)
        {
            int result = 0;
            string qry = "insert into student values(@name,@Java,@DotNet,@SQL,@Angular,@percentage)";
            cmd = new SqlCommand(qry,con);
            con.Open();
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@Java", student.Java);
            cmd.Parameters.AddWithValue("@DotNet", student.DotNet);
            cmd.Parameters.AddWithValue("@SQL", student.SQL);
            cmd.Parameters.AddWithValue("@Angular", student.Angular);
            double percentage = CalculatePercentage(student.Java,student.DotNet,student.SQL, student.Angular);
            cmd.Parameters.AddWithValue("@percentage", percentage);

            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int EditStudent(Student student)
        {
            int result = 0;
            string qry = "update student set name=@name, java=@Java, DotNet=@DotNet, SQL=@SQL, Angular=@Angular, percentage=@percentage where id=@id";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@Java", student.Java);
            cmd.Parameters.AddWithValue("@DotNet", student.DotNet);
            cmd.Parameters.AddWithValue("@SQL", student.SQL);
            cmd.Parameters.AddWithValue("@Angular", student.Angular);
            double percentage = CalculatePercentage(student.Java, student.DotNet, student.SQL, student.Angular);
            cmd.Parameters.AddWithValue("@percentage", percentage);
            cmd.Parameters.AddWithValue("@id", student.Id);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public Student GetStudentById(int id)
        {
            Student student = new Student();
            string qry = "select * from student where id =@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    student.Id = Convert.ToInt32(reader["id"]);
                    student.Name = reader["name"].ToString();
                    student.Java = Convert.ToDouble(reader["Java"]);
                    student.SQL = Convert.ToDouble(reader["SQL"]);
                    student.DotNet = Convert.ToDouble(reader["DotNet"]);
                    student.Angular = Convert.ToDouble(reader["Angular"]);
                    student.Percentage = Convert.ToDouble(reader["percentage"]);
                }
            }
            con.Close();
            return student;
        }
        public int DeleteStudent(int id)
        {
            int result = 0;
            string qry = "delete from student where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
