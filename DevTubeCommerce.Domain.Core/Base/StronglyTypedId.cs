﻿using DevTubeCommerce.Framework.Exceptions;
using DevTubeCommerce.Framework.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Domain.Core.Base
{
    public abstract class StronglyTypedId<T> : ValueObject<StronglyTypedId<T>>
    {
        private Guid _id;

        public Guid Value
        {
            get { return _id; }
            private set
            {
                if (value == Guid.Empty)
                    throw new BusinessRuleException(Error.InvalidId);

                _id = value;
            }
        }

        protected StronglyTypedId(Guid value)
        {
            Value = value;
        }

        protected override bool EqualsCore(StronglyTypedId<T> other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                return Value.GetHashCode();
            }
        }
    }
}
