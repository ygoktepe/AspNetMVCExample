using AspNetMVCOrnek_BussinessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMVCOrnek_EntityLayer.Entities;
using AspNetMVCOrnek_BussinessLayer;

namespace AspNetMVCOrnek_UI.Controllers
{
    public class HomeController : Controller
    {
        StudentRepo sRepo = new StudentRepo();
        public ActionResult Index()
        {
            var list = sRepo.Queryable().Where(x=> !x.IsDeleted).ToList();
            Logger.LogMessage($"Home/Index çağrıldı {list.Count} adet öğrenci listelendi");
            return View(list); //sayfaya model gönderdim
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //bu modelle gelen datayı kayıt edelim
              model.RegisteredDate = DateTime.Now;
               int sonuc= sRepo.Insert(model);
                if (sonuc>0)
                {
                    TempData["AddStudentSuccessMessage"] =
                        "Yeni öğrenci eklendi.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("","Beklenmedik bir hata oldu!");
                    return View(model);
                    //2. yöntem
                   // throw new Exception("Beklenmedik bir hata oldu!");
                }
            }
            catch (Exception ex)
            {
                //ex loglansın
                Logger.LogMessage($"Home/AddStudent sayfası hata oldu. {ex}");
                ModelState.AddModelError("", "Beklenmedik bir hata oldu!");
               return View(model);
                //2. yöntem
                //TempData["AddStudentErrorMessage"] =
                //    "Beklenmedik bir hata oldu! Tekrar deneyiniz!";
                //return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult OgrenciSil(int id)
        {
            try
            {
                if (id > 0)
                {
                    Student s = sRepo.GetById(id);
                    if (s == null)
                    {
                        TempData["OgrenciSilHataMesaji"]= "Öğrenci bulunamadığı için silme işlemi yapılamadı!";
                        return RedirectToAction("Index","Home");
                    }
                    //student var!
                    s.IsDeleted = true;
                    sRepo.Update(); // soft delete
                    TempData["OgrenciSilMesaji"] = "Öğrenci Silindi";
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    throw new Exception("HATA: Gönderilen id bilgisi istenilen formatta değildir!");
                }
            }
            catch (Exception ex)
            {
                // ex loglansın
                TempData["OgrenciSilHataMesaji"] = "Beklenmedik bir hata oldu! "+ ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult DeleteStudent(int? id)
        {
            try
            {
                if (id>0)
                {
                    var student = sRepo.GetById(id.Value);
                    if (student==null)
                    {
                        throw new Exception("Öğrenci bulunamadı. Tekrar deneyiniz!");
                    }
                    return View(student);
                }
                else
                {
                    throw new Exception("id değeri düzgen verilmelidir!");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Beklenmedik bir sorun oldu. " + ex.Message);
               // ModelState.AddModelError("", $"Beklenmedik bir sorun oldu. {ex.Message}");
                return View();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteStudent(Student model)
        {
            try
            {
                var student = sRepo.GetById(model.Id);
                sRepo.Delete(student); //hard delete
                ViewBag.DeleteResult = $"{student.Name} {student.Surname} isimli öğrenci silindi";
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir sorun oldu. " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult SilOgrenci(int id)
        {
            try
            {
                if (id>0)
                {
                    var student = sRepo.GetById(id);
                    if (student==null)
                    {
                        throw new Exception("HATA:Öğrenci bulunamadı!");
                    }
                    sRepo.Delete(student);
                    return Json(new { success = true, message = "Öğrenci silindi" });
                }
                else
                {
                    throw new Exception("HATA: id parametresi düzgün verilmedi!");
                }
            }
            catch (Exception ex)
            {
                return Json(new {success=false, message=ex.Message });
            }
        }

        [HttpGet]

        public JsonResult OgrenciBilgileri(int id)
        {
            try
            {
                if (id>0)
                {
                    var student = sRepo.GetById(id);
                    if (student == null)
                    {
                        throw new Exception("HATA: Öğrenci Bulunamadı!");

                    }
                    return Json(new { success = true, data = student },JsonRequestBehavior.AllowGet);
                }
                else
                {
                    throw new Exception("HATA: id parametresi düzgün verilmelidir!");
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public ActionResult OgrenciGuncelle(Student model)
        {
            try
            {
                var student = sRepo.GetById(model.Id); 
                if(student == null)
                {
                    throw new Exception("Öğrenci bulunamadığı için güncelleme yapılamadı!");
                }
                student.Name = model.Name;
                student.Surname = model.Surname;
                sRepo.Update();
                TempData["OgrenciGuncelleMesaji"] = "Öğrenci bilgileri güncellendi";
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {

                TempData["OgrenciGuncelleHataMesaji"] = "Beklenmedik bir hata oldu! Hata: " + ex.Message;
                return RedirectToAction("Index", "Home");

            }
        }

    }
}