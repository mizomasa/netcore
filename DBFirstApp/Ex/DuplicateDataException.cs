using System;
namespace DBFirstApp.Ex
{
    public class DuplicateDataException : Exception
    {
        public DuplicateDataException(string id)
            : base(string.Format("対象のデータが存在しております。>>id={0}", id))
        {
        }
    }
}
