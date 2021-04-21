using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.SeedWork
{
    public interface ITreeNode
    {
        object PId { get; set; }
        object Id { get; set; }
        string Name { get; set; }
    }
    public interface ITreeNode<T>: ITreeNode
    {
        List<T> Children { get; set; }
    }
}
