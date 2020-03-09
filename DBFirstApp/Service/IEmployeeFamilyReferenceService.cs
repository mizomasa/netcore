using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBFirstApp.Domain.Employees;
using DBFirstApp.Ex;
using DBFirstApp.Service.Employees.Domain;

namespace DBFirstApp.Service
{
    public interface IEmployeeFamilyReferenceService
    {
        Task<EmployeeFamilyReferenceResponse> Handle(EmployeeFamilyReferenceRequest request);
    }

    public class EmployeeFamilyReferenceService : IEmployeeFamilyReferenceService
    {
        private IEmployeeRepository _EmployeeRepository;
        private IEmployeeService _EmployeeService;

        public EmployeeFamilyReferenceService(IEmployeeRepository employeeRepository,
            IEmployeeService employeeService)
        {
            _EmployeeRepository = employeeRepository;
            _EmployeeService = employeeService;
        }
        public async Task<EmployeeFamilyReferenceResponse> Handle(EmployeeFamilyReferenceRequest request)
        {
            if (!_EmployeeService.Exists(request.EmployeeId))
            {
                throw new NotFoundException(request.EmployeeId);
            }

            var empOptional = await _EmployeeRepository.FindByAsync(request.EmployeeId);
            var emp = empOptional;
            var response = new EmployeeFamilyReferenceResponse(
                request.EmployeeId, emp.FamilyIterator
                );

            return response;
        }
    }

    public class EmployeeFamilyReferenceRequest
    {
        public string EmployeeId { get; set; }
    }

    public class EmployeeFamilyReferenceResponse
    {
        public string EmployeeID { get; }
        public List<Family> Families { get; private set; }

        public EmployeeFamilyReferenceResponse(string employeeID, IEnumerator<Human> families)
        {
            EmployeeID = employeeID;
            Families = new List<EmployeeFamilyReferenceResponse.Family>();

            do
            {
                var human = families.Current;
                Families.Add(new Family(
                    humanId: human.HumanId.Value,
                    firstName: human.FullName.FirstName,
                    lastName: human.FullName.LastName,
                    birthDay: human.Birthday.Value,
                    sex: human.Sex.Value,
                    relationShip: human.Relationship.Name
                    )); ;
            } while (families.MoveNext());
        }

        public class Family
        {
            public Family(int humanId, string firstName, string lastName, DateTime birthDay, int sex, string relationShip)
            {
                HumanId = humanId;
                FirstName = firstName;
                LastName = lastName;
                BirthDay = birthDay;
                Sex = sex;
                RelationShip = relationShip;
            }

            public int HumanId { get; }
            public string FirstName { get;  }
            public string LastName { get;  }
            public DateTime BirthDay { get; }
            public int Sex { get;  }
            public string RelationShip { get; }

        }
    }
}
