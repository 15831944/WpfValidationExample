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

using System;
using System.Linq;
using WpfValidation.ValidationRules;

namespace WpfValidation.Bindings
{
    public class FormatBinding : ValidationBinding
    {
        private TypeCode _typeCode;

        public TypeCode TypeCode
        {
            get => _typeCode;
            set
            {
                var formatRule = ValidationRules.OfType<ValueTypeValidationRule>().FirstOrDefault();
                if (formatRule == null)
                {
                    formatRule = new ValueTypeValidationRule();
                    ValidationRules.Add(formatRule);
                }

                formatRule.TypeCode = value;
                formatRule.ErrorContent = $"不是有效的{value}类型数据/ Input string was not a valid {value} format";
                _typeCode = value;
            }
        }
    }
}