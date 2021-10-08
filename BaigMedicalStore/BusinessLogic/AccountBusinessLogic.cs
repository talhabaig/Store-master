
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Kendo.Mvc.UI;
using System.Collections;
using System.Data.Entity.Core.Objects;
using Microsoft.Owin.Security;
using BaigMedicalStore.Models;

namespace BaigMedicalStore.BusinessLogic
{

    public class AccountBusinessLogic : BusinessLogicBase
    {
        public ApplicationUserManager UserManager => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        public ApplicationSignInManager SignInManager => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
        private IAuthenticationManager AuthenticationManager => HttpContext.Current.GetOwinContext().Authentication;
         
        public async Task<ApplicationUser> FindUserAsync(string userName, string password)
        {
            return await UserManager.FindAsync(userName, password);
        }

        public async Task<ApplicationUser> GetLoggedinUserAsync(LoginViewModel model)
        {
            ApplicationUser user = await FindUserAsync(model.Username, model.Password);

            if (user == null)
            {
                throw new Exception("User Not found");
            }

            return user;
        }

        //public RegisterViewModel GetRegisterViewModel(long userId)
        //{
        //    RoleManagerBusinessLogic roleManagerBusinessLogic = new RoleManagerBusinessLogic();

        //    RegisterViewModel viewModel = new RegisterViewModel();
        //    if (userId != 0)
        //    {
        //        var siteUser = db.SiteUsers.Where(x => x.UserId == userId).FirstOrDefault();

        //        if (siteUser != null)
        //        {
        //            viewModel.FirstName = siteUser.FirstName;
        //            viewModel.LastName = siteUser.LastName;
        //            viewModel.Address = siteUser.Address;
        //            //viewModel.UserId = siteUser.AspNetUser.Id;
        //            viewModel.UserId = siteUser.UserId;
        //            viewModel.Status = siteUser.Status;

        //            if (siteUser.AspNetUser != null)
        //            {
        //                viewModel.Email = siteUser.AspNetUser.Email;
        //                viewModel.Username = GetUserNameWithoutTenant(siteUser.AspNetUser.UserName);
        //                viewModel.PhoneNumber = siteUser.AspNetUser.PhoneNumber;

        //                if (siteUser.AspNetUser.AspNetRoles.FirstOrDefault() != null)
        //                {
        //                    viewModel.RoleName = roleManagerBusinessLogic.GetRoleNameWithoutTenant(siteUser.AspNetUser.AspNetRoles.FirstOrDefault().Name);
        //                }
        //            }

        //            if (siteUser.SiteUserSubjects != null)
        //            {
        //                viewModel.SubjectIdList = siteUser.SiteUserSubjects.Select(x => x.SubjectId).ToList();
        //            }

        //            if (siteUser.SiteUserPrograms != null)
        //            {
        //                viewModel.ProgramIdList = siteUser.SiteUserPrograms.Select(x => x.ProgramId).ToList();
        //            }
        //        }
        //        else
        //        {
        //            throw new NotFoundException();
        //        }
        //    }
        //    else
        //    {
        //        viewModel.Status = (int)Enumeration.EntityStatus.Active;
        //    }

        //    viewModel = PopulateRegisterViewModelList(viewModel);

        //    return viewModel;
        //}

        //public async Task SendEmailVerifyCodeAsync(ApplicationUser user)
        //{
        //    var requestContext = HttpContext.Current.Request.RequestContext;

        //    // Send an email with this link
        //    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //    var callbackUrl = new UrlHelper(requestContext).Action("ConfirmEmail", "Account", new { userId = user.Id, code = code });

        //    //Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

        //    EmailTemplateBusinessLogic objEmailTemplateBusinessLogic = new EmailTemplateBusinessLogic();

        //    var emailTemplate = objEmailTemplateBusinessLogic.GetEmailTemplate(Enumeration.EmailCode.ConfirmationEmail);
        //    string emailBody = string.Empty;
        //    emailBody = HttpUtility.HtmlDecode(emailTemplate.Body);

        //    ////replacing placeholders in emailbody with actual data

        //    SiteUser siteUser = GetSiteUser(user.Id);

