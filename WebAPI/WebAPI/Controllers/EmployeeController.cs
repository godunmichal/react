using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();


            string query = @"
                SELECT EmployeeID, EmployeeName, Department, MailID, 
                convert(varchar(10),DOJ,120 ) DOJ FROM 
                dbo.Employees
                            ";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();

                //string doj = emp.DOJ.ToString().Split(' ')[0];

                string query = @"
                insert into dbo.Employees
                (EmployeeName,
                Department,
                MailID,
                DOJ)
                Values 
                (
                '" + emp.EmployeeName + @"'
                ,'" + emp.Department + @"'
                ,'" + emp.MailID + @"'
                ,'" + emp.DOJ + @"'
                )
                    ";
                    
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Added Succesfully";
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }
        public string Put(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"
                UPDATE dbo.Employees set 
                EmployeeName = '" + emp.EmployeeName+ @"'
                , Department = '" + emp.Department + @"'
                , MailID = '" + emp.MailID + @"'
                , DOJ = '" + emp.DOJ + @"'
                where EmployeeID= " + emp.EmployeeID + @"
                ";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Updated Succesfully";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }
        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();

                string query = @"

                DELETE FROM dbo.Employees where EmployeeID = " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }

                return "Deleted Succesfully";
            }
            catch (Exception)
            {
                return "Failed to Delete";
            }
        }
    }
}
