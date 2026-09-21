using MOROPO.SINERGIAGOB.Core.Persistence.Entities.Contract;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public sealed class ElementSource : AbstractNomenclator
    {
        public string Alias { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Alias)) return Name;

            return $"{Name} ({Alias})";
        }
    }
}