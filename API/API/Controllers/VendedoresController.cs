using DataConexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using System.Data.Entity;

namespace API.Controllers
{
    public class VendedoresController : ApiController
    {
       
        private VendedoresEntities dbContext = new VendedoresEntities();

        //Visualiza los registros {api/vendedores]
        [HttpGet]
        public IEnumerable<seller> Get()
        {

            List<seller> v = new List<seller>();
            string ciudad = string.Empty;

            foreach (VENDEDOR item in dbContext.VENDEDORs)
            {

                //var query = from p in dbContext.CIUDADs
                //            where p.CODIGO.Equals(item.CODIGO_CIUDAD)
                //            select p;

                var query = dbContext.CIUDADs.Where(x => x.CODIGO == item.CODIGO_CIUDAD);

                //foreach (var p in query)
                //{
                //    ciudad = p.DESCRIPCION;
                //}


                ciudad = query.Select(x => x.DESCRIPCION).FirstOrDefault();


                v.Add(new seller { CODIGO = item.CODIGO, NOMBRE = item.NOMBRE, APELLIDO = item.APELLIDO, NUMERO_IDENTIFICACION = item.NUMERO_IDENTIFICACION, CIUDAD = ciudad });

            }

            return v;

        }

        [HttpGet]
        public seller Get(int id)
        {
            seller _seller = new seller();

            //using (VendedoresEntities vendedoresentities = new VendedoresEntities())
            //{
            //    var resVend = vendedoresentities.VENDEDORs.FirstOrDefault(e => e.CODIGO == id);



            //    var resCiudad = vendedoresentities.CIUDADs.FirstOrDefault(x => x.CODIGO == resVend.CODIGO_CIUDAD);

            //    resVend.CODIGO_CIUDAD = resCiudad.CODIGO;


            //    return resVend;
            //}








            var res = dbContext.VENDEDORs.Where(p => p.CODIGO == id);
            _seller.CODIGO_CIUDAD = res.Select(x => x.CODIGO_CIUDAD).FirstOrDefault();

            foreach (var x in res)
            {
                _seller.CODIGO = x.CODIGO;
                _seller.NOMBRE = x.NOMBRE;
                _seller.APELLIDO = x.APELLIDO;
                _seller.NUMERO_IDENTIFICACION = x.NUMERO_IDENTIFICACION;

            }

            return _seller;



        }

        // crea REGISTROS {API/VENDEDORES/}
        [HttpPost]
        public IHttpActionResult AgregaVendedor([FromBody]VENDEDOR vend)
        {

            if (ModelState.IsValid)
            {
                dbContext.VENDEDORs.Add(vend);
                dbContext.SaveChanges();
                return Ok(vend);

            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPut]

        public IHttpActionResult ActualizarUsuario(int id, [FromBody]VENDEDOR vend)
        {

            var UserExist = dbContext.VENDEDORs.Count(c => c.CODIGO == id) > 0;

            if (UserExist)
            {

                dbContext.Entry(vend).State = EntityState.Modified;
                dbContext.SaveChanges();
                return Ok();

            }
            else
            {
                return NotFound();
            }

        }


        // Borra REGISTROS {API/VENDEDORES/1}

        [HttpDelete]
        public IHttpActionResult EliminarVendedor(int id)
        {
            var vend = dbContext.VENDEDORs.Find(id);

            if (vend != null)
            {
                dbContext.VENDEDORs.Remove(vend);
                dbContext.SaveChanges();
                return Ok(vend);
            }
            else
            {
                return NotFound();

            }

        }


    }
}
