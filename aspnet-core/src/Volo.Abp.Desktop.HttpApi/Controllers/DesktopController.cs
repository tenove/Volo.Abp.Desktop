using Volo.Abp.Desktop.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Desktop.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class DesktopController : AbpControllerBase
{
    protected DesktopController()
    {
        LocalizationResource = typeof(DesktopResource);
    }
}
