using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paystack_tps.Models;

namespace paystack_tps.Controllers
{
    public class HomeController : Controller
    {

        public string publicKey = "pk_test_928270706ef382a34e56ef1b87a8e7a062db16f0";
        public string secretKey = "sk_test_4c80d4884df2bbba72a9a7d5b6e4b146903e61eb";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Transfers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] Supplier model)
        {
            try
            {
                string requestUrl = "https://api.paystack.co/transferrecipient";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
                var _response = await client.PostAsJsonAsync<PaystackAddRecipientModel>(requestUrl, new PaystackAddRecipientModel
                {
                    acocunt_number = model.AccountNumber,
                    bank_code = model.BankCode,
                    name = model.Name
                });

                var response = await _response.Content.ReadAsStringAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> InitiateTransfer([FromBody] PaystackInitiateTransferModel model)
        {
            try
            {
                string requestUrl = "https://api.paystack.co/transferrecipient";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
                var _response = await client.PostAsJsonAsync<PaystackInitiateTransferModel>(requestUrl, new PaystackInitiateTransferModel
                {
                    amount = model.amount,
                    recipient = model.recipient,
                   
                });

                var response = await _response.Content.ReadAsStringAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpGet]
        public async Task<IActionResult> VerifyBankAccount([FromQuery] string accountNumber, [FromQuery] string bankCode)
        {
            try
            {
                string requestUrl = $"https://api.paystack.co/bank/resolve?account_number={accountNumber}&bank_code={bankCode}";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
                var _response = await client.GetAsync(requestUrl);

                var response = await _response.Content.ReadAsStringAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
