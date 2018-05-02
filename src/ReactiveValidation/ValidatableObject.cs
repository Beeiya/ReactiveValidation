﻿using System;
using System.Collections;
using System.ComponentModel;

namespace ReactiveValidation
{
    public class ValidatableObject : IValidatableObject
    {
        public ValidatableObject()
        {}


        public IObjectValidator Validator { get; protected set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        bool INotifyDataErrorInfo.HasErrors => Validator?.IsValid == false || Validator?.HasWarnings == true;


        public virtual void OnPropertyMessagesChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return Validator?.GetMessages(propertyName);
        }
    }
}
