using DAL.Contex;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DAL
{
    public class DBConnection
    {
        InterviewsManagerContext contex=new InterviewsManagerContext();
       
        public List<Employee> GetAllEmployees()
        {
            List<Employee> resultList = new List<Employee>();
            foreach (var row in contex.Employees)
            {
                int id;
                int.TryParse(row.Id.ToString(), out id);
                string FirstName = row.FirstName.ToString();
                string LastName = row.LastName.ToString();
                int Age;
                int.TryParse(row.Age.ToString(), out Age);
                int StartOfWorkYear;
                int.TryParse(row.StartOfWorkYear.ToString(), out StartOfWorkYear);
                string City = row.City.ToString();
                string Street = row.Street.ToString();
                string RoleInCompany = row.RoleInCompany.ToString();
                string PhoneNumber = row.PhoneNumber.ToString();
                string Email = row.Email.ToString();
                Employee e = new Employee();
                e.Id = id;
                e.FirstName = FirstName;
                e.LastName = LastName;
                e.Age = Age;
                e.StartOfWorkYear = StartOfWorkYear;
                e.City = City;
                e.Street = Street;
                e.RoleInCompany = RoleInCompany;
                e.PhoneNumber = PhoneNumber;
                e.Email = Email;
                resultList.Add(e);

            }
            return resultList;
        }
        public List<Candidate> GetAllCandidates()
        {
            List<Candidate> resultList = new List<Candidate>();
            foreach (var row in contex.Candidates)
            {
                int id;
                int.TryParse(row.Id.ToString(), out id);
                string FirstName = row.FirstName.ToString();
                string LastName = row.LastName.ToString();
                int ScoreInSortedTest;
                int.TryParse(row.ScoreInSortedTest.ToString(), out ScoreInSortedTest);
                
                Candidate c = new Candidate();
                c.Id = id;
                c.FirstName = FirstName;
                c.LastName = LastName;
                c.ScoreInSortedTest = ScoreInSortedTest;            
                resultList.Add(c);

            }
            return resultList;
        }
        public List<Interview> GetAllInterviews()
        {
            List<Interview> resultList = new List<Interview>();
            foreach (var row in contex.Interviews)
            {
                int InterviewNumber;
                int.TryParse(row.InterviewNumber.ToString(), out InterviewNumber);
                int InterviewerId;
                int.TryParse(row.InterviewerId.ToString(), out InterviewerId);
                int CandidateId;
                int.TryParse(row.CandidateId.ToString(), out CandidateId);
                DateTime InterviewDate;
                DateTime.TryParse(row.InterviewDate.ToString(), out InterviewDate);
                string RoleInCompany = row.RoleInCompany.ToString();
                int ScoreInInterview;
                int.TryParse(row.ScoreInInterview.ToString(), out ScoreInInterview);
                Interview I = new Interview();
                I.InterviewNumber = InterviewNumber;
                I.InterviewerId = InterviewerId;    
                I.CandidateId = CandidateId;
                I.InterviewDate = InterviewDate;
                I.RoleInCompany = RoleInCompany;
                I.ScoreInInterview = ScoreInInterview;
                resultList.Add(I);

            }
            return resultList;
        }
        bool isEmployee(Employee employee)
        {
            IEnumerable<Employee> e = GetAllEmployees();
            //e.Any(emp => emp.Id == employee.Id);
            foreach (Employee emp in e)
            {
                if (emp.Id == employee.Id)
                {
                    return false;
                }

            }
            return true;
        }
        public bool AddEmployee(Employee employee)
        {
            if(isEmployee(employee))
            {
                using (InterviewsManagerContext contex = new InterviewsManagerContext())
                {
                   
                        contex.Employees.Attach(employee);
                        contex.Entry<Employee>(employee).State =Microsoft.EntityFrameworkCore.EntityState.Modified;
                        contex.SaveChanges();
                      
                   
                }
                return true;
            }
           return false;    
               
        }
    }
   
    
}
