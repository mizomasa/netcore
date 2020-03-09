using System;
using DBFirstApp.Service;
using System.Threading.Tasks;
namespace DBFirstApp.Service.Employees.Domain
{
    public interface IEmployeeService
    {
        public bool Exists(string employeeId);
        public Task<int> getFamilyHumanIdAsync(string humanId);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IHumamRepository _HumamRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IHumamRepository humamRepository)
        {
            _EmployeeRepository = employeeRepository;
            _HumamRepository = humamRepository;
        }

        public bool Exists(string employeeId)
        {
            return _EmployeeRepository.Exists(employeeId);
        }

        public async Task<int> getFamilyHumanIdAsync(string humanId)
        {
            if (!string.IsNullOrEmpty(humanId)) return int.Parse(humanId);
            return await _HumamRepository.FindMaxIdAsync() + 1; //TODO:採番は、テーブルにするか採番テーブルにするか。
        }
    }
}
