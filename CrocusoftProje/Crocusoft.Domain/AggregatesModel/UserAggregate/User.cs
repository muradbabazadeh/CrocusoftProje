using CrocusoftProje.SharedKernel.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrocusoftProje.Domain.AggregatesModel.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime Birthday { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string PasswordHash { get; private set; }

        public bool ForcePasswordChange { get; private set; }

        public DateTime? LastPasswordChangeDateTime { get; private set; }

        public bool Locked { get; private set; }

        private readonly List<UserRole> _roles;

        public IReadOnlyCollection<UserRole> Roles => _roles;

        private readonly List<UserPermission> _permissions;
        public IReadOnlyCollection<UserPermission> Permissions => _permissions;


        protected User()
        {
            _roles = new List<UserRole>();
            _permissions = new List<UserPermission>();
        }

        public User(string userName, string email, string phone, string passwordHash, bool forcePasswordChange, bool locked, string firstName, string lastName, DateTime birthday) : this()
        {
            UserName = userName;
            Email = email;
            Birthday = birthday;
            Phone = phone;
            PasswordHash = passwordHash;
            ForcePasswordChange = forcePasswordChange;
            Locked = locked;
            LastPasswordChangeDateTime = null;
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetDetails(string userName, string email, string phone, string passwordHash, string firstName, string lastName, DateTime birthday)
        {
            UserName = userName;
            Email = email;
            Birthday = birthday;
            Phone = phone;
            PasswordHash = passwordHash;
            LastPasswordChangeDateTime = null;
            FirstName = firstName;
            LastName = lastName;
        }

        public void AddToRole(int roleId)
        {
            _roles.Add(new UserRole(roleId));
        }

        public void AddPermission(int permissionId)
        {
            _permissions.Add(new UserPermission(permissionId));
        }

        public void SetPasswordHash(string oldPasswordHash, string newPasswordHash)
        {
            if (PasswordHash != oldPasswordHash)
            {
                throw new ArgumentException("Invalid old password");
            }

            if (PasswordHash != newPasswordHash)
            {
                PasswordHash = newPasswordHash;
                ForcePasswordChange = false;
                LastPasswordChangeDateTime = DateTime.UtcNow;
            }
        }

        public void ResetPassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            ForcePasswordChange = true;
            LastPasswordChangeDateTime = DateTime.UtcNow;
        }
    }
}
