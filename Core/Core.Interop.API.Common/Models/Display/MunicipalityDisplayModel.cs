using System;

namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class MunicipalityDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Location.Persistence.Entities.Municipality, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public StateDisplayModel State { get; set; }

        public void Import(Framework.Guama.Location.Persistence.Entities.Municipality entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            State = new StateDisplayModel();

            State.Import(entity.State);
        }

        public void Import(object officialPostalContactItemMunicipality)
        {
            throw new NotImplementedException();
        }
    }
}