        //    emailBody = emailBody.Replace("§LINK§", callbackUrl);
        //    emailBody = emailBody.Replace("§USERNAME§", siteUser.FirstName + " " + siteUser.LastName);
        //    await UserManager.SendEmailAsync(user.Id, emailTemplate.Subject, emailBody);
        //}

        public User GetSiteUser(string userId)
        {
            return db.Users.Where(u => u.AspNetUserId == userId).FirstOrDefault();
        }

        //public RegisterViewModel PopulateRegisterViewModelList(RegisterViewModel model)
        //{
        //    var objRoleManagerBusinessLogic = new RoleManagerBusinessLogic();
        //    model.RoleList = objRoleManagerBusinessLogic.GetRolesList();

        //    return model;
        //}

        //public void RegisterUser(RegisterViewModel model)
        //{
        //    RoleManagerBusinessLogic roleManagerBusinessLogic = new RoleManagerBusinessLogic();
        //    var siteUser = new SiteUser();
        //    bool isNewUserCreate = false;

        //    using (var tran = db.Database.BeginTransaction())
        //    {
        //        IdentityResult result = IdentityResult.Success;

        //        if (model.UserId == 0)
        //        {
        //            ApplicationUser user = new ApplicationUser { UserName = GetUserNameWithTenant(model.Username), Email = model.Email, TenantId = tenantId, PhoneNumber = model.PhoneNumber };
        //            result = CreateUser(user, model.Password);

        //            if (!result.Succeeded)
        //            {
        //                throw new InvalidUserInfoExceptions(string.Join(",", result.Errors));
        //            }

        //            model.UserId = user.Id;
        //            isNewUserCreate = true;
        //        }

        //        if (result.Succeeded)
        //        {
        //            if (!isNewUserCreate)
        //            {
        //                RemoveRoleFromUser(model.UserId, roleManagerBusinessLogic.GetRoleNameWithTenant(model.RoleName));

        //                siteUser = db.SiteUsers.Where(x => x.UserId == model.UserId).FirstOrDefault();
        //                db.SiteUserPrograms.RemoveRange(siteUser.SiteUserPrograms.Select(x => x).ToList());
        //                db.SiteUserSubjects.RemoveRange(siteUser.SiteUserSubjects.Select(x => x).ToList());
        //            }

        //            result = AssignRole(model.UserId, roleManagerBusinessLogic.GetRoleNameWithTenant(model.RoleName));

        //            if (result.Succeeded)
        //            {
        //                siteUser.UserId = model.UserId;
        //                siteUser.Status = model.Status;
        //                siteUser.FirstName = model.FirstName;
        //                siteUser.LastName = model.LastName;
        //                siteUser.Address = model.Address;
        //                siteUser.TenantId = tenantId;

        //                if (model.RoleName == UserRole.Teacher)
        //                {
        //                    if (model.SubjectIdList != null)
        //                    {
        //                        foreach (var subjectId in model.SubjectIdList)
        //                        {
        //                            SiteUserSubject siteUserSubject = new SiteUserSubject();
        //                            siteUserSubject.UserId = siteUser.UserId;
        //                            siteUserSubject.SubjectId = subjectId;

        //                            siteUser.SiteUserSubjects.Add(siteUserSubject);
        //                        }
        //                    }

        //                    if (model.ProgramIdList != null)
        //                    {
        //                        foreach (var programId in model.ProgramIdList)
        //                        {
        //                            SiteUserProgram siteUserProgram = new SiteUserProgram();
        //                            siteUserProgram.UserId = siteUser.UserId;
        //                            siteUserProgram.ProgramId = programId;

        //                            siteUser.SiteUserPrograms.Add(siteUserProgram);
        //                        }
        //                    }
        //                }

        //                if (isNewUserCreate)
        //                {
        //                    siteUser.CreatedDate = DateTime.UtcNow;
        //                    siteUser.CreatedBy = GetLoggedInUserId();
        //                    db.SiteUsers.Add(siteUser);
        //                }
        //                else
        //                {
        //                    UpdatePhoneNumber(model.UserId, model.PhoneNumber);
        //                    UpdateEmail(model.UserId, model.Email);

