using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicaAccesoDatos.BaseDatos
{
     public class RepositorioHorarios : IRepositorioHorarios
    {
        public LibreriaContext Contexto { get; set; }

        public RepositorioHorarios(LibreriaContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Horario obj)
        {
            try
            {
                obj.Validar();
                Contexto.Horarios.Add(obj);
                Contexto.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo dar de alta el horario", e);
            }
        }

        public IEnumerable<Horario> FindAll()
        {
            try
            {
                return Contexto.Horarios.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudieron encontrar horarios", e);
            }
        }

        public Horario FindById(int Id)
        {
            try
            {
                if (Id == 0) throw new Exception("El id de horario no puede ser 0");
                return Contexto.Horarios.Find(Id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo encontrar el horario", e);
            }
        }

        public void Remove(int Id)
        {
            try
            {
                //Validar que no se encuentre vinculado con ningun partido. 
                Horario horarioABorrar = FindById(Id);
                if (horarioABorrar == null) throw new Exception("No existe el horario a borrar");
                Contexto.Horarios.Remove(horarioABorrar);
                Contexto.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo borrar el horario", e);
            }
        }

        public void Update(Horario obj)
        {
            try
            {
                obj.Validar();
                Contexto.Horarios.Update(obj);
                Contexto.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo actualizar el horario", e);
            }
        }
    }
}
