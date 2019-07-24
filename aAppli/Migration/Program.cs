using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDBEntities db = new MyDBEntities();

            Tmp.MyTmpDBEntities tmpDb = new Tmp.MyTmpDBEntities();

            foreach (var item in tmpDb.Credit)
            {
                if (!tmpDb.Article.Any(a => a.ID == item.ArticleId))
                {
                    Credit credit = new Credit
                    {
                        ArticleId = 0,
                        Coordonnes = item.Coordonnes,
                        DateDeVente = item.DateDeVente,
                        Designation = item.Designation,
                        EstablishmentId = 2,
                        ModePaiement = item.ModePaiement,
                        NomClient = item.NomClient,
                        ParentId = item.ParentId,
                        PrixAchat = item.PrixAchat,
                        PrixVente = item.PrixVente,
                        QT = item.QT,
                        SN = item.SN
                    };

                    db.Credit.AddObject(credit);
                    db.SaveChanges();
                }
            }

            foreach (var item in tmpDb.Vente)
            {
                if (!tmpDb.Article.Any(a => a.ID == item.ArticleId))
                {
                    Vente vente = new Vente
                    {
                        ArticleId = 0,
                        DateDeVente = item.DateDeVente,
                        Designation = item.Designation,
                        EstablishmentId = 2,
                        PrixAchat = item.PrixAchat,
                        PrixVente = item.PrixVente,
                        QT = item.QT,
                        SN = item.SN
                    };

                    db.Vente.AddObject(vente);
                    db.SaveChanges();
                }
            }

            foreach (var item in tmpDb.Article)
            {
                Article article = new Article
                {
                    Designation = item.Designation,
                    EstablishmentId = 2,
                    IsGenerique = item.IsGenerique,
                    PrixAchat = item.PrixAchat,
                    PrixVente = item.PrixVente,
                    QT = item.QT,
                    SN = item.SN
                };

                db.Article.AddObject(article);
                db.SaveChanges();

                Tmp.Credit crd = tmpDb.Credit.FirstOrDefault(c => c.ArticleId == item.ID);
                if (crd != null)
                {
                    Credit credit = new Credit
                    {
                        ArticleId = article.ID,
                        Coordonnes = crd.Coordonnes,
                        DateDeVente = crd.DateDeVente,
                        Designation = crd.Designation,
                        EstablishmentId = 2,
                        ModePaiement = crd.ModePaiement,
                        NomClient = crd.NomClient,
                        ParentId = crd.ParentId,
                        PrixAchat = crd.PrixAchat,
                        PrixVente = crd.PrixVente,
                        QT = crd.QT,
                        SN = crd.SN
                    };

                    db.Credit.AddObject(credit);
                    db.SaveChanges();
                }

                Tmp.Vente vnt = tmpDb.Vente.FirstOrDefault(c => c.ArticleId == item.ID);
                if (vnt != null)
                {
                     Vente vente = new Vente
                    {
                        ArticleId = article.ID,
                        DateDeVente = vnt.DateDeVente,
                        Designation = vnt.Designation,
                        EstablishmentId = 2,
                        PrixAchat = vnt.PrixAchat,
                        PrixVente = vnt.PrixVente,
                        QT = vnt.QT,
                        SN = vnt.SN
                    };

                    db.Vente.AddObject(vente);
                    db.SaveChanges();
                }
            }
        }
    }
}
