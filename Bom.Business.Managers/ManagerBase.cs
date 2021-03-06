﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using Core.Common.Exceptions;
using NLog;

namespace Bom.Business.Managers
{
    public class ManagerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ManagerBase()
        {
            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                logger.Error("Throwing AuthorizationValidationException: {0}", ex.Message);
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                logger.Error("Throwing FaultException: {0}", ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                var message = new StringBuilder(ex.Message);

                Action<Exception> traverse = null;
                traverse = (n) =>
                {
                    if (n.InnerException == null) return;
                    message.AppendFormat("Inner Exception: {0}", n.InnerException.Message);
                    traverse(n.InnerException);
                };

                traverse(ex);

                logger.Error("Throwing exception: {0}", message.ToString());
                throw new FaultException(message.ToString());
            }
        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                logger.Error("Throwing AuthorizationValidationException: {0}", ex.Message);
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                logger.Error("Throwing FaultException: {0}", ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error("Throwing Exception: {0}", ex.Message);
                throw new FaultException(ex.Message);
            }
        }
    }
}
