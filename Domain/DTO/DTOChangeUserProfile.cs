using Autoglass.Domain.DTO;
using System.Collections.Generic;

namespace Autoglass.Application.DTO
{
    public class DTOChangeUserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<DTOUserProfilePermission> Permissions { get; set; }

        public DTOChangeUserProfile()
        {
            Permissions = new List<DTOUserProfilePermission>();
        }
    }
}
