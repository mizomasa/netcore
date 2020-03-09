using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBFirstApp.Domain;
using DBFirstApp.Domain.Employees;
using DBFirstApp.Domain.Employees.ValueObject;
using DBFirstApp.Ex;
using DBFirstApp.Models;
using DBFirstApp.Service.Employees.Domain;
using Microsoft.CodeAnalysis;

namespace DBFirstApp.Service
{
    public interface ICreateEmployeeService
    {
        Task<UserCreateResponse> Handle(UserCreateRequest request);
    }

    public class CreateEmployeeService : ICreateEmployeeService
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IHumamRepository _HumamRepository;
        private readonly IEmployeeService _EmployeeService;
        public CreateEmployeeService(IEmployeeRepository employeeRepository,
            IHumamRepository humamRepository,
            IEmployeeService employeeService)
        {
            _EmployeeRepository = employeeRepository;
            _HumamRepository = humamRepository;
            _EmployeeService = employeeService;
        }


        public async Task<UserCreateResponse> Handle(UserCreateRequest request)
        {
            if (_EmployeeService.Exists(request.EmployeeId)){
                throw new DuplicateDataException(request.EmployeeId);
            }

            Optional<Employee> emp = await _EmployeeRepository.FindByAsync(request.EmployeeId);


            //TODO:Factory
            //TODO:Domainサービスに移行

            var maxHumanID = await _HumamRepository.FindMaxIdAsync() + 1;
            Employee newEmp = new Employee(
                employeeId: new EmployeeId(request.EmployeeId),
                email: new Email(request.Email),
                employeeAttr: new Human(
                    humanId: new HumanId(maxHumanID ),
                    fullName: new FullName(request.FirstName, request.LastName),
                    sex: new Sex(request.Sex),
                    birthday: new Birthday(request.Birthday),
                    relationship: new Relationship("0") //TODO
                    )
                ) ;

            await _EmployeeRepository.SaveAsync(newEmp);
            _EmployeeRepository.SaveChangedAsync();

            return new UserCreateResponse(
                employeeId: newEmp.EmployeeId.Value,
                email: newEmp.Email.Value,
                humanId: newEmp.HumanId,
                firstName: newEmp.FirstName,
                lastName: newEmp.LastName,
                sex: newEmp.Sex,
                birthday: newEmp.Birthday,
                createdBy: "",//TODO
                createdOn: DateTime.Now,//TODO
                updatedBy: "",//TODO.
                updatedOn: DateTime.Now//TODO
                );
        }
    }

    public class UserCreateRequest
    {

        public string EmployeeId { get;  }
        public string Email { get;  }

        public string FirstName { get;  }
        public string LastName { get;  }
        public int Sex { get;  }
        public DateTime Birthday { get;  }

        public UserCreateRequest(string employeeId, string email, string firstName, string lastName, int sex, DateTime birthday)
        {
            EmployeeId = employeeId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Birthday = birthday;
        }
    }

    public class UserCreateResponse
    {
        public string EmployeeId { get;  }
        public string Email { get;  }

        public int HumanId { get;  }
        public string FirstName { get;  }
        public string LastName { get;  }
        public int Sex { get; set; }
        public DateTime Birthday { get;  }

        public string CreatedBy { get;  }
        public DateTime? CreatedOn { get;  }

        public string UpdatedBy { get;  }
        public DateTime? UpdatedOn { get;  }

        public UserCreateResponse(string employeeId,
            string email,
            int humanId,
            string firstName,
            string lastName,
            int sex,
            DateTime birthday,
            string createdBy,
            DateTime? createdOn,
            string updatedBy,
            DateTime? updatedOn)
        {
            EmployeeId = employeeId;
            Email = email;
            HumanId = humanId;
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Birthday = birthday;
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            UpdatedBy = updatedBy;
            UpdatedOn = updatedOn;
        }
    }
}
