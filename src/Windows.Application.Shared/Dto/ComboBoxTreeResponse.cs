using System.Collections.Generic;
using Windows.SeedWork;

namespace Windows.Application.Shared.Dto
{
    public class ComboBoxTreeResponse:ITreeNode<ComboBoxTreeResponse>
    {
        public object Id { get; set; }
        public object PId { get; set; }
        public string Name { get; set; }
        public List<ComboBoxTreeResponse> Children { get; set; } = new List<ComboBoxTreeResponse>();
    }
}
