using DAL;
using DAL.Contex;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class InterviewsManagerBL
    {
        DBConnection dbConnection;
        InterviewsManagerContext contex = new InterviewsManagerContext();
        public InterviewsManagerBL()
        {
            dbConnection = new DBConnection();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return dbConnection.GetAllEmployees();
        }

        public IEnumerable<Candidate> GetAllCandidate()
        {
            return dbConnection.GetAllCandidates();
        }
        public IEnumerable<string> GetNameOfCandidate()
        {
            if (dbConnection != null)
            {
                IEnumerable<Candidate> candidate = dbConnection.GetAllCandidates();
                var CandidateName = candidate.Select(e => e.FirstName+" "+e.LastName).Distinct();
                return CandidateName;
            }
            return Enumerable.Empty<string>();
        }
        public IEnumerable<string> GetRoleInCompanyWithoutRepetitions()
        {
            if (dbConnection != null)
            {
                IEnumerable<Employee> employees = dbConnection.GetAllEmployees();
                var uniqueRoles = employees.Select(e => e.RoleInCompany).Distinct();
                return uniqueRoles;
            }
          return Enumerable.Empty<string>();
        }
        public IEnumerable<string> GetCity()
        {
            if (dbConnection != null)
            {
                IEnumerable<Employee> employees = dbConnection.GetAllEmployees();
                var city = employees.Select(e => e.City).Distinct();
                return city;
            }
            return Enumerable.Empty<string>();
        }
        public IEnumerable<int> GetStartYear()
        {
            if (dbConnection != null)
            {
                IEnumerable<Employee> employees = dbConnection.GetAllEmployees();
                var year = employees.Select(e => e.StartOfWorkYear).Distinct();
                return year;
            }
            return Enumerable.Empty<int>();
        }
        public IEnumerable<int> GetAge(int minage, int maxage)
        {
            if (dbConnection != null)
            {
                IEnumerable<Employee> employees = dbConnection.GetAllEmployees();
                var ages = employees
                    .Where(e => e.Age >= minage && e.Age <= maxage) // מסנן את העובדים לפי גיל
                    .Select(e => e.Age); // מחזיר את הגיל של העובדים המסוננים
                return ages;
            }
            return Enumerable.Empty<int>();
        }


        public bool Addemployee(Employee employee)
        {
            return dbConnection.AddEmployee(employee);
        }
        public IEnumerable<dynamic>GetInterviewsDetails(string name)
        {
            string[] fullname = name.Split(' ');
            var interviewDetails = from i in contex.Interviews
                                   join c in contex.Candidates on i.CandidateId equals c.Id
                                   join e in contex.Employees on i.InterviewerId equals e.Id
                                   where c.FirstName == fullname[0] && c.LastName == fullname[1]
                                   orderby i.InterviewDate descending
                                   select new
                                   {
                                       InterviewNUmber = i.InterviewNumber,
                                       RoleInCompany = i.RoleInCompany,
                                       InterviewDate = i.InterviewDate,
                                       InterviewerName = e.FirstName + " " + e.LastName,
                                       InterviewerPhone = e.PhoneNumber
                                   };
            return interviewDetails.ToList();
        }
    }
}
