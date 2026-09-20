namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Dashboard;

public class BaseDomainDashboardPageModel : Contract.AbstractDashboardPageModel
{
    private bool hasAnyOrganizationalUnit;
    private bool hasAnyOrganiuzationalStructure;
    private bool hasAnyAdministrativeEntityKind;
    private bool hasAnyAdministrativeEntity;

    public bool HasAnyOrganizationalUnit { get => hasAnyOrganizationalUnit; set => SetProperty(ref hasAnyOrganizationalUnit, value); }

    public bool HasAnyOrganiuzationalStructure { get => hasAnyOrganiuzationalStructure; set => SetProperty(ref hasAnyOrganiuzationalStructure, value); }

    public bool HasAnyAdministrativeEntityKind { get => hasAnyAdministrativeEntityKind; set => SetProperty(ref hasAnyAdministrativeEntityKind, value); }

    public bool HasAnyAdministrativeEntity { get => hasAnyAdministrativeEntity; set => SetProperty(ref hasAnyAdministrativeEntity, value); }
}
