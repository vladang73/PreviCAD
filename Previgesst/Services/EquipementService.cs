using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using Previgesst.ViewModels.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;


using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace Previgesst.Services
{
    public class EquipementService
    {
        private EquipementRepository equipementRepository;
        //private EquipementArticuloRepository equipementArticuloRepository;

        private SourceEnergieRepository sourceEnergieRepository;
        private DispositifRepository dispositifRepository;
        private MaterielRepository materielRepository;


        public EquipementService(EquipementRepository equipementRepository, SourceEnergieRepository sourceEnergieRepository, DispositifRepository dispositifRepository, MaterielRepository materielRepository)
        {
            this.equipementRepository = equipementRepository;
            this.sourceEnergieRepository = sourceEnergieRepository;
            this.dispositifRepository = dispositifRepository;
            this.materielRepository = materielRepository;
            //this.equipementArticuloRepository = equipementArticuloRepository;
        }

        public IEnumerable<EquipementDDLViewModel> GetAllAsEquipementDDLViewModel(int ClientId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (langue == "fr")
            {
                var DDL = equipementRepository.AsQueryable().Where(s => s.ClientId == ClientId).OrderBy(s => s.NomEquipement).Select(s => new EquipementDDLViewModel()
                {

                    NomEquipement = s.NomEquipement,
                    EquipementId = s.EquipementId,
                    NumeroEquipment = s.NumeroEquipement

                });

                return DDL;
            }
            else
            {
                var DDL = equipementRepository.AsQueryable().Where(s => s.ClientId == ClientId).OrderBy(s => s.NomEquipementEN).Select(s => new EquipementDDLViewModel()
                {

                    NomEquipement = s.NomEquipementEN,
                    EquipementId = s.EquipementId,
                    NumeroEquipment = s.NumeroEquipement


                });
                return DDL;
            }

        }




        internal DataSourceResult GetReadListeEquipements(DataSourceRequest request, int ClientId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";
            var time = DateTime.Now.ToLongTimeString();
            var result = equipementRepository.AsQueryable().OrderBy(x => x.NomEquipement).Where(x => x.ClientId == ClientId).OrderBy(x => x.NomEquipement).Select(x => new EquipementViewModel()
            {
                ClientId = x.ClientId,
                EquipementId = x.EquipementId,
                NomEquipement = x.NomEquipement,
                NomEquipementEN = x.NomEquipementEN,
                NumeroEquipment = x.NumeroEquipement,
                NomFichier = x.NomFichier,
                NomClient = x.Client.Nom,
                Suppressible = x.FicheCadenassage.Count == 0,
                Thumbnail = baseURL + "Images/Cadenassage/Equipements/" + x.EquipementId.ToString() + "/thumb.jpg?time=" + time,

                Task = x.Task,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                RiskAnalysis = x.RiskAnalysis ?? false,
                LockoutProcedure = x.LockoutProcedure ?? false,
                SafeWorkProcedure = x.SafeWorkProcedure ?? false,

                //Accessories = x.Accessories,
                //Deposit = x.Deposit,
                //Energy = x.Energy,
                //Nomenclature = x.Nomenclature,
                Function = x.Function,
                YearOfProduction = x.YearOfProduction,
                NumberOfSerie = x.NumberOfSerie,

                QRCode = baseURL + x.QRCode ?? "",
                //QRCode = baseURL + "Images/Cadenassage/Equipements/" + x.EquipementId.ToString() + "/QRCode.jpg?time=" + time,

            }).ToDataSourceResult(request);

            return result;
        }

        internal IEnumerable<EquipementArticuloViewModel> GetEquipementArticuloes(int equipementId)
        {
            //var result = equipementRepository.Get(equipementId).EquipementArticulo
            var result = (
                from eq in equipementRepository.Get(equipementId).EquipementArticulo
                join eng in sourceEnergieRepository.AsQueryable() on eq.Energy equals eng.SourceEnergieId
                join dis in dispositifRepository.AsQueryable() on eq.Deposit equals dis.DispositifId
                join mat in materielRepository.AsQueryable() on eq.Accessories equals mat.MaterielId

                select new EquipementArticuloViewModel
                {
                    EquipementArticuloID = eq.EquipementArticuloID,
                    EquipementId = eq.EquipementId,
                    Accessories = eq.Accessories,
                    Deposit = eq.Deposit,
                    Energy = eq.Energy,

                    AccessoriesDescription = mat.Description,
                    DepositDescription = dis.Description,
                    EnergyDescription = eng.Description,
                    Nomenclature = eq.Nomenclature,

                })
                .ToList();

            return result;
        }


        public void SaveEquipement(EquipementViewModel model)
        {
            var item = equipementRepository.Get(model.EquipementId);
            if (item == null)
                item = new Models.Equipement();

            item.EquipementId = model.EquipementId;
            item.ClientId = model.ClientId;
            item.NomFichier = model.NomFichier;
            item.NomEquipement = model.NomEquipement;
            item.NomEquipementEN = model.NomEquipementEN;
            item.NumeroEquipement = model.NumeroEquipment;



            if (item.EquipementId > 0)
                equipementRepository.Update(item);
            else
                equipementRepository.Add(item);

            equipementRepository.SaveChanges();
            item = equipementRepository.Get(item.EquipementId);
            model.EquipementId = item.EquipementId;
            model.NomClient = item.Client.Nom;
        }

        public void SaveEquipementNew(EquipementViewModel model, string mode)
        {
            var item = equipementRepository.Get(model.EquipementId);
            if (item == null)
                item = new Models.Equipement();

            item.EquipementId = model.EquipementId;

            if (mode == "Equipment")
            {
                item.Model = model.Model;
                item.Manufacturer = model.Manufacturer;
                item.Task = model.Task;
                item.RiskAnalysis = model.RiskAnalysis;
                item.LockoutProcedure = model.LockoutProcedure;
                item.SafeWorkProcedure = model.SafeWorkProcedure;
                item.NumberOfSerie = model.NumberOfSerie;
                item.YearOfProduction = model.YearOfProduction;
                item.Function = model.Function;
            }
            //else if (mode == "Energy")
            //{
            //    item.Energy = model.Energy;
            //    item.Deposit = model.Deposit;
            //    item.Accessories = model.Accessories;
            //    item.Nomenclature = model.Nomenclature;
            //}


            if (item.EquipementId > 0)
                equipementRepository.Update(item);
            else
                equipementRepository.Add(item);

            equipementRepository.SaveChanges();
            item = equipementRepository.Get(item.EquipementId);
            model.EquipementId = item.EquipementId;
            model.NomClient = item.Client.Nom;
        }

        //public void SaveEquipementArticula(EquipementArticuloViewModel model)
        //{
        //    var parent = equipementRepository.Get(model.EquipementId);
        //    if (parent == null) return;

        //    var item = parent.EquipementArticulo.FirstOrDefault(x => x.EquipementArticuloID == model.EquipementArticuloID);
        //    if (item == null)
        //        item = new Models.EquipementArticulo();

        //    item.EquipementId = model.EquipementId;


        //    item.Energy = model.Energy;
        //    item.Deposit = model.Deposit;
        //    item.Accessories = model.Accessories;
        //    item.Nomenclature = model.Nomenclature;


        //    if (item.EquipementArticuloID == 0)
        //    {
        //        parent.EquipementArticulo.Add(item);
        //        //equipementRepository.Update(parent);
        //    }

        //    equipementRepository.SaveChanges();
        //}

        //public bool DeleteEquipementArticula(int equipementArticuloID)
        //{
        //    var item = equipementArticuloRepository.Get(equipementArticuloID);
        //    if (item == null) return false;

        //    equipementArticuloRepository.Delete(equipementArticuloID);
        //    equipementRepository.SaveChanges();

        //    return true;
        //}


        public bool Supprimer(EquipementViewModel model)
        {
            var equipement = equipementRepository.Get(model.EquipementId);
            if (equipement == null)
                return true;


            equipementRepository.Delete(equipement.EquipementId);
            equipementRepository.SaveChanges();

            return true;
        }

        public string SaveEquipementQR(int equipementId)
        {
            var repertoire = @"/Images/Cadenassage/Equipements/" + equipementId + "/";

            if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(repertoire)))
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(repertoire));


            ////////////////////////////////////////////////////////////////////


            //ViewBag.item = Helpers.URLHelper.GetBaseUrl();

            ///////////////////////////////////////////////////////

            int width = 100;
            var encoder = new QrEncoder(ErrorCorrectionLevel.Q);
            var myCode = new QrCode();

            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";

            string qrString = string.Format("Equipment#{0}", equipementId);

            if (encoder.TryEncode(qrString, out myCode))
            {
                GraphicsRenderer dRenderer = new GraphicsRenderer(new FixedModuleSize(width, QuietZoneModules.Two), Brushes.Black, Brushes.White);

                using (var ms = new MemoryStream())
                {
                    dRenderer.WriteToStream(myCode.Matrix, ImageFormat.Png, ms);

                    string fileFullPath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(repertoire), "QRCode.png");

                    using (FileStream file = new FileStream(fileFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        //byte[] bytes = new byte[file.Length];
                        //file.Read(bytes, 0, (int)file.Length);
                        //ms.Write(bytes, 0, (int)file.Length);

                        //ms.CopyTo(file);

                        ms.WriteTo(file);
                    }
                    //return File(ms.ToArray(), "image/png");
                }
            }



            ////////////////////////////////////////////////////////////////////

            var item = equipementRepository.Get(equipementId);
            if (item != null)
            {
                item.QRCode = repertoire + "QRCode.png";
                if (string.IsNullOrEmpty(item.NumeroEquipement)) item.NumeroEquipement = "-";

                equipementRepository.Update(item);
                equipementRepository.SaveChanges();

                return repertoire + "QRCode.png?t=" + DateTime.Now.Ticks.ToString();
            }

            return "";
        }


        internal List<EquipementQRViewModel> GetEquipementsWithQR(int ClientId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";
            var time = DateTime.Now.ToLongTimeString();

            var result = equipementRepository.AsQueryable()
                        .Where(x => x.ClientId == ClientId)
                        .Where(x => x.QRCode != null)
                        .OrderBy(x => x.NomEquipement)
                        .Select(x => new EquipementQRViewModel()
                        {
                            ClientId = x.ClientId,
                            EquipementId = x.EquipementId,
                            NomEquipement = x.NomEquipement,
                            NomClient = x.Client.Nom,

                            QRCode = baseURL + x.QRCode ?? "",
                        })
                        .ToList();

            return result;
        }

    }
}