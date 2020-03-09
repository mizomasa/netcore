using System;
using DBFirstApp.Domain.Employees;
using System.Threading.Tasks;
using DBFirstApp.Service.Employees.Domain;
using Microsoft.CodeAnalysis;
using DBFirstApp.Ex;
using DBFirstApp.Domain.Employees.ValueObject;

namespace DBFirstApp.Service
{
    public interface IEmployeeFamilySaveService
    {
        Task<EmployeeFamilySaveResponse> HandleAsync(EmployeeFamilySaveRequest request);
    }

    public class EmployeeFamilySaveService : IEmployeeFamilySaveService
    {

        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IEmployeeService _EmployeeService;
        public EmployeeFamilySaveService(IEmployeeRepository employeeRepository,
            IEmployeeService employeeService)
        {
            _EmployeeRepository = employeeRepository;
            _EmployeeService = employeeService;
        }

        public async Task<EmployeeFamilySaveResponse> HandleAsync(EmployeeFamilySaveRequest request)
        {
            if (!_EmployeeService.Exists(request.EmployeeId))
            {
                throw new NotFoundException(request.EmployeeId);
            }

            var humanID = await _EmployeeService.getFamilyHumanIdAsync(request.HumanId);


            Employee employe = await _EmployeeRepository.FindByAsync(request.EmployeeId);
            employe.Append(new Human(
                    new HumanId(humanID),
                    new FullName(request.FirstName, request.LastName),
                    new Sex(request.Sex),
                    new Birthday(request.Birthday),
                    new Relationship(request.RelationShip))
                );
            await _EmployeeRepository.SaveAsync(employe);
            await _EmployeeRepository.SaveChangedAsync();
            return new EmployeeFamilySaveResponse();
        }
    }

    public class EmployeeFamilySaveRequest
    {
        public string EmployeeId { get; set; }

        public string HumanId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string RelationShip { get; set; }
        public int Sex { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class EmployeeFamilySaveResponse
    {

    }
}
