﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NGWalks.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           if(context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
   
}
