﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using N5Now.Common.Exceptions;
using N5Now.Common.Resources;
using N5Now.Domain.DTO;
using Newtonsoft.Json;
using Serilog;

namespace N5Now.Handlers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Metodo encargado de capturar todas las Excepciones del proyecto,
        /// Se debe agregar el decorador a cada controller [TypeFilter(typeof(CustomExceptionFilterAttribute))]
        /// </summary>
        /// <param name="exception"> Parametro donde queda capturada la Exception </param>
        public override void OnException(ExceptionContext context)
        {
            HttpResponseException responseException = new HttpResponseException();

            ResponseDto response = new ResponseDto();
            if (context.Exception is BusinessException)
            {
                responseException.Status = StatusCodes.Status400BadRequest;
                response.Message = context.Exception.Message;
                context.ExceptionHandled = true;
                Log.Error(context.Exception, response.Message);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                responseException.Status = StatusCodes.Status401Unauthorized;
                response.Message = GeneralMessages.Error401;
                context.ExceptionHandled = true;
                Log.Error(GeneralMessages.Error401);
            }
            else
            {
                responseException.Status = StatusCodes.Status500InternalServerError;
                response.Result = JsonConvert.SerializeObject(context.Exception);
                response.Message = GeneralMessages.Error500;
                context.ExceptionHandled = true;

                //Add Logs
                Log.Fatal(context.Exception, GeneralMessages.Error500);

            }

            context.Result = new ObjectResult(responseException.Value)
            {
                StatusCode = responseException.Status,
                Value = response
            };

            if (responseException.Status == StatusCodes.Status500InternalServerError)
                context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = GeneralMessages.Error500;

        }
    }
}
