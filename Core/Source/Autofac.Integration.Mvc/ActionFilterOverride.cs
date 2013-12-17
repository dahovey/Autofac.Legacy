﻿// This software is part of the Autofac IoC container
// Copyright © 2013 Autofac Contributors
// http://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Security;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Autofac.Integration.Mvc
{
    [SecurityCritical]
    internal class ActionFilterOverride : IActionFilter, IOverrideFilter
    {
        readonly IActionFilter _wrappedFilter;

        public ActionFilterOverride(IActionFilter wrappedFilter)
        {
            _wrappedFilter = wrappedFilter;
        }

        public Type FiltersToOverride
        {
            [SecurityCritical]
            get { return typeof(IActionFilter); }
        }

        [SecurityCritical]
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _wrappedFilter.OnActionExecuting(filterContext);
        }

        [SecurityCritical]
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _wrappedFilter.OnActionExecuted(filterContext);
        }
    }
}
