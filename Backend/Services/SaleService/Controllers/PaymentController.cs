<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleService.Models;
using SaleService.OtherModels;
using SaleService.Repositories.InvoiceFoodRepository;
using SaleService.Repositories.InvoiceRepository;
using SaleService.Service.RabbitMQServices;
using SaleService.Services.SessionServices;
using SaleService.Services.VNPayService;
using SaleService.ViewModels;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
=======
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
>>>>>>> 960a83c (commit)

namespace SaleService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
<<<<<<< HEAD
        private readonly IVnPayService _vnPayService;
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IInvoiceFoodRepo _invoiceFoodRepo;
        private readonly SalePublisherService _salePublisherService;
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _config;
        private readonly IDistributedCache _distributedCache;

        public PaymentController(
            IInvoiceRepo invoiceRepo,
            IInvoiceFoodRepo invoiceFoodRepo,
            SalePublisherService salePublisherService,
            ILogger<PaymentController> logger,
            IConfiguration config,
            IVnPayService vnPayService,
            IDistributedCache distributedCache)
        {
            _vnPayService = vnPayService;
            _invoiceRepo = invoiceRepo;
            _invoiceFoodRepo = invoiceFoodRepo;
            _salePublisherService = salePublisherService;
            _config = config;
            _logger = logger;
            _distributedCache = distributedCache;
        }

        private async Task<InvoiceRequestModel> GetInvoiceFromCacheAsync()
        {
            var invoiceJson = await _distributedCache.GetStringAsync("Invoice");
            return string.IsNullOrEmpty(invoiceJson) ? new InvoiceRequestModel() : JsonSerializer.Deserialize<InvoiceRequestModel>(invoiceJson);
        }

        private async Task SetInvoiceToCacheAsync(InvoiceRequestModel invoice)
        {
            var invoiceJson = JsonSerializer.Serialize(invoice);
            await _distributedCache.SetStringAsync("Invoice", invoiceJson);
        }

        [HttpPost("createInvoice")]
        public async Task<IActionResult> CreatePayment([FromBody] VnPaymentRequestModel paymentRequest)
        {
            if (paymentRequest == null)
            {
                return BadRequest("Invalid payment request data.");
            }

            var invoice = new InvoiceRequestModel
            {
                CreatedDate = paymentRequest.Invoice.CreatedDate,
                CustomerId = paymentRequest.Invoice.CustomerId,
                EmployeeId = paymentRequest.Invoice.EmployeeId,
                PromotionId = paymentRequest.Invoice.PromotionId,
                PaymentMethod = paymentRequest.Invoice.PaymentMethod,
                Total = paymentRequest.Invoice.Total,
                SeatIds = paymentRequest.Invoice.SeatIds,
                FoodRequests = paymentRequest.Invoice.FoodRequests ?? new List<FoodRequestModel>(),
                Schedule = paymentRequest.Invoice.Schedule
            };

            await SetInvoiceToCacheAsync(invoice);
            _logger.LogInformation("Invoice cached: {InvoiceId}", invoice.CreatedDate);

            try
            {
                var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, paymentRequest);

                if (string.IsNullOrEmpty(paymentUrl))
                {
                    return StatusCode(500, "Unable to generate payment URL.");
                }

                return Ok(new { redirectUrl = paymentUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the payment. Request: {Request}", JsonSerializer.Serialize(paymentRequest));
                return StatusCode(500, "An error occurred while creating the payment.");
            }
        }

        [HttpGet("paymentReturn")]
        public async Task<IActionResult> PaymentReturn([FromQuery] Dictionary<string, string> query)
        {
            var queryCollection = new QueryCollection(query.ToDictionary(kvp => kvp.Key, kvp => new Microsoft.Extensions.Primitives.StringValues(kvp.Value)));
            var paymentResponse = _vnPayService.PaymentExecute(queryCollection);

            if (!paymentResponse.Success)
            {
                _logger.LogError("Payment verification failed. Response: {Response}", paymentResponse);
                return BadRequest("Payment verification failed.");
            }

            try
            {
                var invoiceRequestModel = await GetInvoiceFromCacheAsync();
                _logger.LogInformation("Invoice from cache: {InvoiceId}", invoiceRequestModel.CreatedDate);

                var invoiceCreate = new Invoice
                {
                    CreatedDate = invoiceRequestModel.CreatedDate,
                    CustomerId = invoiceRequestModel.CustomerId,
                    EmployeeId = invoiceRequestModel.EmployeeId,
                    PromotionId = invoiceRequestModel.PromotionId,
                    PaymentMethod = invoiceRequestModel.PaymentMethod,
                    Total = invoiceRequestModel.Total,
                    Status = "Completed",
                };

                var createdInvoice = await _invoiceRepo.CreateInvoiceAsync(invoiceCreate);
                _logger.LogInformation("Invoice created successfully. Invoice ID: {InvoiceId}", createdInvoice.Id);

                if (invoiceRequestModel.FoodRequests != null && invoiceRequestModel.FoodRequests.Any())
                {
                    foreach (var foodRequest in invoiceRequestModel.FoodRequests)
                    {
                        var invoiceFood = new InvoiceFood
                        {
                            InvoiceId = createdInvoice.Id,
                            FoodId = foodRequest.FoodId,
                            Quantity = foodRequest.Quantity
                        };
                        await _invoiceFoodRepo.CreateInvoiceFoodAsync(invoiceFood);
                        _logger.LogInformation("Invoice food created successfully. Food ID: {FoodId}", invoiceFood.FoodId);
                    }
                }

                var schedules = invoiceRequestModel.SeatIds?.Select(seatId => new OriginalSchedule
                {
                    MovieId = invoiceRequestModel.Schedule.MovieId,
                    Date = invoiceRequestModel.Schedule.Date,
                    Time = invoiceRequestModel.Schedule.Time,
                    SeatId = seatId,
                    InvoiceId = createdInvoice.Id,
                }).ToList();
                if (schedules != null)
                {
                    _salePublisherService.UpdateInvoiceIdInSchedule(schedules);
                    _logger.LogInformation("Update invoice successfully!");
                }

                var redirectUrl = $"{_config["VnPay:VnPayFrontendReturnUrl"]}?success={paymentResponse.Success}&orderId={paymentResponse.OrderId}&transactionId={paymentResponse.TransactionId}";
                return Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the payment return. Response: {Response}", paymentResponse);
                return StatusCode(500, "An error occurred while processing the payment return.");
            }
        }
    }
