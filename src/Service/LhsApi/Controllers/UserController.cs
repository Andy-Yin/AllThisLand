using Lhs.Common;
using Lhs.Interface;
using LhsAPI.Dtos.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Core.Util;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;
using LhsApi.Dtos.Response;
using LhsAPI.Dtos.Response.User;
using U9Service;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[AuthFilter]
    public class UserController : PlatformControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserPositionRepository _userPositionRepo;
        private readonly ICompanyRepository _companyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectUserFlowPositionRepository _projectUserFlowPositionRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserController(
            IUserRepository userRepository,
            IUserPositionRepository userPositionRepo,
            ICompanyRepository companyRepository,
            IDepartmentRepository departmentRepository,
            IPositionRepository positionRepository,
            IProjectRepository projectRepository,
            IProjectUserFlowPositionRepository projectUserFlowPositionRepository,
            ICustomerRepository customerRepository)
        {
            _userRepo = userRepository;
            _userPositionRepo = userPositionRepo;
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _projectRepository = projectRepository;
            _projectUserFlowPositionRepository = projectUserFlowPositionRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// 登录-后台
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ResponseMessage> Login(ReqLogin req)
        {
            var result = new ResponseMessage
            {
                ErrCode = MessageResultCode.Error,
                ErrMsg = CommonMessage.LoginFailed,
                Data = string.Empty
            };
            var userInfo = await _userRepo.Login(req.Phone, req.Password);
            if (userInfo == null)
            {
                return result;
            }
            var userPermissions = await _positionRepository.GetUserPermission(userInfo.Id);
            result.ErrMsg = CommonMessage.GetSuccess;
            result.Data = new RespUserInfo(userInfo) { PermissionList = userPermissions };
            result.ErrCode = MessageResultCode.Success;
            return result;
        }

        /// <summary>
        /// 登录-APP
        /// </summary>
        [HttpPost("loginForAPP")]
        [AllowAnonymous]
        public async Task<ResponseMessage> LoginForAPP(ReqLogin req)
        {
            var result = new ResponseMessage
            {
                ErrCode = MessageResultCode.Error,
                ErrMsg = "没有该用户对应的项目",
                Data = ""
            };
            var userInfo = await _userRepo.Login(req.Phone, req.Password);
            if (userInfo == null)
            {
                return result;
            }

            // 作为工长的审批列表
            var flowList = await _projectUserFlowPositionRepository.GetProjectUserFlowPositionListByUserIdAndType(userInfo.Id, UserConst.ConstructionMasterPositionId);
            if (flowList.Any())
            {
                var user = new RespLoginForAPP(userInfo)
                {
                    RoleId = UserConst.ConstructionMasterPositionId//工长的FlowId
                };

                result.ErrMsg = CommonMessage.GetSuccess;
                result.Data = user;
                result.ErrCode = MessageResultCode.Success;
                return result;
            }

            // 作为监理的审批列表
            flowList = await _projectUserFlowPositionRepository.GetProjectUserFlowPositionListByUserIdAndType(userInfo.Id, UserConst.SuperVisorPositionId);
            if (flowList.Any())
            {
                var user = new RespLoginForAPP(userInfo)
                {
                    RoleId = UserConst.SuperVisorPositionId//监理的FlowId
                };

                result.ErrMsg = CommonMessage.GetSuccess;
                result.Data = user;
                result.ErrCode = MessageResultCode.Success;
                return result;
            }

            return result;
        }

        /// <summary>
        /// 登录-微信
        /// </summary>
        [HttpPost("weChat/login")]
        [AllowAnonymous]
        public async Task<ResponseMessage> LoginForWeChat(ReqLogin req)
        {
            var result = new ResponseMessage
            {
                ErrCode = MessageResultCode.Error,
                ErrMsg = CommonMessage.LoginFailed,
                Data = string.Empty
            };
            var userInfo = await _customerRepository.GetCustomerInfo(req.Phone);
            if (userInfo == null || userInfo.Password != req.Password)
            {
                result.ErrMsg = CommonMessage.UserNotExistOrPwdError;
                return result;
            }
            result.ErrMsg = CommonMessage.GetSuccess;
            result.Data = new RespLoginForWeChat(userInfo);
            result.ErrCode = MessageResultCode.Success;

            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        [HttpPost("editPassword")]
        public async Task<ResponseMessage> EditPassword(ReqEditPassword req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (string.IsNullOrEmpty(req.Phone) || string.IsNullOrEmpty(req.RePassword))
            {
                return result;
            }
            // 获取用户信息
            var dbUser = await _userRepo.GetUserInfo(req.Phone);
            if (dbUser == null)
            {
                result.ErrMsg = "用户不存在";
                return result;
            }
            dbUser.Password = req.RePassword;
            dbUser.EditTime = DateTime.Now;
            var updateStatus = await _userRepo.UpdateAsync(dbUser);
            if (updateStatus)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 修改密码-微信
        /// </summary>
        [HttpPost("weChat/editPassword")]
        public async Task<ResponseMessage> EditPasswordForWeChat(ReqEditPassword req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (string.IsNullOrEmpty(req.Phone) || string.IsNullOrEmpty(req.RePassword))
            {
                return result;
            }
            // 获取用户信息
            var dbUser = await _customerRepository.GetCustomerInfo(req.Phone);
            if (dbUser == null)
            {
                result.ErrMsg = CommonMessage.UserNotExist;
                return result;
            }
            dbUser.Password = req.RePassword;
            dbUser.EditTime = DateTime.Now;
            var updateStatus = await _customerRepository.UpdateAsync(dbUser);
            if (updateStatus)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        [HttpGet("UserInfo")]
        public async Task<ResponseMessage> UserInfo([FromQuery]ReqUserInfo req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var user = await _userRepo.GetUserInfo(req.Phone);
            if (user == null)
            {
                result.ErrMsg = "用户不存在。";
                return result;
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = user;
            return result;
        }

        /// <summary>
        /// 获取用户列表-后台
        /// </summary>
        [HttpGet("list")]
        public async Task<ResponseMessage> UserList([FromQuery]ReqUserList req)
        {
            var response = new RespBasePage()
            {
                DataList = new List<RespListUserInfo>()
            };

            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };
            var dataList = await _userRepo.GetUserList(req.RegionName, req.CompanyId, req.DepartmentId
                , req.PositionId, req.Keywords, req.PageNum);
            if (dataList.Items.Any())
            {
                var userIds = dataList.Items.Select(n => n.Id).ToList();
                var userPosition = await _userPositionRepo.GetUserPositions(userIds);
                response.DataList = dataList.Items.Select(n => new RespListUserInfo(n, userPosition)).ToList();
                response.TotalCount = (int)dataList.TotalCount;
                response.TotalPage = (int)dataList.TotalPages;
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;

            result.Data = response;
            return result;
        }

        /// <summary>
        /// 启用或者禁用用户-后台
        /// </summary>
        [HttpPost("isUsed")]
        public async Task<ResponseMessage> IsUsed(ReqEnableOrDisableUser req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Id <= 0)
            {
                return result;
            }
            // 获取用户信息
            var dbUser = await _userRepo.SingleAsync(req.Id);
            if (dbUser == null)
            {
                return result;
            }
            dbUser.IsUsed = req.IsUsed;
            dbUser.EditTime = DateTime.Now;
            var updateStatus = await _userRepo.UpdateAsync(dbUser);
            if (updateStatus)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 用户重置密码-后台
        /// </summary>
        [HttpPost("resetPwd")]
        public async Task<ResponseMessage> ResetPwd(ReqEnableOrDisableUser req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Id <= 0)
            {
                return result;
            }
            // 获取用户信息
            var dbUser = await _userRepo.SingleAsync(req.Id);
            if (dbUser == null)
            {
                return result;
            }
            dbUser.Password = UserConst.DefaultPassword;
            dbUser.EditTime = DateTime.Now;
            var updateStatus = await _userRepo.UpdateAsync(dbUser);
            if (updateStatus)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 获取大区/店面/部门/岗位数据-后台
        /// </summary>
        [HttpGet("basicData")]
        public async Task<ResponseMessage> BasicData([FromQuery]ReqAuth req)
        {
            var repData = new RespBasicData();
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = repData
            };
            var companies = await _companyRepository.GetAllCompany();
            var departments = await _departmentRepository.GetAllDepartment();
            var positions = await _positionRepository.GetAllPosition();

            repData.RegionList = companies.GroupBy(n => n.RegionName)
                .Select(r => new Region
                {
                    RegionName = r.Key,
                    CompanyList = r.Select(item => new Company(item)
                    {
                        DepartmentList = departments.Where(depart => depart.CompanyId == item.CompanyId).ToList()
                            .Select(z => new Department(z)).ToList()
                    }).ToList()
                }).ToList();
            repData.PositionList = positions.Select(n => new Position(n)).ToList();

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = repData;
            return result;
        }

        /// <summary>
        /// 获取分公司-后台、APP
        /// </summary>
        [HttpGet("Companies")]
        public async Task<ResponseMessage> Companies([FromQuery]ReqAuth req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var dataList = await _companyRepository.GetAllCompany();

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList.Any() ? dataList.Select(n => new RespCompanyInfo(n)) : new List<RespCompanyInfo>();
            return result;
        }

        /// <summary>
        /// 更新用户-U9使用
        /// </summary>
        [HttpPost("UpdateUser")]
        public async Task<ResponseMessage> UpdateUser(ReqUpdateUser req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var user = new T_User
            {
                U9UserId = req.UserId,
                Name = req.Name,
                Phone = req.Phone,
                Sex = req.Sex,
                Password = UserConst.DefaultPassword,
                IsUsed = true,
                IsDel = false,
                CompanyId = req.CompanyId,
                DepartmentId = req.DepartmentId,
                EditTime = DateTime.Now,
                CreateTime = DateTime.Now
            };
            // 获取用户信息
            var dbUser = await _userRepo.GetUserInfoByU9UserId(req.UserId);
            //新增用户
            if (dbUser == null)
            {
                var addStatus = await _userRepo.AddAsync(user) > 0;
                if (addStatus)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //更新时，保留创建时间、密码、启用状态
            user.CreateTime = dbUser.CreateTime;
            user.IsUsed = dbUser.IsUsed;
            user.Password = dbUser.Password;
            var updateStatus = await _userRepo.UpdateAsync(user);
            if (updateStatus)
            {

                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 禁用或者删除用户-U9使用
        /// </summary>
        [HttpPost("DeleteUser")]
        public async Task<ResponseMessage> DeleteUser(ReqDeleteUser req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取用户信息
            var dbUser = await _userRepo.GetUserInfoByU9UserId(req.UserId);
            if (dbUser == null)
            {
                return result;
            }
            dbUser.IsDel = true;
            dbUser.EditTime = DateTime.Now;
            var updateStatus = await _userRepo.UpdateAsync(dbUser);
            if (updateStatus)
            {

                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 更新分公司-U9使用
        /// </summary>
        [HttpPost("UpdateCompany")]
        public async Task<ResponseMessage> UpdateCompany(ReqUpdateCompany req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = new T_Company()
            {
                CompanyId = req.CompanyId,
                Name = req.Name,
                ParentId = req.ParentId,
                RegionName = req.RegionName,
                IsDel = false,
                EditTime = DateTime.Now,
                CreateTime = DateTime.Now
            };
            // 获取用户信息
            var dbData = await _companyRepository.GetCompany(req.CompanyId);
            //新增用户
            if (dbData == null)
            {
                var addStatus = await _companyRepository.AddAsync(data) > 0;
                if (addStatus)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //更新时，保留创建时间、密码、启用状态
            data.CreateTime = dbData.CreateTime;
            var updateStatus = await _companyRepository.UpdateAsync(data);
            if (updateStatus)
            {

                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 禁用或者删除分公司-U9使用
        /// </summary>
        [HttpPost("DeleteCompany")]
        public async Task<ResponseMessage> DeleteCompany(ReqDeleteCompany req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var deleteStatus = await _companyRepository.DeleteCompany(req.CompanyId);
            if (deleteStatus)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 更新部门-U9使用
        /// </summary>
        [HttpPost("UpdateDepartment")]
        public async Task<ResponseMessage> UpdateDepartment(ReqUpdateDepartment req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = new T_Department()
            {
                DepartmentId = req.DepartmentId,
                Name = req.Name,
                CompanyId = req.CompanyId,
                IsDel = false,
                EditTime = DateTime.Now,
                CreateTime = DateTime.Now
            };
            // 获取用户信息
            var dbData = await _departmentRepository.GetDepartment(req.CompanyId);
            //新增用户
            if (dbData == null)
            {
                var addStatus = await _departmentRepository.AddAsync(data) > 0;
                if (addStatus)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //更新时，保留创建时间、密码、启用状态
            data.CreateTime = dbData.CreateTime;
            var updateStatus = await _departmentRepository.UpdateAsync(data);
            if (updateStatus)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 禁用或者删除部门-U9使用
        /// </summary>
        [HttpPost("DeleteDepartment")]
        public async Task<ResponseMessage> DeleteDepartment(ReqDeleteDepartment req)
        {
            var result = new ResponseMessage
            {

                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var deleteStatus = await _departmentRepository.DeleteDepartment(req.DepartmentId);
            if (deleteStatus)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 获取组织相关信息-调用U9示例
        /// </summary>
        [HttpGet("GetJFOrganization")]
        public async Task<GetJFOrganization> GetJFOrganization([FromQuery]ReqAuth req)
        {
            var client = new JFClient();
            var auth = GetAuthInfo();
            //var response = await client.GetJFOrganizationAsync(CommonConst.U9ServiceSource, auth.TimeSign, auth.Key);
            return JsonHelper.DeserializeJsonToObject<GetJFOrganization>("");
        }
    }
}
