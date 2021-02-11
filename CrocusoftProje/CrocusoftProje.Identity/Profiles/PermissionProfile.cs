using AutoMapper;
using CrocusoftProje.Domain.AggregatesModel.PermissionAggregate;
using CrocusoftProje.Domain.AggregatesModel.RoleAggregate;
using CrocusoftProje.Domain.AggregatesModel.UserAggregate;
using CrocusoftProje.Identity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrocusoftProje.Identity.Profiles
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {

            CreateMap<Permission, PermissionDTO>();

            CreateMap<PermissionParameter, PermissionParameterDTO>();

            CreateMap<RolePermission, PermissionDTO>().ConstructUsing(ConvertRolePermission);
            CreateMap<UserPermission, PermissionDTO>().ConstructUsing(ConvertUserPermission);

            CreateMap<User, UserProfileDTO>()
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());
            CreateMap<User, GroupUserDTO>();
        }

        private PermissionDTO ConvertRolePermission(RolePermission rolePermission, ResolutionContext resolutionContext)
        {
            PermissionDTO permissionDTO = resolutionContext.Mapper.Map<PermissionDTO>(rolePermission.Permission);
            foreach (PermissionParameterDTO parameter in permissionDTO.Parameters)
            {
                parameter.Values = rolePermission.ParameterValues.Where(r => r.PermissionParameterId == parameter.Id).Select(v => v.Value).ToList();
            }

            return permissionDTO;
        }

        private PermissionDTO ConvertUserPermission(UserPermission userPermission, ResolutionContext resolutionContext)
        {
            PermissionDTO permissionDTO = resolutionContext.Mapper.Map<PermissionDTO>(userPermission.Permission);
            foreach (PermissionParameterDTO parameter in permissionDTO.Parameters)
            {
                parameter.Values = userPermission.ParameterValues.Where(r => r.PermissionParameterId == parameter.Id).Select(v => v.Value).ToList();
            }

            return permissionDTO;
        }
    }
}
