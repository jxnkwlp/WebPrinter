﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        public Action<AuthenticationTokenCreateContext> OnCreate { get; set; }
        public Func<AuthenticationTokenCreateContext, Task> OnCreateAsync { get; set; }
        public Action<AuthenticationTokenReceiveContext> OnReceive { get; set; }
        public Func<AuthenticationTokenReceiveContext, Task> OnReceiveAsync { get; set; }

        public virtual void Create(AuthenticationTokenCreateContext context)
        {
            if (OnCreateAsync != null && OnCreate == null)
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            if (OnCreate != null)
            {
                OnCreate.Invoke(context);
            }
        }

        public virtual  Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            if (OnCreateAsync != null && OnCreate == null)
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            if (OnCreateAsync != null)
            {
                 OnCreateAsync.Invoke(context).Wait();
            }
            else
            {
                Create(context);
            }
            return TaskHelpers.FromResult<object>(null);
        }

        public virtual void Receive(AuthenticationTokenReceiveContext context)
        {
            if (OnReceiveAsync != null && OnReceive == null)
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }

            if (OnReceive != null)
            {
                OnReceive.Invoke(context);
            }
        }

        public virtual  Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            if (OnReceiveAsync != null && OnReceive == null)
            {
                throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
            }
            if (OnReceiveAsync != null)
            {
                 OnReceiveAsync.Invoke(context).Wait();
            }
            else
            {
                Receive(context);
            }

            return TaskHelpers.FromResult<object>(null);
        }
    }
}
