﻿#region License

// Copyright (c) 2005-2013, CellAO Team
// 
// 
// All rights reserved.
// 
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
// 
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//     * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
// EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 
// Last modified: 2013-10-30 22:52
// Created:       2013-10-30 17:25

#endregion

namespace CellAO.Database.Dao
{
    #region Usings ...

    using System;
    using System.Data;
    using System.Linq;

    using Dapper;

    #endregion

    public static class OrganizationDao
    {
        public static bool OrgExists(string name)
        {
            using (IDbConnection conn = Connector.GetConnection())
            {
                return
                    conn.Query<int>("SELECT count(*) FROM organizations WHERE Name=@name", new { name })
                        .Single()
                        .Equals(1);
            }
        }

        public static bool OrgExists(int orgId)
        {
            using (IDbConnection conn = Connector.GetConnection())
            {
                return
                    conn.Query<int>("SELECT count(*) FROM organizations WHERE ID=@orgId", new { orgId })
                        .Single()
                        .Equals(1);
            }
        }

        public static bool CreateOrganization(string desiredOrgName, DateTime creationDate, int leaderId)
        {
            bool canBeCreated = !OrgExists(desiredOrgName);
            if (canBeCreated)
            {
                using (IDbConnection conn = Connector.GetConnection())
                {
                    conn.Execute(
                        "INSERT INTO organizations (creation, Name, LeaderID, GovernmentForm) VALUES (@creation, @name, @leaderid, 0)",
                        new { creation = creationDate, name = desiredOrgName, leaderid = leaderId });
                }
            }
            return canBeCreated;
        }

        public static int GetOrganizationId(string orgName)
        {
            using (IDbConnection conn = Connector.GetConnection())
            {
                return conn.Query<int>("SELECT ID FROM organizations WHERE Name=@name", new { name = orgName }).Single();
            }
        }

        public static int GetGovernmentForm(int orgId)
        {
            using (IDbConnection conn = Connector.GetConnection())
            {
                return
                    conn.Query<int>("SELECT GovernmentForm FROM organizations WHERE ID=@orgId", new { orgId }).Single();
            }
        }

        public static DBOrganization GetOrganizationData(int orgId)
        {
            if (!OrgExists(orgId))
            {
                return null;
            }
            using (IDbConnection conn = Connector.GetConnection())
            {
                return conn.Query<DBOrganization>("SELECT * FROM organizations WHERE ID=@orgId", new { orgId }).Single();
            }
        }

        public static void SetNewPrez(int orgId, int newLeaderId)
        {
            using (IDbConnection conn = Connector.GetConnection())
            {
                conn.Execute("UPDATE organizations SET LeaderID=@leaderId WHERE ID=@orgId", new { newLeaderId, orgId });
            }
        }
    }
}