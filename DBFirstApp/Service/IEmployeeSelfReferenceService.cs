using System;
using System.Threading.Tasks;
using DBFirstApp.Domain;
using DBFirstApp.Domain.Employees;
using DBFirstApp.Ex;
namespace DBFirstApp.Service
{
    public interface IEmployeeSelfReferenceService
    {
        Task<EmployeeSelfReferenceResponse> Handle(EmployeeSelfReferenceRequest request);
    }

    public class EmployeeSelfReferenceService : IEmployeeSelfReferenceService
    {
        private readonly IEmployeeRepository _EmployeeRepository;


        public EmployeeSelfReferenceService(IEmployeeRepository employeeRepository)
        {
            _EmployeeRepository = employeeRepository;
        }

        public async Task<EmployeeSelfReferenceResponse> Handle(EmployeeSelfReferenceRequest request)
        {
            var employee = await _EmployeeRepository.FindByAsync(request.EmployeeID);
            if (employee==null) throw new NotFoundException(request.EmployeeID);

            return new EmployeeSelfReferenceResponse(employee);
        }
    }

    public class EmployeeSelfReferenceRequest
    {
        public string EmployeeID { get; private set; }
        public EmployeeSelfReferenceRequest(string employeeID)
        {
            this.EmployeeID = employeeID;
        }
    }

    public class EmployeeSelfReferenceResponse
    {
        public string EmployeeID { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Sex { get; private set; }
        public DateTime Birthday { get; private set; }

        public EmployeeSelfReferenceResponse(Employee employee)
        {
            this.EmployeeID = employee.EmployeeId.Value;
            this.Email = employee.Email.Value;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Sex = employee.Sex;
            this.Birthday = employee.Birthday;
        }
    }
}
