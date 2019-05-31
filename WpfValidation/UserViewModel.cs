//  ****************************************************************************
//  Ranplan Wireless Network Design Ltd.
//  __________________
//   All Rights Reserved. [2019]
// 
//  NOTICE:
//  All information contained herein is, and remains the property of
//  Ranplan Wireless Network Design Ltd. and its suppliers, if any.
//  The intellectual and technical concepts contained herein are proprietary
//  to Ranplan Wireless Network Design Ltd. and its suppliers and may be
//  covered by U.S. and Foreign Patents, patents in process, and are protected
//  by trade secret or copyright law.
//  Dissemination of this information or reproduction of this material
//  is strictly forbidden unless prior written permission is obtained
//  from Ranplan Wireless Network Design Ltd.
// ****************************************************************************

using System.ComponentModel;
using System.Windows.Input;
using FluentValidation;
using Prism.Commands;
using RanOpt.iBuilding.Common.UI;

namespace WpfValidation
{
    public class UserViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private static readonly UserViewModelValidator Validator = new UserViewModelValidator();
        private int _age;
        private float _height;
        private string _name;

        public UserViewModel()
        {
            OkCommand = new DelegateCommand(OK);
        }

        public string Name
        {
            get => _name;
            set => PropertyChanged.RaiseIfChanged(this, ref _name, value, nameof(Name));
        }

        public int Age
        {
            get => _age;
            set => PropertyChanged.RaiseIfChanged(this, ref _age, value, nameof(Age));
        }

        public float Height
        {
            get => _height;
            set => PropertyChanged.RaiseIfChanged(this, ref _height, value, nameof(Height));
        }

        public ICommand OkCommand { get; }

        public string this[string columnName] => Validator.GetError(this, columnName);

        public string Error => Validator.GetError(this, string.Empty);

        public event PropertyChangedEventHandler PropertyChanged;

        private void OK()
        { }
    }

    public class UserViewModelValidator : ViewModelValidator<UserViewModel>
    {
        public UserViewModelValidator() : base(",")
        {
            RuleFor(viewModel => viewModel.Name).NotEmpty().WithMessage("名称不能为空/ Name can not be empty");

            RuleFor(viewModel => viewModel.Age)
                .GreaterThan(0)
                .WithMessage("年龄必须大于0/ Age must greater than zero")
                .LessThanOrEqualTo(100)
                .WithMessage("年龄必须小于等于100/ Age must less than or equal to 100");

            RuleFor(viewModel => viewModel.Height)
                .GreaterThan(1.5f).WithMessage("高度必须大于1.5/ Height must greater than 1.5");
        }
    }
}