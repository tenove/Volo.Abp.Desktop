using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Desktop.Localization;
using Volo.Abp.Application.Services;

namespace Volo.Abp.Desktop;

/* Inherit your application services from this class.
 */
public abstract class DesktopAppService : ApplicationService
{
    protected DesktopAppService()
    {
        LocalizationResource = typeof(DesktopResource);
    }
}
