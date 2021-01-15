using Common.EF.Library.Models;
using GenericServices;
using ShowRoom.Core.Mvvm;
using ShowRoom.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShowRoom.Modules.EmployeeManagment.DataLayer.Dtos
{

    public partial class EmployeeDto : IdDataModelBase, ILinkToEntity<Employee>
    {
        private string _FirstName;
        private string _LastName;
        private string _UserName;
        private string _Email;
        private bool? _IsActive;

        [Required]
        [StringLength(30)]
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                SetProperty(ref _FirstName, value);
            }
        }
        [Required]
        [StringLength(30)]
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                SetProperty(ref _LastName, value);
            }
        }

        [Required]
        [StringLength(31)]
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                SetProperty(ref _UserName, value);
            }
        }
        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetProperty(ref _Email, value);
            }
        }

        public bool? IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                SetProperty(ref _IsActive, value);
            }
        }

    }
}
