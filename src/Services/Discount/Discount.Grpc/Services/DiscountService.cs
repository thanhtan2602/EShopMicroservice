﻿using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
            {
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };
            }

            logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

            var response = coupon.Adapt<CouponModel>();

            return response;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Create Discount successfully");

            var response = coupon.Adapt<CouponModel>();
            return response;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Update Discount successfully");

            var response = coupon.Adapt<CouponModel>();
            return response;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext
                .Coupons
                .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found."));
            }

            dbContext.Coupons.Remove(coupon);

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Delete Discount successfully");

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
