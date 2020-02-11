﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Features.RQName.SimpleTree;

namespace Microsoft.CodeAnalysis.Features.RQName.Nodes
{
    internal class RQOrdinaryMethodPropertyOrEventName : RQMethodPropertyOrEventName
    {
        // the construct type should always match the containing member.
        // I don't think we need to expose this, shouldn't you know this from your containing member?
        private readonly string _constructType;

        public readonly string Name;

        internal RQOrdinaryMethodPropertyOrEventName(string constructType, string name)
        {
            _constructType = constructType;
            Name = name;
        }

        public override string OrdinaryNameValue
        {
            get { return Name; }
        }

        public static RQOrdinaryMethodPropertyOrEventName CreateConstructorName()
        {
            return new RQOrdinaryMethodPropertyOrEventName(RQNameStrings.MethName, RQNameStrings.SpecialConstructorName);
        }

        public static RQOrdinaryMethodPropertyOrEventName CreateDestructorName()
        {
            return new RQOrdinaryMethodPropertyOrEventName(RQNameStrings.MethName, RQNameStrings.SpecialDestructorName);
        }

        public static RQOrdinaryMethodPropertyOrEventName CreateOrdinaryIndexerName()
        {
            return new RQOrdinaryMethodPropertyOrEventName(RQNameStrings.PropName, RQNameStrings.SpecialIndexerName);
        }

        public static RQOrdinaryMethodPropertyOrEventName CreateOrdinaryMethodName(string name)
        {
            return new RQOrdinaryMethodPropertyOrEventName(RQNameStrings.MethName, name);
        }

        public static RQOrdinaryMethodPropertyOrEventName CreateOrdinaryEventName(string name)
        {
            return new RQOrdinaryMethodPropertyOrEventName(RQNameStrings.EventName, name);
        }

        public static RQOrdinaryMethodPropertyOrEventName CreateOrdinaryPropertyName(string name)
        {
            return new RQOrdinaryMethodPropertyOrEventName(RQNameStrings.PropName, name);
        }

        public override SimpleGroupNode ToSimpleTree()
        {
            return new SimpleGroupNode(_constructType, Name);
        }
    }
}
