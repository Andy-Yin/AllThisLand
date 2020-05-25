using System;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Helper
{
    public class MaterialMachine
    {
        public static bool Measure(ref T_ProjectMaterialItem material)
        {
            material.Status = EnumMaterialItemStatus.ToSubmitOrder2;
            material.MeasureStatus = true;
            material.EditTime = DateTime.Now;

            return true;
        }

        public static bool SubmitOrder(ref T_ProjectMaterialItem material)
        {
            material.Status = EnumMaterialItemStatus.ToConfirmOrder3; //待订单确认
            material.OrderStatus = true;
            material.EditTime = DateTime.Now;

            return true;
        }

        public static bool ConfirmOrder(ref T_ProjectMaterialItem material)
        {
            material.OrderStatus = true;
            if (material.Type == EnumMaterialType.Main)
            {
                material.Status = EnumMaterialItemStatus.ToBeDelivery4;
            }
            else
            {
                material.Status = EnumMaterialItemStatus.ToBeConfirmReceive7;
            }

            material.EditTime = DateTime.Now;
            return true;
        }

        public static bool Delivery(ref T_ProjectMaterialItem material)
        {
            material.DeliveryStatus = true;
            if (material.Type == EnumMaterialType.Local)
            {
                return false;
            }
            material.Status = EnumMaterialItemStatus.ToBeInStorage5;
            material.EditTime = DateTime.Now;
            return true;
        }

        public static bool InStorage(ref T_ProjectMaterialItem material)
        {
            material.InStorageStatus = true;

            if (material.Type == EnumMaterialType.Local)
            {
                return false;
            }
            material.Status = EnumMaterialItemStatus.ToBeOutStorageApply6;
            material.EditTime = DateTime.Now;
            return true;
        }

        public static bool OutStorageApply(ref T_ProjectMaterialItem material)
        {
            material.OutStorageApplyStatus = true;
            // 进入待收货状态
            material.Status = EnumMaterialItemStatus.ToBeConfirmReceive7;
            material.EditTime = DateTime.Now;
            return true;
        }

        public static bool ConfirmReceive(ref T_ProjectMaterialItem material)
        {
            material.ConfirmOrderStatus = true;

            material.Status = EnumMaterialItemStatus.ToBeInstall8;
            material.EditTime = DateTime.Now;
            return true;
        }

        public static bool Install(ref T_ProjectMaterialItem material)
        {
            material.InstallStatus = true;
            material.Status = EnumMaterialItemStatus.ToBeSettlement9; //待安装状态
            material.EditTime = DateTime.Now;
            return true;
        }
    }
}
