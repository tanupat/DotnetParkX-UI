using System.Net;
using System.Security.Claims;
using Line.Login;
using Line.Login.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkXUI.Models.Auth;
using ParkXUI.Utility;
using ParkXUI.ViewModel.Auth;
using Profile = Line.Login.Models.Profile;

namespace ParkXUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly HttpClientUtility _httpClientUtility;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(HttpClientUtility httpClientUtility, IConfiguration configuration,ILogger<AuthController> logger)
        {
            _httpClientUtility = httpClientUtility;
            _configuration = configuration;
            _logger = logger;
   
        }
        

        private string GetDomainUrl()
        {
            var request = HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }

        // หน้าล็อกอิน
        public IActionResult Login(string error = null)
        {
            ViewBag.Error = error;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = new
                {
                    email = login.email,
                    password = login.password,
                    register = false
                };
                var apiResultCheckUser = await _httpClientUtility.PostAsync("auth/ValidateMember", user);

                if (apiResultCheckUser.HttpStatus != HttpStatusCode.OK)
                {
                    return RedirectToAction("Login", new { error = apiResultCheckUser.MessageError });
                }
                if (apiResultCheckUser.HttpStatus == HttpStatusCode.OK)
                {
                    var dataMember = JsonConvert.DeserializeObject<UserModel>(apiResultCheckUser.Data);
                    if (dataMember.memberKey == null)
                    {
                        return RedirectToAction("Login", new { error = "การเข้าสู่ระบบล้มเหลว" });
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, dataMember.data.fullName),
                            new Claim(ClaimTypes.NameIdentifier, dataMember.memberKey),
                            new Claim(ClaimTypes.Role, PolicyLogin.Member.ToString()),
                        };
                        if (!string.IsNullOrEmpty(dataMember.data.wrapUserId))
                        {
                            claims.Add(new Claim("WrapUserId", dataMember.data.wrapUserId));
                        }

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), authProperties);
                        return RedirectToAction("Profile", "User");
                    }
                }
            }
            return View(login);
        }

        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse", "Auth")
                });
        }

        public ActionResult LineLogin()
        {
            var domainUrl = GetDomainUrl();
            var redirectUri = $"{domainUrl}/Auth/LineResponse";
            var state = Guid.NewGuid().ToString();
            var client = new LineLoginClient(_configuration["LineKeys:ClientId"], _configuration["LineKeys:ClientSecret"], redirectUri, state, Scope.Profile | Scope.OpenId | Scope.Email);
            var authUri = client.GetAuthUri();

            return Redirect(authUri);
        }

        public async Task<IActionResult> LineResponse(string code, string state)
        {
            var domainUrl = GetDomainUrl();
            var redirectUri = $"{domainUrl}/Auth/LineResponse";
            
            var client = new LineLoginClient(_configuration["LineKeys:ClientId"], _configuration["LineKeys:ClientSecret"], redirectUri, state, Scope.Profile | Scope.OpenId | Scope.Email);
            TokenResponse authenticateResult = await client.GetToken(code);
            
            Profile  profile = await client.GetProfile(authenticateResult.AccessToken);
            
            if (profile == null)
            {
                return RedirectToAction("Login", new { error = "การเข้าสู่ระบบล้มเหลว" });
                
            }
            
            var dataPost = new
            {
                LineId = authenticateResult.JWTPayload.Sub,
                Register = false
            };

            var apiResult = await _httpClientUtility.PostAsync("auth/ValidateMember", dataPost);
            if (apiResult.HttpStatus == HttpStatusCode.OK)
            {
                if (apiResult.Data == null)
                {
                    var dataInsert = new
                    {
                        lineId = authenticateResult.JWTPayload.Sub,
                        email = authenticateResult.JWTPayload.Email,
                        picture = authenticateResult.JWTPayload.Picture,
                        memberKey = authenticateResult.JWTPayload.Nonce,
                        name = authenticateResult.JWTPayload.Name,
                        platform = 0,
                        service = state.ToUpper(),
                        description = "Line Login for " + state.ToUpper(),
                        segment = "Line",
                        register = true
                    };

                    var apiResultInsert = await _httpClientUtility.PostAsync("auth/ValidateMember", dataInsert);
                    if (apiResultInsert.HttpStatus != HttpStatusCode.OK)
                    {
                        return RedirectToAction("Login", new { error = apiResultInsert.MessageError });
                    }
                    else
                    {
                        apiResult = apiResultInsert;
                    }
                }

                var user = JsonConvert.DeserializeObject<UserModel>(apiResult.Data);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.data.fullName),
                    new Claim(ClaimTypes.NameIdentifier, user.memberKey),
                    new Claim(ClaimTypes.Role, PolicyLogin.Member.ToString()),
                };
                if (!string.IsNullOrEmpty(user.data.wrapUserId))
                {
                    claims.Add(new Claim("WrapUserId", user.data.wrapUserId));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Profile", "User");
            }
            else
            {
                return RedirectToAction("Login", new { error = apiResult.MessageError });
            }
        }

        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (result.Succeeded)
                {
                    var Google = result.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var DataPost = new
                    {
                        GoogleId = Google,
                        Register = false
                    };

                    var apiResult = await _httpClientUtility.PostAsync("auth/ValidateMember", DataPost);
                    if (apiResult.HttpStatus == HttpStatusCode.OK)
                    {
                        if (apiResult.Data == null)
                        {
                            var dataInsert = new
                            {
                                googleId = Google,
                                register = true,
                                email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
                                fullName = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                                name = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                            };

                            var apiResultInsert = await _httpClientUtility.PostAsync("auth/ValidateMember", dataInsert);
                            if (apiResultInsert.HttpStatus != HttpStatusCode.OK)
                            {
                                return RedirectToAction("Login", new { error = apiResultInsert.MessageError });
                            }
                            else
                            {
                                apiResult = apiResultInsert;
                            }
                        }

                        var user = JsonConvert.DeserializeObject<UserModel>(apiResult.Data);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.data.fullName),
                            new Claim(ClaimTypes.NameIdentifier, user.memberKey),
                            new Claim(ClaimTypes.Role, PolicyLogin.Member.ToString()),
                        };
                        if (!string.IsNullOrEmpty(user.data.wrapUserId))
                        {
                            claims.Add(new Claim("WrapUserId", user.data.wrapUserId));
                        }

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), authProperties);
                        return RedirectToAction("Profile", "User");
                    }
                    else
                    {
                        return RedirectToAction("Login", new { error = apiResult.MessageError });
                    }
                }
                else
                {
                    return RedirectToAction("Login", new { error = "การเข้าสู่ระบบล้มเหลว" });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Auth", new { error = e.Message.ToString() });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register(string error = null)
        {
            ViewBag.Error = error;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var CcheckExistRegisterUser = new
                {
                    email = user.email,
                    register = false
                };
                var apiResultCheckExist = await _httpClientUtility.PostAsync("auth/ValidateMember", CcheckExistRegisterUser);

                if (apiResultCheckExist.HttpStatus != HttpStatusCode.OK)
                {
                    return RedirectToAction("Register", new { error = apiResultCheckExist.MessageError });
                }
                if (apiResultCheckExist.HttpStatus == HttpStatusCode.OK)
                {
                    var dataMember = JsonConvert.DeserializeObject<UserModel>(apiResultCheckExist.Data);
                    if (dataMember.memberKey != null)
                    {
                        return RedirectToAction("Register", new { error = "อีเมลนี้มีอยู่แล้ว" });
                    }
                }

                var RegisterUser = new
                {
                    email = user.email,
                    password = user.password,
                    fullName = user.fullName,
                    register = true
                };
                var apiResult = await _httpClientUtility.PostAsync("auth/ValidateMember", RegisterUser);
                if (apiResult.HttpStatus == HttpStatusCode.OK)
                {
                    var userRegister = JsonConvert.DeserializeObject<UserModel>(apiResult.Data);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userRegister.data.fullName),
                        new Claim(ClaimTypes.NameIdentifier, userRegister.memberKey),
                        new Claim(ClaimTypes.Role, PolicyLogin.Member.ToString()),
                    };
                    if (!string.IsNullOrEmpty(userRegister.data.wrapUserId))
                    {
                        claims.Add(new Claim("WrapUserId", userRegister.data.wrapUserId));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Profile", "User");
                }
                else
                {
                    return RedirectToAction("Register", new { error = apiResult.MessageError });
                }
            }
            return View(user);
        }
    }
}
