using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBFirstApp.Domain;
using DBFirstApp.Domain.Employees;
using DBFirstApp.Ex;
namespace DBFirstApp.Service
{
    public interface IEmployeeDetailReferenceService
    {
        Task<EmployeeDetailReferenceResponse> Handle(EmployeeDetailReferenceRequest request);
    }

    public class EmployeeDetailReferenceService : IEmployeeDetailReferenceService
    {
        private readonly IEmployeeRepository _EmployeeRepository;


        public EmployeeDetailReferenceService(IEmployeeRepository employeeRepository)
        {
            _EmployeeRepository = employeeRepository;
        }

        public async Task<EmployeeDetailReferenceResponse> Handle(EmployeeDetailReferenceRequest request)
        {
            var employee = await _EmployeeRepository.FindByAsync(request.EmployeeID);
            if (employee ==null) throw new NotFoundException(request.EmployeeID);

            return new EmployeeDetailReferenceResponse(employee, employee.FamilyIterator);
        }
    }

    public class EmployeeDetailReferenceRequest
    {
        public string EmployeeID { get; private set; }
        public EmployeeDetailReferenceRequest(string employeeID)
        {
            this.EmployeeID = employeeID;
        }
    }

    public class EmployeeDetailReferenceResponse
    {
        public string EmployeeID { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Sex { get; private set; }
        public DateTime Birthday { get; private set; }

        public List<Family> Families { get; private set; }

        public EmployeeDetailReferenceResponse(Employee employee, IEnumerator<Human> families)
        {
            this.Families = new List<Family>();
            this.EmployeeID = employee.EmployeeId.Value;
            this.Email = employee.Email.Value;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Sex = employee.Sex;
            this.Birthday = employee.Birthday;
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
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime BirthDay { get; }
        public int Sex { get; }
        public string RelationShip { get; }

    }
}
