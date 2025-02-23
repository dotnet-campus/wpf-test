﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Xaml.Tools.XamlDom
{
    [DebuggerDisplay("{Value}")]
    public class ValueNode : ItemNode
    {
        [DefaultValue(null)]
        public object Value
        {
            get { return _value; }
            set { CheckSealed(); _value = value; }
        }

        public override bool IsSealed
        {
            get { return _isSealed; }
        }

        public override void Seal()
        {
            _isSealed = true;
        }

        private void CheckSealed()
        {
            if (_isSealed)
            {
                throw new NotSupportedException("The ValueNode is sealed");
            }
        }
        private bool _isSealed;
        private object _value;
    }
}
