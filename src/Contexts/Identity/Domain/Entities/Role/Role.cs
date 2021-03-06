﻿using System;
using System.Collections.Generic;
using System.Text;
using Aggregates;

namespace eShop.Identity.Role
{
    public class Role : Aggregates.Entity<Role, State>
    {
        private Role() { }

        public void Activate()
        {
            Rule("Destroyed", x => x.Destroyed, "Role is already destroyed");
            Rule("Disabled", x => !x.Disabled, "Role is not disabled");

            Apply<Events.Activated>(x => { x.RoleId = Id; });
        }

        public void Deactivate()
        {
            Rule("Destroyed", x => x.Destroyed, "Role is already destroyed");
            Rule("Disabled", x => x.Disabled, "Role is already disabled");

            Apply<Events.Deactivated>(x => { x.RoleId = Id; });
        }

        public void Define(string name)
        {
            Apply<Events.Defined>(x =>
            {
                x.RoleId = Id;
                x.Name = name;
            });
        }

        public void Destroy()
        {
            Rule("Disabled", x => !x.Disabled, "Role is currently active");

            Apply<Events.Destroyed>(x => { x.RoleId = Id; });
        }

        public void Revoke()
        {
            Rule("Destroyed", x => x.Destroyed, "Role is already destroyed");

            Apply<Events.Revoked>(x => { x.RoleId = Id; });
        }
    }
}
