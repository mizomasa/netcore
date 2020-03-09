using System;
namespace DBFirstApp.Ex
{
    public class NotFoundException :Exception
    {
        public NotFoundException(string id)
            :base(string.Format("対象のデータが見つかりません.>>id={id}",id))
        {
        }
    }
}
