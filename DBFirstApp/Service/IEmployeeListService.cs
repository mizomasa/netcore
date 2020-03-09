using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DBFirstApp.Domain.Employees;
namespace DBFirstApp.Service
{
    public interface IEmployeeListService
    {
        Task<EmployeeListResponse> HandleAsync(EmployeeListRequest request);
    }

    public class EmployeeListService : IEmployeeListService
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmployeeListService(IEmployeeRepository employeeRepository)
        {
            _EmployeeRepository = employeeRepository;
        }

        public async Task<EmployeeListResponse> HandleAsync(EmployeeListRequest request)
        {
           var employees = await _EmployeeRepository.FindAll();
           return new EmployeeListResponse(employees);
        }
    }

    public class EmployeeListRequest
    {
        
    }

    public class EmployeeListResponse
    {
        public List<EmployeeList> Employees { get; private set; }

        public EmployeeListResponse(List<Employee> employees)
        {
            Employees = new List<EmployeeList>();
            employees.ForEach(emp => Employees.Add(new EmployeeList(emp)));
        }

        public class EmployeeList
        {
            
            public string EmployeeId { get; private set; }
            public string HumanId { get; private set; }
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public string Sex { get; private set; }
            public int Age { get; private set; }

            public EmployeeList(Employee employee)
            {
                this.EmployeeId = employee.EmployeeId.Value;
                this.HumanId = employee.HumanId.ToString();
                this.FirstName = employee.FirstName;
                this.LastName = employee.LastName;
                this.Sex = employee.Sex == 0 ?"男":"女"; //TODO:Domainサービス？
                this.Age = employee.Age;
            }
        }
    }

}
