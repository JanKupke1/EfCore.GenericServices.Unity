using Prism.Mvvm;

namespace ShowRoom.Core.Mvvm
{
    /// <summary>
    /// this is the base class of all dto with an id 
    /// </summary>
    public class IdDataModelBase : BindableBase
    {
        private uint _Id;

        public uint Id
        {
            get
            {
                return _Id;
            }
            set
            {
                SetProperty(ref _Id, value);
            }
        }
    }
}
