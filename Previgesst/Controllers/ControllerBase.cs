using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Previgesst.Enums;
using Previgesst.Helpers;
using NLog;

namespace Previgesst.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private const string XMLHttpRequest = "XMLHttpRequest";
        private const string XRequestedWithHeadername = "X-Requested-With";
        private const string JSONErrorMessage = "Désolé, une erreur est survenue dans la requête.";

        protected readonly ILogger logger;

        protected ILogger Logger
        {
            get
            {
                return logger;
            }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected ControllerBase(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Display a success notification
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        protected void ShowSuccess(string title, string message = "")
        {
            ShowMessage(title, message, NotificationDefinition.SuccessTemplate);
        }

        /// <summary>
        /// Display an info notification
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        protected void ShowInfo(string title, string message = "")
        {
            ShowMessage(title, message, NotificationDefinition.InfoTemplate);
        }

        /// <summary>
        /// Display a warning notification
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        protected void ShowWarning(string title, string message = "")
        {
            ShowMessage(title, message, NotificationDefinition.WarningTemplate);
        }

        /// <summary>
        /// Display an error notification
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        protected void ShowError(string title, string message = "")
        {
            ShowMessage(title, message, NotificationDefinition.ErrorTemplate);
        }

        /// <summary>
        /// Save a success message to be displayed after a redirect. 
        /// Use <see cref="ShowSavedMessage"/> to display the message after the redirect
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        protected void SaveSuccessMessage(string title, string message = "")
        {
            SaveMessage(title, message, NotificationDefinition.SuccessTemplate);
        }

        /// <summary>
        /// Save an info message to be displayed after a redirect. 
        /// Use <see cref="ShowSavedMessage"/> to display the message after the redirect
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        protected void SaveInfoMessage(string title, string message = "")
        {
            SaveMessage(title, message, NotificationDefinition.InfoTemplate);
        }

        /// <summary>
        /// Save a warning message to be displayed after a redirect. 
        /// Use <see cref="ShowSavedMessage"/> to display the message after the redirect
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        protected void SaveWarningMessage(string title, string message = "")
        {
            SaveMessage(title, message, NotificationDefinition.WarningTemplate);
        }

        /// <summary>
        /// Save an error message to be displayed after a redirect. 
        /// Use <see cref="ShowSavedMessage"/> to display the message after the redirect
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        protected void SaveErrorMessage(string title, string message = "")
        {
            SaveMessage(title, message, NotificationDefinition.ErrorTemplate);
        }

        /// <summary>
        /// Save a success message to be displayed after a redirect. 
        /// Use <see cref="ShowSavedMessage"/> to display the message after the redirect
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        /// <param name="template">Template used to display the message. Use <see cref="NotificationDefinition"/>.</param>
        protected void SaveMessage(string title, string message, string template)
        {
            TempData["NotificationTitle"] = title;
            TempData["NotificationMessage"] = message;
            TempData["NotiticationTemplate"] = template;
        }

        /// <summary>
        /// Display a previously saved message. Does not show any message if nothing was saved
        /// See <see cref="SaveInfoMessage"/>, 
        /// <see cref="SaveSuccessMessage"/>, 
        /// <see cref="SaveWarningMessage"/>, 
        /// <see cref="SaveErrorMessage"/> or 
        /// <see cref="SaveMessage"/>.
        /// </summary>
        protected void ShowSavedMessage()
        {
            if (TempData.ContainsKey("NotificationTitle"))
            {
                ShowMessage((string)TempData["NotificationTitle"],
                            (string)TempData["NotificationMessage"],
                            (string)TempData["NotiticationTemplate"]);
            }
        }

        /// <summary>
        /// Display a notification
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="message">Notification message</param>
        /// <param name="template">Template used to display the message. Use <see cref="NotificationDefinition"/>.</param>
        protected void ShowMessage(string title, string message, string template)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                ViewBag.NotificationTitle = title;
                ViewBag.NotificationMessage = message ?? "";
                ViewBag.NotiticationTemplate = (string.IsNullOrWhiteSpace(template) ? NotificationDefinition.InfoTemplate : template);
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            Logger.Error(filterContext.Exception, filterContext.Exception.Message);
#if !DEBUG    //Ne pas intercepter l'erreur en debug
            CreateExceptionContextResult(filterContext);
#endif

            base.OnException(filterContext);
        }

        protected virtual void CreateExceptionContextResult(ExceptionContext exceptionContext)
        {
            exceptionContext.ExceptionHandled = true;
            if (exceptionContext.HttpContext.Request.Headers[XRequestedWithHeadername] == XMLHttpRequest)
            {
                //Return JSON
                exceptionContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { error = true, message = JSONErrorMessage }
                };
            }
            else
            {
                //Redirect user to error page
                exceptionContext.Result = this.RedirectToAction(nameof(ErrorController.Index),
                                                                typeof(ErrorController).GetUrlName());
            }
        }
    }
}