using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projects.Data;
using projects.Models;

namespace projects.Controllers;

public class HomeController : Controller
{
   private readonly DataContext _context;
   public HomeController(DataContext context)
   {
      _context=context;
   }
   public IActionResult Page(){
      
    return View();
   }

   [HttpPost]
   public async Task<IActionResult> Page(User model){
      var kullanici= await _context.Users.FirstOrDefaultAsync(o=>o.UserName==model.UserName);
      if(kullanici==null){
        ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
        return View();
      }else if(kullanici.UserPassword != model.UserPassword){
          ModelState.AddModelError(string.Empty, "Hatalı şifre.");
          return View();
      }else{
         HttpContext.Session.SetString("KullaniciAdi", kullanici.UserName);
         return RedirectToAction("Liste");
      } 
   }
   
   public async Task<IActionResult> Film(){  
      string mySession=HttpContext.Session.GetString("KullaniciAdi");
      ViewBag.MySessionValue= mySession;
      return View(await _context.Films.ToListAsync());
   }
   [HttpPost]
   public async Task<IActionResult>Film(String FilmName){
      string KullaniciAdi=HttpContext.Session.GetString("KullaniciAdi");
      var list=await _context.Lists.FirstOrDefaultAsync(vt=>vt.UserName==KullaniciAdi&&vt.FilmName==FilmName);
      if(list==null){
      List listt=new List();
      listt.Date=DateTime.Now;
      listt.FilmName=FilmName; listt.UserName=KullaniciAdi;  
      _context.Lists.Add(listt);
      await _context.SaveChangesAsync();
      return  RedirectToAction("Liste","Home");}
      else{
          return  RedirectToAction("Liste","Home");
      }
   }
   public async Task<IActionResult> Liste(List<List> veriler){
      string KullaniciAdi=HttpContext.Session.GetString("KullaniciAdi");
      ViewBag.MySessionValue= KullaniciAdi;
       veriler=await _context.Lists.Where(vt=>vt.UserName==KullaniciAdi).ToListAsync();
      return View(veriler);
   }
  [HttpPost]
  public async Task<IActionResult> Liste(string FilmName){
   string KullaniciAdi=HttpContext.Session.GetString("KullaniciAdi");
   var obje=await _context.Lists.FirstOrDefaultAsync(vt=>vt.UserName==KullaniciAdi&&vt.FilmName==FilmName);
   _context.Lists.Remove(obje);
   await _context.SaveChangesAsync();
   return RedirectToAction("Film","Home");
  }
   public IActionResult Register(){
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Register(User model){
      var kullanici=await _context.Users.FirstOrDefaultAsync(vt=>vt.UserName==model.UserName);
      if(kullanici !=null){
          ModelState.AddModelError(string.Empty, "Böyle bir kullanıcı adı zaten var farklı bir kullanıcı adı deneyin");
          return View();
      }

      if(model.UserName==null || model.UserPassword==null){
         ModelState.AddModelError(string.Empty, "Boş alan olamaz.Lütfen alanları doldurunuz.");
          return View();
      }else{
     _context.Users.Add(model);
     await _context.SaveChangesAsync();
     return RedirectToAction("Page","Home");}
   }  
   public IActionResult LogOut(){
      HttpContext.Session.Clear();   
      return RedirectToAction("Page","Home");
   }
}
