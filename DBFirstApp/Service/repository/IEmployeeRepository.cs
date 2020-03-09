using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBFirstApp.Domain;
using DBFirstApp.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using DBFirstApp.Domain.Employees;
using DBFirstApp.Domain.Employees.ValueObject;
namespace DBFirstApp.Service
{
    public interface IEmployeeRepository
    {
        bool Exists(string employeeId);
        Task<List<Employee>> FindAll();
        Task<Employee> FindByAsync(string employeeId);
        Task<Employee> SaveAsync(Employee employee);
        Task<int> SaveChangedAsync();
    }

    public class EmployeeRepository : MydatabaseContext, IEmployeeRepository
    {
        public EmployeeRepository(DbContextOptions<MydatabaseContext> options)
            : base(options)
		{
        }

        public bool Exists(string employeeId)
        {
            return this.TEmployee.Count(e => e.EmployeeId == employeeId) != 0;
        }

        public async Task<List<Employee>> FindAll()
        {

            var result = await TEmployee.Join(
                    THuman,
                    emp => emp.HumanId,
                    hum => hum.Id,
                    (emp, hum) => new { emp.EmployeeId,hum.Id ,hum.FirstName,hum.LastName,hum.Sex,hum.Birthday,emp.MailAddress }
                ).ToArrayAsync();

            List<Employee> emps = new List<Employee>();

            foreach (var item in result)
            {
                emps.Add(
                    new Employee(
                        employeeId : new EmployeeId(item.EmployeeId),
                        email : new Email(item.MailAddress),
                        employeeAttr : new Human(

                            humanId: new HumanId(item.Id),
                            fullName: new FullName(item.FirstName, item.LastName),
                            birthday: new Birthday(item.Birthday),
                            sex: new Sex(item.Sex),
                            relationship : new Relationship("0")　//TODO
                            )
                    )
                );
            }
            return emps;
        }

        public async Task<Employee> FindByAsync(string employeeId)
        {
            var query = TEmployee
                .Join(THuman,
                    emp => emp.HumanId,
                    hum => hum.Id,
                    (emp, hum) => new
                    {
                        emp.EmployeeId,
                        emp.MailAddress,
                        emp.HumanId,
                        hum.FirstName,
                        hum.LastName,
                        hum.Sex,
                        hum.Birthday,
                    }
                )
                .GroupJoin(TFamily,
                    rt => rt.EmployeeId,
                    lt => lt.EmployeeId,
                    (emp, family) =>
                    new
                    {
                        emp,
                        family
                    }).SelectMany(
                        x => x.family.DefaultIfEmpty(),
                        (x,fam) => new
                        {
                            x.emp,
                            x.family,
                            fam.HumanId,
                            fam.EmployeeId,
                            fam.RelationShip
                        }
                    )
                .GroupJoin(THuman,
                    x => x.HumanId,
                    Human => Human.Id,
                    (j2, family) => new
                    {
                        j2,
                        family
                    }
                ).SelectMany(
                        x => x.family.DefaultIfEmpty(),
                        (x, fam) => new
                        {
                            x.j2.emp.EmployeeId,
                            x.j2.emp.MailAddress,
                            x.j2.emp.HumanId,
                            x.j2.emp.FirstName,
                            x.j2.emp.LastName,
                            x.j2.emp.Birthday,
                            x.j2.emp.Sex,
                            //x.j2.emp.,
                            famliyId =fam.Id,
                            family_FirstName= fam.FirstName,
                            family_LastName = fam.LastName,
                            family_Sex = fam.Sex,
                            family_Birthday = fam.Birthday,
                        }
                    )
                .Where(e => e.EmployeeId == employeeId);

            var res = await query.ToArrayAsync();
            if (res.Count() == 0) return null;

            var employee = new Employee(
                employeeId: new EmployeeId(res[0].EmployeeId),
                email: new Email(res[0].MailAddress),
                employeeAttr: new Human(
                    new HumanId(res[0].HumanId),
                    new FullName(res[0].FirstName, res[0].LastName),
                    new Sex(res[0].Sex),
                    new Birthday(res[0].Birthday),
                    new Relationship("0")
                    )
                ) ;
            res.ToList().ForEach(e =>
              employee.Append(new Human(
                    new HumanId(e.famliyId),
                    new FullName(e.family_FirstName, e.family_LastName),
                    new Sex(e.family_Sex),
                    new Birthday(e.family_Birthday),
                    new Relationship("1") //TODO
                  ))
            );
            return (employee);
        }

        private async Task<int> SaveEmployee(Employee employee)
        {
            var tEmp = this.TEmployee.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId.Value);

            if (tEmp == null)
            {
                tEmp = new TEmployee();
                this.TEmployee.Add(tEmp);
            }
            tEmp.EmployeeId = employee.EmployeeId.Value;
            tEmp.MailAddress = employee.Email.Value;
            tEmp.HumanId = employee.EmployeeAttr.HumanId.Value;

            var tHum = this.THuman.FirstOrDefault(e => e.Id == tEmp.HumanId);
            if (tHum == null)
            {
                tHum = new THuman();
                this.THuman.Add(tHum);
            }
            tHum.Id = employee.EmployeeAttr.HumanId.Value;
            tHum.FirstName = employee.FirstName;
            tHum.LastName = employee.LastName;
            tHum.Sex = employee.Sex;
            tHum.Birthday = employee.Birthday;

            return 0;
        }

        private async Task<int> SaveFamily(Employee employee)
        {
            //employee.FamilyIterator.Reset();

            //家族情報
            do
            {
                var family = employee.FamilyIterator.Current;
                var tFamily = this.TFamily.FirstOrDefault(e =>
                    e.EmployeeId == employee.EmployeeId.Value
                    && e.HumanId == family.HumanId.Value);

                if (tFamily == null)
                {
                    tFamily = new TFamily();

                    tFamily.EmployeeId = employee.EmployeeId.Value;
                    tFamily.HumanId = family.HumanId.Value;
                    tFamily.RelationShip = int.Parse(family.Relationship.Value);

                    this.TFamily.Add(tFamily);
                }
                else
                {
                    tFamily.EmployeeId = employee.EmployeeId.Value;
                    tFamily.HumanId = family.HumanId.Value;
                    tFamily.RelationShip = int.Parse(family.Relationship.Value);

                }
                var tHuman = THuman.FirstOrDefault(e => e.Id == family.HumanId.Value);
                if (tHuman == null)
                {
                    tHuman = new THuman();

                    tHuman.Id = family.HumanId.Value;
                    tHuman.FirstName = family.FullName.FirstName;
                    tHuman.LastName = family.FullName.LastName;
                    tHuman.Birthday = family.Birthday.Value;
                    tHuman.Sex = family.Sex.Value;

                    THuman.Add(tHuman);
                }
                else
                {
                    tHuman.Id = family.HumanId.Value;
                    tHuman.FirstName = family.FullName.FirstName;
                    tHuman.LastName = family.FullName.LastName;
                    tHuman.Birthday = family.Birthday.Value;
                    tHuman.Sex = 4;

                }
            } while (employee.FamilyIterator.MoveNext());
            return 0;

        }

        public async Task<Employee> SaveAsync(Employee employee)
		{
            //社員情報
            await SaveEmployee(employee);

            //家族情報
            await SaveFamily(employee);

            return employee;
		}

        public async Task<int> SaveChangedAsync()
        {
            int res = await SaveChangesAsync();
            return res;
        }
    }
}
