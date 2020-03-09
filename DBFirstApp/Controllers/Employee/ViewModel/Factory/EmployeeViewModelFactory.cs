using System;
using System.Collections.Generic;
using DBFirstApp.Service;
using DBFirstApp.Controllers.Employee.ViewModel;
using System.Threading.Tasks;

namespace DBFirstApp.Controllers.Employee.ViewModel.Factory
{
    public class EmployeeViewModelFactory
    {
        public static List<EmployeeListViewModel> ToListViewModel(EmployeeListResponse responce)
        {

            var viewModel = new List<EmployeeListViewModel>();

            responce.Employees.ForEach(emp => viewModel.Add(new EmployeeListViewModel()
            {
                EmployeeId = emp.EmployeeId,
                HumanId = emp.HumanId,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Age = emp.Age.ToString() //TODO:ToStringいらない
            }));

            return viewModel;
        }

        public static EmployeeSelfViewModel ToSelfViewModle(EmployeeSelfReferenceResponse responce)
        {
            return new EmployeeSelfViewModel()
            {
                Id = responce.EmployeeID,
                FirstName = responce.FirstName,
                LastName = responce.LastName,
                Email = responce.Email,
                Sex = responce.Sex,

            };
        }


        public static EmployeeSelfViewModel ToSelfViewModle(EmployeeSelfSaveResponse responce)
        {
            return new EmployeeSelfViewModel()
            {
                Id = responce.EmployeeID,
                FirstName = responce.FirstName,
                LastName = responce.LastName,
                Email = responce.Email,
                Sex = responce.Sex,

            };
        }

        public static EmployeeFamilyViewModel ToFamilyViewModel(EmployeeFamilyReferenceResponse response)
        {
            var viewModel = new EmployeeFamilyViewModel()
            {
                EmployeeId = response.EmployeeID,
                Family = new Family(),
                Families = new List<Family>() 

            };
            response.Families.ForEach(e => viewModel.Families.Add(
                new Family(
                    humanId : e.HumanId.ToString(),
                    lastName: e.LastName,
                    firstName: e.FirstName,
                    relationShip: e.RelationShip,
                    sex: e.Sex,
                    birthday: e.BirthDay
                )));
            return viewModel;
        }

        internal static EmployeeDetailAllViewModel ToDetailViewModel(EmployeeDetailReferenceResponse responce)
        {
            var viewModel = new EmployeeDetailAllViewModel();
            viewModel.Self.Id = responce.EmployeeID;
            viewModel.Self.FirstName = responce.FirstName;
            viewModel.Self.LastName = responce.LastName;
            viewModel.Self.Email = responce.Email;
            viewModel.Self.Sex = responce.Sex;


            responce.Families.ForEach(e =>
                viewModel.Family.Families.Add(new Family() {
                    HumanId = e.HumanId.ToString(),
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Birthday = e.BirthDay,
                    Sex = e.Sex,
                    RelationShip = e.RelationShip
                }

            ));
            return viewModel;

        }
    }
}
