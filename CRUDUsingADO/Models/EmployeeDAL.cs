using System.Data.SqlClient;

namespace CRUDUsingADO.Models
{
    public class EmployeeDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;

        public EmployeeDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string connstr = this.configuration.GetConnectionString("DefaultConnection");
            con = new SqlConnection(connstr);
        }

        // list
        public List<Employee> GetEmployees()
        {
            List<Employee> employeelist = new List<Employee>();
            string qry = "select * from employee";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dr["id"]);
                    employee.Name = dr["name"].ToString();
                    employee.City = dr["city"].ToString();
                    employee.Salary = Convert.ToDouble(dr["salary"]);
                    employeelist.Add(employee);
                }
            }
            con.Close();
            return employeelist;
        }


        // add
        public int AddEmployee(Employee emp)
        {
            int result = 0;
            string qry = "insert into employee values(@name,@city,@salary)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", emp.Name);
            cmd.Parameters.AddWithValue("@city", emp.City);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        //edit
        public int EditEmployee(Employee emp)
        {
            int result = 0;
            string qry = "update employee set name=@name,city=@city, salary=@salary where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", emp.Name);
            cmd.Parameters.AddWithValue("@city", emp.City);
            cmd.Parameters.AddWithValue("@salary", emp.Salary);
            cmd.Parameters.AddWithValue("@id", emp.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        //select single emp
        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            string qry = "select * from employee where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    employee.Id = Convert.ToInt32(dr["id"]);
                    employee.Name = dr["name"].ToString();
                    employee.City = dr["city"].ToString();
                    employee.Salary = Convert.ToDouble(dr["salary"]);
                }
            }
            con.Close();
            return employee;
        }

        // delete
        public int DeleteEmployee(int id)
        {
            int result = 0;
            string qry = "delete from employee where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }


    }
}