=======
        private readonly string _vnpUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string _merchantId = "MDNUWEVW";
        private readonly string _secretKey = "60ED1DZEMBLRXS8H8YNGPOZTLFQXTYUM";

        [HttpPost("createPayment")]
        public IActionResult CreatePayment([FromBody] Invoice invoice)
        {
            if (!invoice.Total.HasValue)
            {
                return BadRequest("Total amount is required");
            }

            var vnpayRequest = new VnPayRequest
            {
                Amount = (long)(invoice.Total.Value * 100), 
                OrderId = invoice.CustomerId.ToString(),
                ReturnUrl = "http://localhost:2000/payment/return",
                VnpHashSecret = _secretKey,
                VnpMerchantId = _merchantId
            };

            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var vnpayUrl = vnpayRequest.GenerateUrl(_vnpUrl, clientIp);
            return Redirect(vnpayUrl);
        }

        [HttpGet("return")]
        public IActionResult PaymentReturn()
        {
            var query = Request.Query;
            var responseCode = query["vnp_ResponseCode"];
            var orderId = query["vnp_TxnRef"];
            // Validate the response and update your system

            return Redirect("http://localhost:2000/");
        }
    }

    public class VnPayRequest
    {
        public long Amount { get; set; }
        public string OrderId { get; set; }
        public string ReturnUrl { get; set; }
        public string VnpHashSecret { get; set; }
        public string VnpMerchantId { get; set; }

        public string GenerateUrl(string baseUrl, string clientIp)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "vnp_Version", Uri.EscapeDataString("2.0.0") },
                { "vnp_Command", Uri.EscapeDataString("pay") },
                { "vnp_TmnCode", Uri.EscapeDataString(VnpMerchantId) },
                { "vnp_Amount", Uri.EscapeDataString(Amount.ToString()) },
                { "vnp_CurrCode", Uri.EscapeDataString("VND") },
                { "vnp_TxnRef", Uri.EscapeDataString(OrderId) },
                { "vnp_OrderInfo", Uri.EscapeDataString("Thanh toán đơn hàng") },
                { "vnp_ReturnUrl", Uri.EscapeDataString(ReturnUrl) },
                { "vnp_IpAddr", Uri.EscapeDataString(clientIp) },
                { "vnp_CreateDate", Uri.EscapeDataString(DateTime.Now.ToString("yyyyMMddHHmmss")) },
            };

            var sortedQueryParams = queryParams.OrderBy(x => x.Key).ToList();
            var hashData = string.Join("&", sortedQueryParams.Select(x => $"{x.Key}={x.Value}"));
            var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(VnpHashSecret));
            var hash = BitConverter.ToString(hmac.ComputeHash(Encoding.ASCII.GetBytes(hashData))).Replace("-", "").ToUpper();

            return $"{baseUrl}?{hashData}&vnp_SecureHash={hash}";
        }

    }
>>>>>>> 960a83c (commit)
}
