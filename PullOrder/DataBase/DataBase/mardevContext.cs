using System;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBase
{
    public partial class mardevContext : DbContext
    {
        public mardevContext()
        {
        }

        public mardevContext(DbContextOptions<mardevContext> options)
            : base(options)
        {
        }

        public long p_GetNextOrderNumberSeq(int x)
        {
            return x;
        }

        public long RetrieveBatchTransactionSeq(int x)
        {
            return x;
        }


        public virtual DbSet<Api> Api { get; set; }
        public virtual DbSet<ApiCallback> ApiCallback { get; set; }
        public virtual DbSet<ApiUser> ApiUser { get; set; }
        public virtual DbSet<ApiUserPlatformToken> ApiUserPlatformToken { get; set; }
        public virtual DbSet<CompleteCheckoutNotification> CompleteCheckoutNotification { get; set; }
        public virtual DbSet<CompleteSalesRequest> CompleteSalesRequest { get; set; }
        public virtual DbSet<CompleteSalesRequestItem> CompleteSalesRequestItem { get; set; }
        public virtual DbSet<EbayApiAuthSession> EbayApiAuthSession { get; set; }
        public virtual DbSet<EbayOrder> EbayOrder { get; set; }
        public virtual DbSet<EbayOrderTransaction> EbayOrderTransaction { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistory { get; set; }
        public virtual DbSet<LogPullOrder> LogPullOrder { get; set; }
        public virtual DbSet<QueuePullOrder> QueuePullOrder { get; set; }
        public virtual DbSet<QueuePullOrderUserToken> QueuePullOrderUserToken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=team-dev-mysqldb.cxkbhs9nvapa.ap-southeast-1.rds.amazonaws.com;User Id=dev-db-admin;Password=55zCPN52UhrcmkEhH5wtFahbTv2FS8Nv;Database=mar-dev");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Api>(entity =>
            {
                entity.Property(e => e.ApiId)
                    .HasColumnName("api_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiKey)
                    .IsRequired()
                    .HasColumnName("api_key")
                    .HasColumnType("char(36)");

                entity.Property(e => e.AppName)
                    .IsRequired()
                    .HasColumnName("app_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");
            });

            modelBuilder.Entity<ApiCallback>(entity =>
            {
                entity.HasIndex(e => e.ApiId)
                    .HasName("IX_ApiCallBack_apiId");

                entity.Property(e => e.ApiCallbackId)
                    .HasColumnName("api_callback_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.CallbackUrl)
                    .IsRequired()
                    .HasColumnName("callback_url")
                    .HasColumnType("text");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.TopicArn)
                    .IsRequired()
                    .HasColumnName("topic_arn")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<ApiUser>(entity =>
            {
                entity.HasIndex(e => e.ApiId)
                    .HasName("IX_ApiUser_apiId");

                entity.HasIndex(e => new { e.ApiId, e.UserKey })
                    .HasName("IX_ApiUser_apiId_userKey")
                    .IsUnique();

                entity.HasIndex(e => new { e.ApiId, e.ApiUserId, e.IsDeleted })
                    .HasName("IX_ApiUser_apiId_apiUserId_isDeleted");

                entity.Property(e => e.ApiUserId)
                    .HasColumnName("api_user_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");

                entity.Property(e => e.UserKey)
                    .IsRequired()
                    .HasColumnName("user_key")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UserPrivateKey)
                    .IsRequired()
                    .HasColumnName("user_private_key")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.UserToken)
                    .IsRequired()
                    .HasColumnName("user_token")
                    .HasColumnType("text");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CallBackUrl)
                    .HasColumnName("callback_url")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<ApiUserPlatformToken>(entity =>
            {
                entity.HasIndex(e => new { e.ApiId, e.ApiUserId })
                    .HasName("IX_ApiUserPlatformToken_apiId_userId");

                entity.HasIndex(e => new { e.ApiId, e.PlatformCode, e.PlatformUserKey })
                    .HasName("UQ_ApiUserPlatformToken_apiId_platformCode_PlatformUserkey")
                    .IsUnique();

                entity.HasIndex(e => new { e.ApiId, e.ApiUserId, e.ApiUserPlatformTokenId, e.IsDeleted })
                    .HasName("IX_ApiUserPlatformToken_apiId_userId_tokenId_isDeleted");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("api_user_platform_token_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasColumnName("access_token")
                    .HasColumnType("text");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.ExpirationTime).HasColumnName("expiration_time");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");

                entity.Property(e => e.PlatformCode)
                    .IsRequired()
                    .HasColumnName("platform_code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PlatformUserKey)
                    .IsRequired()
                    .HasColumnName("platform_user_key")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.PlatformUsername)
                    .IsRequired()
                    .HasColumnName("platform_username")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<CompleteCheckoutNotification>(entity =>
            {
                entity.HasIndex(e => e.EbayItemId)
                    .HasName("IDX_CompleteCheckoutNotification_ebayItemId");

                entity.HasIndex(e => e.VariationSku)
                    .HasName("IDX_CompleteCheckoutNotification_variationSku");

                entity.Property(e => e.CompleteCheckoutNotificationId)
                    .HasColumnName("complete_checkout_notification_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AutoPay)
                    .HasColumnName("auto_pay")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.EbayItemId)
                    .IsRequired()
                    .HasColumnName("ebay_item_id")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EbayOrderId)
                    .IsRequired()
                    .HasColumnName("ebay_order_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EbayReceipentId)
                    .IsRequired()
                    .HasColumnName("ebay_receipent_id")
                    .HasColumnType("text");

                entity.Property(e => e.EbayTransactionId)
                    .IsRequired()
                    .HasColumnName("ebay_transaction_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EiasToken)
                    .IsRequired()
                    .HasColumnName("eias_token")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ItemSku)
                    .HasColumnName("item_sku")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.NotificationReceiveOn).HasColumnName("notification_receive_on");

                entity.Property(e => e.NotifictionEvtName)
                    .IsRequired()
                    .HasColumnName("notifiction_evt_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TransactionSite)
                    .HasColumnName("transactionSite")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.VariationSku)
                    .HasColumnName("variation_sku")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<CompleteSalesRequest>(entity =>
            {
                entity.HasIndex(e => new { e.ApiId, e.ApiUserId })
                    .HasName("IX_CompleteSalesRequest_apiId_apiUserId");

                entity.Property(e => e.CompleteSalesRequestId)
                    .HasColumnName("complete_sales_request_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.CallBackUrl)
                    .HasColumnName("call_back_url")
                    .HasColumnType("text");

                entity.Property(e => e.NumberOfItems)
                    .HasColumnName("number_of_items")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestBy)
                    .IsRequired()
                    .HasColumnName("request_by")
                    .HasColumnType("char(36)");

                entity.Property(e => e.RequestOn).HasColumnName("request_on");
            });

            modelBuilder.Entity<CompleteSalesRequestItem>(entity =>
            {
                entity.HasIndex(e => new { e.ApiId, e.ApiUserId, e.CompleteSalesRequestItemId })
                    .HasName("IX_CompleteSalesRequestItem_apiId_apiUserId_requestId");

                entity.Property(e => e.CompleteSalesRequestItemId)
                    .HasColumnName("complete_sales_request_item_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.CompleteSalesRequestId)
                    .IsRequired()
                    .HasColumnName("complete_sales_request_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.Courier)
                    .IsRequired()
                    .HasColumnName("courier")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EbayOrderTransactionId)
                    .IsRequired()
                    .HasColumnName("ebay_order_transaction_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ErrorCode)
                    .HasColumnName("error_code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("error_message")
                    .HasColumnType("text");

                entity.Property(e => e.ShipDate).HasColumnName("ship_date");

                entity.Property(e => e.Success)
                    .HasColumnName("success")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.TrackingNumber)
                    .IsRequired()
                    .HasColumnName("tracking_number")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<EbayApiAuthSession>(entity =>
            {
                entity.HasKey(e => e.SessionId);

                entity.Property(e => e.SessionId)
                    .HasColumnName("session_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.EbaySessionId)
                    .IsRequired()
                    .HasColumnName("ebay_session_id")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ExpireOn).HasColumnName("expire_on");

                entity.Property(e => e.Platform)
                    .IsRequired()
                    .HasColumnName("platform")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasColumnType("varchar(36)");
            });

            modelBuilder.Entity<EbayOrder>(entity =>
            {
                //entity.HasIndex(e => e.MarketPlaceOrderNumber)
                //    .HasName("UQ_EbayOrder_market_place_order_number")
                //    .IsUnique();

                //entity.HasIndex(e => new { e.ApiId, e.ApiUserId })
                //    .HasName("IX_EbayOrder_apiId_apiUserid");

                //entity.HasIndex(e => new { e.ApiId, e.ApiUserId, e.OrderId })
                //    .HasName("UQ_EbayOrder_apiId_apiUserId_orderId")
                //    .IsUnique();

                entity.Property(e => e.EbayOrderId)
                    .HasColumnName("ebay_order_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("api_user_platform_token_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BuyerEbayUserId)
                    .HasColumnName("buyer_ebay_user_id")
                    .HasColumnType("text");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit");

                entity.Property(e => e.MarketPlaceOrderNumber)
                    .IsRequired()
                    .HasColumnName("market_place_order_number")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");

                entity.Property(e => e.OrderCreateAt).HasColumnName("order_create_at");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasColumnName("order_id")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasColumnName("order_status")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.OrderSubtotal)
                    .HasColumnName("order_subtotal")
                    .HasColumnType("decimal(18,3)");

                entity.Property(e => e.OrderTotal)
                    .HasColumnName("order_total")
                    .HasColumnType("decimal(18,3)");

                entity.Property(e => e.PaymentTime).HasColumnName("payment_time");

                entity.Property(e => e.ShippingAddressId)
                    .HasColumnName("shipping_address_id")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingCityName)
                    .HasColumnName("shipping_city_name")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingCountry)
                    .HasColumnName("shipping_country")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingCountryName)
                    .HasColumnName("shipping_country_name")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingName)
                    .HasColumnName("shipping_name")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingPhone)
                    .HasColumnName("shipping_phone")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingPostalCode)
                    .HasColumnName("shipping_postal_code")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingService)
                    .HasColumnName("shipping_service")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingState)
                    .HasColumnName("shipping_state")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingStreet)
                    .HasColumnName("shipping_street")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingStreet1)
                    .HasColumnName("shipping_street1")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingStreet2)
                    .HasColumnName("shipping_street2")
                    .HasColumnType("text");

                entity.Property(e => e.ShippingTimeMax)
                    .HasColumnName("shipping_time_max")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShippingTimeMin)
                    .HasColumnName("shipping_time_min")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<EbayOrderTransaction>(entity =>
            {
                //entity.HasIndex(e => e.MarketPlaceTransactionNumber)
                //    .HasName("UQ_EbayOrderTransaction_maketPlaceTransactionNumber")
                //    .IsUnique();

                //entity.HasIndex(e => new { e.ApiId, e.ApiUserId })
                //    .HasName("IX_EbayOrderTransaction_apiId_apiUserId");

                //entity.HasIndex(e => new { e.ApiId, e.ApiUserId, e.EbayOrderId, e.ItemId, e.TransactionId })
                //    .HasName("UQ_EbayOrderTransaction_apiId_apiUsrId_ebayOrderid_ItmId_transId")
                //    .IsUnique();

                entity.Property(e => e.EbayOrderTransactionId)
                    .HasColumnName("ebay_order_transaction_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("api_user_platform_token_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BuyerEmail)
                    .HasColumnName("buyer_email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.BuyerUserId)
                    .HasColumnName("buyer_user_id")
                    .HasColumnType("text");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.EbayOrderId)
                    .IsRequired()
                    .HasColumnName("ebay_order_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsHandleByApp)
                    .HasColumnName("is_handle_by_app")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsShipped)
                    .HasColumnName("is_shipped")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ItemSite)
                    .IsRequired()
                    .HasColumnName("item_site")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ItemSku)
                    .HasColumnName("item_sku")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ListingType)
                    .HasColumnName("listing_type")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.MarketPlaceTransactionNumber)
                    .IsRequired()
                    .HasColumnName("market_place_transaction_number")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");

                entity.Property(e => e.Platform)
                    .HasColumnName("platform")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.QuantityPurchase)
                    .HasColumnName("quantity_purchase")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShipmentCarrierUsed)
                    .HasColumnName("shipment_carrier_used")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ShipmentTrackingNumber)
                    .HasColumnName("shipment_tracking_number")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ShippedTime).HasColumnName("shipped_time");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TransactionPrice)
                    .HasColumnName("transaction_price")
                    .HasColumnType("decimal(18,3)");

                entity.Property(e => e.TransactionSiteId)
                    .IsRequired()
                    .HasColumnName("transaction_site_id")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.VariationSku)
                    .HasColumnName("variation_sku")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId);

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(95)");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<LogPullOrder>(entity =>
            {
                entity.HasKey(e => e.PullOrderBgplogId);

                entity.Property(e => e.PullOrderBgplogId)
                    .HasColumnName("pullorder_log_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("api_user_platform_token_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProcessEndTime).HasColumnName("process_end_time").HasColumnType("datetime");

                entity.Property(e => e.ProcessMessage).HasColumnName("Process_log_message").HasColumnType("text");

                entity.Property(e => e.ProcessStartTime).HasColumnName("process_beg_time").HasColumnType("datetime");

                entity.Property(e => e.ProcessStatus).HasColumnName("process_status").HasColumnType("int(11)");
            });

            modelBuilder.Entity<QueuePullOrder>(entity =>
            {
                entity.HasKey(e => e.PullOrderQueueId);

                entity.Property(e => e.PullOrderQueueId)
                    .HasColumnName("pullorder_queue_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("apiuser_platform_token_Id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PullOrderFromID)
                    .HasColumnName("pullorder_from_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasColumnName("order_Id")
                    .HasColumnType("text");

                entity.Property(e => e.SiteCode)
                    .IsRequired()
                    .HasColumnName("site_code")
                    .HasColumnType("text");
                entity.Property(e => e.IsUsed)
                    .IsRequired()
                    .HasColumnName("is_used")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<QueuePullOrderUserToken>(entity =>
            {
                entity.HasKey(e => e.LoginUserTokenQueueId);

                entity.Property(e => e.LoginUserTokenQueueId)
                    .HasColumnName("LoginUserTokenQueueID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("apiUserPlatformTokenId")
                    .HasColumnType("bigint(11)");

                entity.Property(e => e.NumberOfDays)
                    .HasColumnName("numberOfDays")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsUsed)
                   .IsRequired()
                   .HasColumnName("IsUsed")
                   .HasColumnType("bit(1)");

                entity.Property(e => e.OrderTotal)
                 .HasColumnName("order_total")
                 .HasColumnType("int(11)");
            });
        }
    }
}