        //                    siteUser.ModifiedDate = DateTime.UtcNow;
        //                    siteUser.ModifiedBy = GetLoggedInUserId();
        //                    db.Entry(siteUser).State = EntityState.Modified;
        //                }

        //                db.SaveChanges();
        //                tran.Commit();
        //            }
        //            else
        //            {
        //                throw new InvalidUserInfoExceptions(string.Join(",", result.Errors));
        //            }
        //        }
        //    }
        //}

        //public string ChangeUserStatus(long userId, int status)
        //{
        //    return ToggleActiveStatus<SiteUser>(cal => cal.UserId == userId);
        //}

        //public DataSourceResult GetRegisterUsers(DataSourceRequest request)
        //{
        //    RoleManagerBusinessLogic roleManagerBusinessLogic = new RoleManagerBusinessLogic();

        //    Hashtable fltr = new Hashtable();
        //    CommonFunction.PopulateFiltersInHashTable(request.Filters, fltr);

        //    string sortBy = string.Empty;
        //    if (request.Sorts.Any())
        //        sortBy = request.Sorts[0].Member + " " + request.Sorts[0].SortDirection;

        //    ObjectParameter objparam = new ObjectParameter("TotalRecords", System.Data.DbType.Int16);

        //    var PhoneNumber = fltr.ContainsKey("PhoneNumber") ? fltr["PhoneNumber"].ToString().Trim() : "";
        //    var Username = fltr.ContainsKey("Username") ? fltr["Username"].ToString().Trim() : "";
        //    var Email = fltr.ContainsKey("Email") ? fltr["Email"].ToString().Trim() : "";

        //    var queryResult = db.Users_Get(Username, Email, PhoneNumber, request.Page, request.PageSize, sortBy, tenantId, -1, objparam).ToList()
        //         .Select(s => new RegisterViewModel()
        //         {
        //             UserId = s.UserId,
        //             FullName = s.FullName,
        //             IsActive = s.IsActive,
        //             Username = GetUserNameWithoutTenant(s.UserName),
        //             Email = s.Email,
        //             Status = s.Status,
        //             RoleName = roleManagerBusinessLogic.GetRoleNameWithoutTenant(s.RoleName),
        //             PhoneNumber = s.PhoneNumber,
        //             EditUrl = CryptographyUtility.GetEncryptedQueryString(new { userId = s.UserId })
        //         });

        //    DataSourceResult dsr = new DataSourceResult();
        //    dsr.Data = queryResult;
        //    dsr.Total = (int)objparam.Value;

        //    return dsr;
        //}


        public IdentityResult CreateUser(ApplicationUser user, string password)
        {
            return UserManager.Create(user, password);
        }

        //public void UpdatePhoneNumber(long userId, string phoneNumber)
        //{
        //    var user = UserManager.FindById(userId);
        //    if (user != null)
        //    {
        //        user.PhoneNumber = phoneNumber;
        //        UserManager.Update(user);
        //    }
        //}

        //public void UpdateEmail(long userId, string email)
        //{
        //    var user = UserManager.FindById(userId);
        //    user.Email = email;
        //    UserManager.Update(user);
        //}

        public IdentityResult AssignRole(string userId, string roleName)
        {
            return UserManager.AddToRole(userId, roleName);
        }

        public IdentityResult RemoveRoleFromUser(string userId, string roleName)
        {
            IdentityResult result = IdentityResult.Success;
            if (UserManager.IsInRole(userId, roleName))
            {
                result = UserManager.RemoveFromRole(userId, roleName);
            }

            return result;
        }

        public async Task<bool> SetPassword(string userId)
        {
            var siteUserId = db.AspNetUsers.FirstOrDefault(u => u.Id == userId);

            if (siteUserId != null)
            {
                var result = await UserManager.RemovePasswordAsync(userId);
                if (result.Succeeded)
                {
                    result = await UserManager.AddPasswordAsync(userId, "123456"); // AppConstants.DefaultResetPassword);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public string GetCurrentUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public ApplicationUser FindUserByName(string userName)
        {
            return UserManager.FindByName(userName);
        }

        public void SignIn(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

    }
}
