using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Volo.Abp.Desktop.DynamicMenu
{
    public interface IMenuItem
    {
        [CanBeNull]
        string ParentName { get; }

        [Key]
        [NotNull]
        string Name { get; }

        [NotNull]
        string DisplayName { get; }

        [CanBeNull]
        string PageTag { get; }

        [CanBeNull]
        string Icon { get; }

        [CanBeNull]
        string Permission { get; }
    }
}