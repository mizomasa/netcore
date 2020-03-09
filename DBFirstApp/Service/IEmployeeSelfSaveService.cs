using System;
using System.Threading.Tasks;
using DBFirstApp.Domain.Employees.ValueObject;

namespace DBFirstApp.Service
{
    public interface IEmployeeSelfSaveService
    {
        Task<EmployeeSelfSaveResponse> HandleAsync(EmployeeSelfSaveRequest request);
    }

    public class EmployeeSelfSaveService : IEmployeeSelfSaveService
    {
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmployeeSelfSaveService(IEmployeeRepository employeeRepository)
        {
            _EmployeeRepository = employeeRepository;
        }

        public async Task<EmployeeSelfSaveResponse> HandleAsync(EmployeeSelfSaveRequest request)
        {
            var employee = await _EmployeeRepository.FindByAsync(request.EmployeeID);

            employee.ChangeName(new FullName(request.FirstName,request.LastName));
            employee.ChangeSex(new Sex(request.Sex));
            employee.ChangeEmail(new Email(request.Email));

            await _EmployeeRepository.SaveAsync(employee);
            await _EmployeeRepository.SaveChangedAsync();
            return new EmployeeSelfSaveResponse(employee.EmployeeId.Value,
                employee.Email.Value,
                employee.EmployeeAttr.FullName.FirstName,
                employee.EmployeeAttr.FullName.LastName,
                employee.EmployeeAttr.Sex.Value,
                employee.EmployeeAttr.Birthday.Value) { };
        }
    }

    public class EmployeeSelfSaveRequest
    {
        public string EmployeeID { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public int Sex { get; }

        public DateTime Birthday { get; }

        public EmployeeSelfSaveRequest(string employeeId, string email, string firstName, string lastName, int sex, DateTime birthday)
        {
            EmployeeID = employeeId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Birthday = birthday;
        }
    }
    public class EmployeeSelfSaveResponse
    {
        public string EmployeeID { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public int Sex { get; }

        public DateTime Birthday { get; }

        public EmployeeSelfSaveResponse(string employeeId, string email, string firstName, string lastName, int sex, DateTime birthday)
        {
            EmployeeID = employeeId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Birthday = birthday;
        }
    }
}
